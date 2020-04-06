using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public PoolObject poolObjectPrefab;
    public int poolCount;

    private GameObject CreateObject()
    {
        GameObject temp = Instantiate(poolObjectPrefab.gameObject);
        temp.transform.position = transform.position;
        temp.transform.rotation = transform.rotation;
        temp.transform.SetParent(transform);
        temp.SetActive(false);
        return temp;
    }

    public GameObject GetObject(Transform parent)
    {
        if (transform.childCount > 0)
        {
            GameObject temp = transform.GetChild(0).gameObject;
            temp.transform.SetParent(parent);
            return temp;
        }
        else
        {
            return CreateObject();
        }
    }

    public void ReturObject(GameObject poolObject)
    {
        if (transform.childCount < poolCount)
        {
            poolObject.SetActive(false);
            poolObject.transform.SetParent(transform);
        }
        else
        {
            Destroy(poolObject);
        }
    }
    
    private void Awake()
    {
        for (int i = 0; i < poolCount; i++)
        {
            CreateObject();
        }
    }
}
