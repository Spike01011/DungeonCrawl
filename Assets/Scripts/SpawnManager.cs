using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> itemList = new List<GameObject>();
    private Dictionary<GameObject, float> spawnsAndCosts = new Dictionary<GameObject, float>();
    private GameObject player;

    private Dictionary<GameObject, float> affordableSpawns;

    private float itemSpawnRate = 10;
    private float spawnLocationVarietyRange;
    private float overallMult;
    private float experienceMult;
    internal float experience;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        experience = 100;
        spawnsAndCosts[enemyList[0]] = 15;
        spawnsAndCosts[enemyList[1]] = 20;
        InvokeRepeating("ManageMult", 60, 60);
        overallMult = 1.0f;
        experienceMult = 1.0f;
        spawnLocationVarietyRange = 50;
        Invoke("IncreaseDropRate", 120);
        InvokeRepeating("DifficultyBoost", 300, 300);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        affordableSpawns = spawnsAndCosts.Where(x => x.Value <= experience).ToDictionary(x => x.Key, x => x.Value);
        int randomEnemyToSpawn = Random.Range(0, affordableSpawns.Count);
        if (affordableSpawns.Count > 0)
        {
            float maxX = Random.Range(player.transform.position.x + 20, player.transform.position.x + spawnLocationVarietyRange);
            float minX = Random.Range(player.transform.position.x - 20, player.transform.position.x - spawnLocationVarietyRange);
            float maxZ = Random.Range(player.transform.position.z + 20, player.transform.position.z + spawnLocationVarietyRange);
            float minZ = Random.Range(player.transform.position.z - 20, player.transform.position.z - spawnLocationVarietyRange);
            float[] xPotentialPos = new float[] { minX, maxX };
            float[] zPotentialPos = new float[] { minZ, maxZ };
            float xPos = xPotentialPos[Random.Range(0, xPotentialPos.Length)];
            float zPos = zPotentialPos[Random.Range(0, zPotentialPos.Length)];
            var currentSpawnedObject = Instantiate(spawnsAndCosts.Keys.ElementAt(randomEnemyToSpawn), new Vector3(xPos, 0, zPos), player.transform.rotation).GetComponent<MyEntity>();
            currentSpawnedObject.attackSpeedMult = overallMult;
            currentSpawnedObject.damageMult = overallMult;
            currentSpawnedObject.experienceMult = experienceMult;
            currentSpawnedObject.hpMult = overallMult;
            currentSpawnedObject.speedMult = overallMult;
            currentSpawnedObject.experience = spawnsAndCosts.Values.ElementAt(randomEnemyToSpawn);
            experience -= spawnsAndCosts.Values.ElementAt(randomEnemyToSpawn);
        }
        affordableSpawns.Clear();
    }

    void ManageMult()
    {
        overallMult *= 1.1f;
        experienceMult *= 1.1f;
    }

    void DifficultyBoost()
    {
        overallMult *= 1.2f;
    }

    public void TryToSpawnItem(Vector3 location)
    {
        int itemSpawnRng = Random.Range(0, 100);
        if (itemSpawnRng <= (itemSpawnRate) - 1)
        {
            var randomItemToSpawn = Random.Range(0, itemList.Count);
            Instantiate(itemList[randomItemToSpawn], location, Quaternion.Euler(0, 0, 0));
        }
    }

    void IncreaseDropRate()
    {
        itemSpawnRate += 1;
        if (itemSpawnRate < 20)
        {
            Invoke("IncreaseDropRate", 120);
        }
    }
}
