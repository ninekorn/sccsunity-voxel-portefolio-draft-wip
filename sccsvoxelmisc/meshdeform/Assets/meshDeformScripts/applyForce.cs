using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class applyForce : MonoBehaviour {

    Rigidbody rb;
    bool applyForceOnce = false;
    public float forceMultiplicator = 0;
    public Vector3 force = new Vector3(-5, -5, -5);
    public Transform parentObject;

    void Start ()
    {

        rb = transform.GetComponent<Rigidbody>();
    }

    void Update()
    {

        Physics.IgnoreCollision(parentObject.GetComponent<MeshCollider>(), GetComponent<MeshCollider>());
        Vector3 realPos = transform.position;
        Vector3 velocity = rb.velocity;

        if (rb.isKinematic == false)
        {
            if (applyForceOnce == false)
            {
                
                rb.AddForce(force * forceMultiplicator, ForceMode.Force);
                applyForceOnce = true;
            }
        }
        //ProjectileHelper.UpdateProjectile(ref realPos, ref velocity, -9.81f, 0f);

        //transform.gameObject.GetComponent<Rigidbody>().AddForce(velocity, ForceMode.Impulse);


        transform.position = realPos;
    }
}
