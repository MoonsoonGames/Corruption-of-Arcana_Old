using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationManager : MonoBehaviour
{
    public E_Levels navScene;
    LoadSettings loadSettings;
    NavigationNode[] nodes;

    public bool stopEvents = false;
    public bool stopLevels = false;

    bool alreadyTravelling = false;

    private void Start()
    {
        loadSettings = LoadSettings.instance;

        nodes = GameObject.FindObjectsOfType<NavigationNode>();

        ResetNodes();
        SetNodes();
    }

    public void ResetNodes()
    {
        foreach (var item in nodes)
        {
            item.SetAvailable(false);

            item.stopEvents = stopEvents;
            item.stopLevels = stopLevels;
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
        if (alreadyTravelling == false)
        {
            loadSettings.TravelStart(newNode);

            //Debug.Log(newNode.GenerateNavigationEvent());

            newNode.GenerateNavigationEvent();

            ResetNodes();
            SetNodes();

            alreadyTravelling = true;
        }
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
