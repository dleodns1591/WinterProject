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
    // 적 같은 경우는 카드가 뒤집어져 있기 때문에 Card_Front 와 Card_Back을 만들어준다.
    [SerializeField] Sprite Card_Front;
    [SerializeField] Sprite Card_Back;

    // 카드에 담겨진 스탯이 무엇인지 알기위해 public으로 Item을 가져온다.
    public Item item;
    // 카드가 앞 면인지 뒷 면인지 확인하기 위해 만들어준다.
    bool isFront;
    // 확대나 축소, 드래그를 하든 기본위치로 돌아와야 되기 때문에 만들어준다.
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
            // 앞 면일 경우 Character에는 item sprite를 넣어준다, 이름, 공격력, 체력 모두 item에 정보대로 넣어준다.
            Character.sprite = this.item.sprite;
            Name_TMP.text = this.item.Name;
            Attack_TMP.text = this.item.Attack.ToString();
            Health_TMP.text = this.item.Health.ToString();
        }
        else
        {
            // 뒷 면일 경우 sprite는 카드 뒷면을 넣어주고, 이름, 공격력, 체력은 비워둔다.
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
            // Use_Dotween이 true라면 Dotween을 사용하여 부드럽게 움직인다.
            transform.DOMove(prs.Pos, Dotween_Time);
            transform.DORotateQuaternion(prs.Rot, Dotween_Time);
            transform.DOScale(prs.Scale, Dotween_Time);
        }
        else
        {
            // Use_Dotween이 false라면 Dotween을 사용하지 않고 일반적으로 움직인다.
            transform.position = prs.Pos;
            transform.rotation = prs.Rot;
            transform.localScale = prs.Scale;
        }
    }

    private void OnMouseOver()
    {
        // 마우스가 그 콜라이더 영역에 올라가 있는 상태라면 Update처럼 계속 호출한다.
        if(isFront)
        {
            // 앞면일 경우 Card_MouseOver을 호출한다.
            Card_Manager.Inst.Card_MouseOver(this);
        }
    }

    private void OnMouseExit()
    {
        // 마우스가 그 콜라이더 영역에 나온 상태라면 한 번 호출한다.
        if(isFront)
        {
            // 앞면일 경우 Card_MouseExit를 호출한다.
            Card_Manager.Inst.Card_MouseExit(this);
        }
    }

    private void OnMouseDown()
    {
        // 마우스로 누르고 있는 상태라면 호출한다.
        if(isFront)
        {
            Card_Manager.Inst.Card_MouseDown();
        }
    }

    private void OnMouseUp()
    {
        // 마우스를 때고 있는 상태라면 호출한다.
        if (isFront)
        {
            Card_Manager.Inst.Card_MouseUp();
        }
    }
}
