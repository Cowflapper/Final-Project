using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thirdController : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody playerRb;
    private Transform Playertr;
    private Vector3 force;
    public float XYmultiplier;
    public float jumpforce;
    public bool onground;
    public GameObject endscreen;
    void Awake()
    {
        playerRb = GetComponent<Rigidbody>();
        Playertr = GetComponent<Transform>();
    }
    void Start()
    {
        Time.timeScale = 1.0f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 || Input.GetKeyDown("space") == true)
        {
            force.x = Input.GetAxis("Horizontal") * XYmultiplier;
            force.y = Input.GetAxis("Vertical") * XYmultiplier;
            playerRb.velocity = new Vector3(force.x, playerRb.velocity.y, force.y);
            if ((Input.GetKeyDown("space") == true) && onground)
            {
               playerRb.AddForce(0, jumpforce,0);
            }
        }
        if (transform.position.y <= -10)
        {
            endscreen.SetActive(true);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Floor":
                onground = true;
                break;
            case "Enemy":
                endscreen.SetActive(true);
                Time.timeScale = 0.2f;
                break;
            default:
                break;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Floor":
                onground = false;    
                break;
            default:
                break;
        }
    }
}
