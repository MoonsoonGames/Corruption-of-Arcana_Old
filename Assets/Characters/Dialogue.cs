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

    public bool checkpoint = false;

    public Object dialogue;

    public LoadSceneMode sceneMode;

    public QuestObjective[] completeObjectives;

    // Start is called before the first frame update
    private void Start()
    {
        sceneString = sceneToLoad.ToString();

        currentScene = SceneManager.GetActiveScene();

        sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
        loadSettings = LoadSettings.instance;
    }

    public bool LoadScene()
    {
        if (sceneLoader != null && loadSettings != null)
        {
            if (CanSpeak())
            {
                if (checkpoint)
                {
                    loadSettings.SetCheckpoint();

                    PlayerController controller = GameObject.Find("Player").GetComponent<PlayerController>();

                    if (controller != null)
                    {
                        Debug.Log(SceneManager.GetActiveScene());
                        loadSettings.checkPointPotionCount = controller.GetPotions();
                        loadSettings.checkPointPos = controller.transform.position;
                        loadSettings.checkPointScene = SceneManager.GetActiveScene();
                    }
                }

                loadSettings.dialogueFlowChart = dialogue;
                loadSettings.loadSceneMultiple = sceneMode == LoadSceneMode.Additive;
                sceneLoader.LoadSpecifiedScene(sceneString, sceneMode, dialogue);

                if (completeObjectives.Length != 0)
                {
                    foreach (var item in completeObjectives)
                    {
                        item.CompleteGoal();
                    }
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    #region Enable/ Disable Dialogue

    public Quest[] requireQuests;
    public QuestObjective[] requireObjectives;
    public bool requireAll = true;

    public Quest[] disableQuests;
    public QuestObjective[] disableObjectives;

    public bool CanSpeak()
    {
        bool enableDialogue = false;

        bool contains1 = false;
        bool containsAll = true;

        foreach (var item in requireQuests)
        {
            if (item.isComplete)
            {
                contains1 = true;
            }
            else
            {
                containsAll = false;
            }
        }

        foreach (var item in requireObjectives)
        {
            if (item.completed)
            {
                contains1 = true;
            }
            else
            {
                containsAll = false;
            }
        }

        enableDialogue = (containsAll) || (!requireAll && contains1);

        foreach (var item in disableQuests)
        {
            if (item.isComplete)
            {
                enableDialogue = false;
            }
        }

        foreach (var item in disableObjectives)
        {
            if (item.completed)
            {
                enableDialogue = false;
            }
        }

        return enableDialogue;
    }

    #endregion
}
