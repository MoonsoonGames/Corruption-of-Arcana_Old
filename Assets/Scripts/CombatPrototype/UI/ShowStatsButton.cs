using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowStatsButton : MonoBehaviour
{
    [HideInInspector]
    public EnemyManager manager;
    public Enemy controller;

    public Button button;

    public Sprite hidden, revealed;

    private void Start()
    {
        CheckReveal();
    }

    public void CheckReveal()
    {
        if (button != null && hidden != null && revealed != null)
        {
            if (controller.CheckRevealed())
            {
                button.image.sprite = revealed;
            }
            else
            {
                button.image.sprite = hidden;
            }
        }
    }

    public void ButtonClicked()
    {
        if (manager != null && controller != null)
        {
            manager.EnemyInfo(controller);
        }
        else
        {
            Debug.Log("Does not have a controller or manager");
        }
    }
}
