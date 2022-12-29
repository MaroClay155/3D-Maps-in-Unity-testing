using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube1_script : MonoBehaviour
{
    public Sprite Sinewave;
    private SpriteRenderer rend;
    // Start is called before the first frame update
    void Start()
    {
        //this.gameObject.GetComponent<SpriteRenderer>().sprite = Sinewave;
        //gameObject.material.mainTexture = Image_Texture;
        rend = GetComponent<SpriteRenderer>();
        Sinewave = Resources.Load<Sprite>("sinewave");
        rend.sprite = Sinewave;
        //Material m = new Material(Load())

    }

    // Update is called once per frame
    void Update()
    {
        rend.sprite = Sinewave;
    }
}
