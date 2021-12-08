using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    CharacterController characterController;
    Rigidbody rb;

    public float baseSneakSpeed = 20f;
    public float baseMoveSpeed = 50f;
    public float baseSprintSpeed = 80f;
    float moveSpeed;

    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    private float turnCamera;
    public float sensitivity = 5;

    public int maxHealth = 50;
    public int health;
    public int maxArcana = 35;
    public int arcana;
    public Slider healthBar;
    public Slider arcanaBar;

    public Text Location;

    private Vector3 moveDirection = Vector3.zero;

    public static PlayerController instance;

    private SceneLoader sceneLoader;

    private LoadSettings loadSettings;

    private Vector3 targetPos;

    public int maxPotions = 5;
    int potionCount = 3;

    bool interact = false;
    Dialogue dialogue;

    public GameObject interactImage;

    bool canMove = false;

    void Start()
    {
        //Load position
        moveSpeed = baseMoveSpeed;
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        sceneLoader = GameObject.FindObjectOfType<SceneLoader>();

        LoadSettings[] loadSettingsArray = GameObject.FindObjectsOfType<LoadSettings>();

        if (interactImage != null)
        {
            interactImage.SetActive(false);
        }

        foreach (var item in loadSettingsArray)
        {
            if (item.CheckMain())
            {
                loadSettings = item;

                health = loadSettings.health;

                if (loadSettings.died)
                {
                    potionCount = loadSettings.checkPointPotionCount;
                    loadSettings.potionCount = loadSettings.checkPointPotionCount;
                }
                else
                {
                    potionCount = loadSettings.potionCount;
                }
                
                Vector3 spawnPos = loadSettings.RequestPosition(SceneManager.GetActiveScene().name);

                //SetupTransform(spawnPos);
                
                StartCoroutine(IDelayStartTransform(2f, spawnPos));

                loadSettings.died = false;
            }
            else
            {
                Destroy(item); //There is already one in the scene, delete this one
            }

        }

        loadSettingsArray = GameObject.FindObjectsOfType<LoadSettings>();

        //Debug.Log("Length: " + loadSettingsArray.Length);
        //Debug.Break();
    }

    IEnumerator IDelayStartTransform(float delay, Vector3 newSpawnPos)
    {
        yield return new WaitForSeconds(delay);
        //Debug.Log("Should be able to move");
        //SetupTransform(newSpawnPos);
        canMove = true;
    }

    void SetupTransform(Vector3 targetPosition)
    {
        transform.position = targetPosition;
    }

    void Update()
    {
        if (canMove)
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

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    moveSpeed = baseSprintSpeed;
                }
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    moveSpeed = baseSneakSpeed;
                }
                if (!Input.GetKey(KeyCode.LeftShift) & !Input.GetKey(KeyCode.LeftControl))
                {
                    moveSpeed = baseMoveSpeed;
                }
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            moveDirection.y -= gravity * Time.deltaTime;

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);
            //rb.velocity += moveDirection;

            //turnCamera = Input.GetAxis("Mouse X") * sensitivity;
            //if (turnCamera != 0)
            //{
            //    //Code for action on mouse moving horizontally
            //    transform.eulerAngles += new Vector3(0, turnCamera, 0);
            //}

            if (Input.GetButton("Interact") && interact && dialogue != null)
            {
                loadSettings.Checkpoint(SceneManager.GetActiveScene());
                dialogue.LoadScene();
            }
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
        SavePlayerPos();

        if (other.gameObject.CompareTag("commonEnemy") || other.gameObject.CompareTag("bossEnemy"))
        {
            EnemyController enemyController = other.GetComponent<EnemyController>();

            if (enemyController != null)
            {
                enemyController.LoadCombat(sceneLoader);
            }
        }

        else if (other.gameObject.CompareTag("NPC"))
        {
            //Debug.Log("Can Interact");
            interact = true;
            dialogue = other.gameObject.GetComponent<Dialogue>();

            if (interactImage != null)
            {
                interactImage.SetActive(true);
            }
        }

        #region Thoth location triggers

        if (other.gameObject.CompareTag("Thoth Mid City"))
        {
            Location.text = "Thoth - MidCity".ToString();
        }
        else if (other.gameObject.CompareTag("Thoth Market"))
        {
            Location.text = "Thoth - Market".ToString();
        }
        else if (other.gameObject.CompareTag("Thoth Bridge"))
        {
            Location.text = "Thoth - Bridge".ToString();
        }
        else if (other.gameObject.CompareTag("Thoth East housing"))
        {
            Location.text = "Thoth - East houses".ToString();
        }
        else if (other.gameObject.CompareTag("Thoth West housing"))
        {
            Location.text = "Thoth - West houses".ToString();
        }
        else if (other.gameObject.CompareTag("Thoth Open Sea"))
        {
            Location.text = "Thoth - Open Sea".ToString();
        }

        #endregion
    }

    public void SavePlayerPos()
    {
        string scene = SceneManager.GetActiveScene().name;
        if (loadSettings != null)
        {
            if (scene == E_Levels.Thoth.ToString())
            {
                loadSettings.playerPosInThoth = transform.position;
            }
            else if (scene == E_Levels.Clearing.ToString())
            {
                loadSettings.playerPosInClearing = transform.position;
            }
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            Debug.Log("Can't Interact");
            interact = false;
            dialogue = null;

            if (interactImage != null)
            {
                interactImage.SetActive(false);
            }
        }
    }

    public int GetPotions()
    {
        return potionCount;
    }

    public bool CheckPotions(int value)
    {
        return potionCount >= value;
    }

    public void ChangePotions(int value, bool spend)
    {
        if (spend)
        {
            potionCount = Mathf.Clamp(potionCount - value, 0, maxPotions);
        }
        else
        {
            potionCount = Mathf.Clamp(potionCount + value, 0, maxPotions);
        }

        if (potionCount == 0)
        {
            //combatManager.HealingItem.SetActive(false);
        }

        //combatManager.HealingLeft.text = potionCount.ToString();

        if (loadSettings != null)
        {
            loadSettings.potionCount = potionCount;
        }
    }
}