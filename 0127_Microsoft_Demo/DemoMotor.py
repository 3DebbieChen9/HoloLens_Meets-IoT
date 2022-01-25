from telnetlib import DO
from PyQt5 import QtWidgets, QtGui, QtCore
from MotorUI_v2 import Ui_MainWindow
import sys
    

class MainWindow(QtWidgets.QMainWindow):
    def __init__(self):
        super(MainWindow, self).__init__()
        self.ui = Ui_MainWindow()
        self.ui.setupUi(self)


if __name__ == '__main__':
    app = QtWidgets.QApplication([])
    window = MainWindow()
    window.show()
    sys.exit(app.exec_())