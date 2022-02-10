using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item // class Item에는 이름, 공격력, 체력, 스프라이트가 담겨져 있다.
{
    public string Name;
    public int Attack;
    public int Health;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "Item_So", menuName = "Scirptable Object/Item_So")]
public class Item_So : ScriptableObject
{
    public Item[] items; // 이름, 공격력, 체력, 스프라이트가 담겨져 있는 배열
}
