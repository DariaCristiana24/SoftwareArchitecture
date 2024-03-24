using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

//Manages all the towers
public class TowerManager : MonoBehaviour
{
    List<TowerController> activeTowers = new List<TowerController>();

    [SerializeField]
    GameObject buyingPlanePrefab;

    TowerBuilder selectedTowerBuilder;
    TowerController selectedTowerController;

    [SerializeField]
    List<Tower> towers = new List<Tower>();

    [SerializeField]
    List<Upgrade> upgrades = new List<Upgrade>();


    public bool SelectedTowerInUse = false;

    [SerializeField]
    int maxRange;
    [SerializeField]
    int maxDamage;
    [SerializeField]
    int maxSpeed;
    void Awake()
    {
        EventBus<SelectTowerBuilderEvent>.OnEvent += SelectTowerBuilder;
        EventBus<SelectTowerControllerEvent>.OnEvent += SelectTowerController;
        EventBus<UpgradeTowerEvent>.OnEvent += UpgradeTower;
        EventBus<BuyTowerEvent>.OnEvent += BuyTower;
    }

    void OnDestroy()
    {
        EventBus<SelectTowerBuilderEvent>.OnEvent -= SelectTowerBuilder;
        EventBus<SelectTowerControllerEvent>.OnEvent -= SelectTowerController;
        EventBus<UpgradeTowerEvent>.OnEvent -= UpgradeTower;
        EventBus<BuyTowerEvent>.OnEvent -= BuyTower;
    }

    public void BuyTower(BuyTowerEvent buyTowerEvent)
    {
        //buy a new tower if a slot is selected
        if (selectedTowerBuilder != null)
        {
            TowerController newTower = Instantiate(buyTowerEvent.towerPrefab, selectedTowerBuilder.transform.position, Quaternion.identity).GetComponent<TowerController>();
            activeTowers.Add(newTower);
            GameManager.Instance.AddMoney(-buyTowerEvent.price);

        }

        UnselectBuilderSlot();
    }

    // selects a tower slot for buying a tower
    public void SelectTowerBuilder(SelectTowerBuilderEvent selectTowerEvent)
    {
        selectedTowerBuilder = selectTowerEvent.towerBuilder;
        SelectedTowerInUse = true;
    }

    //resets the builder slot
    public void UnselectBuilderSlot()
    {
        if(selectedTowerBuilder) selectedTowerBuilder.Unselect();
        selectedTowerBuilder = null;
        SelectedTowerInUse = false;
        StartCoroutine(GameManager.Instance.GetUIManager().ShowBuyingPanel(false));
    }

    //Upgrades a tower
    public void UpgradeTower(UpgradeTowerEvent upgradeTowerEvent)
    {
        if (selectedTowerController)
        {
            switch (upgradeTowerEvent.type)
            {
                case Upgrade.UpgradeTypeEnum.Range:
                    selectedTowerController.UpgradeRange(upgradeTowerEvent.power);
                    break;
                case Upgrade.UpgradeTypeEnum.Damage:
                    selectedTowerController.UpgradeDamage(upgradeTowerEvent.power);
                    break;
                case Upgrade.UpgradeTypeEnum.Speed:
                    selectedTowerController.UpgradeSpeed(upgradeTowerEvent.power);
                    break;
            }
            GameManager.Instance.AddMoney(-upgradeTowerEvent.price);
        }
        UnselectTowerControllerSlot();
    }

    //selects a tower controller
    public void SelectTowerController(SelectTowerControllerEvent selectTowerControllerEvent)
    {
        selectedTowerController = selectTowerControllerEvent.towerController;
        SelectedTowerInUse = true;

    }

    //resets the upgrade slot (towerController)
    public void UnselectTowerControllerSlot()
    {
        selectedTowerController = null;
        SelectedTowerInUse = false;
        StartCoroutine(GameManager.Instance.GetUIManager().ShowUpgradingPanel(false));
    }

    //destroys a tower (used in the upgrade ui)
    public void RemoveTower()
    {
        if (selectedTowerController)
        {
            activeTowers.Remove(selectedTowerController);
            Destroy(selectedTowerController.gameObject);
        }
    }

    //checks if the upgrade is possible based on the limit for each upgrade
    public bool CheckIfTowerUpgradeIsMax(Upgrade.UpgradeTypeEnum type)
    {
        if (selectedTowerController)
        {
            switch (type)
            {
                case Upgrade.UpgradeTypeEnum.Range:
                    return selectedTowerController.CheckIfTooBigRange(maxRange);
                case Upgrade.UpgradeTypeEnum.Damage:
                    return selectedTowerController.CheckIfTooHighDamage(maxDamage);
                case Upgrade.UpgradeTypeEnum.Speed:
                    return selectedTowerController.CheckIfTooHighSpeed(maxSpeed);

            }
        }
        return false;
    }

    public GameObject GetBuyingPlanePrefab()
    {
        return buyingPlanePrefab;
    }
    public List<Tower> GetTowerSlots()
    {
        return towers;
    }

    public List<Upgrade> GetUpgradeSlots()
    {
        return upgrades;
    }

}
