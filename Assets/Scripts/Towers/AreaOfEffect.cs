using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//handles the effect area of the towers
public class AreaOfEffect : MonoBehaviour
{
    [SerializeField]
    List<Enemy> enemyList = new List<Enemy>();  //list contining all enemies that can be attacked by tower

    void Awake()
    {
        EventBus<EnemyDeathEvent>.OnEvent += removeEnemy;
    }

    void OnDestroy()
    {
        EventBus<EnemyDeathEvent>.OnEvent -= removeEnemy;
    }

    void Update()
    {
        if (enemyList.Count > 0) //removing any null enemies
        {
            if (enemyList[0] == null)
            {
                enemyList.RemoveAt(0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy") 
        { 
            enemyList.Add(other.GetComponent<Enemy>());

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Remove(other.GetComponent<Enemy>());

        }
    }

    //returns the first enemy that enetered the area of effect
    public Enemy GetFirstEnemy()
    {

        if (enemyList.Count > 0)
        {
            return enemyList[0];
        }
        else
        {
            return null;
        }


    }

    //return al the locations of enemies in te area of effect
    public List<Transform> GetEnemyTransformList()
    {
        List<Transform> enemiesTransform = new List<Transform>();
        foreach (Enemy enemy in enemyList)
        {
            if (enemy != null)
            {
                enemiesTransform.Add(enemy.transform);
            }
        }
        return enemiesTransform;
    }

    public void ResizeArea(int scale)
    {
        transform.localScale = new Vector3(scale, 0.01f, scale);
    }

    private void removeEnemy(EnemyDeathEvent enemyDeathEvent)
    {
        if (enemyList.Contains(enemyDeathEvent.enemy))
        {
            enemyList.Remove(enemyDeathEvent.enemy);
        }
    }

}
