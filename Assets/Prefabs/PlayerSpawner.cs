using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public LoadSettings loadSettings;
    public GameObject player;
    Camera cam;
    public UIManager UIManager;
    public Slider healthBar;
    public Slider arcanaBar;
    public GameObject interactImage;

    public MinimapScript minimap;
    public Compass compass;

    // Start is called before the first frame update
    void Start()
    {
        LoadSettings[] loadSettingsArray = GameObject.FindObjectsOfType<LoadSettings>();

        if (interactImage != null)
        {
            interactImage.SetActive(false);
        }

        foreach (var item in loadSettingsArray)
        {
            if (item.CheckMain())
            {
                SpawnPlayer(item);
            }
            else
            {
                Destroy(item); //There is already one in the scene, delete this one
            }
        }    
    }

    void SpawnPlayer(LoadSettings loadSettingsRef)
    {
        Vector3 spawnPos = loadSettingsRef.RequestPosition(SceneManager.GetActiveScene().name);

        GameObject playerRef = Instantiate(player, spawnPos, transform.rotation) as GameObject;

        playerRef.name = "Player";

        PlayerController controller = playerRef.GetComponent<PlayerController>();

        controller.healthBar = healthBar;
        controller.arcanaBar = arcanaBar;
        controller.interactImage = interactImage;

        cam = playerRef.GetComponentInChildren<Camera>();
        UIManager.player = playerRef;
        UIManager.Camera = cam.gameObject;

        minimap.player = playerRef.transform;
        compass.player = playerRef.transform;
    }
}
