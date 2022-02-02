using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyDialogue : MonoBehaviour
{
    LoadSettings loadSettings;

    public SphereCollider colliderRef;

    public bool requireTutorial = false;
    public bool requireDialogue = false;
    
    // Start is called before the first frame update
    void Start()
    {
        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        CheckDialogue();
    }

    public void CheckDialogue()
    {
        if (loadSettings != null && colliderRef != null)
        {
            //Debug.Log(loadSettings.dialogueComplete);
            colliderRef.enabled = (((loadSettings.dialogueComplete && requireTutorial) || !requireTutorial)
                                && ((loadSettings.prologueComplete && requireDialogue) || !requireDialogue));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
