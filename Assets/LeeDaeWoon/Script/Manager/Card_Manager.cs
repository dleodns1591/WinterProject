using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Card_Manager : MonoBehaviour
{
    // �̱������� ����� �ش�. ������ Manager�� �ϳ��� �����ϱ� �����̴�.
    public static Card_Manager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] Item_So item_So; // Item_So�� �������� ���Ͽ� private�� ������ �Ѵ�.
    [SerializeField] GameObject Card_Prefab; // Card �������� �����ϱ� ���Ͽ� ���ش�.
    // �÷��̾ �����ϴ� ī��� ���� �����ϴ� ī�带 ���� ���ֱ� ���Ͽ� ����Ʈ�� My_Card, Enemey_Card�� ������ش�.
    [SerializeField] List<Card> My_Card;
    [SerializeField] List<Card> Enemy_Card;
    [SerializeField] Transform Card_SpawnPoint; // ī���� ���� ��ġ
    [SerializeField] Transform My_CardLeft;
    [SerializeField] Transform My_CardRight;
    [SerializeField] Transform Enemy_CardLeft;
    [SerializeField] Transform Enemy_CardRight;
    [SerializeField] ECard_State eCard_State;

    List<Item> Item_Buffer;
    // ���õ� ī�带 ��´�.
    Card Select_Card;
    bool isMy_CardDrag;
    bool On_MyCardArea;
    // Nothing = ���콺�� �ø� �� ����. Can_MouseOver = ���콺�� �ø� �� �ִ�. Can_MouseDrag = ���콺�� �巡�� ���� �����ϴ�.
    enum ECard_State { Nothing, Can_MouseOver, Can_MouseDrag }

    void Start()
    {
        SetUp_ItemBuffer();
        Turn_Manager.OnAdd_Card += Add_Card;
    }

    void Update()
    {
        if(isMy_CardDrag)
        { 
            // isMy_CardDrag�� true��� Card_Drag �Լ��� ȣ���Ѵ�.
            Card_Drag();
        }

        Detect_CardArea();
        Set_ECardState();
    }

    void OnDestroy()
    {
        Turn_Manager.OnAdd_Card -= Add_Card;
    }

    private void SetUp_ItemBuffer()
    {
        Item_Buffer = new List<Item>(8);
        for (int i = 0; i < item_So.items.Length; i++)
        {
            Item item = item_So.items[i];
            Item_Buffer.Add(item);

        }

        // ī�尡 ���� �� �Ȱ��� ������ ������ �����Ѵ�.
        for (int i = 0; i < Item_Buffer.Count; i++)
        {
            int Rand = Random.Range(i, Item_Buffer.Count);
            Item Temp = Item_Buffer[i];
            Item_Buffer[i] = Item_Buffer[Rand];
            Item_Buffer[Rand] = Temp;
        }
    }

    public Item PopItem()
    {
        // �츮 �Ǵ� ���� ī�带 ���� �� ���� �� �տ� �ִ� index�� �̾Ƽ� return item�� �Ѵ�.
        // ���� ī�带 �� �̰� �� �̻� �������� ���ٸ�, SetUp_ItemBuffer�� ȣ����� �ٽ� ī�带 ä�� �ִ´�.
        if (Item_Buffer.Count == 0)
        {
            SetUp_ItemBuffer();
        }

        Item item = Item_Buffer[0];
        Item_Buffer.RemoveAt(0);
        return item;
    }

    private void Add_Card(bool isMine)
    {
        // ī�忡 ������ �ֱ� ������ SetUp�� PopItem�� ȣ���Ѵ�. �׸��� isMine���� �ڱ��ڽ����� ������ ���ش�. 
        var Card_Object = Instantiate(Card_Prefab, Card_SpawnPoint.position, Utill.QI);
        var Card = Card_Object.GetComponent<Card>();
        Card.SetUp(PopItem(), isMine);
        (isMine ? My_Card : Enemy_Card).Add(Card);

        Set_OriginOrder(isMine);
        Card_Alignment(isMine);
    }

    private void Set_OriginOrder(bool isMine)
    {
        // �� �Լ��� order�� ������ �ֱ� ���� �Լ�
        int Count = isMine ? My_Card.Count : Enemy_Card.Count;
        for (int i = 0; i < Count; i++)
        {
            var Target_Card = isMine ? My_Card[i] : Enemy_Card[i];
            Target_Card?.GetComponent<Order>().Set_OriginOrder(i);
        }
    }

    private void Card_Alignment(bool isMine)
    {
        List<PRS> Origin_CardPRS = new List<PRS>();
        if (isMine)
        {
            Origin_CardPRS = Round_Alignment(My_CardLeft, My_CardRight, My_Card.Count, 0.5f, Vector3.one * 0.8f);
        }
        else
        {
            Origin_CardPRS = Round_Alignment(Enemy_CardLeft, Enemy_CardRight, Enemy_Card.Count, -0.5f, Vector3.one * 0.8f);
        }


        // isMine�� true��� My_Card�� �����ϴ°Ű�, false��� Enemy_Card�� �����Ѵ�.
        var Target_Card = isMine ? My_Card : Enemy_Card;
        for (int i = 0; i < Target_Card.Count; i++)
        {
            var target_Card = Target_Card[i];
            target_Card.Origin_PRS = Origin_CardPRS[i];
            target_Card.Move_Transform(target_Card.Origin_PRS, true, 0.7f);
        }
    }

    List<PRS> Round_Alignment(Transform Left_Tr, Transform Right_Tr, int Obj_Count, float Height, Vector3 Scale)
    {
        float[] Obj_Lerps = new float[Obj_Count];
        List<PRS> Results = new List<PRS>(Obj_Count);

        // ī���� ����
        switch (Obj_Count)
        {
            // ī�尡 �� �� �϶��� �߾�, ��,�� ���� ���� ������� ī��� ī�� ���̰� ����� �ִ�.
            case 1:
                Obj_Lerps = new float[] { 0.5f }; break;
            case 2:
                Obj_Lerps = new float[] { 0.27f, 0.73f }; break;
            case 3:
                Obj_Lerps = new float[] { 0.1f, 0.5f, 0.9f }; break;
            default:
                float Interval = 1f / (Obj_Count - 1);
                for(int i = 0; i< Obj_Count; i++)
                {
                    Obj_Lerps[i] = Interval * i;
                }
                break;
        }

        // ���� ������
        for (int i = 0; i < Obj_Count; i++)
        {
            // Obj_Lerps�� 0 ���� 1 ���� ���� �����Ƿ� [ 0�̸� Left_Tr, 0.5�̸� ���� �߰���, 1�̸� Right_Tr ���� ���´�. ]
            var Target_Pos = Vector3.Lerp(Left_Tr.position, Right_Tr.position, Obj_Lerps[i]);
            var Target_Rot = Utill.QI;

            // ī���� ������ 4�� �̻��� �� ȸ���� �ؾ��Ѵ�.
            if(Obj_Count >= 4)
            {
                float Curve = Mathf.Sqrt(Mathf.Pow(Height, 2) - Mathf.Pow(Obj_Lerps[i] - 0.5f, 2));
                Curve = Height >= 0 ? Curve : -Curve;
                Target_Pos.y += Curve;
                Target_Rot = Quaternion.Slerp(Left_Tr.rotation, Right_Tr.rotation, Obj_Lerps[i]);
            }
            // ���� ī���� ������ 4�庸�� ���� ������ ��� ���� ����� ���� �ʰ� �ٷ� �߰��Ѵ�.
            Results.Add(new PRS(Target_Pos, Target_Rot, Scale));
            
        }
        return Results;

    }

    // ��� ī���� ���콺�� �ø��� ������ ������ Card_Manger���� �������ش�.
    #region My_Card

    public void Card_MouseOver(Card card)
    {
        // �ε����� ���� Nothing���� �صд�.
        if(eCard_State == ECard_State.Nothing)
        {
            return;
        }

        // ���콺�� �÷����� ī�尡 Select_Card�� �ȴ�.
        Select_Card = card;
        Enlarge_Card(true, card);
    }

    public void Card_MouseExit(Card card)
    {
        Enlarge_Card(false, card);
    }

    public void Card_MouseDown()
    {
        // Can_MousseDrag�� �ƴϸ��� return�� ���ش�.
        if(eCard_State != ECard_State.Can_MouseDrag)
        {
            return;
        }

        // ���콺�� ������ ���� �� 
        isMy_CardDrag = true;
    }

    public void Card_MouseUp()
    {
        // ���콺�� ���� ���� ��
        isMy_CardDrag = false;

        // Can_MouseDrag�� false��� return�� ���ش�.
        if(eCard_State != ECard_State.Can_MouseDrag)
        {
            return; 
        }

        if(On_MyCardArea)
        {
            Entity_Manager.Inst.Remove_MyEmpty_Entity();
        }
    }

    private void Card_Drag()
    {
        if(eCard_State != ECard_State.Can_MouseDrag)
        {
            return;
        }

        // ī�� �巡�� �ϰ� ���� �� 
        if(!On_MyCardArea)
        {
            // On_MyCardArea�� false��� �����Ѵ�.
            // �巡�׸� �ؼ� �ʵ忡 ī�尡 ���ִٸ� �� ��ġ�� �Ű��ش�.
            Select_Card.Move_Transform(new PRS(Utill.Mouse_Pos, Utill.QI, Select_Card.Origin_PRS.Scale), false);
            Entity_Manager.Inst.Insert_MyEmptyEntity(Utill.Mouse_Pos.x);
        }
    }

    private void Detect_CardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utill.Mouse_Pos, Vector3.forward);
        // Layer�� ���� MyCardArea Layer�� �־��ش�.
        int Layer = LayerMask.NameToLayer("MyCardArea");
        On_MyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == Layer);
    }

    private void Enlarge_Card(bool isEnlarge, Card card)
    {
        // ���콺�� ī������ �÷��������� Ȯ��Ǵ� �Լ�
        // isEnlarge�� true��� Ȯ���̰�, false��� ����̴�.    
        if(isEnlarge)
        {
            Vector3 Enlarge_Pos = new Vector3(card.Origin_PRS.Pos.x, -2f, -10f);
            card.Move_Transform(new PRS(Enlarge_Pos, Utill.QI, Vector3.one * 1f), false);
        }
        else
        {
            card.Move_Transform(card.Origin_PRS, false);
        }

        card.GetComponent<Order>().Set_MostFrontOrder(isEnlarge);
    }

    private void Set_ECardState()
    {
        if(Turn_Manager.Inst.isLoading)
        {
            // ������ ���۵����� �ʾ��� ��, �� isLoading�� true�� ���ȿ��� Nothing���� �صд�.
            eCard_State = ECard_State.Nothing;
        }

        else if(!Turn_Manager.Inst.My_Turn)
        {
            // �� ���� �ƴ϶�� ���콺�� �ø� �� �ִ�.
            eCard_State = ECard_State.Can_MouseOver;
        }

        else if(Turn_Manager.Inst.My_Turn)
        {
            // �� ���� ���ȿ��� ī�带 �巡�� �� �� �ִ�.
            eCard_State = ECard_State.Can_MouseDrag;
        }
    }

    #endregion
}
