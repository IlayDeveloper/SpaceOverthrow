using System;
using System.Collections;
using UnityEngine;

public class DestroyByTime : MonoBehaviour
{
    public float timer;

    private void Awake()
    {
        StartCoroutine(Destroy());
    }

    IEnumerator Destroy()
    {
        yield return new WaitForSeconds(timer);
        Destroy(gameObject);
    }
}