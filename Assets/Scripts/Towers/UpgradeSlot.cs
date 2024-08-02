using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

//class in charge of the UI for upgrading towers
public class UpgradeSlot : MonoBehaviour
{
    private Button button;
    private Image image;
    public float Price { set; private get; }
    public int UpgradePower { set; private get; }
    public Upgrade.UpgradeTypeEnum UpgradeType { set; private get; }

    private bool affordable;
    private bool isMaxPower;


    private void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(UpgradeTower);
    }

    private void OnEnable()
    {
        StartCoroutine(setAffordability());
        StartCoroutine(checkIfMaxPower());

    }

    private void UpgradeTower()
    {
        if (affordable && !isMaxPower)
        {
            EventBus<UpgradeTowerEvent>.Publish(new UpgradeTowerEvent(UpgradeType, Price, UpgradePower));
        }
    }

    //checks if the player can afford the upgrade
    private IEnumerator setAffordability()
    {
        yield return new WaitForSeconds(0.01f);
        if (GameManager.Instance.GetMoney() >= Price)
        {
            image.color = Color.white;
            affordable = true;
        }
        else
        {
            image.color = Color.red;
            affordable = false;
        }
    }

    //enforces the limit for each upgrade
    private IEnumerator checkIfMaxPower()
    {
        yield return new WaitForSeconds(0.01f);
        isMaxPower = GameManager.Instance.GetTowerManager().CheckUpgradeMax(UpgradeType);
        if (isMaxPower)
        {
            image.color = Color.red;
        }
    }

}
