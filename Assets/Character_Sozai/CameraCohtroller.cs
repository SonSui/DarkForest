using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCohtroller : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject player;
    float playerX;
    float playerY;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float newY=0;
        playerX = player.transform.position.x;
        playerY = player.transform.position.y;
        if (playerY > -6) newY = 0;
        if (playerY < -6) newY = -11;
        transform.position = new Vector3 (playerX,newY, transform.position.z);
        
    }
}
