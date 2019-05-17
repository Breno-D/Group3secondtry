using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : MonoBehaviour
{
    private SpriteRenderer theSR;
    public Sprite defaultImg, pressedImg;
    public KeyCode keyToPress;
    void Start()
    {
        theSR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            theSR.sprite = pressedImg;
        }

        if(Input.GetKeyUp(keyToPress))
        {
            theSR.sprite = defaultImg;
        }
    }
}
