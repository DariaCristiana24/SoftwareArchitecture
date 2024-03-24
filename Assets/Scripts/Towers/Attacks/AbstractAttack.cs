using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractAttack : MonoBehaviour
{
    [SerializeField]
    protected ProjectileController projectilePrefab;

    [SerializeField]
    protected float projectileSpeed = 2f;

    protected Transform targetTransform;
    protected List<Transform> targetsTransform = new List<Transform>();

    public void SetTarget(Transform pTargetTransform)
    {
        targetTransform = pTargetTransform;
    }

    public void SetMultipleTargets(List<Transform> pTargetsTransform)
    {
        targetsTransform = pTargetsTransform;
    }

    public abstract void Attack(Transform towerLocation, int damage);
}
