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

    public void LoadDefaultScene(Object dialogueFlowChart)
    {
        #region Check Navigation Scenes
        //Debug.Log(sceneToLoad);
        foreach (var level in navScenes)
        {
            if (level == sceneToLoad)
            {
                //Debug.Log(level + " is the same");
                loadSettings.lastLevel = level;

                LoadDialogue(dialogueFlowChart);
                SceneManager.LoadScene(sceneString);
                return;
            }
        }

        #endregion

        LoadDialogue(dialogueFlowChart);

        SceneManager.LoadScene(sceneString);
    }

    public void LoadLastScene(Object dialogueFlowChart)
    {
        //Set load settings level to new level
        LoadDialogue(dialogueFlowChart);
        SceneManager.LoadScene(loadSettings.lastLevelString);
    }

    public void LoadCheckpointScene(Object dialogueFlowChart)
    {
        //Set load settings level to new level
        LoadDialogue(dialogueFlowChart);
        SceneManager.LoadScene(loadSettings.checkPointString);
    }

    public void LoadSpecifiedScene(string scene, LoadSceneMode sceneMode, Object dialogueFlowChart)
    {
        //Set load settings level to new level

        LoadDialogue(dialogueFlowChart);
        SceneManager.LoadScene(scene, sceneMode);
    }

    void LoadDialogue(Object dialogueFlowChart)
    {
        //Debug.Log(dialogueFlowChart);
        if (dialogueFlowChart != null)
        {
            loadSettings.dialogueFlowChart = dialogueFlowChart;
            Debug.Log(loadSettings.dialogueFlowChart);
        }
    }
}
