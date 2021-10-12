using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerController : MonoBehaviour
{
    Vector3 RiverRescueNPC = new Vector3(25, 1.75f, -48);
    Vector3 ThothInn = new Vector3();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "ThothRiver")
        {
            this.transform.position = RiverRescueNPC;
            //this.transform.position = playerRiverRescue;
            //Lock player in position
            //Load rescue dialogue
            //Unlock player
        }
        else if (col.gameObject.CompareTag("InnWayIn"))
        {
            SceneManager.LoadScene("Inn");
        }

        else if (col.gameObject.CompareTag("InnWayOut"))
        {
            //Load previous scene
            this.transform.position = ThothInn;
        }
    }
}