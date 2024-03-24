using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Debuffs the player's speed
public class DebuffAttack : AbstractAttack
{
    public override void Attack(Transform towerLocation, int damage)
    {
        if (targetsTransform.Count > 0)
        {
            foreach (Transform enemyTransform in targetsTransform)
            {

                Vector3 projectileVelocity = (enemyTransform.position - towerLocation.position).normalized * projectileSpeed;
                ProjectileController projectileController = Instantiate<ProjectileController>(projectilePrefab);
                projectileController.transform.position = towerLocation.position;
                projectileController.SetVelocity(projectileVelocity);
                projectileController.SetDamage(damage);
            }
        }
    }
}
