using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Tower as a scriptable object used mostly for the buying interface
[CreateAssetMenu(fileName = "New Tower", menuName = "Tower")]
public class Tower : ScriptableObject
{
    public int ID = 0;
    public string TowerName;
    public Sprite TowerImage;
    public int Price = 10;
    public GameObject TowerPrefab;
}
