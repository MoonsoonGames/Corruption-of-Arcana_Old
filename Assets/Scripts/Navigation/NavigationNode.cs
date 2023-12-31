using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigationNode : MonoBehaviour
{
    NavigationManager navManager;
    SceneLoader sceneLoader;
    E_Levels navScene;
    public E_Levels dialogueScene;
    public int entranceCount = 0;
    
    public NavigationNode[] possibleNodes;
    public Object path;

    public Object[] possibleEvents;
    public float ignoreChance = 0.8f;

    public Button button;
    public GameObject marker;
    public Color currentColour;
    public Color availableColour;
    public Color unavailableColour;
    public bool canTravelTo = false;

    [HideInInspector]
    public bool stopEvents = false;
    [HideInInspector]
    public bool stopLevels = false;

    public Sprite[] backgrounds;

    public void Setup(NavigationManager navigationManager, E_Levels newNavScene, bool current)
    {
        navManager = navigationManager;
        navScene = newNavScene;

        sceneLoader = GetComponentInChildren<SceneLoader>();

        if (current)
        {
            foreach (var item in possibleNodes)
            {
                item.SetAvailable(true);
            }

            SetCurrent();
        }
    }

    public void DrawPaths()
    {
        if (marker.activeSelf)
        {
            foreach (var item in possibleNodes)
            {
                if (item.marker.activeSelf)
                {
                    GameObject pathRef = Instantiate(path, this.gameObject.transform) as GameObject;
                    LineRenderer pathRenderer = pathRef.GetComponent<LineRenderer>();

                    Vector3 startPos = this.gameObject.transform.position;
                    startPos.z -= 200;
                    pathRenderer.SetPosition(0, startPos);
                    Vector3 endPos = item.gameObject.transform.position;
                    endPos.z -= 200;
                    pathRenderer.SetPosition(1, endPos);
                }
            }
        }
    }

    public void SetCurrent()
    {
        button.interactable = true;
        canTravelTo = true;

        button.image.color = currentColour;
    }

    public void SetAvailable(bool available)
    {
        if (CheckQuestsInProgress() && CheckQuestsCompleted())
        {
            button.interactable = available;
            canTravelTo = available;
            button.image.raycastTarget = available;
            marker.SetActive(true);

            if (available)
            {
                button.image.color = availableColour;
            }
            else
            {
                button.image.color = unavailableColour;
            }
        }
        else
        {
            button.image.color = new Color(0, 0, 0, 0);
            button.interactable = false;
            button.image.raycastTarget = false;
            canTravelTo = false;
            marker.SetActive(false);
        }
    }

    public void StartTravelling()
    {
        navManager.StartTravelling(this);
    }

    public string GenerateNavigationEvent()
    {
        string generateEvent = " - ";

        if (possibleEvents.Length > 0)
        {
            Object navEvent = possibleEvents[Random.Range(0, possibleEvents.Length)];

            generateEvent = navEvent.name;

            LoadSettings.instance.spawnPlacement = entranceCount;

            if (!stopEvents)
            {
                StartEvent(navEvent);
            }
        }
        else
        {
            generateEvent = "Arrive";

            if (!stopLevels)
            {
                sceneLoader.LoadDefaultScene(null);
            }
        }
        
        return generateEvent;
    }

    #region Load Navigation Event

    void StartEvent(Object navEvent)
    {
        if (navEvent == navManager.ignoreEvent && Random.Range(0, 1) < ignoreChance)
        {
            navManager.ResetTravel();
            return;
        }

        if (sceneLoader != null && LoadSettings.instance != null)
        {
            LoadSettings.instance.background = (backgrounds.Length > 0 ? ChooseBackgrounds() : null);
            LoadSettings.instance.dialogueFlowChart = navEvent;
            sceneLoader.LoadSpecifiedScene(dialogueScene.ToString(), LoadSceneMode.Single, null);
        }
    }

    #endregion

    Sprite ChooseBackgrounds()
    {
        return backgrounds[Random.Range(0, backgrounds.Length)];
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
