# -*- coding: utf-8 -*-

# Form implementation generated from reading ui file 'MotorUI.ui'
#
# Created by: PyQt5 UI code generator 5.15.6
#
# WARNING: Any manual changes made to this file will be lost when pyuic5 is
# run again.  Do not edit this file unless you know what you are doing.

from PyQt5 import QtCore, QtGui, QtWidgets
import MotorStatus as ms

class Ui_MainWindow(object):
	from MotorUI_Action import do1_clicked, do2_clicked, do3_clicked, do4_clicked, \
								di1_clicked, di2_clicked, di3_clicked, di4_clicked, di5_clicked, di6_clicked,\
								pos_changed
	
	def setupUi(self, MainWindow):
		self.MOTOR = ms.MotorStatus()
		self.MOTOR.getStatusCode()
		MainWindow.setObjectName("MainWindow")
		MainWindow.resize(517, 734)
		self.centralwidget = QtWidgets.QWidget(MainWindow)
		self.centralwidget.setObjectName("centralwidget")
		self.groupBox = QtWidgets.QGroupBox(self.centralwidget)
		self.groupBox.setGeometry(QtCore.QRect(90, 10, 101, 131))
		font = QtGui.QFont()
		font.setPointSize(16)
		self.groupBox.setFont(font)
		self.groupBox.setObjectName("groupBox")
		self.Do1 = QtWidgets.QPushButton(self.groupBox)
		self.Do1.setGeometry(QtCore.QRect(0, 30, 100, 100))
		font = QtGui.QFont()
		font.setPointSize(24)
		self.Do1.setFont(font)
		self.Do1.setLayoutDirection(QtCore.Qt.LeftToRight)
		self.Do1.setStyleSheet("background-color: rgb(255, 223, 31);\n"
"border-color: rgb(255, 223, 31);")
		self.Do1.setObjectName("Do1")
		self.groupBox_11 = QtWidgets.QGroupBox(self.centralwidget)
		self.groupBox_11.setGeometry(QtCore.QRect(90, 600, 131, 80))
		font = QtGui.QFont()
		font.setPointSize(16)
		self.groupBox_11.setFont(font)
		self.groupBox_11.setObjectName("groupBox_11")
		self.Pos = QtWidgets.QLineEdit(self.groupBox_11)
		self.Pos.setGeometry(QtCore.QRect(10, 30, 113, 41))
		font = QtGui.QFont()
		font.setPointSize(20)
		self.Pos.setFont(font)
		self.Pos.setObjectName("Pos")
		self.groupBox_2 = QtWidgets.QGroupBox(self.centralwidget)
		self.groupBox_2.setGeometry(QtCore.QRect(210, 10, 101, 131))
		font = QtGui.QFont()
		font.setPointSize(16)
		self.groupBox_2.setFont(font)
		self.groupBox_2.setObjectName("groupBox_2")
		self.Do2 = QtWidgets.QPushButton(self.groupBox_2)
		self.Do2.setGeometry(QtCore.QRect(0, 30, 100, 100))
		font = QtGui.QFont()
		font.setPointSize(24)
		self.Do2.setFont(font)
		self.Do2.setLayoutDirection(QtCore.Qt.LeftToRight)
		self.Do2.setStyleSheet("background-color: rgb(255, 223, 31);\n"
"border-color: rgb(255, 223, 31);")
		self.Do2.setObjectName("Do2")
		self.groupBox_3 = QtWidgets.QGroupBox(self.centralwidget)
		self.groupBox_3.setGeometry(QtCore.QRect(90, 150, 101, 131))
		font = QtGui.QFont()
		font.setPointSize(16)
		self.groupBox_3.setFont(font)
		self.groupBox_3.setObjectName("groupBox_3")
		self.Do3 = QtWidgets.QPushButton(self.groupBox_3)
		self.Do3.setGeometry(QtCore.QRect(0, 30, 100, 100))
		font = QtGui.QFont()
		font.setPointSize(24)
		self.Do3.setFont(font)
		self.Do3.setLayoutDirection(QtCore.Qt.LeftToRight)
		self.Do3.setStyleSheet("background-color: rgb(255, 223, 31);\n"
"border-color: rgb(255, 223, 31);")
		self.Do3.setObjectName("Do3")
		self.groupBox_4 = QtWidgets.QGroupBox(self.centralwidget)
		self.groupBox_4.setGeometry(QtCore.QRect(210, 150, 101, 131))
		font = QtGui.QFont()
		font.setPointSize(16)
		self.groupBox_4.setFont(font)
		self.groupBox_4.setObjectName("groupBox_4")
		self.Do4 = QtWidgets.QPushButton(self.groupBox_4)
		self.Do4.setGeometry(QtCore.QRect(0, 30, 100, 100))
		font = QtGui.QFont()
		font.setPointSize(24)
		self.Do4.setFont(font)
		self.Do4.setLayoutDirection(QtCore.Qt.LeftToRight)
		self.Do4.setStyleSheet("background-color: rgb(255, 223, 31);\n"
"border-color: rgb(255, 223, 31);")
		self.Do4.setObjectName("Do4")
		self.groupBox_6 = QtWidgets.QGroupBox(self.centralwidget)
		self.groupBox_6.setGeometry(QtCore.QRect(90, 300, 111, 131))
		font = QtGui.QFont()
		font.setPointSize(16)
		self.groupBox_6.setFont(font)
		self.groupBox_6.setObjectName("groupBox_6")
		self.Di1 = QtWidgets.QPushButton(self.groupBox_6)
		self.Di1.setGeometry(QtCore.QRect(10, 30, 100, 100))
		font = QtGui.QFont()
		font.setPointSize(24)
		self.Di1.setFont(font)
		self.Di1.setLayoutDirection(QtCore.Qt.LeftToRight)
		self.Di1.setStyleSheet("border-radius: 50px;\n"
"\n"
"background-color: rgb(201, 201, 201);")
		self.Di1.setObjectName("Di1")
		self.groupBox_7 = QtWidgets.QGroupBox(self.centralwidget)
		self.groupBox_7.setGeometry(QtCore.QRect(210, 300, 111, 131))
		font = QtGui.QFont()
		font.setPointSize(16)
		self.groupBox_7.setFont(font)
		self.groupBox_7.setObjectName("groupBox_7")
		self.Di2 = QtWidgets.QPushButton(self.groupBox_7)
		self.Di2.setGeometry(QtCore.QRect(10, 30, 100, 100))
		font = QtGui.QFont()
		font.setPointSize(24)
		self.Di2.setFont(font)
		self.Di2.setLayoutDirection(QtCore.Qt.LeftToRight)
		self.Di2.setStyleSheet("border-radius: 50px;\n"
"\n"
"background-color: rgb(201, 201, 201);")
		self.Di2.setObjectName("Di2")
		self.groupBox_8 = QtWidgets.QGroupBox(self.centralwidget)
		self.groupBox_8.setGeometry(QtCore.QRect(330, 300, 111, 131))
		font = QtGui.QFont()
		font.setPointSize(16)
		self.groupBox_8.setFont(font)
		self.groupBox_8.setObjectName("groupBox_8")
		self.Di3 = QtWidgets.QPushButton(self.groupBox_8)
		self.Di3.setGeometry(QtCore.QRect(10, 30, 100, 100))
		font = QtGui.QFont()
		font.setPointSize(24)
		self.Di3.setFont(font)
		self.Di3.setLayoutDirection(QtCore.Qt.LeftToRight)
		self.Di3.setStyleSheet("border-radius: 50px;\n"
"\n"
"background-color: rgb(201, 201, 201);")
		self.Di3.setObjectName("Di3")
		self.groupBox_9 = QtWidgets.QGroupBox(self.centralwidget)
		self.groupBox_9.setGeometry(QtCore.QRect(330, 440, 111, 131))
		font = QtGui.QFont()
		font.setPointSize(16)
		self.groupBox_9.setFont(font)
		self.groupBox_9.setObjectName("groupBox_9")
		self.Di6 = QtWidgets.QPushButton(self.groupBox_9)
		self.Di6.setGeometry(QtCore.QRect(10, 30, 100, 100))
		font = QtGui.QFont()
		font.setPointSize(24)
		self.Di6.setFont(font)
		self.Di6.setLayoutDirection(QtCore.Qt.LeftToRight)
		self.Di6.setStyleSheet("border-radius: 50px;\n"
"\n"
"background-color: rgb(201, 201, 201);")
		self.Di6.setObjectName("Di6")
		self.groupBox_10 = QtWidgets.QGroupBox(self.centralwidget)
		self.groupBox_10.setGeometry(QtCore.QRect(210, 440, 111, 131))
		font = QtGui.QFont()
		font.setPointSize(16)
		self.groupBox_10.setFont(font)
		self.groupBox_10.setObjectName("groupBox_10")
		self.Di5 = QtWidgets.QPushButton(self.groupBox_10)
		self.Di5.setGeometry(QtCore.QRect(10, 30, 100, 100))
		font = QtGui.QFont()
		font.setPointSize(24)
		self.Di5.setFont(font)
		self.Di5.setLayoutDirection(QtCore.Qt.LeftToRight)
		self.Di5.setStyleSheet("border-radius: 50px;\n"
"\n"
"background-color: rgb(201, 201, 201);")
		self.Di5.setObjectName("Di5")
		self.groupBox_12 = QtWidgets.QGroupBox(self.centralwidget)
		self.groupBox_12.setGeometry(QtCore.QRect(90, 440, 111, 131))
		font = QtGui.QFont()
		font.setPointSize(16)
		self.groupBox_12.setFont(font)
		self.groupBox_12.setObjectName("groupBox_12")
		self.Di4 = QtWidgets.QPushButton(self.groupBox_12)
		self.Di4.setGeometry(QtCore.QRect(10, 30, 100, 100))
		font = QtGui.QFont()
		font.setPointSize(24)
		self.Di4.setFont(font)
		self.Di4.setLayoutDirection(QtCore.Qt.LeftToRight)
		self.Di4.setStyleSheet("border-radius: 50px;\n"
"\n"
"background-color: rgb(201, 201, 201);")
		self.Di4.setObjectName("Di4")
		MainWindow.setCentralWidget(self.centralwidget)
		self.statusbar = QtWidgets.QStatusBar(MainWindow)
		self.statusbar.setObjectName("statusbar")
		MainWindow.setStatusBar(self.statusbar)

		self.retranslateUi(MainWindow)
		self.Do1.clicked.connect(self.do1_clicked)
		self.Do2.clicked.connect(self.do2_clicked)
		self.Do3.clicked.connect(self.do3_clicked)
		self.Do4.clicked.connect(self.do4_clicked) 
		self.Di1.clicked.connect(self.di1_clicked)
		self.Di2.clicked.connect(self.di2_clicked) 
		self.Di3.clicked.connect(self.di3_clicked) 
		self.Di4.clicked.connect(self.di4_clicked) 
		self.Di5.clicked.connect(self.di5_clicked) 
		self.Di6.clicked.connect(self.di6_clicked)  
		self.Pos.textChanged.connect(self.pos_changed)
		QtCore.QMetaObject.connectSlotsByName(MainWindow)

	def retranslateUi(self, MainWindow):
		_translate = QtCore.QCoreApplication.translate
		MainWindow.setWindowTitle(_translate("MainWindow", "MainWindow"))
		self.groupBox.setTitle(_translate("MainWindow", "Do1"))
		self.Do1.setText(_translate("MainWindow", "OFF"))
		self.groupBox_11.setTitle(_translate("MainWindow", "Pos"))
		self.Pos.setText(_translate("MainWindow", "0"))
		self.groupBox_2.setTitle(_translate("MainWindow", "Do2"))
		self.Do2.setText(_translate("MainWindow", "OFF"))
		self.groupBox_3.setTitle(_translate("MainWindow", "Do3"))
		self.Do3.setText(_translate("MainWindow", "OFF"))
		self.groupBox_4.setTitle(_translate("MainWindow", "Do4"))
		self.Do4.setText(_translate("MainWindow", "OFF"))
		self.groupBox_6.setTitle(_translate("MainWindow", "Di1"))
		self.Di1.setText(_translate("MainWindow", "OFF"))
		self.groupBox_7.setTitle(_translate("MainWindow", "Di2"))
		self.Di2.setText(_translate("MainWindow", "OFF"))
		self.groupBox_8.setTitle(_translate("MainWindow", "Di3"))
		self.Di3.setText(_translate("MainWindow", "OFF"))
		self.groupBox_9.setTitle(_translate("MainWindow", "Di6"))
		self.Di6.setText(_translate("MainWindow", "OFF"))
		self.groupBox_10.setTitle(_translate("MainWindow", "Di5"))
		self.Di5.setText(_translate("MainWindow", "OFF"))
		self.groupBox_12.setTitle(_translate("MainWindow", "Di4"))
		self.Di4.setText(_translate("MainWindow", "OFF"))
