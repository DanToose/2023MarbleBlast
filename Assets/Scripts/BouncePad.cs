using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncePad : MonoBehaviour
{
    public GameObject player;
    public Rigidbody rb;
    public float bouncePower;

    public bool killMovement;

    private float xVelocity;
    private float zVelocity;
    
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = player.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (killMovement == false)
            {
                xVelocity = rb.velocity.x;
                zVelocity = rb.velocity.z;
                rb.velocity = new(xVelocity, bouncePower, zVelocity);
            }
            else
            {
                rb.velocity = new(0, bouncePower, 0);
            }
        }
    }

}
