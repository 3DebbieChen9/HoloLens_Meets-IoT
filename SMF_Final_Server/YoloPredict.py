
# pip install yolov4
# pip install opencv-python
# pip install tensorflow

import cv2
import os
from yolov4.tf import YOLOv4
from numba import jit, cuda # for GPU

# path to where you store the three files
topFolder = "C:\\Users\\cgvlab\\Documents\\Debbie\\SMF\\FinalProject\\yolov4_in_python"

# set paths
names = os.path.join(topFolder, 'obj.names')
cfg = os.path.join(topFolder, 'testing-yolov4-obj.cfg')
weights = os.path.join(topFolder, 'yolov4-obj_best.weights')

# define yolo model
yolo = YOLOv4()
yolo.config.parse_names(names)
yolo.config.parse_cfg(cfg)
yolo.make_model()
yolo.load_weights(weights, weights_type="yolo")

# for GPU
# @jit(target ="cuda") 
def predict(model, inputFrame, prob_thresh=0.25):
    # model <- use the yolo model defined in top level
    # prob_thresh <- any result's probability lower than this will be ignored, default is set to 0.25
    # output:
    #  -> Tuple('ObjectName', float(probability))  return the one with the highest probability
    #  0  if nothing is found

    objectDict = {0: 'cellphone', 1: 'motor', 2: 'notebook'}

    # frame = cv2.imread(image_path)
    frame = cv2.cvtColor(inputFrame, cv2.COLOR_BGR2RGB)

    results = model.predict(frame, prob_thresh=prob_thresh)

    objects = []
    for box in results:
        if box[5] < prob_thresh:
            continue
        objects.append( (objectDict[int(box[4])], box[5]) )
    if not objects == []:
        objects.sort(key = lambda x : x[1], reverse = True)
        topResult = objects[0]
        # print(f'{topResult[0]}, {topResult[1]}')
        return topResult
    else:
        # print('No objects found')
        return 0

# if __name__=="__main__":
#     # examples
#     predict(yolo, "C:\\Users\\cgvlab\\Documents\\Debbie\\SMF\\FinalProject\\test\\m120.jpg")
#     predict(yolo, "C:\\Users\\cgvlab\\Documents\\Debbie\\SMF\\FinalProject\\test\\m120.jpg", prob_thresh= 0.5)