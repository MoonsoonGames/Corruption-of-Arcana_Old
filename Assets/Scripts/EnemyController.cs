using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public bool boss = false;

    public Object[] enemies = new Object[3];
    public QuestObjective objective;

    private LoadSettings loadSettings;
    private SceneLoader sceneLoader;

    public E_Levels combatScene;
    public Vector2 goldReward;
    public float potionReward;
    public string itemReward;

    public string enemyName;

    // Start is called before the first frame update
    void Awake()
    {
        loadSettings = LoadSettings.instance;
        sceneLoader = GetComponent<SceneLoader>();

        if (loadSettings != null)
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

            loadSettings.goldReward = goldReward;
            loadSettings.potionReward = potionReward;
            loadSettings.itemReward = itemReward;

            if (objective != null && objective.canComplete)
            {
                loadSettings.currentFightObjective = objective;
            }

            loadSettings.lastLevelString = SceneManager.GetActiveScene().name;

            if (sceneLoader != null)
                sceneLoader.LoadSpecifiedScene(combatScene.ToString(), LoadSceneMode.Single, null);
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
