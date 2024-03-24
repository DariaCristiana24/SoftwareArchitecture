using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

//Show text feedback next to the enemies
public class EnemyFeedback : MonoBehaviour
{
    [SerializeField]
    TextMeshPro moneyFeedback;
    // Start is called before the first frame update
    void Awake()
    {
        EventBus<GainMoneyEvent>.OnEvent += GainMoney;
    }

    void OnDestroy()
    {
        EventBus<GainMoneyEvent>.OnEvent -= GainMoney;
    }


    //Show a number next to the enemy with how much money was gained
    private void GainMoney(GainMoneyEvent gainMoneyEvent)
    {
        TextMeshPro text = Instantiate(moneyFeedback, gainMoneyEvent.placeOfDeath.position + new Vector3(0,1,0), Quaternion.Euler(90, 0, 0));
        moneyFeedback.SetText("+" + gainMoneyEvent.money + " $");
    }
}
