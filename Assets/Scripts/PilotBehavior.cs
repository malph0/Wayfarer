using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilot : MonoBehaviour
{
    public float _horizontal;
    public float _vertical;

    public float BoundaryMod = 1.0f;

    public Rigidbody2D _rb;

    public GameObject bullet;
    public float bulletSpeed = 100f;
    private bool _isFiring;

    public PlayerBehavior player;

    AudioSource _source;
    [SerializeField] AudioClip Ship_Damaged;
    [SerializeField] AudioClip Shoot;

    // Start is called before the first frame update
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody2D>();
        _source = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameBehavior.Instance.State == GameBehavior.GameStates.Play && GameBehavior.Instance._isDead == false)
        {
            Time.timeScale = 1.0f;

            _vertical = Input.GetAxis("Vertical") * GameBehavior.Instance.ShipSpeed;

            _horizontal = Input.GetAxis("Horizontal") * GameBehavior.Instance.RotateSpeed;

            transform.Rotate(Vector3.back * _horizontal * Time.deltaTime);

            transform.Translate(Vector3.up * _vertical * Time.deltaTime);

            _isFiring |= Input.GetKeyDown(KeyCode.Space);


        }
        //else
        //{
        //    Time.timeScale = 0.0f;
        //}


        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       // if (collision.gameObject.CompareTag("Boundary"))
           // Bump = true;
            
            Debug.Log("Collision!");

        if (collision.gameObject.CompareTag("Asteroid"))
        {
            GameBehavior.Instance.Damage();
            _source.PlayOneShot(Ship_Damaged);
            if(GameBehavior.Instance._isDead == true)
            {
                Time.timeScale = 0.0f;
            }
        }
    }

    private void FixedUpdate()
    {
        if (GameBehavior.Instance.State == GameBehavior.GameStates.Play && GameBehavior.Instance._isDead == false)
        {

            // This works fine, but the rotation is really slippery and feels imprecise.
            //_rb.AddTorque(_horizontal * -1 * Time.deltaTime, ForceMode2D.Force);

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
        //_rb.AddForce(Vector3.fwd * _vertical * Time.deltaTime, ForceMode2D.Impulse);

        
    }

   
}
