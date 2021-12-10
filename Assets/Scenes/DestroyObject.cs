using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    LoadSettings loadSettings;

    // Start is called before the first frame update
    void Start()
    {
        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (loadSettings.enemiesKilled.Contains("CrystalFight"))
        {
            Destroy(this.gameObject);
        }
    }
}
