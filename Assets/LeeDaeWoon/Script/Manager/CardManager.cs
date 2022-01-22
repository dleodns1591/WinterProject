using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CardManager : MonoBehaviour
{
    public static CardManager Inst { get; private set; }
    private void Awake() => Inst = this;

    [SerializeField] Item_SO item_SO;
    [SerializeField] GameObject Card_Prefab;
    [SerializeField] List<Card> MyCard;
    [SerializeField] List<Card> A_test;
    [SerializeField] Transform Card_Position;
    [SerializeField] Transform MyCard_Left;
    [SerializeField] Transform MyCard_Right;
    //[SerializeField] ECardState eCardState;

    List<Item> itemBuffer;
    Card selectCard;
    bool isMycardDrag;
    bool onMyCardArea;
    //enum ECardState { Nothing, CanMouseOver, CanMouseDrag }

    void Start()
    {
        Setup_ItemBuffer();
        TurnManager.OnAddCard += AddCard;
    }

    private void OnDestroy()
    {
        TurnManager.OnAddCard -= AddCard;
    }

    void Update()
    {
        if(isMycardDrag)
        { 
            CardDrag();
        }
        DetectCardArea();
        //SetECardState();
    }

    void Setup_ItemBuffer()
    {
        itemBuffer = new List<Item>(100);
        for (int i = 0; i < item_SO.items.Length; i++)
        {
            Item item = item_SO.items[i];
            for (int j = 0; j < item.percent; j++)
            {
                itemBuffer.Add(item);
            }
        }

        for (int i = 0; i < itemBuffer.Count; i++)
        {
            int rand = Random.Range(i, itemBuffer.Count);
            Item temp = itemBuffer[i];
            itemBuffer[i] = itemBuffer[rand];
            itemBuffer[rand] = temp;
        }
    }

    public Item PopItem()
    {
        if (itemBuffer.Count == 0)
        {
            Setup_ItemBuffer();
        }

        Item item = itemBuffer[0];
        itemBuffer.RemoveAt(0);
        return item;
    }

    private void AddCard(bool isMine)
    {
        var cardObject = Instantiate(Card_Prefab, Card_Position.position, Util.QI);
        var card = cardObject.GetComponent<Card>();
        card.Setup(PopItem(), isMine);
        (isMine ? MyCard : A_test).Add(card);

        SetOriginOrder(isMine);
        CardAlignment(isMine);
    }

    private void SetOriginOrder(bool isMine)
    {
        int count = isMine ? MyCard.Count : A_test.Count;
        for (int i = 0; i < count; i++)
        {
            var targetCard = isMine ? MyCard[i] : A_test[i];
            targetCard?.GetComponent<Order>().SetOrginOrder(i);
        }
    }

    private void CardAlignment(bool isMine)
    {
        List<PRS> originCardPRSs = new List<PRS>();
        if (isMine)
        {
            originCardPRSs = RoundAlignment(MyCard_Left, MyCard_Right, MyCard.Count, 0.5f, Vector3.one * 1.2f);
        }

        var TargetCard = isMine ? MyCard : A_test;
        for (int i = 0; i < TargetCard.Count; i++)
        {
            var targetCard = TargetCard[i];

            targetCard.originPRS = originCardPRSs[i];
            targetCard.MoveTransform(targetCard.originPRS, true, 0.7f);
        }
    }

    List<PRS> RoundAlignment(Transform leftTr, Transform rightTr, int objCount, float height, Vector3 scale)
    {
        float[] objLerps = new float[objCount];
        List<PRS> results = new List<PRS>(objCount);

        switch (objCount)
        {
            case 1: objLerps = new float[] { 0.5f }; break;
            case 2: objLerps = new float[] { 0.27f, 0.73f }; break;
            case 3: objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float interval = 1f / (objCount - 1);
                for (int i = 0; i < objCount; i++)
                    objLerps[i] = interval * i;
                break;
        }

        for(int i = 0; i < objCount; i++)
        {
            var targetPos = Vector3.Lerp(leftTr.position, rightTr.position, objLerps[i]);
            var targetRot = Util.QI;
            if(objCount >= 4)
            {
                float curve = Mathf.Sqrt(Mathf.Pow(height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2));
                curve = height >= 0 ? curve : -curve;
                targetPos.y += curve;
                targetRot = Quaternion.Slerp(leftTr.rotation, rightTr.rotation, objLerps[i]);
            }
            results.Add(new PRS(targetPos, targetRot, scale));
        }
        return results;
    }


    #region MyCard
    public void CardMouseOver(Card card)
    {
        //if (eCardState == ECardState.Nothing)
        //    return;

        selectCard = card;
        EnlargeCard(true, card);
    }

    public void CardMouseExit(Card card)
    {
        EnlargeCard(false, card);
    }

    public void CardMouseDown()
    {
        //if (eCardState != ECardState.CanMouseDrag)
        //    return;

        isMycardDrag = true;
    }

    public void CardMouseUp()
    {
        isMycardDrag = false;

        //if (eCardState != ECardState.CanMouseDrag)
        //    return;
    }

    private void CardDrag()
    {
        if(!onMyCardArea)
        {
            selectCard.MoveTransform(new PRS(Util.MousePos, Util.QI, selectCard.originPRS.scale), false);
        }
    }

    private void DetectCardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Util.MousePos, Vector3.forward);
        int layer = LayerMask.NameToLayer("MyCardArea");
        onMyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }

    private void EnlargeCard(bool isEnlarge, Card card)
    {
        if(isEnlarge)
        {
            Vector3 enlargePos = new Vector3(card.originPRS.pos.x, -1.2f, -10f);
            card.MoveTransform(new PRS(enlargePos, Util.QI, Vector3.one * 1.9f), false); //마우스 포인트가 카드 위에 있을 때 카드의 크기조절
        }
        else
        {
            card.MoveTransform(card.originPRS, false);
        }
        card.GetComponent<Order>().SetMostFrontOrder(isEnlarge);
    }

    //private void SetECardState()
    //{
    //    if(TurnManager.Inst.isLoading)
    //    {
    //        eCardState = ECardState.Nothing;
    //    }
    //    else if(!TurnManager.Inst.myTurn)
    //    {
    //        eCardState = ECardState.CanMouseOver;
    //    }
    //    else if(TurnManager.Inst.myTurn)
    //    {
    //        eCardState = ECardState.CanMouseDrag;
    //    }
    //}

    #endregion
}
