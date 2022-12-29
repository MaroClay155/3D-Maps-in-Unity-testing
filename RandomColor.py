import socket
import random
from time import sleep


def sendRandomColors():
    s = socket.socket()          
              
   # connect to the server on local computer 
    s.connect(('172.29.240.1', 1755))
    s.send(
      ( str(random.randint(0,255)) + ","+ 
        str(random.randint(0,255)) + ","+ 
        str(random.randint(0,255)) + ","+ 
        str(random.randint(0,255))
      ).encode()
        )
    
    s.close()

for i in range(100):
    sendRandomColors()
    print('Done Sending '+str(i))
    sleep(1)

