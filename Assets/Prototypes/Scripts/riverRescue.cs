using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class riverRescue : MonoBehaviour
{
    Vector3 RiverRescueNPC = new Vector3(25, 1.75f, -48);
    public GameObject player;

    private void OnCollisionEnter(Collision collision)
    {
        player.transform.position = RiverRescueNPC;
        //this.transform.position = playerRiverRescue;
        //Lock player in position
        //Load rescue dialogue
        //Unlock player
    }

}