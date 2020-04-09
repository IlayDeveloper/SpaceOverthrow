using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : PoolObject
{
    public float speed;
    public float lifeTime;
    public GameObject explosion;
    private float timeToDestroy;
    
    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * speed * Time.deltaTime;
        timeToDestroy -= Time.deltaTime;
        if (timeToDestroy <= 0)
        {
            PoolManager.Instance.ReturnObject(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Enemy enemy = other.gameObject.GetComponentInParent<Enemy>();
        if (enemy != null)
        {
            enemy.DestroyThisEnemy();
        }

        PoolObjectDestroy();
        Instantiate(explosion, transform.position, transform.rotation);
    }

    private void OnEnable()
    {
        timeToDestroy = lifeTime;
    }
}
