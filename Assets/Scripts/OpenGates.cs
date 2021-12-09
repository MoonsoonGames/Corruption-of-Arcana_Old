using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGates : MonoBehaviour
{
    LoadSettings loadSettings;

    // Start is called before the first frame update
    void Start()
    {
        LoadSettings[] loadSettingsArray = GameObject.FindObjectsOfType<LoadSettings>();

        //Debug.Log(loadSettingsArray.Length);

        foreach (var item in loadSettingsArray)
        {
            if (item.CheckMain())
            {
                loadSettings = item;
            }
        }

        if (loadSettings != null && loadSettings.dialogueComplete)
        {
            Destroy(this.gameObject);
        }
    }
}
