#!/usr/bin/env python3

import cv2
import socket
import sys
import base64
import numpy as np
import time
import json

class TimePeriod():
    def __init__(self, skip_steps=20, start_t=time.time()):
        import time
        
        self.start_t = start_t

        self.last_t = start_t

        self.skip_steps = skip_steps

        self.call_times = 0

        self.total_t = 0

    def accumulate(self, period_t):
        self.call_times += 1

        if self.call_times > self.skip_steps:
            self.total_t += period_t

    def calc_period(self):
        period_t = time.time() - self.last_t

        period_fps = 1 / period_t

        self.accumulate(period_t)

        self.last_t = time.time()

        if self.total_t != 0:
            total_fps = (self.call_times - self.skip_steps) / self.total_t

        else:
            total_fps = 0 

        return period_t, period_fps, self.total_t, total_fps


def ParseRecvJsonMsg(json_str):
    json_obj = json.loads(json_str)

    real_img = json_obj['Real']

    return real_img

def run(s):
    print('waiting for connection ...')

    conn, addr = s.accept()
    print('connect succeed.')
    print('wait for receive data_len.')

    time_period = TimePeriod(skip_steps=30)
    
    runYolo = False

    while True:

        head = conn.recv(10) # 顯示下一包的大小(要傳多少)

        if head == b'':
            raise OSError

        data_len = int(head.decode('utf-8').strip('\x00')) # 10 byte 來表示資料大小
        count = data_len
        data_str = ''

        while count > 0:
#            print('count: ', count)
            data = conn.recv(count)

            if count == data_len and data == b'':
                raise OSError

            data_decode = data.decode('utf-8')

            data_str += data_decode

            count -= len(data)

            if count == 0:
#                print('data', data_str[-20:])

                imgb64_real = ParseRecvJsonMsg(data_str)
                img_real_decode = base64.b64decode(imgb64_real)
                real_nparr = np.fromstring(img_real_decode, np.uint8) 
                img_real = cv2.imdecode(real_nparr, cv2.IMREAD_COLOR)
                # cv2.imshow('real', img_real)

                # YoloPredict
                # if runYolo:
                #     yoloResult = YoloPredict.predict(YoloPredict.yolo, img_real, prob_thresh= 0.7)
                #     if yoloResult == 0:
                #         data = "No Object Found"
                #         print("No Object Found")
                #     elif yoloResult[0] == "motor":
                #         # runYolo = False
                #         motorStatus = GetValue.thingWorx_getValue()
                #         yoloAccuracy = yoloResult[1]
                #         data = f"{yoloResult[0]} {yoloResult[1]: .2f}|{motorStatus}"
                #         print( f"{yoloResult[0]} {yoloResult[1]: .2f}")
                #     else:
                #         data = f"{yoloResult[0]} {yoloResult[1]: .2f}"
                #         print(data)
                # else:
                #     motorStatus = GetValue.thingWorx_getValue()
                #     data = f"motor {yoloAccuracy: .2f}|{motorStatus}"
                #     print(f"motor {yoloAccuracy: .2f}")
                with open("MotorStatus.txt", "r") as f:
                    data = "Motor|" + f.readline()
                conn.send(data.encode()) # 回傳訊息給眼鏡

#                print('new_img', new_img.shape)

#                cv2.waitKey(0)

                k = cv2.waitKey(1) & 0xFF

                if k == 27 or k== ord('q'):
                    break
      
        period_t, period_fps, total_t, total_fps = time_period.calc_period()

        print(f'fps: {period_fps:6.3f}, average fps: {total_fps:6.3f}', end='\r')


if __name__ == '__main__':
    if len(sys.argv) < 2 or sys.argv[1] == False:
        debug = False

    elif sys.argv[1] == True:
        debug = True
       
    host = '140.114.79.246' # CGV Lab
    port = 8001

    s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
    s.bind((host, port))
    s.listen(1)

    while True:
        if debug == True:
            run(s)

        else:
            try: 
                run(s)
 
            except KeyboardInterrupt:
                sys.exit()
 
            except:
                s.shutdown(2) 
                s.close()
 
                s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
      
                s.setsockopt(socket.SOL_SOCKET, socket.SO_REUSEADDR, 1)
                s.bind((host, port))
                s.listen(1)
    
    
    

