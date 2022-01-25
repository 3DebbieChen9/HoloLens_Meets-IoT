/*
*  MediaCaptureUnity.cs
*  HoloLensCamCalib
*
*  This file is a part of HoloLensCamCalib.
*
*  HoloLensCamCalib is free software: you can redistribute it and/or modify
*  it under the terms of the GNU Lesser General Public License as published by
*  the Free Software Foundation, either version 3 of the License, or
*  (at your option) any later version.
*
*  HoloLensCamCalib is distributed in the hope that it will be useful,
*  but WITHOUT ANY WARRANTY; without even the implied warranty of
*  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
*  GNU Lesser General Public License for more details.
*
*  You should have received a copy of the GNU Lesser General Public License
*  along with HoloLensCamCalib.  If not, see <http://www.gnu.org/licenses/>.
*
*  Copyright 2020 Long Qian
*
*  Author: Long Qian
*  Contact: lqian8@jhu.edu
*
*/



using System.Collections.Generic;
using System.Collections;
using System.Threading;
using UnityEngine;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Threading.Tasks;
using UnityEngine.Rendering;

#if !UNITY_EDITOR && UNITY_METRO
using Windows.Media;
using Windows.Media.Capture;
using Windows.Media.Capture.Frames;
using Windows.Media.Audio;
using Windows.Media.Devices;
using Windows.Foundation;
using Windows.Media.MediaProperties;
using Windows.Graphics.Imaging;
using System.Threading;
using System.Linq;
using System.Collections.Concurrent;
using Windows.Devices.Enumeration;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.FileProperties;
using Windows.Storage;
using Windows.Media.Effects;
using System.Collections;
using Windows.Foundation.Collections;
using Windows.Graphics.DirectX.Direct3D11;
using Windows.Media.Core;
using Windows.Security.Cryptography;
#endif
using Newtonsoft.Json;

using System;
using System.IO;
using System.Text;
using UnityEngine.UI;
using SocketMsgAttributeSpace;

using System.Management;
using System.Linq;

/// <summary>
/// The MediaCaptureUnity class manages video access of HoloLens. Many of the code
/// came from HoloLensARToolKit.
///
/// Author:     Long Qian
/// Email:      lqian8@jhu.edu
/// </summary>

public class MediaCaptureUnity : MonoBehaviour
{
    private static MediaCaptureUnity s_Instance;
    public static MediaCaptureUnity Instance
    {
        get
        {
            if (s_Instance == null)
            {
                s_Instance = FindObjectOfType(typeof(MediaCaptureUnity)) as MediaCaptureUnity;
            }
            return s_Instance;
        }
    }


    public byte[] Real_ImgBytes = null;
    public int targetVideoWidth, targetVideoHeight;
    private float targetVideoFrameRate = 30f;
    private int videoWidth = 0;
    private int videoHeight = 0;
    SMsgAttribute tempMsg;
    public SocketManager m_SocketManager;


    private enum CaptureStatus
    {
        Clean,
        Initialized,
        Running
    }
    private CaptureStatus captureStatus = CaptureStatus.Clean;

    public static string TAG = "MediaCaptureUnity";

