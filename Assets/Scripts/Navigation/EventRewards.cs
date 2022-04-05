using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventRewards : MonoBehaviour
{
    GameObject rewards;

    private void Start()
    {
        rewards = GameObject.FindObjectOfType<Rewards>().gameObject;
        rewards.SetActive(false);
    }

    public void GiveRewards()
    {
        if (rewards != null)
        {
            rewards.SetActive(true);
        }
    }
}
