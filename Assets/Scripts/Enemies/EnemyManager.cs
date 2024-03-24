using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Manages all the enemies
public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    Transform targetLocation;

    [SerializeField]
    List<Enemy> enemies = new List<Enemy>();

    void Awake()
    {
        EventBus<EnemyDeathEvent>.OnEvent += EnemyDeath;
        EventBus<AddEnemyToEnemyListEvent>.OnEvent += addEnemy;
    }

    void OnDestroy()
    {
        EventBus<EnemyDeathEvent>.OnEvent -= EnemyDeath;
        EventBus<AddEnemyToEnemyListEvent>.OnEvent -= addEnemy;
    }

    //An enemy died
    public void EnemyDeath(EnemyDeathEvent enemyDeathEvent) 
    {
        enemies.Remove(enemyDeathEvent.enemy);
    }

    //Add an enemy to the list
    public void addEnemy(AddEnemyToEnemyListEvent addEnemyToEnemyListEvent)
    {
        enemies.Add(addEnemyToEnemyListEvent.enemy);
    }

    //Get the amount of enemies
    public int GetEnemyCount()
    {
        return enemies.Count;
    }


}
