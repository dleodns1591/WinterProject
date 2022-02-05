using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item
{
    public string Name; // �̸�
    public int Attack; // ���ݷ�
    public int Defense; //����
    public string Ability; //�ɷ�
    public Sprite sprite;
    public int Cost; //�ڽ�Ʈ
    public int Maintenance; //������
}


[CreateAssetMenu(fileName = "ItemSo", menuName = "Scriptable Object/ItemSo")]
public class Item_So : ScriptableObject
{
    public Item[] items; //�迭
     
    void Start()
    {

    }

    void Update()
    {
        
    }
}
