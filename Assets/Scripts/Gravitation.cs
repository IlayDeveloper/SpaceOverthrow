using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Gravitation : MonoBehaviour
{
    private float planetMass;
    public static List<GravityTarget> targets { get; set; }
    void Start()
    {
        planetMass = GetComponent<Rigidbody>().mass;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(GamePlayController.Instance.state != GamePlayController.State.Play) return;
        
        foreach (var target in targets)
        {
            AddForce(target);
        }
    }

    private void AddForce(GravityTarget target)
    {
        float force = target.gravityRate * (planetMass * target.rigidBody.mass) / Vector3.SqrMagnitude(target.transform.position - transform.position);
        target.rigidBody.AddForce(( transform.position - target.transform.position ).normalized * force);
    }
}
