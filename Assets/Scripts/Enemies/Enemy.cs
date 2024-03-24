using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

//handles the enemy behaviour 
public class Enemy : MonoBehaviour
{
    [SerializeField]
    int life = 10;
    [SerializeField]
    int speed = 3;
    [SerializeField]
    int money = 10;
    [SerializeField]
    Slider lifeBar;

    private NavMeshAgent agent;
    private Transform finish;
    private Coroutine coroutineDebuff;

    private void Start()
    {
        if (GameManager.Instance != null)
        {
            finish = GameManager.Instance.FinishLocation; //get the finish location so the ai can find the path
        }
        else
        {
            Debug.Log("GameManager missing...");
        }
        EventBus<AddEnemyToEnemyListEvent>.Publish(new AddEnemyToEnemyListEvent(this)); //adds enemy to the list
        setUpNavMeshAgent(); //sets up the navMesh so 
        createLifeBar(); // creates the ui life bar

    }


    //Enemy Dies
    public void OnDeath(bool killedByPlayer)
    {
        EventBus<EnemyDeathEvent>.Publish(new EnemyDeathEvent(this));

        if (killedByPlayer)
        {
            //Gain money     
            EventBus<GainMoneyEvent>.Publish(new GainMoneyEvent(money, transform));
        }

        //Destroy the enemy
        StartCoroutine(destroyEnemy());
    }

    //Short delay before deestroying the enemy to prevent bugs
    private IEnumerator destroyEnemy()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }

    //Enemy takes damage
    public void GetHit(int damage)
    {
        life -= damage; 
        updateLifeBar();

        if (life <= 0)
        {
            //kill the enemy and collect money
            OnDeath(true);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Enemy gets hit with a normal projectile
        if(other.tag == "Projectile")
        {
            //Enemy takes damage based on the projectile
            GetHit(other.GetComponent<ProjectileController>().GetDamage());
        }
        //Enemy gets hit with a speed debuff projectile
        if (other.tag == "DebuffProjectile")
        {
            //Enemy can taek damage if the projectiel says so
            GetHit(other.GetComponent<ProjectileController>().GetDamage());

            //Enemy gains the same slower speed while it keeps getttign hit with this projectile
            agent.speed = speed/1.5f;
            if (coroutineDebuff ==null)
            {
                coroutineDebuff = StartCoroutine(SpeedDebuff());
            }
            else
            {
                StopCoroutine(coroutineDebuff);
                coroutineDebuff = StartCoroutine(SpeedDebuff());
            }
        }
    }


    //Slows down the enemy
    private IEnumerator SpeedDebuff()
    {
        yield return new WaitForSeconds(1);
        agent.speed = speed;
    }

    //Sets up the navMesh for the enemies
    private void setUpNavMeshAgent()
    {
        agent = GetComponent<NavMeshAgent>();
        if (finish)
        {
            agent.SetDestination(finish.position);
        }
        else 
        {
            Debug.Log("Missing destination for enemies...");
        }
        agent.speed = speed;
    }


    //Creates the Life Bar UI
    private void createLifeBar() 
    {
        lifeBar.maxValue = life;
        lifeBar.value = life;
    }


    //Updates the life bar UI
    private void updateLifeBar() 
    {
        lifeBar.value = life;
    }


}
