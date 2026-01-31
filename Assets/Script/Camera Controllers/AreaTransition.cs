using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class AreaTransition : MonoBehaviour
{
    private CameraController cam;
    public Vector2 newMinPos, newMaxPos;
    public Vector3 movePlayer;
    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    private async void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            await Task.Delay(250);
            cam.minPos = newMinPos;
            cam.maxPos = newMaxPos;
            other.transform.position += movePlayer;
        }
    }
}
