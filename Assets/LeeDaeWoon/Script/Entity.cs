using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] Item item;

    public int Attack;
    public int Defense;
    public int Cost;
    public bool isMine;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Setup(Item item)
    {
        Attack = item.Attack;
        Defense = item.Defense;
        Cost = item.Cost;
    }
}
