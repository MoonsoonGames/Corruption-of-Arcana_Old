using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerSpawner : MonoBehaviour
{
    public LoadSettings loadSettings;
    public GameObject player;
    GameObject cam;
    public MenuManager menuManager;
    public Slider healthBar;
    public Slider arcanaBar;
    public GameObject interactImage;

    public MinimapScript minimap;
    public Compass compass;
    public CompassNoIcon compass2;

    public Text location;

    // Start is called before the first frame update
    void Start()
    {
        loadSettings = LoadSettings.instance;

        if (interactImage != null)
        {
            interactImage.SetActive(false);
        }

        SpawnPlayer();
    }

    void SpawnPlayer()
    {
        Vector3 spawnPos = loadSettings.RequestPosition(SceneManager.GetActiveScene().name);
        Quaternion spawnRot = loadSettings.RequestRotation(SceneManager.GetActiveScene().name);

        GameObject playerRef = Instantiate(player, spawnPos, spawnRot) as GameObject;

        playerRef.name = "Player";

        PlayerController controller = playerRef.GetComponent<PlayerController>();
        cam = playerRef.GetComponentInChildren<PlayerCameraController>().gameObject;

        controller.healthBar = healthBar;
        controller.arcanaBar = arcanaBar;
        controller.interactImage = interactImage;
        menuManager.Player = playerRef;
        menuManager.PlayerCamera = cam.gameObject;
        menuManager.compass.player = controller.gameObject.transform;
        menuManager.compass2.player = controller.gameObject.transform;

        minimap.player = playerRef.transform;
        compass.player = playerRef.transform;
        compass2.player = playerRef.transform;

        controller.Location = location;

        controller.Setup();
    }
}
