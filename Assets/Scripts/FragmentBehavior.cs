using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentBehavior : MonoBehaviour
{
    Rigidbody2D FragmentRB;
    [SerializeField] AudioClip FragmentHit;

    public GameObject PowerUp;

    float powerPotential;

    bool launching; 
    // Start is called before the first frame update
    void Start()
    {
        FragmentRB = GetComponent<Rigidbody2D>();
        

        launching = true;

        powerPotential = Random.Range(0.0f, 1);
        Debug.LogFormat($"{powerPotential}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameBehavior.Instance.AsteroidCount -= 1;
            GameBehavior.Instance.UpdateScore(1);
            if (powerPotential >= 0.85f && GameBehavior.Instance.powerUpPresent == false)
            {
                Debug.Log("Power up created!");
                Instantiate(PowerUp, this.transform.position, this.transform.rotation);
                GameBehavior.Instance.powerUpPresent = true;
            }
            Destroy(gameObject);
        
           
        }
    }

    private void FixedUpdate()
    {
        if (launching == true)
        {
            FragmentRB.AddForce(new Vector2(Random.value, Random.value) * Time.deltaTime * GameBehavior.Instance.LaunchMultiplier * 20, ForceMode2D.Impulse);
            FragmentRB.AddTorque(10 * Random.Range(-5, 5) * Time.deltaTime, ForceMode2D.Impulse);
            launching = false;
        }
    }
}
