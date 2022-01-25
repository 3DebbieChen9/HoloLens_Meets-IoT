class MotorStatus:
    def __init__(self):
        self.Do1 = False
        self.Do2 = False
        self.Do3 = False
        self.Do4 = False
        self.Di1 = False
        self.Di2 = False
        self.Di3 = False
        self.Di4 = False
        self.Di5 = False
        self.Di6 = False
        self.Pos = 0
    
    def getStatusCode(self):
        doStatus = f"Do1-{self.Do1},Do2-{self.Do2},Do3-{self.Do3},Do4-{self.Do4},"
        diStatus = f"Di1-{self.Di1},Di2-{self.Di2},Di3-{self.Di3},Di4-{self.Di4},Di5-{self.Di5},Di6-{self.Di6},"
        posStatus = f"Pos-{self.Pos}"
        with open("MotorStatus.txt", "w") as f:
            f.write(doStatus + diStatus + posStatus)
        
        return doStatus + diStatus + posStatus
