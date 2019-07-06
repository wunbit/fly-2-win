using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column_Script : MonoBehaviour
{
   void OnTriggerEnter2D(Collider2D other)
    {
         if (other.GetComponent<Bird_Scrpt>() != null)  //Aqui no esta buscando el objeto en si sino el script (lo cual es raro pero bueno)
         {
             GameControl.instance.BirdScored ();
         }
    }
}
