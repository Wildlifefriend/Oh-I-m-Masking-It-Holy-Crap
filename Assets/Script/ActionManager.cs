using UnityEngine;
using System.Collections;

public class ActionManager : MonoBehaviour
{
    
    float baseSpeed;
    float currentSpeed;
    float maxSpeed = 25f;



    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartAction(Mask mask, Player player)
    {
        //Gives the player a temporary Dash, can be done in the air
        if(mask is HorseMask)
        {
            Debug.Log("Got Here 4");
            // This starts the timer-based function
            if (Input.GetMouseButtonDown(1))
            {
                Debug.Log("Right Click???");
                StartCoroutine(squirrelAbility(player));
            }
            
        }
        
        if(mask is SpiderMask)
        {

        }

        if(mask is MoleMask)
        {

        }

        if(mask is FrogMask)
        {

        }

        if(mask is DragonMask)
        {

        }
    }

    IEnumerator squirrelAbility(Player player)
    {
        float end = 0.5f;
        float start = 0f; 

        while (start <= end)
        {
            currentSpeed = start * maxSpeed * (start / end);
            player.setSpeed(currentSpeed);

            yield return null;
        }

        player.setSpeed(baseSpeed);
    }
}
