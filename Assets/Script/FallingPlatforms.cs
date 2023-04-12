using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatforms : MonoBehaviour
{

    public float fallingTime;

    private TargetJoint2D target;
    private BoxCollider2D boxColl;

    // Start é chamado antes da primeira atualização de quadro
    void Start()
    {
        target = GetComponent<TargetJoint2D>();
        boxColl = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
           Invoke("Falling", fallingTime);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if(collider.gameObject.layer == 9)
        {
           Destroy(gameObject);
        }
    }
    
    void Falling()
    {
        target.enabled = false;
        boxColl.isTrigger = true;
    }
    
}

