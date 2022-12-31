using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class PythonApp : MonoBehaviour
{
    static Socket listener;
    private CancellationTokenSource source;
    public ManualResetEvent allDone;
    public Renderer objectRenderer;
    private Texture my_Image;

    public static readonly int PORT = 1755;
    public static readonly int WAITTIME = 1;


    PythonApp()
    {
        source = new CancellationTokenSource();
        allDone = new ManualResetEvent(false);
    }

    // Start is called before the first frame update
    async void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        await Task.Run(() => ListenEvents(source.Token));   
    }

    // Update is called once per frame
    void Update()
    {
        objectRenderer.material.mainTexture = my_Image;
    }

    private void ListenEvents(CancellationToken token)
    {
        IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        IPAddress ipAddress = ipHostInfo.AddressList.FirstOrDefault(ip => ip.AddressFamily == AddressFamily.InterNetwork);
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PORT);

         
        listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

         
        try
        {
            listener.Bind(localEndPoint);
            listener.Listen(10);

             
            while (!token.IsCancellationRequested)
            {
                allDone.Reset();

                print("Waiting for a connection... host :" + ipAddress.MapToIPv4().ToString() + " port : " + PORT);
                listener.BeginAccept(new AsyncCallback(AcceptCallback),listener);

                while(!token.IsCancellationRequested)
                {
                    if (allDone.WaitOne(WAITTIME))
                    {
                        break;
                    }
                }
      
            }

        }
        catch (Exception e)
        {
            print(e.ToString());
        }
    }

    void AcceptCallback(IAsyncResult ar)
    {  
        Socket listener = (Socket)ar.AsyncState;
        Socket handler = listener.EndAccept(ar);
 
        allDone.Set();

        ImageObject img = new ImageObject();
        img.workSocket = handler;
        handler.BeginReceive(img.buffer, 0, ImageObject.BufferSize, 0, new AsyncCallback(ReadCallback), img);
    }

    void ReadCallback(IAsyncResult ar)
    {
        ImageObject img = (ImageObject)ar.AsyncState;
        Socket handler = img.workSocket;

        int read = handler.EndReceive(ar);
  
        if (read > 0)
        {
            img.colorCode.Append(Encoding.ASCII.GetString(img.buffer, 0, read));
            handler.BeginReceive(img.buffer, 0, ImageObject.BufferSize, 0, new AsyncCallback(ReadCallback), img);
        }
        else
        {
            if (img.colorCode.Length > 1)
            { 
                string content = img.colorCode.ToString();
                print($"Read {content.Length} bytes from socket.\n Data : {content}");
                SetImage(content);
            }
            handler.Close();
        }
    }

    //Set color to the Material
    private void SetImage (string data) 
    {
        string[] colors = data.Split(',');
        Debug.Log(colors);
        //my_Image = byteArrayTo

    }

    private void OnDestroy()
    {
        source.Cancel();
    }

    public class ImageObject
    {
        public Socket workSocket = null;
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder colorCode = new StringBuilder();
    }
}