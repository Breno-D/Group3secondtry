﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed = false;
    public KeyCode keyToPress;
    public AudioSource tosse;

    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(keyToPress))
        {
            if(canBePressed)
            {
                gameObject.SetActive(false);

                //GameManager.instance.NoteHit();

                if (Mathf.Abs(transform.position.x) > 0.25f)
                {
                    GameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                } else if (Mathf.Abs(transform.position.x) > 0.05f)
                {
                    GameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }else
                {
                    GameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }

            } 
        }
    }

     private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Activator")
        {
            canBePressed = true;
        }
        
        if(other.tag == "MissedNotes")
        {
            canBePressed = false;
            tosse.Play();
            GameManager.instance.NoteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
              
        }
        
    }

     private void OnTriggerExit2D(Collider2D other) {
        if(other.tag == "Activator")
        {
           // canBePressed = false;
            
            //GameManager.instance.NoteMissed();
        }
        
    }
}
