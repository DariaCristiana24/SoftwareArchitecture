using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Upgrade for the towers as a scriptable object
[CreateAssetMenu(fileName = "New Upgrade", menuName = "Upgrade")]
public class Upgrade : ScriptableObject
{
    public string UpgradeName;
    public int Price = 5;
    public int UpgradePower;

    public enum UpgradeTypeEnum {Range, Damage, Speed }
    public UpgradeTypeEnum UpgradeType;
}
