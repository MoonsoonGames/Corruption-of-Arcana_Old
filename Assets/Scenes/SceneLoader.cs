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

        loadSettings = LoadSettings.instance;
    }

    void LoadScene(string scene)
    {
        bool contains = false;

        int index = 9999999;

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (scene == SceneManager.GetSceneAt(i).name)
            {
                Debug.Log(scene + " || " + SceneManager.GetSceneAt(i).name);
                loadSettings.SetPlayerInput(true);
                contains = true;
                index = i;
            }
        }

        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            if (i != index)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneAt(i));
            }
        }

        if (!contains)
        {
            SceneManager.LoadScene(scene);
        }
    }

    public void LoadDefaultScene(Object dialogueFlowChart)
    {
        #region Check Navigation Scenes
        //Debug.Log(sceneToLoad);
        foreach (var level in navScenes)
        {
            if (level == sceneToLoad)
            {
                //Debug.Log(level + " is the same");
                SetLoadSettingsScene(level.ToString());

                LoadDialogue(dialogueFlowChart);
                LoadScene(sceneString);
                return;
            }
        }

        #endregion

        SetLoadSettingsScene(SceneManager.GetActiveScene().name);

        LoadDialogue(dialogueFlowChart);

        LoadScene(sceneString);
    }

    public void LoadLastScene(Object dialogueFlowChart)
    {
        //Set load settings level to new level
        LoadDialogue(dialogueFlowChart);
        LoadScene(loadSettings.lastLevel.ToString());
    }
    
    public void LoadCheckpointScene(Object dialogueFlowChart)
    {
        //Set load settings level to new level
        loadSettings.died = true;
        loadSettings.LoadCheckpointData();

        LoadDialogue(dialogueFlowChart);
        LoadScene(loadSettings.checkPointString);
    }

    public void LoadSpecifiedScene(string scene, LoadSceneMode sceneMode, Object dialogueFlowChart)
    {
        SetLoadSettingsScene(SceneManager.GetActiveScene().name);

        //Set load settings level to new level
        LoadDialogue(dialogueFlowChart);
        SceneManager.LoadScene(scene, sceneMode);
    }

    public void LoadMainMenu()
    {
        //Set load settings level to new level
        SceneManager.LoadScene(E_Levels.SplashScreen.ToString(), LoadSceneMode.Single);
    }

    void LoadDialogue(Object dialogueFlowChart)
    {
        //Debug.Log(dialogueFlowChart);
        if (dialogueFlowChart != null)
        {
            loadSettings.dialogueFlowChart = dialogueFlowChart;
            //Debug.Log(loadSettings.dialogueFlowChart);
        }
    }

    void SetLoadSettingsScene(string newScene)
    {
        if (GetCurrentSceneEnum() == E_Levels.CombatPrototype || (GetCurrentSceneEnum() == E_Levels.Dialogue))
        {
            //Do nothing
            //Debug.Log(GetCurrentSceneEnum().ToString() + " Scene is: " + E_Levels.CombatPrototype + " or " + E_Levels.Dialogue);
        }
        else
        {
            //Debug.Log(GetCurrentSceneEnum().ToString() + " Scene is not: " + E_Levels.CombatPrototype + " or " + E_Levels.Dialogue);
            loadSettings.lastLevel = (E_Levels)System.Enum.Parse(typeof(E_Levels), newScene);
        }
    }

    E_Levels GetCurrentSceneEnum()
    {
        return (E_Levels)System.Enum.Parse(typeof(E_Levels), SceneManager.GetActiveScene().name);
    }
}
