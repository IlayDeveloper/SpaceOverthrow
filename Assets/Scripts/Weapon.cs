using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public PoolObject projectilePrefab;
    public float fireRate;
    private float timeToFire=1f;

    // Update is called once per frame
    void Update()
    {
        timeToFire -= Time.deltaTime * fireRate;
        if (timeToFire <= 0)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                GameObject temp = PoolManager.Instance.GetPoolObject(projectilePrefab, transform.position, transform.rotation, transform);
 
            }

            timeToFire = 1f;
        }
        
    }
}
