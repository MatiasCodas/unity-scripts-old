using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionEffect : MonoBehaviour
{
    public float explosionForce = 100;
    public float radius = 100f;


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player")||col.CompareTag("Bola"))
        {
            Rigidbody2D rb = col.GetComponent<Rigidbody2D>();

            var explosionDir = rb.transform.position - transform.position;
            var explosionDistance = explosionDir.magnitude;

            explosionDir /= explosionDistance;

            rb.AddForce(Mathf.Lerp(100, explosionForce, ( explosionDistance)) * explosionDir, ForceMode2D.Force);
            Debug.Log(rb);
            Debug.Log(explosionDistance);
            Debug.Log(explosionDir);
            //rb.AddExplosionForce(explosionForce, transform.position, radius);
        }
    }
}
