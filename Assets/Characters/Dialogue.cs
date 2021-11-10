using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public E_Levels sceneToLoad;
    string sceneString;
    Scene currentScene;

    SceneLoader sceneLoader;

    LoadSettings loadSettings;

    // Start is called before the first frame update
    private void Start()
    {
        sceneString = sceneToLoad.ToString();

        currentScene = SceneManager.GetActiveScene();

        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();
    }

    public void LoadScene()
    {
        if (sceneLoader != null)
        {
            if (loadSettings != null)
                loadSettings.dialogueComplete = true;

            sceneLoader.LoadSpecifiedScene(sceneString, LoadSceneMode.Single);
        }
    }
}
