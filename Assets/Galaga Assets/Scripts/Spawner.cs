using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public Color gizmoColor = Color.red;

    public enum SpawnTypes
    {
        Normal,Once,Wave,TimedWave
    }

    public enum EnemyLevels
    {
        Easy,Medium,Hard,Boss
    }
    // ---------------------------
    // End of the Enums
    //----------------------------

    //----------------------------
    public EnemyLevels enemyLevel = EnemyLevels.Easy;

    public GameObject EasyEnemy;
    public GameObject MediumEnemy;
    public GameObject HardEnemy;
    public GameObject BossEnemy;
    private Dictionary<EnemyLevels, GameObject> Enemies = new Dictionary<EnemyLevels, GameObject>(4);

    public int totalEnemy = 10;
    private int numEnemy = 0;
    private int spawnedEnemy = 0;
    // End of Enemy Settings
    
    //the ID of the Spawner
    private int SpawnID;

    //----------------------------------
    // Different spawner states and ways of doing them
    //----------------------------------
    private bool waveSpawn = false;
    public bool Spawn = true;
    public SpawnTypes spawnType = SpawnTypes.Normal;

    // Timed wave controls
    public float waveTimer = 30.0f;
    private float timeTillWave = 0.0f;

    //Wave controls
    public int totalWaves = 5;
    private int numWaves = 0;

    //-------------------------------------
    // End of DifferentSpawn States and Ways of doing them
    //-------------------------------------
	// Use this for initialization

	void Start () {
        // sets a random number for the ID of the Spawner
        SpawnID = Random.Range(1, 500);
        Enemies.Add(EnemyLevels.Easy, EasyEnemy);
        Enemies.Add(EnemyLevels.Medium, MediumEnemy);
        Enemies.Add(EnemyLevels.Hard, HardEnemy);
    }

    // Draws a cube to show where the spawn point is... Useful if you don't have a object that show the spawn point
    void OnDrawGizmos()
    {
        // Sets the color to red
        Gizmos.color = gizmoColor;
        //draws a small cube at the location of the gam object that the script is attached to
        Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
    }

    void Update()
    {
        if (Spawn)
        {
            // Spawns enemies everytime one dies
            if (spawnType == SpawnTypes.Normal)
            {
                // checks to see if the number of spawned enemies is less than the max num of enemies
                if (numEnemy < totalEnemy)
                {
                    // spawns an enemy
                    spawnEnemy();
                }
            }

            // Spawns enemies only once
            else if (spawnType == SpawnTypes.Once)
            {
                // checks to see if the overall spawned num of enemies is more or equal to the total to be spawned
                if (spawnedEnemy >= totalEnemy)
                {
                    //sets the spawner to false
                    Spawn = false;
                }
                else
                {
                    // spawns an enemy
                    spawnEnemy();
                }
            }

            //spawns enemies in waves, so once all are dead, spawns more
            else if (spawnType == SpawnTypes.Wave)
            {
                if (numWaves < totalWaves + 1)
                {
                    if (waveSpawn)
                    {
                        //spawns an enemy
                        spawnEnemy();
                    }
                    if (numEnemy == 0)
                    {
                        // enables the wave spawner
                        waveSpawn = true;
                        //increase the number of waves
                        numWaves++;
                    }
                    if (numEnemy == totalEnemy)
                    {
                        // disables the wave spawner
                        waveSpawn = false;
                    }
                }
            }
            // Spawns enemies in waves but based on time.
            else if (spawnType == SpawnTypes.TimedWave)
            {
                // checks if the number of waves is bigger than the total waves
                if (numWaves <= totalWaves)
                {
                    // Increases the timer to allow the timed waves to work
                    timeTillWave += Time.deltaTime;
                    if (waveSpawn)
                    {
                        //spawns an enemy
                        spawnEnemy();
                    }
                    // checks if the time is equal to the time required for a new wave
                    if (timeTillWave >= waveTimer)
                    {
                        // enables the wave spawner
                        waveSpawn = true;
                        // sets the time back to zero
                        timeTillWave = 0.0f;
                        // increases the number of waves
                        numWaves++;
                        // A hack to get it to spawn the same number of enemies regardless of how many have been killed
                        numEnemy = 0;
                    }
                    if (numEnemy >= totalEnemy)
                    {
                        // diables the wave spawner
                        waveSpawn = false;
                    }
                }
                else
                {
                    Spawn = false;
                }
            }
        }
    }
    // spawns an enemy based on the enemy level that you selected
    private void spawnEnemy()
    {
        GameObject Enemy = (GameObject)Instantiate(Enemies[enemyLevel], gameObject.transform.position, Quaternion.identity);
        Enemy.SendMessage("setName", SpawnID);
        // Increase the total number of enemies spawned and the number of spawned enemies
        numEnemy++;
        spawnedEnemy++;
    }
    // Call this function from the enemy when it "dies" to remove an enemy count
    public void killEnemy(int sName)
    {
        // if the enemy's spawnId is equal to this spawnersID then remove an enemy count
        if (SpawnID == sName)
        {
            numEnemy--;
        }
    }
    //enable the spawner based on spawnerID
    public void enableSpawner(int sName)
    {
        if (SpawnID == sName)
        {
            Spawn = true;
        }
    }
    //disable the spawner based on spawnerID
    public void disableSpawner(int sName)
    {
        if (SpawnID == sName)
        {
            Spawn = false;
        }
    }
    // returns the Time Till the Next Wave, for a interface, ect.
    public float TimeTillWave
    {
        get
        {
            return timeTillWave;
        }
    }
    // Enable the spawner, useful for trigger events because you don't know the spawner's ID.
    public void enableTrigger()
    {
        Spawn = true;
    }
}
