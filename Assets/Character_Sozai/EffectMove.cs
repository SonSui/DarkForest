using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectMove : MonoBehaviour
{
    public GameObject player;
    Vector3 posOri = Vector3.zero;
    Vector3 playerOri = Vector3.zero;
    void Start()
    {
        posOri = transform.position;
        playerOri = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 playerNew = player.transform.position - playerOri;
        Vector3 newPos = new Vector3(posOri.x + playerNew.x * -0.5f, posOri.y, posOri.z);
        transform.position = newPos;
    }
}
