using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Compass : MonoBehaviour {

    public float Offset = 90f;
    public RawImage compassImage;
    public Transform player;

    private void Update()
    {
        compassImage.uvRect = new Rect((player.localEulerAngles.y / 360f)+Offset, 0f, 1f, 1f);
    }
}