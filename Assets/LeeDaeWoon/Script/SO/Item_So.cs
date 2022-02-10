using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item // class Item���� �̸�, ���ݷ�, ü��, ��������Ʈ�� ����� �ִ�.
{
    public string Name;
    public int Attack;
    public int Health;
    public Sprite sprite;
}

[CreateAssetMenu(fileName = "Item_So", menuName = "Scirptable Object/Item_So")]
public class Item_So : ScriptableObject
{
    public Item[] items; // �̸�, ���ݷ�, ü��, ��������Ʈ�� ����� �ִ� �迭
}
