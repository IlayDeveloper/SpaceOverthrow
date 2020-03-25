using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Flock : MonoBehaviour
{
    public int enemyCount;
    public float spotRange;
    public GameObject enemyPrefab;
    private Enemy[] enemies;
    //public Transform target;

    private void Awake()
    {
        Spawn();
    }

    private void Spawn()
    {
        List<Enemy> enemiesPool = new List<Enemy>();
        for (int i = 0; i < enemyCount; i++)
        {
            Vector3 pos = transform.position + new Vector3(Random.Range(-spotRange, spotRange), 0, Random.Range(-spotRange, spotRange));
            GameObject go = Instantiate(enemyPrefab, pos, Quaternion.identity);
            Enemy e = go.GetComponent<Enemy>();
            e.Init( Enemy.State.Waiting, transform, this);
            enemiesPool.Add(e);
        }

        enemies = enemiesPool.ToArray();
    }

    public void Detected(Transform target)
    {
        foreach (var e in enemies)
        {
            if(e == null) return;
            
            e.state = Enemy.State.Following;
            e.target = target;
        }
    }
}
