using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Card : MonoBehaviour
{
    [SerializeField] SpriteRenderer card;
    [SerializeField] SpriteRenderer character;
    [SerializeField] TMP_Text name_TMP;
    [SerializeField] TMP_Text Attack_TMP;
    [SerializeField] TMP_Text Cost_TMP;
    [SerializeField] Sprite cardFront;
    //[SerializeField] Sprite cardBack;

    public Item item;
    bool isFront;
    public PRS originPRS;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Setup(Item item, bool isFront)
    {
        this.item = item;
        this.isFront = isFront;

        if(this.isFront)
        {
            character.sprite = this.item.sprite;
            name_TMP.text = this.item.name;
            Attack_TMP.text = this.item.attack.ToString();
            Cost_TMP.text = this.item.Cost.ToString();
        }
        else
        {
            //card.sprite = cardBack;
            name_TMP.text = "";
            Attack_TMP.text = "";
            Cost_TMP.text = "";
        }
    }

    private void OnMouseOver()
    {
        if(isFront)
        {
            CardManager.Inst.CardMouseOver(this);
        }
    }

    private void OnMouseExit()
    {
        if (isFront)
        {
            CardManager.Inst.CardMouseExit(this);
        }
    }

    private void OnMouseDown()
    {
        if(isFront)
        {
            CardManager.Inst.CardMouseDown();
        }    
    }

    private void OnMouseUp()
    {
        if (isFront)
        {
            CardManager.Inst.CardMouseUp();
        }
    }

    public void MoveTransform(PRS prs, bool useDotween, float dotweenTime = 0)
    {
        if(useDotween)
        {
            transform.DOMove(prs.pos, dotweenTime);
            transform.DORotateQuaternion(prs.rot, dotweenTime);
            transform.DOScale(prs.scale, dotweenTime);
        }
        else
        {
            transform.position = prs.pos;
            transform.rotation = prs.rot;
            transform.localScale = prs.scale;
        }
    }
}
