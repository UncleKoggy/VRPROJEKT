using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public float delay = 3f;
    public float radius = 5f;
    public float force = 700f;

    public GameObject explosionEffect;
    public GameObject expEffClone;

    float countdown;
    bool hasExploded = false;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
      
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "target")
        {
            Instantiate(explosionEffect, transform.position, transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }
            }
            //Destroy(explosionEffect);
            Destroy(gameObject);
        }
    }

    /*public void Explode()
    {
        //countdown = delay;
        //countdown -= Time.deltaTime;
        if (!hasExploded)
        {
            StartCoroutine("WaitAndExplode");


            Instantiate(explosionEffect, transform.position, transform.rotation);

            Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

            foreach (Collider nearbyObject in colliders)
            {
                Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.AddExplosionForce(force, transform.position, radius);
                }
            }
            Destroy(explosionEffect);
            Destroy(gameObject);
            hasExploded = true;
        }
    }
    IEnumerator WaitAndExplode(float delay)
    {
        yield return new WaitForSeconds(delay);
    }*/
}
