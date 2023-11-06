using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilot : MonoBehaviour
{
    public float _horizontal;
    public float _vertical;

    public float BoundaryMod = 1.0f;

    public Rigidbody2D _rb;
    public float knockForce = 5.0f;

    public GameObject bullet;
    public float bulletSpeed = 100f;
    private bool _isFiring;
    public bool isPoweredUp = false;

    public PlayerBehavior player;

    float powerUpSpeed = 1.0f;
    

    AudioSource _source;
    [SerializeField] AudioClip Ship_Damaged;
    [SerializeField] AudioClip Shoot;

    [SerializeField] Material baseShip;
    [SerializeField] Material damagedShip;
    [SerializeField] Material poweredUpShip;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameBehavior.Instance.State == GameBehavior.GameStates.Play && GameBehavior.Instance._isDead == false)
        {
            Time.timeScale = 1.0f;

            _vertical = Input.GetAxis("Vertical") * GameBehavior.Instance.ShipSpeed;

            _horizontal = Input.GetAxis("Horizontal") * GameBehavior.Instance.RotateSpeed;

            //transform.Rotate(Vector3.back * _horizontal * Time.deltaTime);

            transform.Translate(Vector3.up * powerUpSpeed * _vertical * Time.deltaTime);

            _isFiring |= Input.GetKeyDown(KeyCode.Space);

            if (Mathf.Abs(transform.position.x) >= 6.5f)
            {
                transform.position = new Vector3((transform.position.x * -1) + (transform.position.x >= 0 ? 0.1f : -0.1f), transform.position.y, transform.position.z);
            }
            if (Mathf.Abs(transform.position.y) >= 5.1f)
            {
                transform.position = new Vector3(transform.position.x, (transform.position.y * -1) + (transform.position.y >= 0 ? 0.1f : -0.1f), transform.position.z);
            }

        }
        //else
        //{
        //    Time.timeScale = 0.0f;
        //}



    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        Debug.Log("Collision!");

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            GameBehavior.Instance.Damage();
            StartCoroutine(HitCooldown());
            _source.PlayOneShot(Ship_Damaged);
            if (GameBehavior.Instance._isDead == true)
            {
                Time.timeScale = 0.0f;
            }
        }
        

        ContactPoint2D contact = collision.contacts[0];
        Vector3 normal = contact.normal;

        _rb.AddForce(normal * Time.deltaTime * knockForce, ForceMode2D.Impulse);

        //_rb.velocity = normal * knockForce;
    }

    private void OnTriggerEnter2D(Collider2D powerup)
    {
        if (powerup.CompareTag("Powerup"))
        {
            
            isPoweredUp = true;
            StartCoroutine(PowerUpCooldown());
        }
            
    }


    private void FixedUpdate()
    {
        if (GameBehavior.Instance.State == GameBehavior.GameStates.Play && GameBehavior.Instance._isDead == false)
        {

            // This works fine, but the rotation is really slippery and feels imprecise.
            //_rb.AddTorque(_horizontal * -1 * 0.02f * Time.deltaTime, ForceMode2D.Impulse);
            _rb.AddTorque(_horizontal * -1 * powerUpSpeed * Time.deltaTime, ForceMode2D.Force);

            

            if (_isFiring == true)
            {
                GameObject newBullet = Instantiate(bullet, GameObject.Find("Fire Position").transform.position, this.transform.rotation);

                Rigidbody2D bulletRB = newBullet.GetComponent<Rigidbody2D>();

                bulletRB.velocity = this.transform.up * bulletSpeed;

                _source = GetComponent<AudioSource>();
                _source.PlayOneShot(Shoot);
            }

            

            _isFiring = false;
        }
        // How do I get this to go "forward" as opposed to up? It's not Vector3.forward lol

        
        //_rb.AddForce(new Vector2(Mathf.Sin(transform.rotation.z * -2), Mathf.Cos(transform.rotation.z * -2)) * _vertical * Time.deltaTime, ForceMode2D.Impulse);


    }

    IEnumerator HitCooldown()
    {
        gameObject.GetComponent<Collider2D>().isTrigger = true;
        int blinks = 0;
        while (blinks != 6)
        {
            gameObject.GetComponent<SpriteRenderer>().material = damagedShip;
           
            yield return new WaitForSeconds(0.1f);
            blinks += 1;
            gameObject.GetComponent<SpriteRenderer>().material = baseShip;
            yield return new WaitForSeconds(0.1f);
        }
        gameObject.GetComponent<Collider2D>().isTrigger = false;
        
    }

    IEnumerator PowerUpCooldown()
    {
        if (isPoweredUp == true)
        {
            gameObject.GetComponent<SpriteRenderer>().material = poweredUpShip;
            powerUpSpeed = 1.6f;
            GameBehavior.Instance.bulletDamage = 3;
            yield return new WaitForSeconds(6.0f);

            int blinks = 0;
            while (blinks != 6)
            {
                gameObject.GetComponent<SpriteRenderer>().material = poweredUpShip;
                yield return new WaitForSeconds(0.5f);
                blinks += 1;
                gameObject.GetComponent<SpriteRenderer>().material = baseShip;
                yield return new WaitForSeconds(0.5f);
            }

            gameObject.GetComponent<SpriteRenderer>().material = baseShip;
            powerUpSpeed = 1.0f;
            GameBehavior.Instance.bulletDamage = 1;
        }
        
        isPoweredUp = false;
    }

}
