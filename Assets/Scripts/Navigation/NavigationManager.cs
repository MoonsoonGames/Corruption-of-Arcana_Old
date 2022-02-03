using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    public E_Levels navScene;
    LoadSettings loadSettings;
    NavigationNode[] nodes;

    private void Start()
    {
        LoadSettings[] loadSettingsArray = GameObject.FindObjectsOfType<LoadSettings>();

        foreach (var item in loadSettingsArray)
        {
            if (item.CheckMain())
            {
                loadSettings = item;
            }
            else
            {
                Destroy(item); //There is already one in the scene, delete this one
            }
        }

        nodes = GameObject.FindObjectsOfType<NavigationNode>();

        ResetNodes();
        SetNodes();
    }

    public void ResetNodes()
    {
        foreach (var item in nodes)
        {
            item.SetAvailable(false);
        }
    }

    public void SetNodes()
    {
        foreach (var item in nodes)
        {
            item.Setup(this, navScene, item.name == loadSettings.currentNodeID);

            if (item.name == loadSettings.currentNodeID)
            {
                loadSettings.currentNodeID = item.name;
            }
        }
    }

    public void StartTravelling(NavigationNode newNode)
    {
        loadSettings.TravelStart(newNode);

        Debug.Log(newNode.GenerateNavigationEvent());

        ResetNodes();
        SetNodes();
    }

    public List<string> GenerateEvents(List<string> possibleEvents, int eventsCount)
    {
        List<string> events = new List<string>();
        
        for (int i = 0; i < eventsCount; i++)
        {
            events.Add(possibleEvents[Random.Range(0, possibleEvents.Count)]);
        }

        return events;
    }
}
