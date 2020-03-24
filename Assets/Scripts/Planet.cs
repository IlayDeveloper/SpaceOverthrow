using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
    public float selfSpeedRotation;
    public float centerSpeedRotation;
    public Transform target;
    private float radiusLenght;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 radius = transform.position - target.position;
        radiusLenght = radius.magnitude;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        SelfRotation();
        AboutCenterRotation();
    }

    private void SelfRotation()
    {
        transform.localRotation = transform.localRotation * Quaternion.Euler(0, selfSpeedRotation * Time.deltaTime, 0);
    }

    // 
    private void AboutCenterRotation()
    {
        Vector3 radius = transform.position - target.position;

        Vector3 omega = target.up * centerSpeedRotation;

        Vector3 speed = Vector3.Cross(radius, omega);

        //transform.position = transform.position + speed * Time.deltaTime;

        Vector3 newPosition = radius + speed * Time.deltaTime;

        newPosition = Vector3.ClampMagnitude(newPosition, radiusLenght);

        transform.position = newPosition + target.position;
    }
}
