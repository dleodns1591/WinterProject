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

    public Item item; // 카드에 담겨진 아이템이 뭔지 가져온다.
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
        this.item = item; // item을 대입한다.
        this.isFront = isFront; // isFront를 대입한다.

        if(this.isFront) // 카드가 나올경우
        {
            Character.sprite = this.item.sprite; // Character에 item sprite를 넣어준다.
            Name.text = this.item.Name; // Name에 item에 썼던 이름을 넣어준다.
            Attack.text = this.item.Attack.ToString(); // Attack에 item에 썼던 공격력을 넣어준다.
            Defense.text = this.item.Defense.ToString(); // Defense에 item에 썼던 방어력을 넣어준다.
            Cost.text = this.item.Cost.ToString(); // Cost에 item에 썼던 코스트를 넣어준다.
            Ability.text = this.item.Ability; // Ability에 item에 썼던 능력을 넣어준다.
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

    private void OnMouseDown() // 누르고 있을 때
    {
        if(isFront)
        {
            Card_Manager.Inst.Card_MouseDown(); // Card_Manager에 있는 Card_MouseDown 함수를 호출 한다.
        }
    }

    private void OnMouseUp() // 때고 있을 때
    {
        if(isFront)
        {
            Card_Manager.Inst.Card_MouseUp(); // Card_Manager에 있는 Card_MouseUp 함수를 호출 한다.
        }
    }

    public void Move_Transform(PRS Prs, bool Use_Dotween, float Dotween_Time = 0) // prs로 이동을 하는데 Use_Dotween이 true면은 Dotween을 사용하여 부드럽게 움직이며, false면은 일반적으로 위치로 움직인다. Dotween_Time은 움직이는 시간이다.
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
