using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShipMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private ShipAnimation shipAnimation;

    public float launchForce = 10f;
    public float thrustForce = 20f;
    public float torqueForce;
    public float angularDragNormal = 0.5f;
    public float angularDragForce = 0.1f;
    private float torque;

    public float rotationSpeed = 60f;

    public bool launched;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.angularDrag = angularDragNormal;

        shipAnimation = GetComponent<ShipAnimation>();
    }

    private void FixedUpdate()
    {
        if(launched == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                rb.AddForce(transform.up * launchForce, ForceMode2D.Impulse);
                launched = true;
                shipAnimation.isMoving = true;
            }

            if (Input.GetKey(KeyCode.A))
            {
                transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.D))
            {
                transform.Rotate(-Vector3.forward * rotationSpeed * Time.deltaTime);
            }
        }

        else
        {
            if (Input.GetKey(KeyCode.Space))
            {
                rb.AddForce(transform.up * thrustForce);
                rb.angularDrag += angularDragForce;
                launched = true;
                shipAnimation.isMoving = true;
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                rb.angularDrag = angularDragNormal;
                shipAnimation.isMoving = false;
            }

        }
    }

    public void HitTarget()
    {
        shipAnimation.hit = true;
        torque = Random.Range(-torqueForce, torqueForce);
        rb.AddTorque(torqueForce, ForceMode2D.Impulse);
    }

}
