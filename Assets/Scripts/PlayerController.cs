using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    #region Variables

    #region References

    CharacterController characterController;
    Rigidbody rb;

    public GameObject Player;
    public Text Location;
    public static PlayerController instance;
    private SceneLoader sceneLoader;
    private LoadSettings loadSettings;

    public GameObject interactImage;
    bool interact = false;
    Dialogue dialogue;

    #endregion

    #region Stats

    #region Movement

    public bool canMove = false;

    #region Move Speed

    private Vector3 moveDirection = Vector3.zero;
    private Vector3 targetPos; //Spawn position

    public float baseSneakSpeed = 20f;
    public float baseMoveSpeed = 50f;
    public float baseSprintSpeed = 80f;
    float moveSpeed;

    #endregion

    #region Jumping

    public float jumpSpeed = 8.0f;
    public float gravity = 20.0f;

    #endregion

    #region Camera

    private float turnCamera;
    public float sensitivity = 5;

    #endregion

    #endregion

    #region Health and Arcana

    public int maxHealth = 50;
    public int health;
    public int maxArcana = 35;
    public int arcana;
    public Slider healthBar;
    public Slider arcanaBar;

    public int maxPotions = 5;
    int potionCount = 3;

    #endregion

    #endregion

    #endregion

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
                Quaternion spawnRot = loadSettings.RequestRotation(SceneManager.GetActiveScene().name);
                
                //SetupTransform(spawnPos);

                StartCoroutine(IDelayStartTransform(2f, spawnPos, spawnRot));

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

    IEnumerator IDelayStartTransform(float delay, Vector3 newSpawnPos, Quaternion newSpawnRot)
    {
        yield return new WaitForSeconds(delay);
        //Debug.Log("Should be able to move");
        SetupTransform(newSpawnPos, newSpawnRot);
        canMove = true;
    }

    void SetupTransform(Vector3 targetPosition, Quaternion targetRotation)
    {
        transform.position = targetPosition;
        transform.rotation = targetRotation;
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
                if (Input.GetButton("Interact") && interact && dialogue != null)
                {
                    loadSettings.Checkpoint(SceneManager.GetActiveScene());
                    canMove = !dialogue.LoadScene();
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

            Dialogue[] dialogueArray = other.gameObject.GetComponents<Dialogue>();

            foreach (var item in dialogueArray)
            {
                if (item.CanSpeak())
                {
                    dialogue = item;
                }
            }

            /*if (dialogue != null && dialogue.dialogue != null && loadSettings != null)
                loadSettings.dialogueFlowChart = dialogue.dialogue;*/

            if (interactImage != null && dialogue.CanSpeak())
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

        if (other.gameObject.CompareTag("Thoth Open Sea"))
        {
            Player.transform.position = new Vector3(-267.317505f, 31.0455322f, 358.720032f);
            //rescued == true;
            //Run rescuer dialogue
            Debug.Log("Player in the water");
        }

        #endregion

        #region East Clearing location triggers

        if (other.gameObject.CompareTag("EC Camp"))
        {
            Location.text = "Forest Camp".ToString();
        }
        else if (other.gameObject.CompareTag("EC Forest"))
        {
            Location.text = "East Forest".ToString();
        }
        else if (other.gameObject.CompareTag("EC Pond"))
        {
            Location.text = "Hidden Pond".ToString();
        }
        else if (other.gameObject.CompareTag("EC Pass"))
        {
            Location.text = "Mountain Pass".ToString();
        }
        else if (other.gameObject.CompareTag("EC Cave"))
        {
            Location.text = "Cave Enterance".ToString();
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
                loadSettings.playerRotInThoth = transform.rotation;
            }
            else if (scene == E_Levels.EastForestClearing.ToString())
            {
                loadSettings.playerPosInClearing = transform.position;
                loadSettings.playerRotInClearing = transform.rotation;
            }
            else if (scene == E_Levels.Tiertarock.ToString())
            {
                loadSettings.playerPosInTiertarock = transform.position;
                loadSettings.playerRotInTiertarock = transform.rotation;
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

            loadSettings.dialogueFlowChart = null;

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