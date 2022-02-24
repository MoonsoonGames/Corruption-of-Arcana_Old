using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteObjective : MonoBehaviour
{
    public QuestObjective objective;

    public void CompleteGoal()
    {
        if (objective != null)
        {
            objective.CompleteGoal();
        }
    }
}
