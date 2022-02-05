using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer Character;
    [SerializeField] TMP_Text Name;
    [SerializeField] TMP_Text Attack;
    [SerializeField] TMP_Text Defense;
    [SerializeField] TMP_Text Ability;
    [SerializeField] TMP_Text Cost;
    [SerializeField] Sprite Card_Front;

    public Item item; // ī�忡 ����� �������� ���� �����´�.
    bool isFront;
    public PRS originPRS;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void Awake()
    {

    }

    public void Setup(Item item, bool isFront)
    {
        this.item = item; // item�� �����Ѵ�.
        this.isFront = isFront; // isFront�� �����Ѵ�.

        if(this.isFront) // ī�尡 ���ð��
        {
            Character.sprite = this.item.sprite; // Character�� item sprite�� �־��ش�.
            Name.text = this.item.Name; // Name�� item�� ��� �̸��� �־��ش�.
            Attack.text = this.item.Attack.ToString(); // Attack�� item�� ��� ���ݷ��� �־��ش�.
            Defense.text = this.item.Defense.ToString(); // Defense�� item�� ��� ������ �־��ش�.
            Cost.text = this.item.Cost.ToString(); // Cost�� item�� ��� �ڽ�Ʈ�� �־��ش�.
            Ability.text = this.item.Ability; // Ability�� item�� ��� �ɷ��� �־��ش�.
        }
    }

    private void OnMouseOver()
    {
        if(isFront)
        {
            Card_Manager.Inst.Card_MouseOver(this);
        }    
    }

    private void OnMouseExit()
    {
        if(isFront)
        {
            Card_Manager.Inst.Card_MouseExit(this); 
        }
    }

    private void OnMouseDown() // ������ ���� ��
    {
        if(isFront)
        {
            Card_Manager.Inst.Card_MouseDown(); // Card_Manager�� �ִ� Card_MouseDown �Լ��� ȣ�� �Ѵ�.
        }
    }

    private void OnMouseUp() // ���� ���� ��
    {
        if(isFront)
        {
            Card_Manager.Inst.Card_MouseUp(); // Card_Manager�� �ִ� Card_MouseUp �Լ��� ȣ�� �Ѵ�.
        }
    }

    public void Move_Transform(PRS Prs, bool Use_Dotween, float Dotween_Time = 0) // prs�� �̵��� �ϴµ� Use_Dotween�� true���� Dotween�� ����Ͽ� �ε巴�� �����̸�, false���� �Ϲ������� ��ġ�� �����δ�. Dotween_Time�� �����̴� �ð��̴�.
    {
        if(Use_Dotween)
        {
            transform.DOMove(Prs.pos, Dotween_Time);
            transform.DORotateQuaternion(Prs.rot, Dotween_Time);
            transform.DOScale(Prs.Scale, Dotween_Time);
        }
        else
        {
            transform.position = Prs.pos;
            transform.rotation = Prs.rot;
            transform.localScale = Prs.Scale;
        }
    }

    //public void End_Move_Transform(PRS Prs, bool Use_Dotween, float Dotween_Time = 0)
    //{
    //    if (Use_Dotween)
    //    {
    //        transform.DOMove(Prs.pos, Dotween_Time);
    //        transform.DORotateQuaternion(Prs.rot, Dotween_Time);
    //        transform.DOScale(Prs.Scale, Dotween_Time);
    //    }
    //    else
    //    {
    //        transform.position = Prs.pos;
    //        transform.rotation = Prs.rot;
    //        transform.localScale = Prs.Scale;
    //    }
    //}
}
