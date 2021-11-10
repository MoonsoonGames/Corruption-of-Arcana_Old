using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSettings : MonoBehaviour
{
    public float health = 1.2f;

    public bool dialogueComplete = false;

    public bool enemyKilled = false;
    public bool bossKilled = false;

    public bool fightingBoss = false;

    public Vector3 playerPos;
    public Vector3 mamaPos;

    public bool died;

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
