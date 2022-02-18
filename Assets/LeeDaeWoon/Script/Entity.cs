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
    // 대상을 때릴 수 있는 상태인지 확인해준다.
    public bool Attack_Able;
    // 죽음을 판단한다.
    public bool isDie;
    // 정렬을 위해 Origin_Pos를 만들어준다.
    public Vector3 Origin_Pos;

    public AudioSource Card_Sound;

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

    private void OnMouseDown()
    {
        // 마우스를 누르고 있을 때 Entity_Manger에 있는 Entity_MouseDown 함수를 호출시킨다.
        if(isMine)
        {
            Entity_Manager.Inst.Entity_MouseDown(this);
        }
    }

    private void OnMouseUp()
    {        
        // 마우스를 때고 있을 때 Entity_Manger에 있는 Entity_MouseUp 함수를 호출시킨다.
        if (isMine)
        {
            Card_Sound.Play();
            Entity_Manager.Inst.Entity_MouseUp();
        }
    }

    private void OnMouseDrag()
    {
        // 마우스로 드래그를 하고 있을 때 Entity_Manger에 있는 Entity_MouseDrag 함수를 호출시킨다.
        if (isMine)
        {
            Entity_Manager.Inst.Entity_MouseDrag();
        }
    }

    public bool Damage(int Damage)
    {
        // 체력을 데미지 만큼 깎는다.
        // 체력 텍스트에도 표시한다.
        Health -= Damage;
        Health_TMP.text = Health.ToString();

        // 만약 체력이 0보다 작거나 같다면, isDie를 true로 하여 죽어다고 표시한다.
        // return같은 경우에는 죽었다면 true, 안죽었다면 false 해준다.
        if(Health <= 0)
        {
            isDie = true;
            return true;
        }
        return false;
    }
}
