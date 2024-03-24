using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Start of the level where enemies are spawned from
public class Spawner : MonoBehaviour
{
    [SerializeField]
    List<Wave> waves = new List<Wave>();

    private int currentWave = 0;
    void Awake()
    {
        EventBus<StartNextWaveEvent>.OnEvent += StartNextWave;
    }

    void OnDestroy()
    {
        EventBus<StartNextWaveEvent>.OnEvent -= StartNextWave;
    }

    void Start()
    {
        //Improvement
        //Checks if enemy prefabs and their amount is the same 
        foreach (Wave wave in waves)
        {
            while (wave.EnemyPrefabs.Count > wave.EnemiesAmount.Count)
            {
                wave.EnemiesAmount.Add(1);
                Debug.Log(wave.name + " created poorly!");
            }

        }

    }

    //Spawning enemies coroutine
    IEnumerator spawning(int _currentWave)
    {
        GameManager.Instance.FinishedSpawningBool = false;
        for (int i = 0; i < waves[_currentWave].EnemyPrefabs.Count; i++)
        {
             for (int j = 0; j < waves[_currentWave].EnemiesAmount[i]; j++)
             {
                  Instantiate(waves[_currentWave].EnemyPrefabs[i], transform.position, Quaternion.identity);
                  yield return new WaitForSeconds(waves[_currentWave].TimeBetweenSpawns);
             }
        }
        GameManager.Instance.FinishedSpawningBool = true;
    }

    //Starts the next wave
    public void StartNextWave(StartNextWaveEvent startNextWaveEvent) 
    {
        if (waves.Count > currentWave)
        {

            StartCoroutine(spawning(currentWave));
            GameManager.Instance.GetUIManager().UpdateGameStateWaveText(currentWave+1);
            currentWave++;

        }
        if(waves.Count == currentWave)
        {
            GameManager.Instance.FinishedWaves = true;
            currentWave = 0;
        }
    }
}
