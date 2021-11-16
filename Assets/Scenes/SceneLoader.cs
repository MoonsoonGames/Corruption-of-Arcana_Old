using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public E_Levels sceneToLoad;
    string sceneString;
    Scene currentScene;

    // Start is called before the first frame update
    private void Start()
    {
        sceneString = sceneToLoad.ToString();

        currentScene = SceneManager.GetActiveScene();
    }

    public void LoadDefaultScene()
    {
        Debug.Log("Load Scene");
        SceneManager.LoadScene(sceneString);
    }

    public void LoadSpecifiedScene(string scene, LoadSceneMode sceneMode)
    {
        SceneManager.LoadScene(scene, sceneMode);
    }
}
