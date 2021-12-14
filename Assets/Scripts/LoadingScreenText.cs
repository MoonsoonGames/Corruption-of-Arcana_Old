using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class LoadingScreenText : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        text.text = ObjectNames.NicifyVariableName(SceneManager.GetActiveScene().name);


    }
}
