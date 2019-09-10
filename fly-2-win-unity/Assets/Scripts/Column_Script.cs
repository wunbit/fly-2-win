using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column_Script : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Bird_Scrpt>() != null)
        {
            GameControl.instance.BirdScored ();
        }
    }
}
