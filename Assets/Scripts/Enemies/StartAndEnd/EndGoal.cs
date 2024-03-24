using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

//handles th end goal of the level
public class EndGoal : MonoBehaviour
{
    [SerializeField]
    int maxEnemies = 10; 
    [SerializeField]
    TextMeshPro enemyCapacity; 

    private int currentEnemies;

    private void Start()
    {
        UpdateTextEnemyCapacity();
    }

    private void OnTriggerEnter(Collider other)
    {
        //enemies enetering the endGoal damaging it
        if (other.tag == "Enemy" && other.GetComponent<Enemy>()) 
        {
            currentEnemies++;
            //the enemy dies without the player gaining money
            other.GetComponent<Enemy>().OnDeath(false);
            UpdateTextEnemyCapacity();

            //game over if max enemies is reached
            if (currentEnemies >= maxEnemies)
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.GameOver);
            }
        }
    }

    private void UpdateTextEnemyCapacity()
    {
        enemyCapacity.SetText(maxEnemies-currentEnemies  + "/"+ maxEnemies +"\n Enemy Capacity");
    }


}
