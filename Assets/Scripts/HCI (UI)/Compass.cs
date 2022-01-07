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

    public QuestMarkers MammaR;
    public QuestMarkers LtHeirophante;
    public QuestMarkers TownGate;

    private void Start()
    {
        compassUnit = compassImage.rectTransform.rect.width / 360f;

        AddQuestMarker(MammaR);
        AddQuestMarker(LtHeirophante);
        AddQuestMarker(TownGate);
    }

    private void Update()
    {
        compassImage.uvRect = new Rect((player.localEulerAngles.y / 360f) + Offset, 0f, 1f, 1f);

        foreach (QuestMarkers marker in questMarkers)
        {
            marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);
        }
    }

    public void AddQuestMarker(QuestMarkers marker)
    {
        GameObject newMarker = Instantiate(IconPrefab, compassImage.transform);
        marker.image = newMarker.GetComponent<Image>();
        marker.image.sprite = marker.icon;

        questMarkers.Add(marker);
    }

    Vector2 GetPosOnCompass(QuestMarkers marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }
}