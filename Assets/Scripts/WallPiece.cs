using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallPiece : MonoBehaviour
{
    
    Rigidbody rb;
    float tm;
    float expValue;



    void Start()
    {
        tm = 1.0f;
        expValue = -2.0f;
        rb = GetComponent<Rigidbody>();
        rb.drag = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {

        tm -= Time.deltaTime; 
        if(tm < 0 )
        {
            if(rb.velocity.magnitude > 0.0f)
            {
                rb.drag += Mathf.Pow(2, expValue);
            }
            else
            {
                rb.drag = 0.5f;
                rb.isKinematic = true;
            }
            
            expValue += 0.5f;
            tm = 1.0f;
        }

                
        
    }

    void OnCollisionEnter(Collision col)
    {
        

    }
}
