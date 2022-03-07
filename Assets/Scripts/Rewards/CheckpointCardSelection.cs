using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointCardSelection : MonoBehaviour
{
    public GameObject rewards;
    public GameObject disablerewards;

    private void Start()
    {
        disablerewards.SetActive(false);
    }

    public void ShowRewards()
    {
        LoadSettings loadSettings = LoadSettings.instance;

        loadSettings.ResetCards(false);
        rewards.SetActive(true);
    }
}
