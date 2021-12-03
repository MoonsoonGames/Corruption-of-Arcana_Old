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

        switch (sceneToLoad)
        {
            case E_Levels.ThothTesting:
                loadSettings.SetLastLevel(sceneToLoad);
                break;
        }

        #endregion


        Debug.Log("Load Scene");
        SceneManager.LoadScene(sceneString);
    }

    public void LoadLastScene()
    {
        SceneManager.LoadScene(loadSettings.GetLastLevel().ToString());
    }

    public void LoadCheckpointScene()
    {
        SceneManager.LoadScene(loadSettings.GetCheckpointScene().ToString());
    }

    public void LoadSpecifiedScene(string scene, LoadSceneMode sceneMode)
    {
        SceneManager.LoadScene(scene, sceneMode);
    }
}
