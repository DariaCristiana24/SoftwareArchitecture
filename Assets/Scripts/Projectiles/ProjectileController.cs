using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//controls the projectiles
public class ProjectileController : MonoBehaviour
{
    [SerializeField]
    private Vector3 velocity;

    private int damage=0; 
    private float bornTime;

    public void SetVelocity(Vector3 pVelocity)
    {
        velocity = pVelocity;
    }

    void Start()
    {
        bornTime= Time.time;
    }
    void Update()
    {
        projectileMovement();
    }

    void projectileMovement()
    {
        transform.Translate(Time.deltaTime * velocity);

        //destroys the projectile after living for 3 seconds
        if (Time.time - bornTime > 3)
        {
            Destroy(gameObject);
        }
    }

    public int GetDamage()
    {
        return damage;
    }

    public void SetDamage(int pDamage)
    {
        damage = pDamage; 
    }

}
