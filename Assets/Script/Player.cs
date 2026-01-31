using UnityEngine;
using System.Collections.Generic;

public class Player : MonoBehaviour
{

    //how fast the player moves 
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float jumpHeight = 6f;

    [SerializeField]
    private Rigidbody2D rb;

    private bool canJump = false;

    public static LinkedList<Mask> maskList; 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //initial position of the player 
        transform.position = new Vector3(0, 0, 0);
        rb = GetComponent<Rigidbody2D>();
        maskList = new LinkedList<Mask>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    //player movement 
    void move()
    {

        if (Input.GetKey(KeyCode.A))
        {
            transform.position += Vector3.left * speed * Time.deltaTime; 
        }


        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canJump)
            {
                //move the player up (jump)
                rb.AddForce(Vector3.up * jumpHeight, ForceMode2D.Impulse);
            }

            canJump = false;
           
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            canJump = true;
        }
    }

}
