using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Entity : MonoBehaviour
{
    [SerializeField] Item item;
    [SerializeField] SpriteRenderer entity;
    [SerializeField] SpriteRenderer Character;
    [SerializeField] TMP_Text Name_TMP;
    [SerializeField] TMP_Text Attack_TMP;
    [SerializeField] TMP_Text Health_TMP;

    // 현재 공격력과 체력을 나타내 주기 위해 Attack 과 Health 를 만들어준다.
    public int Attack;
    public int Health;
    // 자기 자신인지를 확인해주는 isMin을 만들어준다.
    // isBoss_Empty는 Boss 와 Empty 오브젝트에 넣어준다.
    public bool isMine;
    public bool isBoss_Empty;
    // 정렬을 위해 Origin_Pos를 만들어준다.
    public Vector3 Origin_Pos;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Set_Up(Item item)
    {
        Attack = item.Attack;
        Health = item.Health;

        this.item = item;
        Character.sprite = this.item.sprite;
        Name_TMP.text = this.item.Name;
        Attack_TMP.text = Attack.ToString();
        Health_TMP.text = Health.ToString();
    }

    public void Move_Transform(Vector3 Pos, bool Use_DotWeen, float DotWeen_Time = 0)
    {
        // Use_DotWeen이 true라면 Pos에 원하는 위치를 넣고, Dotween을 사용해 DotWeen_Time 만큼 이동한다.
        // Use_DotWeen이 false라면 Pos 위치로 순간이동 한다.
        if(Use_DotWeen)
        {
            transform.DOMove(Pos, DotWeen_Time);
        }
        else
        {
            transform.position = Pos;
        }
    }
}
