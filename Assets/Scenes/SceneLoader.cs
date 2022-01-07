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

                DestroyDialogue[] destroyArray = GameObject.FindObjectsOfType<DestroyDialogue>();

                foreach (var item in destroyArray)
                {
                    item.CheckDialogue();
                }
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
                loadSettings.lastLevel = level;

                LoadDialogue(dialogueFlowChart);
                LoadScene(sceneString);
                return;
            }
        }

        #endregion

        LoadDialogue(dialogueFlowChart);

        LoadScene(sceneString);
    }

    public void LoadLastScene(Object dialogueFlowChart)
    {
        //Set load settings level to new level
        LoadDialogue(dialogueFlowChart);
        LoadScene(loadSettings.lastLevelString);
    }

    public void LoadCheckpointScene(Object dialogueFlowChart)
    {
        //Set load settings level to new level
        LoadDialogue(dialogueFlowChart);
        LoadScene(loadSettings.checkPointString);
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
