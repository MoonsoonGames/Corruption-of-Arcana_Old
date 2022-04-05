using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompassNoIcon : MonoBehaviour
{
    public float Offset = 90f;
    public RawImage compassImage;
    public Transform player;

    float compassUnit;

    private void Update()
    {
        compassImage.uvRect = new Rect((player.localEulerAngles.y / 360f) + Offset, 1f, 1f, 1f);
    }
}