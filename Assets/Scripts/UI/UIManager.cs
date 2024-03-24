using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//UI manager
public class UIManager : MonoBehaviour
{

    [SerializeField]
    TextMeshProUGUI moneyText;
    [SerializeField]
    TextMeshProUGUI gameState;
    [SerializeField]
    TextMeshProUGUI timerText;
    [SerializeField]
    TextMeshProUGUI gameSpeedText;

    [SerializeField]
    GameObject gameOverMenu;
    [SerializeField]
    GameObject gameWonMenu;

    [SerializeField]
    GameObject buyingPanel;
    [SerializeField]
    GameObject buyingPanelContent;
    [SerializeField]
    GameObject buyingSlotPrefab;

    [SerializeField]
    GameObject upgradingPanel;
    [SerializeField]
    GameObject upgradingPanelContent;
    [SerializeField]
    GameObject upgradingSlotPrefab;

    [SerializeField]
    TextMeshProUGUI statsTextUpgrading;


    List<Tower> towers = new List<Tower>();
    List<Upgrade> upgrades = new List<Upgrade>();

    private void Start()
    {
        CreateTowerBuyingInterface();
        CreateUpgradeInterface();
    }

    //creates the buying interface
    private void CreateTowerBuyingInterface()
    {
        towers = GameManager.Instance.GetTowerManager().GetTowerSlots();
        for(int i = 0; i < towers.Count; i++)
        {
            GameObject newSlot = Instantiate(buyingSlotPrefab, buyingPanelContent.transform);
            var towerName = newSlot.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            var towerPrice = newSlot.transform.Find("Price").GetComponent<TextMeshProUGUI>(); 
            var towerStats = newSlot.transform.Find("Stats").GetComponent<TextMeshProUGUI>();
            var towerImage = newSlot.transform.Find("Image").GetComponent<UnityEngine.UI.Image>();

            towerName.text = towers[i].TowerName;
            towerPrice.text = towers[i].Price.ToString() + "$";

            towerImage.sprite = towers[i].TowerImage;

            newSlot.GetComponent<TowerSlot>().price = towers[i].Price;
            newSlot.GetComponent<TowerSlot>().PrefabTower = towers[i].TowerPrefab;

            int towerRange = towers[i].TowerPrefab.GetComponent<TowerController>().GetRange();
            int towerDamage = towers[i].TowerPrefab.GetComponent<TowerController>().GetDamage();
            int towerSpeed = towers[i].TowerPrefab.GetComponent<TowerController>().GetSpeed();

            towerStats.text = "Range: "+ towerRange + " \r\nDamage: "+towerDamage+"\r\nAttack Speed: " + towerSpeed;
        }
    }

    //creates the upgrading interface
    private void CreateUpgradeInterface()
    {
        upgrades = GameManager.Instance.GetTowerManager().GetUpgradeSlots();
        for (int i = 0; i < upgrades.Count; i++)
        {
            GameObject newSlot = Instantiate(upgradingSlotPrefab, upgradingPanelContent.transform);
            var upgradeName = newSlot.transform.Find("Name").GetComponent<TextMeshProUGUI>();
            var upgradePrice = newSlot.transform.Find("Price").GetComponent<TextMeshProUGUI>();
            var upgradePower = newSlot.transform.Find("UpgradePower").GetComponent<TextMeshProUGUI>();

            upgradeName.text = upgrades[i].UpgradeName;
            upgradePrice.text = upgrades[i].Price.ToString() + "$";
            upgradePower.text = "+" + upgrades[i].UpgradePower.ToString();

            newSlot.GetComponent<UpgradeSlot>().Price = upgrades[i].Price;
            newSlot.GetComponent<UpgradeSlot>().UpgradePower = upgrades[i].UpgradePower;
            newSlot.GetComponent<UpgradeSlot>().UpgradeType = upgrades[i].UpgradeType;
        }
    }

    public void UpdateMoney(float _money)
    {
        moneyText.SetText(_money+"$");
    }

    public void ShowGameOver(bool show)
    {
        gameOverMenu.SetActive(show);
    }
    public void ShowGameWon(bool show)
    {
        gameWonMenu.SetActive(show);
    }

    public void UpdateGameStateWaveText(int _currentWave)
    {
        gameState.SetText("Wave " + _currentWave);
    }
    public void UpdateGameStateBuyingText()
    {
        gameState.SetText("Buying Phase");
    }

    public IEnumerator ShowBuyingPanel(bool show)
    {
        yield return new WaitForSeconds(0.1f);
        buyingPanel.SetActive(show);
    }

    public void UpdateTimerText(float _time)
    {
        if (_time == 0) 
        { 
            timerText.SetText(""); 
        }
        else
        {
            timerText.SetText(_time.ToString());
        }
    }

    //shows upgrading interface
    public IEnumerator ShowUpgradingPanel(bool show)
    {
        yield return new WaitForSeconds(0.1f);
        upgradingPanel.SetActive(show);
    }

    //updates the stats showing in the upgrade interface
    public void UpdateStatsUpgrading(int range, int damage, int speed)
    {
        statsTextUpgrading.SetText("Range: " + range + "\r\n\r\nDamage: "+ damage +"\r\n\r\nAttack Speed: "+ speed );
    }

    public void UpdateGameSpeed(float speed)
    {
        gameSpeedText.SetText("Game Speed: \n " + speed.ToString());
    }


}
