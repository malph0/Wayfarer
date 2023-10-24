using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilot : MonoBehaviour
{
    public KeyCode UpDirection;
    public KeyCode DownDirection;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameBehavior.Instance.State == GameBehavior.GameStates.Play)
        {
            if (Input.GetKey(UpDirection))
            {
                transform.position += new Vector3(0, GameBehavior.Instance.ShipSpeed, 0) * Time.deltaTime;
            }

            else if (Input.GetKey(DownDirection))
            {
                transform.position -= new Vector3(0, GameBehavior.Instance.ShipSpeed, 0) * Time.deltaTime;
            }
        }
    }
}
