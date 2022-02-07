using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMarkers : MonoBehaviour
{
    public Sprite icon;
    public Image image;
    public bool showMarker = true;

    public Vector2 position 
    {
        get { return new Vector2(transform.position.x, transform.position.z); }
    }
}
