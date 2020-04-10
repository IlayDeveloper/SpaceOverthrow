using UnityEngine;

public class Follower : MonoBehaviour
{
    public SpaceShip spaceShip;
    public Camera camera;
    public float down;
    public float up;
    private Vector3 offset;

    void Awake()
    {
        // camera.position = spaceShip.transform.position + spaceShip.transform.up * down;
        offset = camera.transform.position - spaceShip.transform.position;
    }
    
    private void OnValidate()
    {
        // camera.position = spaceShip.transform.position + spaceShip.transform.up * down;
        camera.orthographicSize = down;
    }

    // Update is called once per frame
    void Update()
    {
        if(!spaceShip) return;
        float speed = spaceShip.rigidbody.velocity.magnitude;
        float maxSpeed = spaceShip.maxSpeed;
        // camera.position = spaceShip.transform.position + spaceShip.transform.up * Mathf.Lerp(down, up, speed / maxSpeed);
        camera.orthographicSize =  Mathf.Lerp(down, up, speed / maxSpeed);;
        camera.transform.position = spaceShip.transform.position + offset;
    }
}
