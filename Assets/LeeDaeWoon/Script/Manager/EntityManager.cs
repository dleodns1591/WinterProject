using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public static EntityManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject Entity_Prefab;
    [SerializeField] List<Entity> My_Entities;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
