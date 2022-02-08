using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenGates : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Open();
    }

    public Quest[] requireQuests;
    public QuestObjective[] requireObjectives;

    public bool requireAll = true;
    public bool destroyIfContains = true;

    public void Open()
    {
        bool contains1 = false;
        bool containsAll = true;

        foreach (var item in requireQuests)
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

        foreach (var item in requireObjectives)
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

        if ((((requireAll && containsAll) || (!requireAll && contains1)) && destroyIfContains) || (((!requireAll && !containsAll) || (requireAll && !contains1)) && !destroyIfContains))
        {
            Destroy(this.gameObject);
        }
        else
        {

        }
    }
}
