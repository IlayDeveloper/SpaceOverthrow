using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
   public static PoolManager Instance;
   public List<Pool> pools;
   public Dictionary<GameObject, Pool> poolingOfObjects;
   
   
   private void Awake()
   {
      if (Instance == null)
      {
         Instance = this;
      }
      else
      {
         Destroy(gameObject);
      }
      poolingOfObjects = new Dictionary<GameObject, Pool>();
   }

   public GameObject GetPoolObject(PoolObject poolObject, Vector3 position, Quaternion rotation, Transform parent)
   {
      foreach (var pool in pools)
      {
         if (pool.poolObjectPrefab == poolObject)
         {
            GameObject temp = pool.GetObject(parent);
            temp.transform.position = position;
            temp.transform.rotation = rotation;
            temp.SetActive(true);
            poolingOfObjects.Add(temp, pool);
            return temp;
         }
      }

      return null;
   }

   public void ReturnObject(GameObject poolObject)
   {
      poolObject.SetActive(false);
      poolingOfObjects[poolObject].ReturObject(poolObject);
      poolingOfObjects.Remove(poolObject);
   }
   
}
