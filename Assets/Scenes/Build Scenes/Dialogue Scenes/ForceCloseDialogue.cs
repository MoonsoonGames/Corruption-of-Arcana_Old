using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceCloseDialogue : MonoBehaviour
{
    public SceneLoader sceneLoader;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            Debug.Log("F1");
            sceneLoader.LoadLastScene(null);
        }
    }
}
