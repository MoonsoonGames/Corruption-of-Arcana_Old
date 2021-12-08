using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public E_Levels sceneToLoad;
    string sceneString;
    Scene currentScene;
    LoadSettings loadSettings;

    public E_Levels[] navScenes;

    // Start is called before the first frame update
    private void Start()
    {
        sceneString = sceneToLoad.ToString();

        currentScene = SceneManager.GetActiveScene();

        loadSettings = GameObject.FindObjectOfType<LoadSettings>();
    }

    public void LoadDefaultScene()
    {
        #region Check Navigation Scenes
        //Debug.Log(sceneToLoad);
        foreach (var level in navScenes)
        {
            if (level == sceneToLoad)
            {
                //Debug.Log(level + " is the same");
                loadSettings.lastLevel = level;
                
                SceneManager.LoadScene(sceneString);
                return;
            }
        }

        #endregion

        SceneManager.LoadScene(sceneString);
    }

    public void LoadLastScene()
    {
        //Set load settings level to new level
        SceneManager.LoadScene(loadSettings.lastLevelString);
    }

    public void LoadCheckpointScene()
    {
        //Set load settings level to new level
        SceneManager.LoadScene(loadSettings.GetCheckpointScene().ToString());
    }

    public void LoadSpecifiedScene(string scene, LoadSceneMode sceneMode)
    {
        //Set load settings level to new level

        SceneManager.LoadScene(scene, sceneMode);
    }
}
