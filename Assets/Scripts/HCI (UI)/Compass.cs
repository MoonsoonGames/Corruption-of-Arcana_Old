using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour
{
    public GameObject IconPrefab;
    List<QuestMarkers> questMarkers = new List<QuestMarkers>();

    public float Offset = 90f;
    public RawImage compassImage;
    public Transform player;

    float compassUnit;

    private void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f;

        SetupQuestMarkers();
    }

    private void SetupQuestMarkers()
    {
        QuestMarkers[] questMarkersScripts = GameObject.FindObjectsOfType<QuestMarkers>();

        foreach (var item in questMarkersScripts)
        {
            AddQuestMarker(item);
        }
    }

    private void Update()
    {
        compassImage.uvRect = new Rect((player.localEulerAngles.y / 360f) + Offset, 0f, 1f, 1f);

        foreach (QuestMarkers marker in questMarkers)
        {
            if (marker != null && marker.gameObject.activeSelf/* && marker.showMarker*/)
            {
                marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);
            }
            else
            {
                RemoveQuestMarker(marker);
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