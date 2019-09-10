using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird_Scrpt : MonoBehaviour
{

private Animator anim;
private GameControl GCInstance;
public float UpForce = 250f;
public int rotateSpeed = 10;
private bool isDead = false;
private float startTime;
public bool variablePress = false;
private Rigidbody2D rb2d;



    // Start is called before the first frame updated
    void Start()

    {
        rb2d = GetComponent<Rigidbody2D> ();
        anim = GetComponent<Animator> ();
        GCInstance = GameObject.Find("GameControl").GetComponent<GameControl>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            if (!variablePress)
            {
                if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0))
                {
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(0, UpForce));
                    anim.SetTrigger("Flap");
                }
            }
            else
            {
                if (Input.GetKeyDown (KeyCode.Space) || Input.GetMouseButtonDown (0))
                {
                    startTime = Time.time;
                    //Debug.Log(startTime);
                }
                if (Input.GetKeyUp (KeyCode.Space) || Input.GetMouseButtonUp (0))
                {
                    float heldTime = Mathf.Abs(Time.time - startTime);
                    float ForceMultiplier = heldTime + 0.8f;
                    rb2d.velocity = Vector2.zero;
                    rb2d.AddForce(new Vector2(0, UpForce * ForceMultiplier));
                    anim.SetTrigger("Flap");
                    //Debug.Log(heldTime);
                /*     if (heldTime <= 0.1)
                    {
                        Debug.Log("smallfore");
                        rb2d.velocity = Vector2.zero;
                        rb2d.AddForce(new Vector2(0, UpForce-150));
                        anim.SetTrigger("Flap");
                    }
                    if (heldTime > 0.1 && heldTime < 0.3)
                    {
                        Debug.Log("medsmallfore");
                        rb2d.velocity = Vector2.zero;
                        rb2d.AddForce(new Vector2(0, UpForce-75));
                        anim.SetTrigger("Flap");
                    }
                    if (heldTime > 0.3 && heldTime < 0.5)
                    {
                        Debug.Log("mediumfore");
                        rb2d.velocity = Vector2.zero;
                        rb2d.AddForce(new Vector2(0, UpForce));
                        anim.SetTrigger("Flap");
                    }
                    if (heldTime >= 0.5)
                    {
                        Debug.Log("maxfore");
                        rb2d.velocity = Vector2.zero;
                        rb2d.AddForce(new Vector2(0, UpForce+300));
                        anim.SetTrigger("Flap");
                    } */
                }
            }
        }
        rb2d.rotation = rb2d.velocity.y*rotateSpeed;
    }

    void OnCollisionEnter2D() {
        if (!isDead)
        {
            isDead = true;
            anim.SetTrigger("Die");
            GCInstance.BirdDied();
            rb2d.velocity = Vector2.zero;
        }
    }

}
