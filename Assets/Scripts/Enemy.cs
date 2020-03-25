using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody), typeof(LineRenderer))]
public class Enemy : MonoBehaviour
{
    public enum State
    {
        Waiting, 
        Following
    }
    public State state { 
        get => currentState;
        set
        {
            switch (value)
            {
                case State.Waiting:
                    modelRenderer.material.color = defaultColor;
                    line.enabled = true;
                    break;
                case State.Following:
                    modelRenderer.material.color = activeColor;
                    line.enabled = false;
                    break;
            }
            currentState = value;
        }
    }
    private State currentState;
    public Transform target { get; set; }
    public Flock flock { get; set; }
    public float speed;
    public float rotSpeed;
    public float centerSpeedRotation;
    public float rangeDetection;
    public Transform outPosition;
    public Color defaultColor;
    public Color activeColor;
    public Renderer modelRenderer;
    public GameObject explosion;
    private LineRenderer line;
    private Rigidbody rb;
    private float radiusLenght;

    private void Awake()
    {
        line = GetComponent<LineRenderer>();
        rb = GetComponent<Rigidbody>();
    }

    public void Init(State s, Transform t, Flock f)
    {
        state = s;
        target = t;
        flock = f;
        Vector3 radius = transform.position - target.position;
        radiusLenght = radius.magnitude;
        centerSpeedRotation *= Random.Range(-1f, 1f);
        speed *= Random.Range(0.1f, 1f);
        rotSpeed *= Random.Range(0.1f, 1f);
    }

    private void Update()
    {
        if(target == null) return;

        if (state == State.Waiting)
        {
            line.SetPosition(0, outPosition.position);
            RaycastHit hit;
            Ray ray = new Ray(outPosition.position, outPosition.forward);
        
            if (Physics.Raycast(ray, out hit, rangeDetection))
            {
                line.SetPosition(1, hit.point);
                if (hit.transform.root.tag == "Player")
                {
                    flock.Detected(hit.transform);
                }
            }
            else
            {
                line.SetPosition(1, outPosition.position + outPosition.forward*rangeDetection);
            }
            
            // Rotate about center
            Rotate();
        }
        else if (state == State.Following)
        {
            Follow();
        }
    }

    private void Follow()
    {
        Quaternion q = Quaternion.LookRotation(target.position - transform.position);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, q, rotSpeed);
        rb.AddForce(transform.forward*speed);
        rb.velocity = Vector3.ClampMagnitude(rb.velocity, 50);
    }

    private void Rotate()
    {
        Vector3 radius = transform.position - target.position;

        Vector3 omega = target.up * centerSpeedRotation;

        Vector3 speed = Vector3.Cross(radius, omega);

        Vector3 newPosition = radius + speed * Time.deltaTime;

        newPosition = Vector3.ClampMagnitude(newPosition, radiusLenght);

        transform.position = newPosition + target.position;
        transform.rotation = Quaternion.LookRotation(speed.normalized);
    }

    private void OnCollisionEnter(Collision other)
    {
        GameObject exp = Instantiate(explosion);
        exp.transform.position = transform.position;
        Destroy(gameObject);
    }
}
