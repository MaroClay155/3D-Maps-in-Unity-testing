import socket
from time import sleep
import matplotlib.pyplot as plt 
import numpy as np  
def sendImageViaSocket():

    Fs = 8000
    f = 10 #10Hz
    sample = 8000
    x = np.arange(sample)
    y = np.sin(2 * np.pi * f * x / Fs)
    plt.plot(x, y)
    plt.xlabel('sample(n)')
    plt.ylabel('voltage(V)')
    plt.savefig('sinwave.png')
    my_Image = "sinwave.png"

    my_Socket = socket.socket()
    my_Socket.connect(('172.29.240.1', 1755))

    my_File = open(my_Image, 'rb')
    Image_data = my_File.read(1024)
    while Image_data:
        my_Socket.send( (str(Image_data)+",").encode() )
        Image_data = my_File.read(1024)
    my_File.close()
    print('Done Sending')

    my_Socket.close()

for i in range(1):
    sendImageViaSocket()
    sleep(1)    

