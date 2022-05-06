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
    MenuManager menuManager;

    public GameObject Player;
    public Text Location;
    public static PlayerController instance;

    public GameObject interactImage;
    bool interact = false;
    Dialogue dialogue;

    public Animator modelAnimator;

    public SceneLoader sceneLoader;
    public Object respawnDialogue;

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

    private void Start()
    {
        if (canMove)
        {
            Setup();
        }
    }

    public void Setup()
    {
        //Load position
        moveSpeed = baseMoveSpeed;
        characterController = GetComponent<CharacterController>();
        rb = GetComponent<Rigidbody>();

        if (LoadSettings.instance.checkPoint)
        {
            LoadSettings.instance.SaveCheckpoint(SceneManager.GetActiveScene(), this);
        }

        LoadSettings.instance.died = false;

        health = LoadSettings.instance.health;
        maxHealth = LoadSettings.instance.maxHealth;
        //arcana = loadSettings.arcana;

        StartCoroutine(IDelayMovement(2f));
    }

    IEnumerator IDelayMovement(float delay)
    {
        yield return new WaitForSeconds(delay);
        //Debug.Log("Should be able to move");
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

                Vector3 inputDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));

                moveDirection = transform.TransformDirection(inputDirection);
                moveDirection *= moveSpeed;

                modelAnimator.SetBool("Moving", moveDirection != new Vector3(0, 0, 0));

                //Debug.Log(inputDirection);

                //up 001, right 100, down 00-1, left -100
                if (inputDirection.x > 0)
                {
                    modelAnimator.SetInteger("Direction", 1);
                }
                else if (inputDirection.x < 0)
                {
                    modelAnimator.SetInteger("Direction", 3);
                }
                else if (inputDirection.z > 0)
                {
                    modelAnimator.SetInteger("Direction", 0);
                }
                else if (inputDirection.z < 0)
                {
                    modelAnimator.SetInteger("Direction", 2);
                }
                

                if (Input.GetButton("Jump"))
                {
                    //moveDirection.y = jumpSpeed;
                }

                isRunning = Input.GetButton("Sprint");
                modelAnimator.SetBool("Sprinting", isRunning);

                if (IsRunning)
                {
                    moveSpeed = baseSprintSpeed;
                }
                else
                {
                    moveSpeed = baseMoveSpeed;

                }

                if (Input.GetButton("Interact") && interact && dialogue != null)
                {
                    LoadSettings.instance.alreadyLoading = true;
                    canMove = !dialogue.LoadDialogueScene(this);
                }
            }

            // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
            // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
            // as an acceleration (ms^-2)
            moveDirection.y -= gravity * Time.deltaTime;

            // Move the controller
            characterController.Move(moveDirection * Time.deltaTime);
        }
        else
        {
            modelAnimator.SetBool("Moving", false);
        }

        //Sets the values of the healthbars to their specific values
        if (healthBar != null)
            healthBar.value = health;
        if (arcanaBar != null)
            arcanaBar.value = arcana;
    }

    public void DealDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Debug.Log("Die");
            canMove = false;

            sceneLoader.LoadDefaultScene(respawnDialogue);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (LoadSettings.instance.alreadyLoading == false)
        {
            //Save current position
            SavePlayerPos();

            if (other.gameObject.CompareTag("commonEnemy") || other.gameObject.CompareTag("bossEnemy"))
            {
                EnemyController enemyController = other.GetComponent<EnemyController>();

                if (enemyController != null)
                {
                    LoadSettings.instance.alreadyLoading = true;
                    enemyController.LoadCombat();
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
                        //Debug.Log(item.dialogue.ToString() + " can speak");
                        dialogue = item;

                        if (item.forceDialogue)
                        {
                            LoadSettings.instance.alreadyLoading = true;
                            canMove = !dialogue.LoadDialogueScene(this);
                        }
                    }
                    else
                    {
                        //Debug.Log(item.dialogue.ToString() + " can't speak");
                    }
                }

                /*if (dialogue != null && dialogue.dialogue != null && loadSettings != null)
                    loadSettings.dialogueFlowChart = dialogue.dialogue;*/

                if (interactImage != null && dialogue != null)
                {
                    interactImage.SetActive(true);
                }
            }

            if (Location != null)
            {
                LocationTrigger(other);
            }
        }
    }

    void LocationTrigger(Collider other)
    {
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
            Debug.Log("Player in the water");
        }

        #endregion

        #region East Forest Clearing location triggers

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

        #region Eastern Cave location triggers
        if (other.gameObject.CompareTag("Eastern Cave"))
        {
            Location.text = "Eastern Cave".ToString();
        }
        if (other.gameObject.CompareTag("Cave River"))
        {
            Player.transform.position = new Vector3(101.964149f, 1.21939147f, 264.567352f);
            Debug.Log("Player in the water");
        }
        #endregion
    }

    public void SavePlayerPos()
    {
        string scene = SceneManager.GetActiveScene().name;
        if (LoadSettings.instance != null)
        {
            if (scene == E_Levels.Thoth.ToString())
            {
                LoadSettings.instance.playerPosInThoth = transform.position;
                LoadSettings.instance.playerRotInThoth = transform.rotation;
            }
            else if (scene == E_Levels.EastForestClearing.ToString())
            {
                LoadSettings.instance.playerPosInClearing = transform.position;
                LoadSettings.instance.playerRotInClearing = transform.rotation;
            }
            else if (scene == E_Levels.EasternCave.ToString())
            {
                LoadSettings.instance.playerPosInCave = transform.position;
                LoadSettings.instance.playerRotInCave = transform.rotation;
            }
            else if (scene == E_Levels.Tiertarock.ToString())
            {
                LoadSettings.instance.playerPosInTiertarock = transform.position;
                LoadSettings.instance.playerRotInTiertarock = transform.rotation;
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

            LoadSettings.instance.dialogueFlowChart = null;

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

        if (LoadSettings.instance != null)
        {
            LoadSettings.instance.healingPotionCount = potionCount;
        }
    }
    private bool isGrounded;
    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
        set
        {
            isGrounded = value;
            //modelAnimator.SetBool("Grounded", value);
        }
    }
    /// <summary>
    /// Gets or sets whether a character is moving in the current frame.
    /// </summary>
    private bool isMoving;
    public bool IsMoving
    {
        get
        {
            return isMoving;
        }
        set
        {
            isMoving = value;
            modelAnimator.SetBool("Moving", value);
        }
    }
    /// <summary>
    /// Gets or sets whether a character is running in the current frame.
    /// </summary>
    private bool isRunning;
    public bool IsRunning
    {
        get
        {
            return isRunning;
        }
        set
        {
            isRunning = value;
            //modelAnimator.SetBool("IsRunning", value);
        }
    }
}