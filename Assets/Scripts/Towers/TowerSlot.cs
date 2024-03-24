using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//UI for buying towers controlled by this class
public class TowerSlot : MonoBehaviour
{
    private Button button;
    private Image image;
    public GameObject PrefabTower{ set; private get; }
    public float price { set; private get; }
    private bool affordable;

    void Start()
    {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        button.onClick.AddListener(BuyTower);
    }

    private void OnEnable()
    {
        StartCoroutine( setAffordability());
    }

    private void BuyTower()
    {
        if (affordable)
        {
            EventBus<BuyTowerEvent>.Publish(new BuyTowerEvent(PrefabTower, price));
        }
    }

    //check if towers ar affordable to the player
    private IEnumerator setAffordability()
    {
        yield return new WaitForSeconds(0.01f);
        if (GameManager.Instance.GetMoney()>= price)
        {
            image.color = Color.white;
            affordable = true;
        }
        else
        {
            image.color = Color.red;
            affordable=false;
        }   
    }

}
