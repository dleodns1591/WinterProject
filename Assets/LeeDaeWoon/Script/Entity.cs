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

    // ���� ���ݷ°� ü���� ��Ÿ�� �ֱ� ���� Attack �� Health �� ������ش�.
    public int Attack;
    public int Health;
    // �ڱ� �ڽ������� Ȯ�����ִ� isMin�� ������ش�.
    // isBoss_Empty�� Boss �� Empty ������Ʈ�� �־��ش�.
    public bool isMine;
    public bool isBoss_Empty;
    // ����� ���� �� �ִ� �������� Ȯ�����ش�.
    public bool Attack_Able;
    // ������ �Ǵ��Ѵ�.
    public bool isDie;
    // ������ ���� Origin_Pos�� ������ش�.
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
        // Use_DotWeen�� true��� Pos�� ���ϴ� ��ġ�� �ְ�, Dotween�� ����� DotWeen_Time ��ŭ �̵��Ѵ�.
        // Use_DotWeen�� false��� Pos ��ġ�� �����̵� �Ѵ�.
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
        // ���콺�� ������ ���� �� Entity_Manger�� �ִ� Entity_MouseDown �Լ��� ȣ���Ų��.
        if(isMine)
        {
            Entity_Manager.Inst.Entity_MouseDown(this);
        }
    }

    private void OnMouseUp()
    {        
        // ���콺�� ���� ���� �� Entity_Manger�� �ִ� Entity_MouseUp �Լ��� ȣ���Ų��.
        if (isMine)
        {
            Card_Sound.Play();
            Entity_Manager.Inst.Entity_MouseUp();
        }
    }

    private void OnMouseDrag()
    {
        // ���콺�� �巡�׸� �ϰ� ���� �� Entity_Manger�� �ִ� Entity_MouseDrag �Լ��� ȣ���Ų��.
        if (isMine)
        {
            Entity_Manager.Inst.Entity_MouseDrag();
        }
    }

    public bool Damage(int Damage)
    {
        // ü���� ������ ��ŭ ��´�.
        // ü�� �ؽ�Ʈ���� ǥ���Ѵ�.
        Health -= Damage;
        Health_TMP.text = Health.ToString();

        // ���� ü���� 0���� �۰ų� ���ٸ�, isDie�� true�� �Ͽ� �׾�ٰ� ǥ���Ѵ�.
        // return���� ��쿡�� �׾��ٸ� true, ���׾��ٸ� false ���ش�.
        if(Health <= 0)
        {
            isDie = true;
            return true;
        }
        return false;
    }
}
