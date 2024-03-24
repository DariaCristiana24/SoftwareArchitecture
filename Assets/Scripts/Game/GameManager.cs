using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using System;

//GameManager
public class GameManager : MonoBehaviour
{   
    public static GameManager Instance { get; private set; }
    private float money;


    [SerializeField]
    float startingMoney = 50;
    [SerializeField]
    int buyingTime = 50;

    int timePassed = 0;
    public enum GameState { Playing, Buying, GameOver, GameWon };
    public GameState State;

    [SerializeField]
    float timeScale;

    public Transform FinishLocation;

    public bool FinishedSpawningBool = false;
    public bool FinishedWaves { set; private get; }

    [SerializeField]
    UIManager uIManager;

    EnemyManager enemyManager;
    TowerManager towerManager;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        enemyManager = GetComponent<EnemyManager>();
        towerManager = GetComponent<TowerManager>();
        if(uIManager == null)
        {
            Debug.Log("UI manager missing...");
        }
        if (enemyManager == null)
        {
            Debug.Log("Enemy manager missing...");
        }
        if (towerManager == null)
        {
            Debug.Log("Tower manager missing...");
        }

    }
    void Start()
    {
        money = startingMoney;
        uIManager.UpdateMoney(money);
        UpdateGameState(GameState.Buying);
    }

    void Update()
    {
        if (enemyManager.GetEnemyCount() == 0 && FinishedSpawningBool && State == GameState.Playing)
        {
            //reset the game speed each wave
            timeScale = 1f;
            uIManager.UpdateGameSpeed(timeScale);

            if (!FinishedWaves) 
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.Buying); 
            }
            else
            {
                GameManager.Instance.UpdateGameState(GameManager.GameState.GameWon);
            }
        }

        Time.timeScale = timeScale;
    }

    public void UpdateGameState(GameState _state)
    {
        State = _state;
        switch (State)
        {
            case GameState.Playing:
                EventBus<StartNextWaveEvent>.Publish(new StartNextWaveEvent());
                break;
            case GameState.Buying:
                uIManager.UpdateGameStateBuyingText();
                StartCoroutine(doBuyingTimer());
                break;
            case GameState.GameOver:
                uIManager.ShowGameOver(true);
                State = GameState.GameOver;
                timeScale = 0.01f;
                break;
            case GameState.GameWon:
                uIManager.ShowGameWon(true);
                State = GameState.GameWon;
                timeScale = 0.01f;
                break;
        }
    }

    public void AddMoney(float newMoney)
    {
        money += newMoney;
        uIManager.UpdateMoney(money);
    }


    public float GetMoney()
    {
        return money;
    }

    //buying tiemr in between waves
    IEnumerator doBuyingTimer()
    {
        timePassed = 0;
        uIManager.UpdateTimerText(buyingTime);
        while (timePassed< buyingTime)
        {
            yield return new WaitForSeconds(1);
            timePassed++;
            uIManager.UpdateTimerText(buyingTime-timePassed);
        }
        yield return null;
        UpdateGameState(GameState.Playing);
    }

    public TowerManager GetTowerManager()
    {
        return towerManager;
    }

    public UIManager GetUIManager()
    {
        return uIManager;
    }

    //method called by in game button to skip the timer and start a wave
    public void StartNextWaveButton()
    {
        if (State == GameState.Buying) // allows button to be pressed only while buying
        {
            timePassed = buyingTime-1; //resets the time in the coroutine without showing unwanted numbers
        }
    }

    //increase the game speed through a button
    public void IncreaseGameSpeed()
    {
        if (State == GameState.Playing && timeScale < 5f)
        {
            timeScale += 0.5f;
            uIManager.UpdateGameSpeed(timeScale);
        }
    }

    //decrease the game speed through a button
    public void DecreaseGameSpeed()
    {
        if (State == GameState.Playing && timeScale > 0.5f)
        {
            timeScale -= 0.5f;
            uIManager.UpdateGameSpeed(timeScale);
        }
    }
}
