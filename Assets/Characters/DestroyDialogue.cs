using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDialogue : MonoBehaviour
{
    LoadSettings loadSettings;

    public SphereCollider collider;
    
    // Start is called before the first frame update
    void Start()
    {
        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (loadSettings != null)
        {
            if (!loadSettings.prologueComplete)
            {
                collider.enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
