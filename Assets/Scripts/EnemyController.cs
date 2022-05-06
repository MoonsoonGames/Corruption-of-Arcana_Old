using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public bool boss = false;

    public Object[] enemies = new Object[3];
    public QuestObjective[] objectives;
    public Vector2 goldReward;
    public Weapon weaponReward;

    private LoadSettings loadSettings;
    private SceneLoader sceneLoader;

    public E_Levels combatScene;

    public string enemyName;
    public Sprite background;

    public bool destroy = true;

    // Start is called before the first frame update
    void Awake()
    {
        loadSettings = LoadSettings.instance;
        sceneLoader = GetComponent<SceneLoader>();

        if (sceneLoader == null)
        {
            sceneLoader = GetComponentInChildren<SceneLoader>();
        }

        if (sceneLoader == null)
        {
            sceneLoader = GameObject.FindObjectOfType<SceneLoader>();
        }

        if (loadSettings != null && destroy)
        {
            if (loadSettings.enemiesKilled.Contains(name))
            {
                Debug.Log("Despawning " + name);
                Destroy(this.gameObject);
            }
        }
    }

    public void LoadCombat()
    {
        if (loadSettings != null)
        {
            Debug.Log("Load combat");
            loadSettings.fightingBoss = boss;
            loadSettings.currentFight = name;

            loadSettings.enemies[0] = enemies[0];
            loadSettings.enemies[1] = enemies[1];
            loadSettings.enemies[2] = enemies[2];

            if (background != null)
            {
                loadSettings.background = background;
            }

            loadSettings.goldReward = goldReward;
            loadSettings.rewardWeapon = weaponReward;

            foreach (var item in objectives)
            {
                if (item.canComplete)
                {
                    if (loadSettings.currentFightObjectives.Contains(item) == false)
                    {
                        loadSettings.currentFightObjectives.Add(item);
                    }
                }
            }

            //loadSettings.SetScene(SceneManager.GetActiveScene().name);

            if (sceneLoader != null && sceneLoader.isActiveAndEnabled)
            {
                sceneLoader.LoadSpecifiedScene(combatScene.ToString(), LoadSceneMode.Single, null);
            }
            else
            {
                Debug.Log("No scene loader");
            }
        }
        else
        {
            Debug.Log("no load settings");
        }
    }
}