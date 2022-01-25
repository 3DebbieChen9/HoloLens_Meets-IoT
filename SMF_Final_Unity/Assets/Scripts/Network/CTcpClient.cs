using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class CTcpClient
{
	public delegate void d_Receive(string message, int length);
	public event d_Receive Receive;
	public delegate void d_Warning();
	public event d_Warning Warning;

	private Socket socket_send;
	private Socket socket_receive;
	private string _IP;
    public string IP {
        get { 
            return _IP;
        }
        set {
            _IP = value;
        }
    }
	private int _Port;
    public int Port {
        get { 
            return _Port;
        }
        set {
            _Port = value;
        }
    }
	private Thread thread_connect;
	private static AutoResetEvent sendDone = new AutoResetEvent(false);
	private static ManualResetEvent connectDone = new ManualResetEvent(false);
	System.Collections.Generic.List<ArraySegment<byte>> ArraySegmentList;
	System.ComponentModel.BackgroundWorker worker = new System.ComponentModel.BackgroundWorker();
	bool receiveJson = false;
	int jsonCount;
	string jsonString = "";
	int tempCount = 0;

	public CTcpClient(string ip, int port, AddressFamily family = AddressFamily.InterNetwork, SocketType type = SocketType.Stream, ProtocolType protocol = ProtocolType.Tcp)
	{
		_IP = ip;
		_Port = port;
		socket_send = new Socket(family, type, protocol);

		worker.DoWork += (o,e)=>{
			while (true)
			{
				try
				{
					byte[] tmp = new byte[2048];
						
					int length = socket_send.Receive(tmp);
					string msg = Encoding.UTF8.GetString(tmp);
					Receive(msg, length);

					// if (receiveJson)
					// {
					// 	byte[] tmp = new byte[jsonCount]; 
					
					// 	int length = socket_send.Receive(tmp);
					// 	if (length == 0)
					// 	{
					// 		break;
					// 	}

					// 	string msg = Encoding.UTF8.GetString(tmp, 0, length);
						
					// 	jsonCount =  jsonCount - length;
					// 	tempCount += length;
					// 	jsonString = jsonString + msg;
						
					// 	if (jsonCount == 0)
					// 	{
					// 		Receive(jsonString, tempCount);
					// 		receiveJson = false;
					// 	}
						
					// }
					// else
					// {
					// 	byte[] tmp = new byte[10];
						
					// 	int length = socket_send.Receive(tmp);
					// 	if (length == 0)
					// 	{
					// 		break;
					// 	}

					// 	string msg = Encoding.UTF8.GetString(tmp);
						
					// 	jsonCount = int.Parse(msg);

					// 	jsonString = "";
					// 	tempCount = 0;

					// 	Receive(msg, length);
					// 	receiveJson = true;
					// }
				}
				catch (Exception ex)
				{
					Debug.Log("Receive error: " + ex.Message);
					break;
				}
			}

		};
		
		worker.RunWorkerCompleted += (sender, e)=>
		{			
		};

		Debug.Log("C# TCP client class create IP : " + _IP + ", Port : " + _Port);
	}
	public void StartConnect()
	{
		thread_connect = new Thread(Thread_Connect);
		thread_connect.Start();
		Debug.Log("C# TCP client start");
	}

	public void StopConnect()
	{
		socket_send.Close();
		Debug.Log("C# TCP client stop");
	}

	public bool IsConnected
    {
        get
        {
            if (socket_send == null) return false;
            else return socket_send.Connected;
        }
    }

	public void Send(string msg)
	{
		if (IsConnected) socket_send.Send(Encoding.UTF8.GetBytes(msg));
	}

	public void SendByte(byte[] msg)
	{
		if (IsConnected) 
		{
			try
			{
				socket_send.Send(msg);
			}
			catch(System.Exception e)
			{
				Debug.Log("C# TCP client send error" + e.Message);
				Warning();
			}
		}
		else
		{
			Debug.Log("C# TCP client disconnect");
			Warning();
		}
	}

	public void AsyncSendByte(byte[] head, byte[] json)
	{
		if (IsConnected) 
		{
			try
			{
				ArraySegment<byte> m_Arrayd = new ArraySegment<byte>( head );
				ArraySegment<byte> m_Arrayj = new ArraySegment<byte>( json );
				ArraySegmentList = new System.Collections.Generic.List<ArraySegment<byte>>();
				ArraySegmentList.Add(m_Arrayd);
				ArraySegmentList.Add(m_Arrayj);
				socket_send.BeginSend(ArraySegmentList, SocketFlags.None,  new AsyncCallback(SendCallback), socket_send);

				sendDone.WaitOne();

				// Manual mode need reset for block thread.
				// sendDone.Reset();
			}
			catch(System.Exception e)
			{
				Debug.Log("C# TCP client send error" + e.Message);
				Warning();
			}
		}
		else
		{
			Debug.Log("C# TCP client disconnect");
			Warning();
		}
	}

	private void Thread_Connect()
	{
		try
		{
            //socket_send.Connect(IPAddress.Parse(_IP), _Port);
			socket_send.BeginConnect(IPAddress.Parse(_IP), _Port, new AsyncCallback(ConnectCallback), socket_send);
			
			// ADAT_Data.VpnIP = ((IPEndPoint)socket_send.LocalEndPoint).Address.ToString();
			// Debug.Log("C# TCP client local IP = " + ADAT_Data.VpnIP);

            // Debug.Log("C# TCP client connected");
		}
		catch (Exception e) 
		{
			Debug.Log("C# TCP client connect error: " + e.Message);
		}
	}

	private void ConnectCallback(IAsyncResult ar) 
	{  
		try 
		{  
			// Retrieve the socket from the state object.  
			Socket client = (Socket) ar.AsyncState;  
	
			// Complete the connection.  
			client.EndConnect(ar);
	
			// Signal that the connection has been made.  
			connectDone.Set();  

			// Start screen share.
			SocketManager.Instance.isStreaming = true;

			// Start receive.
			worker.RunWorkerAsync();
			Debug.Log("C# TCP client connected");
		} 
		catch (Exception e) 
		{  
			Debug.Log("C# TCP client connect error" + e.Message); 
		}  
	}  

	private void SendCallback(IAsyncResult ar) 
	{  
		try 
		{   
			sendDone.Set(); 
		}
		catch (Exception e) 
		{  
			Debug.Log("C# TCP client send done error" + e.Message);
		}  
	}  

}
