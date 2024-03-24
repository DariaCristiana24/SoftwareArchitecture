using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A scripptable object wave contaning the enemy types sand amount of enemies
[CreateAssetMenu(fileName = "New Wave", menuName = "Wave")]
public class Wave : ScriptableObject
{
    public int WaveID = 0;
    public string WaveDescription;
    public List<Enemy> EnemyPrefabs = new List<Enemy>();
    public List<int> EnemiesAmount = new List<int>();
    public float TimeBetweenSpawns = 2;

}
