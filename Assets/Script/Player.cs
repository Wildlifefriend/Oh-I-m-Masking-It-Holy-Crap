using UnityEngine;
using System.Collections.Generic;
using System.Collections; 
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    //how fast the player moves 
    [SerializeField]
    private float speed = 5f;

    [SerializeField]
    private float jumpHeight = 6f;

    [SerializeField]
    private float dashForce;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float dashTime;

    private bool canJump = false;

    private int lastDirection = 1; 
    public static LinkedList<Mask> maskList;

    public LinkedListNode<Mask> currentActiveNode;

    public Mask activeMask;

    private float scroll = 0f;

    private bool canDash = true;
    private bool isDashing = false;

    private int maxJump = 1;
    private int jumpRemaining; 


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //initial position of the player 
        transform.position = GetComponent<Transform>().position;
        rb = GetComponent<Rigidbody2D>();
        maskList = new LinkedList<Mask>();

    }

    // Update is called once per frame
    void Update()
    {
        move();
        MaskController();
        performAction();
    }

    //player movement 
    void move()
    {
        if (isDashing) return;
        float moveX = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
            lastDirection = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
            lastDirection = 1;
        }

        rb.linearVelocity = new Vector2(
            moveX * speed,
            rb.linearVelocity.y
        );

        if (Input.GetKeyDown(KeyCode.Space) && jumpRemaining > 0)
        {
            jumpRemaining--;
            rb.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
            
        }
    }

    void performAction()
    {

            //depends on the mask type perform the following actions 
            Mask curr = activeMask;

            if (curr is HorseMask)
            {
                Debug.Log("Current is horse");
                maxJump = 1;
                if (Input.GetMouseButtonDown(1) && canDash)
                {
                    Debug.Log("Currently dashing");
                    StartCoroutine(HorseDash());

                }
            }

            if (curr is FrogMask)
            {
                Debug.Log("Current is frog");
                maxJump = 2;

            }
        
    }


    System.Collections.IEnumerator HorseDash()
    {
        canDash = false;
        isDashing = true;
        rb.linearVelocity = new Vector2(lastDirection * dashForce, rb.linearVelocity.y);

        yield return new WaitForSeconds(dashTime); // dash duration

        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);

        isDashing = false;
    }

    // mask Cycling and Selection
    private void MaskController()
    {
        scroll = Input.GetAxis("Mouse ScrollWheel");
        if (maskList.Count != 0 && scroll != 0f)
        {
            CycleMask();
            showSelection();
        }

        if (Input.GetKey(KeyCode.E))
        {
            SelectMask();
            showActive();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Floor") && !isDashing)
        {
            canDash = true;
            maxJump = 1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Floor"))
        {
            jumpRemaining = maxJump;
            canDash = true;
        
        }
    }

    public void setSpeed(float speed)
    {
        this.speed = speed;
    }

    public float getSpeed()
    {
        return this.speed;
    }

    //Helps for the logic of looping the linked list
    public LinkedListNode<Mask> GetNextMask(LinkedListNode<Mask> currentNode)
    {
        // If next is null, we are at the tail, so return the head
        return currentNode.Next ?? currentNode.List.First;
    }

    public LinkedListNode<Mask> GetPreviousMask(LinkedListNode<Mask> currentNode)
    {
        // If previous is null, we are at the head, so return the tail
        return currentNode.Previous ?? currentNode.List.Last;
    }


    // On a scroll wheel cycle it will change the mask being selected
    private void CycleMask()
    {
        if (maskList.Count == 0) return;

        if (currentActiveNode == null)
        {
            currentActiveNode = maskList.First;
        }

        else
        {
            // Checking for scroll wheel movement
            if (scroll < 0f)
            {
                currentActiveNode = GetNextMask(currentActiveNode);
                // Logic to cycle mask forward
            }
            else if (scroll > 0f)
            {
                currentActiveNode = GetPreviousMask(currentActiveNode);
                // Logic to cycle mask backward
            }
        }

        showSelection();
    }

    private void SelectMask()
    {
        activeMask = currentActiveNode.Value;
    }


    public void pickUpMask(Mask mask)
    {
        // Add to list so we can actually cycle it!
        if (maskList == null) maskList = new LinkedList<Mask>();
        maskList.AddLast(mask);

        // If it's the first mask, show it in the UI immediately
        if (currentActiveNode == null)
        {
            currentActiveNode = maskList.First;
            showSelection();
        }


        // Hide the physical mask object on the ground
        mask.gameObject.SetActive(false);
    }

    public Sprite showActive()
    {
        if (activeMask != null)
        {
            
            return activeMask.icon;
        }

        return null;
    }

    public Sprite showSelection()
    {
        if (currentActiveNode != null)
        {
            
            return currentActiveNode.Value.icon;
        }

        return null;
    }

}




