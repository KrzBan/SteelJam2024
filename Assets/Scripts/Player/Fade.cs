using System;
using System.Collections;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public Action OnFadedOut { get; set; }
    public Action OnFadedIn { get; set; }

    public static Fade Instance;

    public void Out(float time)
    {
        StartCoroutine(IOut(time));
    }
    
    public void In(float time)
    {
        StartCoroutine(IIn(time));
    }

    IEnumerator IOut(float time)
    {
        
    }
    
    IEnumerator IIn(float time)
    {
        
    }
}
