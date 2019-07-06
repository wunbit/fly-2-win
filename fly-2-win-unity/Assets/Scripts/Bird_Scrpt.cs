using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Scrpt : MonoBehaviour
{

private Animator anim;
public float UpForce = 200f;
public int rotateSpeed = 10;
private bool isDead = false;
private Rigidbody2D rb2d;



    // Start is called before the first frame updated
    void Start()

    {
         rb2d = GetComponent<Rigidbody2D> ();
         anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false)
        {
            if (Input.GetKeyDown (KeyCode.Space))
            {
                rb2d.velocity = Vector2.zero;
                rb2d.AddForce(new Vector2(0, UpForce));
                anim.SetTrigger("Flap");
            }
        }
        rb2d.rotation = rb2d.velocity.y*rotateSpeed;
    }

    void OnCollisionEnter2D() {
        isDead = true;
        anim.SetTrigger("Die");
        GameControl.instance.BirdDied();
        rb2d.velocity = Vector2.zero;
    }

}
