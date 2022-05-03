using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplesQuest : MonoBehaviour
{
    public QuestObjective quest;

    public GameObject Apples;
    // Start is called before the first frame update
    void Start()
    {
        Apples.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (quest.canComplete == false || quest.completed)
        {
            Apples.SetActive(false);
        }
        else
        {
            Apples.SetActive(true);
        }

        if (quest.completed == true)
        {
            Destroy(this.gameObject);
        }
    }
}
