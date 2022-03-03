using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueBackdrop : MonoBehaviour
{
    LoadSettings loadSettings;

    Image image;

    public Sprite overrideBG;

    // Start is called before the first frame update
    void Start()
    {
        loadSettings = LoadSettings.instance;

        image = GetComponentInParent<SpawnDialogue>().background;

        SetBGToLoadBG();

        if (overrideBG != null)
        {
            LoadSpecificBG(overrideBG);
        }
    }

    public void SetBGToLoadBG()
    {
        if (image != null && loadSettings != null)
        {
            image.sprite = loadSettings.background;

            //set image colour to white
            image.color = Color.white;
        }
    }

    public void LoadSpecificBG(Sprite sprite)
    {
        if (image != null)
        {
            image.sprite = sprite;

            //set image colour to white
            image.color = Color.white;
        }
    }

    public void ResetLoadSettingsBG()
    {
        if (loadSettings != null)
        {
            loadSettings.background = null;
        }
    }
}
