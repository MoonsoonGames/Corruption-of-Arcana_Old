using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PostCombatHandler : MonoBehaviour
{
    public GameObject rewardsScreen;
    public GameObject victoryScreen;

    public void victoryContinue()
    {
        rewardsScreen.SetActive(true);
        victoryScreen.SetActive(false);
    }

    public void defeatContinue()
    {
        //load thoth w/ black screen
        //loads in Mama R death dialogue
        //resumes player outside trailer
    }

    public void rewardsContinue()
    {
        //reloads player in same location as battle
    }
}
