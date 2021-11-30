using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyController : MonoBehaviour
{
    public static EnemyController instance;

    public bool boss = false;

    public Object[] enemies = new Object[3];

    private LoadSettings loadSettings;

    public E_Levels combatScene;

    // Start is called before the first frame update
    void Awake()
    {
        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (loadSettings != null)
        {
            if (loadSettings.enemiesKilled.ContainsKey(name))
            {
                if (loadSettings.enemiesKilled[name])
                {
                    Destroy(this.gameObject);
                }
            }
            else
            {
                loadSettings.enemiesKilled.Add(name, false);

                foreach (var item in loadSettings.enemiesKilled.Keys)
                {
                    Debug.Log(item);
                }
            }
        }
    }

    public void LoadCombat(SceneLoader sceneLoader)
    {
        if (loadSettings != null)
        {
            loadSettings.fightingBoss = boss;
            loadSettings.currentFight = name;

            loadSettings.enemies[0] = enemies[0];
            loadSettings.enemies[1] = enemies[1];
            loadSettings.enemies[2] = enemies[2];

            if (sceneLoader != null)
                sceneLoader.LoadSpecifiedScene(combatScene.ToString(), LoadSceneMode.Single);
        }
    }
}
