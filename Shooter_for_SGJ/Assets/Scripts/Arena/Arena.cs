using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arena : MonoBehaviour
{
    [SerializeField] private GameObject[] Enemies;
    [SerializeField] private GameObject[] Spawns;
    public int KilledEnemies;
    public int StartAmountOfEnemies = 5;
    public int FinalAmountOfEnemies = 15;
    public int SpawnedEnemies;
    public bool IsFighting;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn()
    {
        if (FinalAmountOfEnemies > KilledEnemies)
        {
            Instantiate(Enemies[Random.Range(0, Enemies.Length)], Spawns[Random.Range(0, Spawns.Length)].transform.position, Quaternion.identity);
            SpawnedEnemies++;
        }
        
       
    }

    public void StartFight()
    {
        IsFighting = true;
        for (int i = 0; i < StartAmountOfEnemies; i++)
        {
            Spawn();
        }
    }

    public bool Check()
    {
        if (SpawnedEnemies == KilledEnemies)
        {
            IsFighting = false;
            return true;
           
        }
        else
        {
            return false;
        }
    }

}
