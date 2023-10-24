using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameBehavior : MonoBehaviour
{
  
    // Singleton
    public static GameBehavior Instance;

    public enum GameStates
    {
        Play,
        Pause
    }
    
    public GameStates State;

    [SerializeField] TextMeshProUGUI pauseMessage;
    [SerializeField] TextMeshProUGUI deathMessage;
 
    public float ShipSpeed = 50.0f;

    public float RotateSpeed = 20.0f;

    public PlayerBehavior player;

    public GameObject Asteroid;
    public float AsteroidSpeed = 50.0f;
    public int AsteroidCount = 0;
    public float LaunchMultiplier = 1;

    public bool _isDead;

    AudioSource _source;
    [SerializeField] AudioClip Asteroid_Destroy;
    [SerializeField] AudioClip Pause_Thud;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
        player.Score = 0;
        player.Health = 100;
        _isDead = false;
    }

        // Start is called before the first frame update
    void Start()
    {
        State = GameStates.Play;
        pauseMessage.enabled = false;
        deathMessage.enabled = false;
        _source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            State = State == GameStates.Play ? GameStates.Pause : GameStates.Play;
            pauseMessage.enabled = !pauseMessage.enabled;
        }

        if (Instance.State == GameStates.Play && AsteroidCount <= 5)
        {
            float _pos1 = Random.Range(0.0f, 1.0f);
            float _pos2 = Random.Range(0.0f, 1.0f);

            GameObject newAsteroid = Instantiate(Asteroid, new Vector3(_pos1 >= 0.5f ? (_pos2 >= 0.5f ? 7 : -7) : Random.Range(-5, 5), _pos1 >= 0.5f ? Random.Range(-4.3f, 4.3f) : (_pos2 >= 0.5f ? 5 : -5), 0), new Quaternion(0, 0, 0, 0)) ;

            AsteroidCount += 1;

            //Rigidbody2D AsteroidRB = newAsteroid.GetComponent<Rigidbody2D>();

            //AsteroidRB.AddForce(
            //    (newAsteroid.transform.position.x >= 6 ?
            //    Vector3.left : Vector3.right)
            //    * 100 * Time.deltaTime, ForceMode2D.Impulse);     
        }
    }

    public void Damage()
    {
        player.Health -= 10;
        if(player.Health == 0)
        {
            deathMessage.enabled = true;
            _isDead = true;
            Time.timeScale = 0.0f;
        }

    }

    public void UpdateScore()
    {
        player.Score += 1;
        _source.PlayOneShot(Asteroid_Destroy);
        LaunchMultiplier += 0.1f;
    }
}
