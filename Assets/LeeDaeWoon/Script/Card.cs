using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;  

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer Character;
    [SerializeField] TMP_Text Name_TMP;
    [SerializeField] TMP_Text Health_TMP;
    [SerializeField] TMP_Text Attack_TMP;
    // �� ���� ���� ī�尡 �������� �ֱ� ������ Card_Front �� Card_Back�� ������ش�.
    [SerializeField] Sprite Card_Front;
    [SerializeField] Sprite Card_Back;

    // ī�忡 ����� ������ �������� �˱����� public���� Item�� �����´�.
    public Item item;
    // ī�尡 �� ������ �� ������ Ȯ���ϱ� ���� ������ش�.
    bool isFront;
    // Ȯ�볪 ���, �巡�׸� �ϵ� �⺻��ġ�� ���ƿ;� �Ǳ� ������ ������ش�.
    public PRS Origin_PRS;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SetUp(Item item, bool isFront)
    {
        this.item = item;
        this.isFront = isFront;

        if (this.isFront)
        {
            // �� ���� ��� Character���� item sprite�� �־��ش�, �̸�, ���ݷ�, ü�� ��� item�� ������� �־��ش�.
            Character.sprite = this.item.sprite;
            Name_TMP.text = this.item.Name;
            Attack_TMP.text = this.item.Attack.ToString();
            Health_TMP.text = this.item.Health.ToString();
        }
        else
        {
            // �� ���� ��� sprite�� ī�� �޸��� �־��ְ�, �̸�, ���ݷ�, ü���� ����д�.
            card.sprite = Card_Back;
            Name_TMP.text = "";
            Attack_TMP.text = "";
            Health_TMP.text = "";
        }    
    }

    public void Move_Transform(PRS prs, bool Use_Dotween, float Dotween_Time = 0)
    {
        if(Use_Dotween)
        {
            // Use_Dotween�� true��� Dotween�� ����Ͽ� �ε巴�� �����δ�.
            transform.DOMove(prs.Pos, Dotween_Time);
            transform.DORotateQuaternion(prs.Rot, Dotween_Time);
            transform.DOScale(prs.Scale, Dotween_Time);
        }
        else
        {
            // Use_Dotween�� false��� Dotween�� ������� �ʰ� �Ϲ������� �����δ�.
            transform.position = prs.Pos;
            transform.rotation = prs.Rot;
            transform.localScale = prs.Scale;
        }
    }

    private void OnMouseOver()
    {
        // ���콺�� �� �ݶ��̴� ������ �ö� �ִ� ���¶�� Updateó�� ��� ȣ���Ѵ�.
        if(isFront)
        {
            // �ո��� ��� Card_MouseOver�� ȣ���Ѵ�.
            Card_Manager.Inst.Card_MouseOver(this);
        }
    }

    private void OnMouseExit()
    {
        // ���콺�� �� �ݶ��̴� ������ ���� ���¶�� �� �� ȣ���Ѵ�.
        if(isFront)
        {
            // �ո��� ��� Card_MouseExit�� ȣ���Ѵ�.
            Card_Manager.Inst.Card_MouseExit(this);
        }
    }

    private void OnMouseDown()
    {
        // ���콺�� ������ �ִ� ���¶�� ȣ���Ѵ�.
        if(isFront)
        {
            Card_Manager.Inst.Card_MouseDown();
        }
    }

    private void OnMouseUp()
    {
        // ���콺�� ���� �ִ� ���¶�� ȣ���Ѵ�.
        if (isFront)
        {
            Card_Manager.Inst.Card_MouseUp();
        }
    }
}
