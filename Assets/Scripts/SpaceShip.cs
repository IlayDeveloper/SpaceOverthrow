using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : MonoBehaviour
{
    public static SpaceShip Instance;
    
    [Header("References")]
    public GameObject explosion;
    public GameObject littleExplosion;
    public Rigidbody rigidbody;
    public ParticleSystem particle;
    public AudioSource engine;
    
    [Header("Fuel")]
    public float maxFuel;
    public float fuelRaschod;
    private float currentFuel;
    public TMPro.TextMeshProUGUI textFuel;
    
    [Header("HP")]
    public float maxHP;
    private float currentHP;
    public TMPro.TextMeshProUGUI textHP;
    
    [Header("Movement")]
    public float maxSpeed;
    public float maxRotation;
    public float accelerationLinear;
    public float accelerationRotation;
    
    private void Awake()
    {
        currentFuel = maxFuel;
        currentHP = maxHP;
        textFuel.text = currentFuel.ToString();
        textHP.text = currentHP.ToString();

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if (currentFuel > 0)
        {
            LinearMovement();
            
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S))
            {
                currentFuel -= fuelRaschod * Time.deltaTime;
                particle.Play();
            }

            RotateMovement();
            textFuel.SetText(currentFuel.ToString());
        }
    }

    private void LinearMovement()
    {
        if (rigidbody.velocity.magnitude < maxSpeed)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rigidbody.AddForce(transform.forward * accelerationLinear);
                if(!engine.isPlaying) engine.Play();
            }
            else
            {
                if(engine.isPlaying) engine.Stop();
            }

            if (Input.GetKey(KeyCode.S))
            {
                rigidbody.AddForce(-transform.forward * accelerationLinear);
            }
        }
    }
    
    private void RotateMovement()
    {
        if(rigidbody.angularVelocity.magnitude < maxRotation)
        {
            if (Input.GetKey(KeyCode.A))
            {
                rigidbody.AddTorque(transform.up * -accelerationRotation);
            }

            if (Input.GetKey(KeyCode.D))
            {
                rigidbody.AddTorque(transform.up * accelerationRotation);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        string tag = collision.gameObject.tag;

        // Add damage
        if(tag == "Sun" || tag == "Planet")
        {
            currentHP = 0;
        }
        else
        {
            currentHP -= (rigidbody.velocity.magnitude / maxSpeed);
        }
        
        // check HP
        if (currentHP <= 0)
        {
            // Add explosion 
            GameObject exp = Instantiate(explosion);
            exp.transform.position = transform.position;
            
            // Destroy ship
            GameObject.Destroy(gameObject);
            currentHP = 0;
        }
        else
        {
            // Add little explosion
            GameObject exp = Instantiate(littleExplosion, transform);
            exp.transform.position = transform.position;
        }
        
        // Update text info
        textHP.text = currentHP.ToString();
    }

    private void OnTriggerEnter(Collider other)
    {
        Fuel fuel = other.gameObject.GetComponent<Fuel>();
        if (fuel != null)
        {
            currentFuel += fuel.storgedFuel;
            if (currentFuel > maxFuel)
            {
                currentFuel = maxFuel;
            }
            Destroy(fuel.gameObject);
        }
    }
}
