using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

//controls a tower
public class TowerController : MonoBehaviour
{
    [SerializeField]
    AbstractAttack attacker;

    [SerializeField]
    private float attackInterval = 1f;

    [SerializeField]
    private float attackSpeed = 1f;
    [SerializeField]
    private int areaOfEffectSize = 10;
    [SerializeField]
    private int damage = 0;
    private bool attacking = true;

    private AreaOfEffect areaOfEffect;
    private Renderer ren;

    private Color defaultColor;

    [SerializeField]
    GameObject towerModel;

    private bool damageMaxed = false;
    private bool speedMaxed = false;

    void Start()
    {
        ren = towerModel.GetComponentInChildren<Renderer>();
        areaOfEffect = GetComponentInChildren<AreaOfEffect>();

        if(ren == null)
        {
            Debug.Log("Renderer on tower missing...");
        }
        if (areaOfEffect == null)
        {
            Debug.Log("AreOfEffect on tower missing...");
        }

        defaultColor = ren.material.color;

        areaOfEffect.ResizeArea(areaOfEffectSize);

        StartCoroutine(attackCoroutine());
    }

    //tower attatck coroutine
    private IEnumerator attackCoroutine()
    {
        while (attacking)
        {
            yield return new WaitForSeconds(attackInterval);
            if (attacker.GetComponent<SingleTargetAttack>())  //tower has one target
            {
                if (areaOfEffect.GetFirstEnemy())
                {
                    attacker.SetTarget(areaOfEffect.GetFirstEnemy().transform);

                }
                else
                {
                    attacker.SetTarget(null) ;
                }

            }
            else //tower has multiple targets
            {
                attacker.SetMultipleTargets(areaOfEffect.GetEnemyTransformList());
            }
            attacker.Attack(transform,damage);
        }
    }

    //visual feedback for player
    private void OnMouseEnter()
    {
        if (!GameManager.Instance.GetTowerManager().SelectedTowerInUse && GameManager.Instance.State == GameManager.GameState.Buying)
        {
            ren.material.color = Color.green;
        }
    }

    private void OnMouseDown()
    {
        //open the upgradibng tower interface
        if (Input.GetMouseButton(0) && !GameManager.Instance.GetTowerManager().SelectedTowerInUse && GameManager.Instance.State == GameManager.GameState.Buying)
        {
            EventBus<SelectTowerControllerEvent>.Publish(new SelectTowerControllerEvent(this));
            GameManager.Instance.GetUIManager().UpdateStatsUpgrading(areaOfEffectSize,damage,(int)attackSpeed);
            StartCoroutine(GameManager.Instance.GetUIManager().ShowUpgradingPanel(true));
        }
    }

    //visual feedback for player
    private void OnMouseExit()
    {
        ren.material.color = defaultColor;
    }


    public void UpgradeRange(int pPower)
    {
        areaOfEffectSize += pPower;
        areaOfEffect.ResizeArea(areaOfEffectSize);
    }

    public void UpgradeSpeed(int pPower)
    {
        attackSpeed += pPower;
        attackInterval = 1/attackSpeed;

        //checks if max speed was reached to give visual feedback
        if (GameManager.Instance.GetTowerManager().CheckUpgradeMax(Upgrade.UpgradeTypeEnum.Speed))
        {
            speedMaxed = true;
            CheckMaxedOut();
        }
    }
    public void UpgradeDamage(int pPower)
    {
        damage += pPower;

        //checks if max damage was reached to give visual feedback
        if (GameManager.Instance.GetTowerManager().CheckUpgradeMax(Upgrade.UpgradeTypeEnum.Damage))
        {
            damageMaxed = true;
            CheckMaxedOut();
        }
    }

    //checks if a tower has damage and speed maxed out in order to give visual feedback
    private void CheckMaxedOut()
    {
        if(damageMaxed && speedMaxed)
        {
            towerModel.transform.localScale *= 1.5f;
        }
    }

    public bool CheckRangeMax(int maxRange)
    {
        if(areaOfEffectSize >=  maxRange) { return true; }
        return false;
    }

    public bool CheckDamageMax(int maxDamage)
    {
        if (damage >= maxDamage ) { return true; }
        return false;
    }
    public bool CheckSpeedMax(int maxSpeed)
    {
        if (attackSpeed >= maxSpeed) { return true; }
        return false;
    }

    public int GetRange()
    {
        return areaOfEffectSize;
    }
    public int GetDamage()
    {
        return damage;
    }

    public int GetSpeed()
    {
        return (int)attackSpeed;
    }
}
