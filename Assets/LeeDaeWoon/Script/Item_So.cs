using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string Name; // 이름
    public int Attack; // 공격력
    public int Defense; //방어력
    public string Ability; //능력
    public Sprite sprite;
    public int Cost; //코스트
    public int Maintenance; //유지비
}


[CreateAssetMenu(fileName = "ItemSo", menuName = "Scriptable Object/ItemSo")]
public class Item_So : ScriptableObject
{
    public Item[] items; //배열
     
    void Start()
    {

    }

    void Update()
    {
        
    }
}
