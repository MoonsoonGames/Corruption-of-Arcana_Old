using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    public E_Levels sceneToLoad;
    string sceneString;
    Scene currentScene;

    SceneLoader sceneLoader;

    LoadSettings loadSettings;

    public bool checkpoint = false;

    public bool tutorialDialogue = false;
    public bool knightDialogue = false;

    public bool requiresTutorial = false;

    public Object dialogue;

    // Start is called before the first frame update
    private void Start()
    {
        sceneString = sceneToLoad.ToString();

        currentScene = SceneManager.GetActiveScene();

        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();
    }

    public void LoadScene()
    {
        if (sceneLoader != null && loadSettings != null)
        {
            if ((loadSettings.dialogueComplete && requiresTutorial) || !requiresTutorial)
            {
                if (tutorialDialogue)
                    loadSettings.dialogueComplete = true;

                if (knightDialogue)
                    loadSettings.prologueComplete = true;

                if (checkpoint)
                {
                    PlayerController controller = GameObject.Find("Player").GetComponent<PlayerController>();

                    if (controller != null)
                    {
                        Debug.Log(SceneManager.GetActiveScene());
                        loadSettings.checkPointPotionCount = controller.GetPotions();

                        loadSettings.checkPointPos = controller.transform.position;

                        loadSettings.checkPointScene = SceneManager.GetActiveScene();
                    }
                }

                sceneLoader.LoadSpecifiedScene(sceneString, LoadSceneMode.Single, dialogue);
            }
        }
    }
}
