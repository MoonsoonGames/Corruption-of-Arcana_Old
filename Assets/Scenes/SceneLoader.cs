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
            case E_Levels.Thoth:
                loadSettings.SetLastLevel(E_Levels.Thoth);
                break;
            case E_Levels.Clearing:
                loadSettings.SetLastLevel(E_Levels.Clearing);
                break;
        }

        #endregion

        Debug.Log("Load Scene");
        loadSettings.currentLevel = sceneToLoad;
        SceneManager.LoadScene(sceneString);
    }

    public void LoadLastScene()
    {
        //Set load settings level to new level
        SceneManager.LoadScene(loadSettings.GetLastLevel().ToString());
    }

    public void LoadCheckpointScene()
    {
        //Set load settings level to new level
        SceneManager.LoadScene(loadSettings.GetCheckpointScene().ToString());
    }

    public void LoadSpecifiedScene(string scene, LoadSceneMode sceneMode)
    {
        //Set load settings level to new level

        #region Check Navigation Scenes

        switch (sceneToLoad)
        {
            case E_Levels.Thoth:
                loadSettings.SetLastLevel(E_Levels.Thoth);
                break;
            case E_Levels.Clearing:
                loadSettings.SetLastLevel(E_Levels.Clearing);
                break;
        }

        #endregion

        SceneManager.LoadScene(scene, sceneMode);
    }
}
