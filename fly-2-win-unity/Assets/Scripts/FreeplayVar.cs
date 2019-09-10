using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeplayVar : MonoBehaviour
{
    public bool freePlay;

    public void Awake()
    {
        DontDestroyOnLoad(this);
    }
}
