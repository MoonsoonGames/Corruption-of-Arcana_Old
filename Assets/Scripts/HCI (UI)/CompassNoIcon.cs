using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassNoIcon : MonoBehaviour
{
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

    bool alreadyCalled = false;

    public void ResetMarkersOnce()
    {
        if (!alreadyCalled)
        {
            alreadyCalled = true;
            Invoke("ChangeQuestMarkers", 1f);
        }
    }

    private void ChangeQuestMarkers()
    {
        updateIcons = false;

        ShowQuestMarker[] markers = GameObject.FindObjectsOfType<ShowQuestMarker>();

        foreach (var item in markers)
        {
            item.CheckObjective();
        }

        foreach (var item in icons)
        {
            Destroy(item);
        }

        Invoke("ResetQuestMarkers", 0.2f);

        Invoke("SetupQuestMarkers", 0.6f);
    }

    private void ResetQuestMarkers()
    {
        questMarkers.Clear();

        for (int i = 0; i < icons.Count; i++)
        {
            Destroy(icons[i]);
        }

        icons.Clear();
    }

    private void Update()
    {
        if (updateIcons)
        {
            compassImage.uvRect = new Rect((player.localEulerAngles.y / 360f) + Offset, 1f, 1f, 1f);

            foreach (QuestMarkers marker in questMarkers)
            {
                if (marker != null && marker.gameObject.activeSelf && marker.showMarker)
                {
                    marker.image.rectTransform.anchoredPosition = GetPosOnCompass(marker);
                }
            }
        }
    }

    Vector2 GetPosOnCompass(QuestMarkers marker)
    {
        Vector2 playerPos = new Vector2(player.transform.position.x, player.transform.position.z);
        Vector2 playerFwd = new Vector2(player.transform.forward.x, player.transform.forward.z);

        float angle = Vector2.SignedAngle(marker.position - playerPos, playerFwd);

        return new Vector2(compassUnit * angle, 0f);
    }
}