using UnityEngine;
using System.Collections.Generic;

public class Mask : MonoBehaviour
{
    protected float jumpHeight; 
    protected float movementSpeed;

 
    [SerializeField]
    public Sprite icon;

    public Sprite MaskIcon => icon;

   

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if it hit the player then 
        if (collision.gameObject.CompareTag("Player"))
        {
            Player p = collision.gameObject.GetComponent<Player>();

            p.pickUpMask(this); 

            addMask(this, Player.maskList);
            //Hide the object
            HideObject();

            printll(Player.maskList);
            
        }
    }

    void addMask(Mask mask, LinkedList<Mask> ll)
    {
        ll.AddLast(mask);
        
    }

    void HideObject()
    {
        //GetComponent<Renderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
    }
    void printll(LinkedList<Mask> ll)
    {
        foreach(Mask i in ll)
        {
            Debug.Log(i.GetType().Name + "\n");
        }
    }
}
