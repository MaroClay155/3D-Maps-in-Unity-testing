using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ArraytoImage : MonoBehaviour
{
    //public RawImage image;
    private Texture2D target;
    private byte[] pcxFile;
    private int timesDone;
    private object pixels;

    //private object pixels;

    // Start is called before the first frame update
    void Start()
    {
        pcxFile = File.ReadAllBytes("Assets/Scripts/mm.png");
        var ms = new MemoryStream(pcxFile);
        //var test1 = Image.FromStream(ms); 
        //foreach(int i in pcxFile)
        //    Debug.Log(pcxFile[i]);
        /*int startPoint = 0;
        int height = 152;
        int width = 152;
        target = new Texture2D(height, width);
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                timesDone++;
                pixels = new Color(pcxFile[startPoint], pcxFile[startPoint + 1], pcxFile[startPoint + 2]);
                startPoint += 4;
                target.SetPixel(x, y, (Color)pixels);
            }
        }
        image.texture = target;
        */
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
