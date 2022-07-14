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

    internal float experience;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        experience = 100;
        spawnsAndCosts[enemyList[0]] = 15;
        spawnsAndCosts[enemyList[1]] = 20;

        StartCoroutine(ManageExpMult());

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float minimumExpCost = spawnsAndCosts.Min(x => x.Value);
        int randomEnemyToSpawn = Random.Range(0, spawnsAndCosts.Count);
        if (experience > 0 && experience >= minimumExpCost)
        {
            float maxX = Random.Range(player.transform.position.x + 20, player.transform.position.x + 100);
            float minX = Random.Range(player.transform.position.x - 20, player.transform.position.x - 100);
            float maxZ = Random.Range(player.transform.position.z + 20, player.transform.position.z + 100);
            float minZ = Random.Range(player.transform.position.z - 20, player.transform.position.z - 100);
            float[] xPotentialPos = new float[] { minX, maxX };
            float[] zPotentialPos = new float[] { minZ, maxZ };
            float xPos = xPotentialPos[Random.Range(0, xPotentialPos.Length)];
            float zPos = zPotentialPos[Random.Range(0, zPotentialPos.Length)];
            var currentSpawnedObject = Instantiate(spawnsAndCosts.Keys.ElementAt(randomEnemyToSpawn), new Vector3(xPos, 0, zPos), player.transform.rotation).GetComponent<Dragon>();
            experience -= spawnsAndCosts.Values.ElementAt(randomEnemyToSpawn);
        }
    }

    IEnumerator ManageExpMult()
    {
        yield return new WaitForSeconds(60);
        enemyList.ForEach(x => x.gameObject.GetComponent<MyEntity>().experienceMult *= 1.1f);
        StartCoroutine(ManageExpMult());
    }
}
