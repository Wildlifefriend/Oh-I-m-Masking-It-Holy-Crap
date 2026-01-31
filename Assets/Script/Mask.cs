using UnityEngine;
using System.Collections.Generic;

public class Mask : MonoBehaviour
{
    protected float jumpHeight; 
    protected float movementSpeed;

    public Sprite icon;

    public Sprite MaskIcon => icon; 

    protected void giveAbility()
    {
        return;
    }

   

    void OnCollisionEnter2D(Collision2D collision)
    {
        //if it hit the player then 
        if (collision.gameObject.CompareTag("Player"))
        {
            
            
            addMask(this, Player.maskList);
            //destory the object
            Destroy(gameObject);

            printll(Player.maskList);
            
        }
    }

    void addMask(Mask mask, LinkedList<Mask> ll)
    {
        ll.AddLast(mask);
        
    }

    void printll(LinkedList<Mask> ll)
    {
        foreach(Mask i in ll)
        {
            Debug.Log(i.GetType().Name + "\n");
        }
    }
}
