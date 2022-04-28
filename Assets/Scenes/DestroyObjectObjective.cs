using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjectObjective : MonoBehaviour
{
    LoadSettings loadSettings;

    public QuestObjective[] objectivesRequired;
    public bool requireAll = true;

    public bool destroyIfContains = true;

    // Start is called before the first frame update
    void Start()
    {
        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        bool contains1 = false;
        bool containsAll = true;

        foreach (var item in objectivesRequired)
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
