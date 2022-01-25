from PyQt5 import QtGui, QtCore,QtWidgets
from PyQt5.QtWidgets import QRubberBand, QWidget, QHBoxLayout, QSizeGrip
from PyQt5.QtCore import QPoint, QRect, Qt
from PyQt5.QtGui import qRed, qGreen, qBlue

def do1_clicked(self):
    self.MOTOR.Do1 = not self.MOTOR.Do1
    self.MOTOR.getStatusCode()
    if self.Do1.text() == "OFF":
        self.Do1.setText("ON")
        self.Do1.setStyleSheet("background-color: rgb(255, 18, 0);\n"
        "border-color: rgb(255, 18, 0);")
    else:
        self.Do1.setText("OFF")
        self.Do1.setStyleSheet("background-color: rgb(255, 223, 31);\n"
        "border-color: rgb(255, 223, 31);")

def do2_clicked(self):
    self.MOTOR.Do2 = not self.MOTOR.Do2
    self.MOTOR.getStatusCode()
    if self.Do2.text() == "OFF":
        self.Do2.setText("ON")
        self.Do2.setStyleSheet("background-color: rgb(255, 18, 0);\n"
        "border-color: rgb(255, 18, 0);")
    else:
        self.Do2.setText("OFF")
        self.Do2.setStyleSheet("background-color: rgb(255, 223, 31);\n"
        "border-color: rgb(255, 223, 31);")

def do3_clicked(self):
    self.MOTOR.Do3 = not self.MOTOR.Do3
    self.MOTOR.getStatusCode()
    if self.Do3.text() == "OFF":
        self.Do3.setText("ON")
        self.Do3.setStyleSheet("background-color: rgb(255, 18, 0);\n"
        "border-color: rgb(255, 18, 0);")
    else:
        self.Do3.setText("OFF")
        self.Do3.setStyleSheet("background-color: rgb(255, 223, 31);\n"
        "border-color: rgb(255, 223, 31);")

def do4_clicked(self):
    self.MOTOR.Do4 = not self.MOTOR.Do4
    self.MOTOR.getStatusCode()
    if self.Do4.text() == "OFF":
        self.Do4.setText("ON")
        self.Do4.setStyleSheet("background-color: rgb(255, 18, 0);\n"
        "border-color: rgb(255, 18, 0);")
    else:
        self.Do4.setText("OFF")
        self.Do4.setStyleSheet("background-color: rgb(255, 223, 31);\n"
        "border-color: rgb(255, 223, 31);")

def di1_clicked(self):
    self.MOTOR.Di1 = not self.MOTOR.Di1
    self.MOTOR.getStatusCode()
    if self.Di1.text() == "OFF":
        self.Di1.setText("ON")
        self.Di1.setStyleSheet("border-radius: 50px;\n"
        "\n"
        "background-color: rgb(250, 250, 250);")
    else:
        self.Di1.setText("OFF")
        self.Di1.setStyleSheet("border-radius: 50px;\n"
        "\n"
        "background-color: rgb(201, 201, 201);")

def di2_clicked(self):
    self.MOTOR.Di2 = not self.MOTOR.Di2
    self.MOTOR.getStatusCode()
    if self.Di2.text() == "OFF":
        self.Di2.setText("ON")
        self.Di2.setStyleSheet("border-radius: 50px;\n"
        "\n"
        "background-color: rgb(250, 250, 250);")
    else:
        self.Di2.setText("OFF")
        self.Di2.setStyleSheet("border-radius: 50px;\n"
        "\n"
        "background-color: rgb(201, 201, 201);")

def di3_clicked(self):
    self.MOTOR.Di3 = not self.MOTOR.Di3
    self.MOTOR.getStatusCode()
    if self.Di3.text() == "OFF":
        self.Di3.setText("ON")
        self.Di3.setStyleSheet("border-radius: 50px;\n"
        "\n"
        "background-color: rgb(250, 250, 250);")
    else:
        self.Di3.setText("OFF")
        self.Di3.setStyleSheet("border-radius: 50px;\n"
        "\n"
        "background-color: rgb(201, 201, 201);")

def di4_clicked(self):
    self.MOTOR.Di4 = not self.MOTOR.Di4
    self.MOTOR.getStatusCode()
    if self.Di4.text() == "OFF":
        self.Di4.setText("ON")
        self.Di4.setStyleSheet("border-radius: 50px;\n"
        "\n"
        "background-color: rgb(250, 250, 250);")
    else:
        self.Di4.setText("OFF")
        self.Di4.setStyleSheet("border-radius: 50px;\n"
        "\n"
        "background-color: rgb(201, 201, 201);")

def di5_clicked(self):
    self.MOTOR.Di5 = not self.MOTOR.Di5
    self.MOTOR.getStatusCode()
    if self.Di5.text() == "OFF":
        self.Di5.setText("ON")
        self.Di5.setStyleSheet("border-radius: 50px;\n"
        "\n"
        "background-color: rgb(250, 250, 250);")
    else:
        self.Di5.setText("OFF")
        self.Di5.setStyleSheet("border-radius: 50px;\n"
        "\n"
        "background-color: rgb(201, 201, 201);")

def di6_clicked(self):
    self.MOTOR.Di6 = not self.MOTOR.Di6
    self.MOTOR.getStatusCode()
    if self.Di6.text() == "OFF":
        self.Di6.setText("ON")
        self.Di6.setStyleSheet("border-radius: 50px;\n"
        "\n"
        "background-color: rgb(250, 250, 250);")
    else:
        self.Di6.setText("OFF")
        self.Di6.setStyleSheet("border-radius: 50px;\n"
        "\n"
        "background-color: rgb(201, 201, 201);")

def pos_changed(self):
    self.MOTOR.Pos = self.Pos.text()
    self.MOTOR.getStatusCode()