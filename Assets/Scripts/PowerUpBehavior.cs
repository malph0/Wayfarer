using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehavior : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameBehavior.Instance.powerUpPresent = false;
            Destroy(gameObject);
        }
            
    
    }

}
