using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScreenText : MonoBehaviour
{
    public Text text;

    private void Start()
    {
        text.text = SceneManager.GetActiveScene().name;
    }
}