    public void ResetMediaCaptureforFocus()
    {
    #if !UNITY_EDITOR && UNITY_METRO
        StartCoroutine(e_ResetMediaCaptureforFocus());
    #endif
    }

#if !UNITY_EDITOR && UNITY_METRO
    [ComImport]
    [Guid("5B0D3235-4DBA-4D44-865E-8F1D0E4FD04D")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    unsafe interface IMemoryBufferByteAccess {
        void GetBuffer(out byte* buffer, out uint capacity);
    }

    private SoftwareBitmap upBitmap = null;
    private SoftwareBitmap _tempBitmap = null;
    private SoftwareBitmap softwareBitmap = null;
    private MediaCapture mediaCapture;
    private MediaFrameReader frameReader = null;

    private int HL = 0;

    bool EncodetaskDone = true;

 

    void EncodeRealImage(SoftwareBitmap soft, Guid encoderId) 
    {
        EncodetaskDone = false;
        Task.Run(async() => {
            using (var ms = new InMemoryRandomAccessStream())
            {
                BitmapEncoder encoder = await BitmapEncoder.CreateAsync(encoderId, ms);
                encoder.SetSoftwareBitmap(soft);

                await encoder.FlushAsync();
                Real_ImgBytes = new byte[ms.Size];
                await ms.ReadAsync(Real_ImgBytes.AsBuffer(), (uint)ms.Size, InputStreamOptions.None);

                tempMsg = new SMsgAttribute();
                tempMsg.Real = System.Convert.ToBase64String(Real_ImgBytes);
                m_SocketManager.SendJson(tempMsg);

                EncodetaskDone = true;
            }
        });
    }

    private async Task<bool> InitializeMediaCaptureAsync() {
        if (captureStatus != CaptureStatus.Clean) {
            Debug.Log(TAG + ": InitializeMediaCaptureAsync() fails because of incorrect status");
            return false;
        }

        if (mediaCapture != null) {
            return false;
        }
    
        var allGroups = await MediaFrameSourceGroup.FindAllAsync();
        int selectedGroupIndex = -1;


        for (int i = 0; i < allGroups.Count; i++) {
            var group = allGroups[i];
            Debug.Log(group.DisplayName + ", " + group.Id);
            // for HoloLens 1
            if (group.DisplayName == "MN34150") {
                selectedGroupIndex = i;
                HL = 1;
                Debug.Log(TAG + ": Selected group " + i + " on HoloLens 1");
                break;
            }
            // for HoloLens 2
            else if (group.DisplayName == "QC Back Camera") {
                selectedGroupIndex = i;
                HL = 2;
                Debug.Log(TAG + ": Selected group " + i + " on HoloLens 2");
                break;
            }
        }

        if (selectedGroupIndex == -1) {
            Debug.Log(TAG + ": InitializeMediaCaptureAsyncTask() fails because there is no suitable source group");
            return false;
        }
        Debug.Log(TAG + " : Initialize mediacapture with the source group.");
        // Initialize mediacapture with the source group.

        mediaCapture = new MediaCapture();
     
        MediaStreamType mediaStreamType = MediaStreamType.VideoPreview;
     
        if (HL == 1) {
            var settings = new MediaCaptureInitializationSettings {
                SourceGroup = allGroups[selectedGroupIndex],
                // This media capture can share streaming with other apps.
                SharingMode = MediaCaptureSharingMode.SharedReadOnly,
                // Only stream video and don't initialize audio capture devices.
                StreamingCaptureMode = StreamingCaptureMode.Video,
                // Set to CPU to ensure frames always contain CPU SoftwareBitmap images
                // instead of preferring GPU D3DSurface images.
                MemoryPreference = MediaCaptureMemoryPreference.Cpu
            };
            await mediaCapture.InitializeAsync(settings);
            Debug.Log(TAG + ": MediaCapture is successfully initialized in SharedReadOnly mode for HoloLens 1.");
            mediaStreamType = MediaStreamType.VideoPreview;
        }
        else if (HL == 2){
            string deviceId = allGroups[selectedGroupIndex].Id;

            Debug.Log(TAG + " : deviceId.");
            // Look up for all video profiles
            IReadOnlyList<MediaCaptureVideoProfile> profileList = MediaCapture.FindKnownVideoProfiles(deviceId, KnownVideoProfile.VideoConferencing);

            // Initialize mediacapture with the source group.
            var settings = new MediaCaptureInitializationSettings {
                VideoDeviceId = deviceId,
                VideoProfile = profileList[0],
                // This media capture can share streaming with other apps.
                //SharingMode = MediaCaptureSharingMode.SharedReadOnly,
                // Only stream video and don't initialize audio capture devices.
                StreamingCaptureMode = StreamingCaptureMode.Video,
                PhotoCaptureSource = PhotoCaptureSource.VideoPreview,
                // Set to CPU to ensure frames always contain CPU SoftwareBitmap images
                // instead of preferring GPU D3DSurface images.
                MemoryPreference = MediaCaptureMemoryPreference.Cpu
                //MemoryPreference = MediaCaptureMemoryPreference.Auto
            };

            await mediaCapture.InitializeAsync(settings).AsTask();

            Debug.Log(TAG + ": MediaCapture is successfully initialized in ExclusiveControl mode for HoloLens 2.");
            mediaStreamType = MediaStreamType.VideoRecord;
        }
       
        try {
            var mediaFrameSourceVideo = mediaCapture.FrameSources.Values.Single(x => x.Info.MediaStreamType == mediaStreamType);
            MediaFrameFormat targetResFormat = null;
            float framerateDiffMin = 60f;

            foreach(var f in mediaFrameSourceVideo.SupportedFormats){

                Debug.Log("video size format width = "+f.VideoFormat.Width+" heigh = "+f.VideoFormat.Height);

            }
            foreach (var f in mediaFrameSourceVideo.SupportedFormats.OrderBy(x => x.VideoFormat.Width * x.VideoFormat.Height)) {
                if (f.VideoFormat.Width == targetVideoWidth && f.VideoFormat.Height == targetVideoHeight ) {
                    if (targetResFormat == null) {
                        targetResFormat = f;
                        framerateDiffMin = Mathf.Abs(f.FrameRate.Numerator / f.FrameRate.Denominator - targetVideoFrameRate);
                    }
                    else if (Mathf.Abs(f.FrameRate.Numerator / f.FrameRate.Denominator - targetVideoFrameRate) < framerateDiffMin) {
                        targetResFormat = f;
                        framerateDiffMin = Mathf.Abs(f.FrameRate.Numerator / f.FrameRate.Denominator - targetVideoFrameRate);
                    }
                }
            }
            if (targetResFormat == null) {
                targetResFormat = mediaFrameSourceVideo.SupportedFormats[0];
                Debug.Log(TAG + ": Unable to choose the selected format, fall back");
            }

            // choose the smallest resolution
            //var targetResFormat = mediaFrameSourceVideoPreview.SupportedFormats.OrderBy(x => x.VideoFormat.Width * x.VideoFormat.Height).FirstOrDefault();
            // choose the specific resolution
            //var targetResFormat = mediaFrameSourceVideoPreview.SupportedFormats.OrderBy(x => (x.VideoFormat.Width == 1344 && x.VideoFormat.Height == 756)).FirstOrDefault();
            await mediaFrameSourceVideo.SetFormatAsync(targetResFormat);
            Debug.Log(TAG + ": mediaFrameSourceVideo.SetFormatAsync()");
            frameReader = await mediaCapture.CreateFrameReaderAsync(mediaFrameSourceVideo, targetResFormat.Subtype);
            Debug.Log(TAG + ": mediaCapture.CreateFrameReaderAsync()");
            frameReader.FrameArrived += OnFrameArrived;
            videoWidth = Convert.ToInt32(targetResFormat.VideoFormat.Width);
            videoHeight = Convert.ToInt32(targetResFormat.VideoFormat.Height);
            Debug.Log(TAG + ": FrameReader is successfully initialized, " + videoWidth + "x" + videoHeight + 
                ", Framerate: " + targetResFormat.FrameRate.Numerator + "/" + targetResFormat.FrameRate.Denominator);

        }
        catch (Exception e) {
            Debug.Log(TAG + ": FrameReader is not initialized");
            Debug.Log(TAG + ": Exception: " + e);
            return false;
        }
        
        captureStatus = CaptureStatus.Initialized;
        return true;
    }
    private async Task<bool> StartFrameReaderAsync() {
        Debug.Log(TAG + " StartFrameReaderAsync() thread ID is " + Thread.CurrentThread.ManagedThreadId);
        if (captureStatus != CaptureStatus.Initialized) {
            Debug.Log(TAG + ": StartFrameReaderAsync() fails because of incorrect status");
            return false;
        }
        
        MediaFrameReaderStartStatus status = await frameReader.StartAsync();
        if (status == MediaFrameReaderStartStatus.Success) {
            Debug.Log(TAG + ": StartFrameReaderAsync() is successful");
            captureStatus = CaptureStatus.Running;
            return true;
        }
        else {
            Debug.Log(TAG + ": StartFrameReaderAsync() is not successful, status = " + status);
            return false;
        }
    }

    private async Task<bool> StopFrameReaderAsync() {
        if (captureStatus != CaptureStatus.Running) {
            Debug.Log(TAG + ": StopFrameReaderAsync() fails because of incorrect status");
            captureStatus = CaptureStatus.Clean;
            return false;
        }
        await frameReader.StopAsync();
        captureStatus = CaptureStatus.Initialized;
        //mediaCapture = null;
        onFrameArrivedProcessing = false;
        EncodetaskDone = true;
        Real_ImgBytes = null;
        Debug.Log(TAG + ": StopFrameReaderAsync() is successful");
        return true;
    }

    private bool onFrameArrivedProcessing = false;
    
    private unsafe void OnFrameArrived(MediaFrameReader sender, MediaFrameArrivedEventArgs args) {
        // TryAcquireLatestFrame will return the latest frame that has not yet been acquired.
        // This can return null if there is no such frame, or if the reader is not in the
        // "Started" state. The latter can occur if a FrameArrived event was in flight
        // when the reader was stopped.
        if (onFrameArrivedProcessing) {
            //Debug.Log(TAG + " OnFrameArrived() is still processing");
            return;
        }
        onFrameArrivedProcessing = true;
        using (var frame = sender.TryAcquireLatestFrame()) {
            if (frame != null) {
                try
                {
                    //Use Gpu (Auto mode)
                    if(frame.VideoMediaFrame.Direct3DSurface != null)
                    {
                        // Change Direct3DSurface to SoftwareBitmap
                        CreateSoftwareBitmapFromSurface(frame.VideoMediaFrame.Direct3DSurface);
                    }

                    // Use Cpu (Cpu mode)
                    if(frame.VideoMediaFrame.SoftwareBitmap != null)
                    {
                        // SoftwareBitmap
                        softwareBitmap = SoftwareBitmap.Convert(frame.VideoMediaFrame.SoftwareBitmap, BitmapPixelFormat.Rgba8, BitmapAlphaMode.Ignore);
                        Interlocked.Exchange(ref _tempBitmap, softwareBitmap);
                    }
                    onFrameArrivedProcessing = false;
                }
                catch (System.Exception e)
                {
                    Debug.Log(TAG + " softwareBitmap" + e.Message);
                    StartCoroutine(ResetMediaCapture());
                    frame.VideoMediaFrame.SoftwareBitmap?.Dispose();
                    onFrameArrivedProcessing = false;
                }
                frame.VideoMediaFrame.SoftwareBitmap?.Dispose();
                
            }
        }
        onFrameArrivedProcessing = false;
    }

    private async void CreateSoftwareBitmapFromSurface(IDirect3DSurface surface)
    {
        softwareBitmap = await SoftwareBitmap.CreateCopyFromSurfaceAsync(surface);
        var m_softwareBitmap = SoftwareBitmap.Convert(softwareBitmap, BitmapPixelFormat.Rgba8, BitmapAlphaMode.Ignore);
        Interlocked.Exchange(ref _tempBitmap, m_softwareBitmap);
    }

    async void InitializeMediaCaptureAsyncWrapper() {
        try
            {
                Application.targetFrameRate = 60;
                captureStatus = CaptureStatus.Clean; 
                
                if (UnityEngine.WSA.Application.RunningOnUIThread())
                {
                     await InitializeMediaCaptureAsync();
                }
                else
                {
                    UnityEngine.WSA.Application.InvokeOnUIThread(() => InitializeMediaCaptureAsync(), waitUntilDone: true);
                }    
            }
        catch (Exception ex)
        {
            // Log an error and prevent activation
            Debug.LogError($"Audio access failure: {ex.Message}.");  
        }
       
    }

    async void StartFrameReaderAsyncWrapper() {
        await StartFrameReaderAsync();
    }

    async void StopFrameReaderAsyncWrapper() {
        await StopFrameReaderAsync();
    }

    // Update is called once per frame
    unsafe void Update() 
    {
        if (captureStatus == CaptureStatus.Running) 
        {
            // Real.
            if (_tempBitmap != null)
            {
                try
                {
                    Interlocked.Exchange(ref upBitmap, _tempBitmap);
                    _tempBitmap = null;
                }
                catch (System.Exception e)
                {
                    Debug.Log("Real Interlocked error" + e.Message);
                    throw;
                }
            }


            if(EncodetaskDone && upBitmap != null)
            {
                EncodeRealImage(upBitmap, BitmapEncoder.JpegEncoderId);
                upBitmap = null;
            }

        }
    }


    void Start() {
        captureStatus = CaptureStatus.Clean;        
        InitializeMediaCaptureAsyncWrapper();    
    }


    private void OnApplicationFocus(bool focusStatus) 
    {
        if(!focusStatus)
        {
            Debug.Log("Out of App");
            ToggleVideo();
        }
        else
        {
            Debug.Log("Back to App");
            StartCoroutine(e_ResetMediaCapture());          
        }
    }
    IEnumerator e_ResetMediaCapture()
    {
        captureStatus = CaptureStatus.Clean;
        mediaCapture = null;

        InitializeMediaCaptureAsyncWrapper();
        
        yield return new WaitForSeconds(1f);
        ToggleVideo();
    }

    IEnumerator e_ResetMediaCaptureforFocus()
    {
        if (captureStatus == CaptureStatus.Initialized || captureStatus == CaptureStatus.Clean) 
        {
            mediaCapture = null;
            captureStatus = CaptureStatus.Clean;
            InitializeMediaCaptureAsyncWrapper();
            yield return new WaitForSeconds(1f);
            ToggleVideo();
        }
        else if (captureStatus == CaptureStatus.Running) 
        {
            StopFrameReaderAsyncWrapper();
            yield return new WaitForSeconds(1f);
            InitializeMediaCaptureAsyncWrapper();
            yield return new WaitForSeconds(1f);
            ToggleVideo();
        } 
    }

    
    public void ToggleVideo() {
        Debug.Log(TAG + " ToggleVideo captureStatus = "+captureStatus);
        if (captureStatus == CaptureStatus.Initialized) {
            StartFrameReaderAsyncWrapper();
        }
        else if (captureStatus == CaptureStatus.Running) {
            StopFrameReaderAsyncWrapper();
        }
    }

#else

    bool EncodetaskDone = true;
    bool isFirst = true;
    byte[] upBitmap = null;
    bool isStream = false;
    public void ToggleVideo()
    {
        isStream = !isStream;
    }


    void Update() 
    {
        if (isFirst)
        {
            // Encode real image for editor test.
            upBitmap = File.ReadAllBytes(Application.dataPath + "/Resources/m115.jpg");
            //upBitmap = File.ReadAllBytes(Application.dataPath + "/Resources/TestImage.png");
            isFirst = false;
        }

        if(EncodetaskDone && upBitmap != null && isStream)
        {
            tempMsg = new SMsgAttribute();
            tempMsg.Real = System.Convert.ToBase64String(upBitmap);
            m_SocketManager.SendJson(tempMsg);
        }
    }

#endif


#if WINDOWS_UWP
    IEnumerator ResetMediaCapture()
    {
        ToggleVideo();
        while (captureStatus == CaptureStatus.Running)
        {
            yield return null;
        }
        ToggleVideo();
    }
#endif
}
