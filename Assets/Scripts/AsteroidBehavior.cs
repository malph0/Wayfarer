using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AsteroidBehavior : MonoBehaviour
{
    public float AsteroidHealth;
    public Rigidbody2D AsteroidRB;

    AudioSource _source;
    [SerializeField] AudioClip AsteroidExplosion;
    [SerializeField] AudioClip AsteroidHit;
    SpriteRenderer _appearance;

    bool launching = false;
 
    // Start is called before the first frame update
    void Start()
    {
        AsteroidRB = GetComponent<Rigidbody2D>();
        _source = GetComponent<AudioSource>();
        _appearance = GetComponent<SpriteRenderer>();

        launching = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(AsteroidHealth <= 0)
        {
            Destroy(gameObject);
            GameBehavior.Instance.AsteroidCount -= 1;
            GameBehavior.Instance.UpdateScore();

        }
        else if(Mathf.Abs(transform.position.x) >= 8 | Mathf.Abs(transform.position.y) >= 7)
        {
            Destroy(gameObject);
            GameBehavior.Instance.AsteroidCount -= 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            AsteroidHealth -= 1;
            Debug.Log("Hit!");
            gameObject.GetComponent<Material>();
            _source.volume = 0.2f;
            _source.PlayOneShot(AsteroidHit);
            
        } 
    }

    private void FixedUpdate()
    {
        if(launching == true)
        {
            AsteroidRB.AddForce(
                (Mathf.Abs(this.transform.position.x) >= 6.5 ?
                (this.transform.position.x >= 0 ? Vector3.left : Vector3.right)
                : (this.transform.position.y >= 0 ? Vector3.down : Vector3.up))
                * 400 * GameBehavior.Instance.LaunchMultiplier * Time.deltaTime, ForceMode2D.Impulse);
            AsteroidRB.AddTorque(10 * Random.Range(-5, 5) * Time.deltaTime, ForceMode2D.Impulse);
            launching = false;
        }
    }
}
