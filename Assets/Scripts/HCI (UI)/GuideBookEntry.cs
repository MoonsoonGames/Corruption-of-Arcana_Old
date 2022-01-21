using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideBookEntry : MonoBehaviour
{
    public string displayName;

    LoadSettings loadSettings;

    // Start is called before the first frame update
    void Start()
    {
        loadSettings = GameObject.FindObjectOfType<LoadSettings>();

        CheckRevealEntry();
    }

    void CheckRevealEntry()
    {
        this.gameObject.SetActive(loadSettings.revealedEntries.Contains(displayName));
    }
}
