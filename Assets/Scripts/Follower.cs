using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    public SpaceShip spaceShip;
    public Transform camera;
    public float down;
    public float up;

    void Start()
    {
        camera.position = spaceShip.transform.position + spaceShip.transform.up * down;
    }

    // Update is called once per frame
    void Update()
    {
        float speed = spaceShip.rigidbody.velocity.magnitude;
        float maxSpeed = spaceShip.maxSpeed;
        camera.position = spaceShip.transform.position + spaceShip.transform.up * Mathf.Lerp(down, up, speed / maxSpeed);
    }
}
