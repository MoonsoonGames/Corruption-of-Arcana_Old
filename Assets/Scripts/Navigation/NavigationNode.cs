using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationNode : MonoBehaviour
{
    NavigationManager navManager;
    SceneLoader sceneLoader;
    E_Levels navScene;
    
    public NavigationNode[] possibleNodes;
    public NavigationEvents[] possibleEvents;

    public Button button;
    public Color currentColour;
    public Color availableColour;
    public Color unavailableColour;
    public bool canTravelTo = false;

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

    public void SetCurrent()
    {
        button.interactable = true;
        canTravelTo = true;

        button.image.color = currentColour;
    }

    public void SetAvailable(bool available)
    {
        button.interactable = available;
        canTravelTo = available;

        if (available)
        {
            button.image.color = availableColour;
        }
        else
        {
            button.image.color = unavailableColour;
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
            NavigationEvents navEvent = possibleEvents[Random.Range(0, possibleEvents.Length)];

            generateEvent = navEvent.eventName;

            navEvent.Setup(sceneLoader, navScene);
            navEvent.StartEvent();
        }
        else
        {
            generateEvent = "Arrive";

            sceneLoader.LoadDefaultScene(null);
        }
        
        return generateEvent;
    }
}
