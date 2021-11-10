using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;

    public float moveSpeed = 20.0f;
    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private float turnCamera;
    public float sensitivity = 5;

    public float maxHealth = 50;
    public float health;
    public float maxArcana = 35;
    public float arcana;
    public Slider healthBar;
    public Slider arcanaBar;

    private Vector3 moveDirection = Vector3.zero;

    public static PlayerController instance;

    private SceneLoader sceneLoader;

    private LoadSettings loadSettings;

    private Vector3 targetPos;

    void Start()
    {
        //Load position

        characterController = GetComponent<CharacterController>();

        sceneLoader = GameObject.Find("SceneLoader").GetComponent<SceneLoader>();
        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (health < maxHealth)
        {
            health = maxHealth;
        }
        if (arcana < maxArcana)
        {
            arcana = maxArcana;
        }

        if (loadSettings != null)
        {
            if (loadSettings.died)
            {
                loadSettings.died = false;

                targetPos = loadSettings.mamaPos;

                targetPos.x = loadSettings.mamaPos.x;
                targetPos.y = loadSettings.mamaPos.y;
                targetPos.z = loadSettings.mamaPos.z;

                Debug.Log("Loading respawn position | " + loadSettings.playerPos + " || " + targetPos);

                transform.position = targetPos;
                Debug.Log(transform.position);
                health = loadSettings.health;

                SetupTransform(targetPos);
            }
            else
            {
                targetPos = loadSettings.playerPos;

                targetPos.x = loadSettings.playerPos.x;
                targetPos.y = loadSettings.playerPos.y;
                targetPos.z = loadSettings.playerPos.z;

                Debug.Log("Loading position | " + loadSettings.playerPos + " || " + targetPos);

                transform.position = targetPos;
                Debug.Log(transform.position);
                health = loadSettings.health;

                SetupTransform(targetPos);
            }
        }
    }

    void SetupTransform(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }

    void Update()
    {
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate
            // move direction directly from axes

            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= moveSpeed;

            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }

            else if (Input.GetKey(KeyCode.LeftShift))
            {
                moveSpeed = 30f;
            }

            else
            {
                moveSpeed = 20f;
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        turnCamera = Input.GetAxis("Mouse X") * sensitivity;
        if (turnCamera != 0)
        {
            //Code for action on mouse moving horizontally
            transform.eulerAngles += new Vector3(0, turnCamera, 0);
        }

        if (health <= 0)
        {
            //Set active game over screen
            //load mama reinfeld trailer scene
            //transform position to mama reinfeld
            //Load mama reinfeld dialogue for respawn
        }

        //Sets the values of the healthbars to their specific values
        if (healthBar != null)
            healthBar.value = health;
        if (arcanaBar != null)
            arcanaBar.value = arcana;
    }

    public void OnTriggerEnter(Collider other)
    {
        //Save current position
        if (loadSettings != null)
            loadSettings.playerPos = transform.position;

        if (other.gameObject.CompareTag("commonEnemy"))
        {
            /*
            CombatHandler.instance.difficultyCommon.enabled = true;
            CombatHandler.instance.difficultyBoss.enabled = false;
            CombatHandler.instance.battleActive = true;
            */

            if (loadSettings != null)
                loadSettings.fightingBoss = false;

            if (sceneLoader != null)
                sceneLoader.LoadDefaultScene();
        }
        else if (other.gameObject.CompareTag("bossEnemy"))
        {
            /*
            CombatHandler.instance.difficultyBoss.enabled = true;
            CombatHandler.instance.difficultyCommon.enabled = false;
            CombatHandler.instance.battleActive = true;
            */

            if (loadSettings != null)
                loadSettings.fightingBoss = true;

            if (sceneLoader != null)
                sceneLoader.LoadDefaultScene();
        }
        else if (other.gameObject.CompareTag("NPC"))
        {
            other.gameObject.GetComponent<Dialogue>().LoadScene();
        }
    }
}