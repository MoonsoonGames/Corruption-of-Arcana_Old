using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusIconSpawner : MonoBehaviour
{
    List<StatusIcon> icons = new List<StatusIcon>();

    public Object statusIconObject;

    public void SetupIcons(Dictionary<StatusParent, int> statusDictionary)
    {
        ClearStatuses();

        float y = 0;
        int x = 0;
        foreach (var item in statusDictionary)
        {
            if (x <= 30)
            {
                SetupIcons(x, y, item.Key, item.Value);
            }
            else
            {
                SetupIcons(x, y - 30f, item.Key, item.Value);
            }

            x++;
        }
    }

    private void SetupIcons(int xOffset, float yOffset, StatusParent statusEffect, int duration)
    {
        GameObject icon = Instantiate(statusIconObject, transform) as GameObject;

        Vector3 newPosition = new Vector3(5f * xOffset, yOffset, 0);
        icon.transform.localPosition += newPosition;

        StatusIcon statusIcon = icon.GetComponent<StatusIcon>();

        statusIcon.Setup(statusEffect, duration);
        icons.Add(statusIcon);
    }

    private void ClearStatuses()
    {
        foreach (var item in icons)
        {
            item.PanelDisplay(false);
            Destroy(item.gameObject);
        }
        icons.Clear();
    }
}
