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

    public int maxHealth = 50;
    public int health;
    public int maxArcana = 35;
    public int arcana;
    public Slider healthBar;
    public Slider arcanaBar;

    private Vector3 moveDirection = Vector3.zero;

    public static PlayerController instance;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        if (health < maxHealth)
        {
            health = maxHealth;
        }
        if (arcana < maxArcana)
        {
            arcana = maxArcana;
        }
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
        healthBar.value = health;
        arcanaBar.value = arcana;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("commonEnemy"))
        {
            CombatHandler.instance.difficultyCommon.SetActive(true);
            CombatHandler.instance.difficultyBoss.SetActive(false);
            SceneManager.LoadScene("Battle Scene");
        }
        else if (other.gameObject.CompareTag("bossEnemy"))
        {
            CombatHandler.instance.difficultyBoss.SetActive(true);
            CombatHandler.instance.difficultyCommon.SetActive(false);
            SceneManager.LoadScene("Battle Scene");
        }
    }
}