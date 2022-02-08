using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject IconPrefab;
    List<QuestMarkers> questMarkers = new List<QuestMarkers>();
    List<GameObject> icons = new List<GameObject>();

    public float Offset = 90f;
    public RawImage compassImage;
    public Transform player;

    float compassUnit;

    bool updateIcons = false;

    private void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f;

        ChangeQuestMarkers();
    }

    public void ChangeQuestMarkers()
    {
        updateIcons = false;

        foreach (var item in icons)
        {
            Destroy(item);
        }

        Invoke("SetupQuestMarkers", 0.1f);

        Invoke("ResetQuestMarkers", 0.2f);
    }

    private void ResetQuestMarkers()
    {
        QuestMarkers[] clearList = GameObject.FindObjectsOfType<QuestMarkers>();

        foreach (var item in clearList)
        {
            if (!questMarkers.Contains(item))
            {
                Debug.Log("Should not appear");
                item.RemoveIcon();
            }
            else
            {
                Debug.Log("Should appear");
            }
        }

        updateIcons = true;

        List<GameObject> clear = new List<GameObject>();

        foreach (var item in icons)
        {
            if (item.GetComponent<Image>().sprite == null)
            {
                clear.Add(item);
            }
        }

        foreach (var item in clear)
        {
            if (icons.Contains(item))
            {
                icons.Remove(item);
            }

            Destroy(item);
        }

        clear.Clear();
    }

    private void SetupQuestMarkers()
    {
        QuestMarkers[] questMarkersScripts = GameObject.FindObjectsOfType<QuestMarkers>();

        questMarkers.Clear();

        foreach (var item in questMarkersScripts)
        {
            AddQuestMarker(item);
        }
    }

    private void Update()
    {
        if (updateIcons)
        {
            compassImage.uvRect = new Rect((player.localEulerAngles.y / 360f) + Offset, 0f, 1f, 1f);

            foreach (QuestMarkers marker in questMarkers)
            {
                if (marker != null && marker.gameObject.activeSelf && marker.showMarker)
                {
                    marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);
                }
            }
        }
    }

    public void AddQuestMarker(QuestMarkers marker)
    {
        GameObject newMarker = Instantiate(IconPrefab, compassImage.transform);

        if (marker.showMarker)
        {
            marker.image = newMarker.GetComponent<Image>();
            marker.image.sprite = marker.icon;

            questMarkers.Add(marker);
        }

        icons.Add(newMarker);
    }

    public void RemoveQuestMarker(QuestMarkers marker)
    {
        questMarkers.Remove(marker);
    }

    Vector2 GetPosOnCompass(QuestMarkers marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }
}