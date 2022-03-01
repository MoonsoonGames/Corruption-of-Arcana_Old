using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public E_Levels sceneToLoad;
    string sceneString;
    Scene currentScene;

    public bool forceDialogue = false;

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

        sceneLoader = GetComponent<SceneLoader>();
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
                        loadSettings.checkPointHealingPotionCount = controller.GetPotions();
                        loadSettings.checkPointPos = controller.transform.position;
                        loadSettings.checkPointScene = SceneManager.GetActiveScene();
                    }
                }

                if (completeObjectives.Length != 0)
                {
                    foreach (var item in completeObjectives)
                    {
                        item.CompleteGoal();
                    }
                }

                loadSettings.SetScene(SceneManager.GetActiveScene().name);

                loadSettings.dialogueFlowChart = dialogue;
                loadSettings.loadSceneMultiple = sceneMode == LoadSceneMode.Additive;
                sceneLoader.LoadSpecifiedScene(sceneString, sceneMode, dialogue);

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

    public bool CanSpeak()
    {
        return CheckQuestsInProgress() && CheckQuestsCompleted();
    }

    #region Quest Progress Requirements

    public Quest[] requireQuestsInProgress;
    public QuestObjective[] requireObjectivesInProgress;
    public bool requireAllInProgress = true;

    public Quest[] disableQuestsInProgress;
    public QuestObjective[] disableObjectivesInProgress;

    public bool CheckQuestsInProgress()
    {
        bool enableNode = false;

        bool contains1 = false;
        bool containsAll = true;

        foreach (var item in requireQuestsInProgress)
        {
            if (item.isActive)
            {
                contains1 = true;
            }
            else
            {
                containsAll = false;
            }
        }

        foreach (var item in requireObjectivesInProgress)
        {
            if (item.canComplete)
            {
                contains1 = true;
            }
            else
            {
                containsAll = false;
            }
        }

        enableNode = (containsAll) || (!requireAllCompleted && contains1);

        foreach (var item in disableQuestsInProgress)
        {
            if (item.isActive)
            {
                enableNode = false;
            }
        }

        foreach (var item in disableObjectivesInProgress)
        {
            if (item.canComplete)
            {
                enableNode = false;
            }
        }

        return enableNode;
    }

    #endregion

    #region Quest Completed Requirements

    public Quest[] requireQuestsCompleted;
    public QuestObjective[] requireObjectivesCompleted;
    public bool requireAllCompleted = true;

    public Quest[] disableQuestsCompleted;
    public QuestObjective[] disableObjectivesCompleted;

    public bool CheckQuestsCompleted()
    {
        bool enableNode = false;

        bool contains1 = false;
        bool containsAll = true;

        foreach (var item in requireQuestsCompleted)
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

        foreach (var item in requireObjectivesCompleted)
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

        enableNode = (containsAll) || (!requireAllCompleted && contains1);

        foreach (var item in disableQuestsCompleted)
        {
            if (item.isComplete)
            {
                enableNode = false;
            }
        }

        foreach (var item in disableObjectivesCompleted)
        {
            if (item.completed)
            {
                enableNode = false;
            }
        }

        return enableNode;
    }

    #endregion
}
