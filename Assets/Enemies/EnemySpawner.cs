using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Object enemy;

    public bool boss;

    private LoadSettings loadSettings;

    // Start is called before the first frame update
    void Start()
    {
        loadSettings = GameObject.Find("LoadSettings").GetComponent<LoadSettings>();

        if (loadSettings != null)
        {
            if (loadSettings.dialogueComplete)
            {
                if ((boss & !loadSettings.bossKilled) || (!boss & !loadSettings.enemyKilled))
                {
                    Vector3 pos = this.transform.position;
                    Quaternion rot = this.transform.rotation;

                    Instantiate(enemy, pos, rot);
                }
            }
        }

        //Destroy(this.gameObject);
    }
}
