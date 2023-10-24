using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    private float _despawnTime = 2.0f;

    
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, _despawnTime);
    }

    private void CheckCollision(Collision2D collision, string contact)
    {
        if(collision.gameObject.CompareTag(contact))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        CheckCollision(collision, "Asteroid");

        CheckCollision(collision, "Boundary");
        
    }
   
}
