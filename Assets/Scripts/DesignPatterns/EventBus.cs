using System;
using UnityEngine;

public abstract class Event { }

//The event bus 
public class EventBus<T> where T : Event
{
    public static event Action<T> OnEvent;

    public static void Publish(T pEvent)
    {
        OnEvent?.Invoke(pEvent);
    }
}

public class StartNextWaveEvent : Event //next wave event 
{
    public StartNextWaveEvent()
    {

    }
}

public class EnemyDeathEvent : Event //enemy died event 
{
    public EnemyDeathEvent(Enemy pEnemy)
    {
        enemy = pEnemy;
    }
    public Enemy enemy;
}

public class AddEnemyToEnemyListEvent : Event //next wave event 
{
    public AddEnemyToEnemyListEvent(Enemy pEnemy)
    {
        enemy = pEnemy;
    }
    public Enemy enemy;
}

public class GainMoneyEvent : Event //next wave event 
{
    public GainMoneyEvent(int pMoney, Transform pPlaceOfDeath)
    {
        money = pMoney;
        placeOfDeath = pPlaceOfDeath; 
        GameManager.Instance.AddMoney(money);
    }

    public int money;
    public Transform placeOfDeath;
}

public class SelectTowerBuilderEvent : Event //Select tower builder event 
{
    public SelectTowerBuilderEvent(TowerBuilder pTowerBuilder)
    {
        towerBuilder = pTowerBuilder;
    }
    public TowerBuilder towerBuilder;
}
public class SelectTowerControllerEvent : Event //Select tower controller event 
{
    public SelectTowerControllerEvent(TowerController pTowerController)
    {
        towerController = pTowerController;
    }
    public TowerController towerController;
}
public class UpgradeTowerEvent : Event //Upgrade tower event 
{
    public UpgradeTowerEvent(Upgrade.UpgradeTypeEnum pType, float pPrice, int pPower)
    {
        type = pType;
        price = pPrice; 
        power = pPower;
    }

    public Upgrade.UpgradeTypeEnum type;
    public float price;
    public int power;

}
public class BuyTowerEvent : Event //Buy tower event 
{
    public BuyTowerEvent(GameObject pTowerPrefab, float pPrice)
    {
        towerPrefab = pTowerPrefab;
        price = pPrice;
    }
    public GameObject towerPrefab;
    public float price;

}



