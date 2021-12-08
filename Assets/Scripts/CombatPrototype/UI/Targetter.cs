using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Targetter : MonoBehaviour
{
    public GameObject[] corners;
    public GameObject[] arrows;
    public bool ally = false;

    // Start is called before the first frame update
    void Start()
    {
        SetVisibility(false);
    }

    public void SetVisibility(bool visible)
    {
        foreach (var item in corners)
        {
            item.SetActive(visible);
        }
        foreach (var item in arrows)
        {
            item.SetActive(!visible);
        }
    }
}
