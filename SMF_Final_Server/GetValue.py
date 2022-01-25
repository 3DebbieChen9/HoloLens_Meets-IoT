from typing import Text
import requests
import json
from io import StringIO

def thingWorx_getValue():
    url = 'https://140.134.200.140:8443/Thingworx/Things/HoloThing/Services/GetPropertyValues'
    headers = {'Accept': 'application/json','Content-Type': 'application/json','appKey': '08cd451a-3244-4ba7-bf02-9e96aebd5272'}
    req = requests.post(url, headers=headers,verify=False)
    status = json.loads(req.content.decode('utf-8'))

    status_code = f'Do3-{status["rows"][0]["Micro800_ARDEMO_Do3"]},Do4-{status["rows"][0]["Micro800_ARDEMO_Do4"]},Di3-{status["rows"][0]["Micro800_ARDEMO_Di3"]},Di4-{status["rows"][0]["Micro800_ARDEMO_Di4"]},Di1-{status["rows"][0]["Micro800_ARDEMO_Di1"]},Pos-{status["rows"][0]["Micro800_ARDEMO_Position"]},Di5-{status["rows"][0]["Micro800_ARDEMO_Di5"]},Di6-{status["rows"][0]["Micro800_ARDEMO_Di6"]},Do1-{status["rows"][0]["Micro800_ARDEMO_Do1"]},Do2-{status["rows"][0]["Micro800_ARDEMO_Do2"]}'

    return status_code