using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AreaTransition : MonoBehaviour
{
    [SerializeField]
    private CameraController cam;
 
    [SerializeField]
    private Vector2 newMinPos, newMaxPos;
    [SerializeField]
    private bool canTeleport;
    [SerializeField]
    private Transform newLocation;

    private async void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && canTeleport)
        {
            Debug.Log("Teleporting player");
            await Task.Delay(250);
            cam.minPos = newMinPos;
            cam.maxPos = newMaxPos;

            Vector3 pos = other.transform.position;
            pos.x = newLocation.position.x;
            pos.y = newLocation.position.y;
            other.transform.position = pos;
        }
    }
}
