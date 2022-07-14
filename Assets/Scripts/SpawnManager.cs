using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public List<GameObject> enemyList = new List<GameObject>();
    public List<GameObject> itemList = new List<GameObject>();
    private Dictionary<List<GameObject>, float> spawnsAndCosts = new Dictionary<List<GameObject>, float>();

    private float experience;
    // Start is called before the first frame update
    void Start()
    {
        experience = 100;
        spawnsAndCosts[new List<GameObject>() { enemyList[0] }] = 30;
    }

    // Update is called once per frame
    void Update()
    {
        int randomEnemyToSpawn = Random.Range(0, spawnsAndCosts.Count);
        foreach (var elem in spawnsAndCosts.Keys.ElementAt(randomEnemyToSpawn))
        {
            //var currentElem = Instantiate(elem);
            //dynamic var controller;
            //if (currentElem.gameObject.name == "Dragon")
            //{
            //    var controller = currentElem.GetComponent<Dragon>();
            //}
            
        }
    }
}
