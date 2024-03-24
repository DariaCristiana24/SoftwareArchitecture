using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Single attack - one bullet for enemy
public class SingleTargetAttack : AbstractAttack
{
    public override void Attack(Transform towerLocation, int damage)
    {
        if (targetTransform != null)
        {
            Vector3 projectileVelocity = (targetTransform.position - towerLocation.position).normalized * projectileSpeed;
            ProjectileController projectileController = Instantiate<ProjectileController>(projectilePrefab);
            projectileController.transform.position = towerLocation.position;
            projectileController.SetVelocity(projectileVelocity);
            projectileController.SetDamage(damage);
        }
    }
}
