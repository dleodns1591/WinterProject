using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Card_Manager : MonoBehaviour
{
    // 싱글톤으로 만들어 준다. 이유는 Manager는 하나만 존재하기 때문이다.
    public static Card_Manager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] Item_So item_So; // Item_So를 가져오기 위하여 private로 선언을 한다.
    [SerializeField] GameObject Card_Prefab; // Card 프리팹을 참조하기 위하여 써준다.
    // 플레이어가 생성하는 카드와 적이 생성하는 카드를 관리 해주기 위하여 리스트로 My_Card, Enemey_Card를 만들어준다.
    [SerializeField] List<Card> My_Card;
    [SerializeField] List<Card> Enemy_Card;
    [SerializeField] Transform Card_SpawnPoint; // 카드의 생성 위치
    [SerializeField] Transform My_CardLeft;
    [SerializeField] Transform My_CardRight;
    [SerializeField] Transform Enemy_CardLeft;
    [SerializeField] Transform Enemy_CardRight;
    [SerializeField] ECard_State eCard_State;

    List<Item> Item_Buffer;
    // 선택된 카드를 담는다.
    Card Select_Card;
    bool isMy_CardDrag;
    bool On_MyCardArea;
    // Nothing = 마우스도 올릴 수 없다. Can_MouseOver = 마우스만 올릴 수 있다. Can_MouseDrag = 마우스로 드래그 까지 가능하다.
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
            // isMy_CardDrag가 true라면 Card_Drag 함수를 호출한다.
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

        // 카드가 나올 때 똑같이 나오는 순서를 방지한다.
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
        // 우리 또는 적이 카드를 뽑을 때 마다 맨 앞에 있는 index를 뽑아서 return item을 한다.
        // 이제 카드를 다 뽑고 더 이상 뽑을것이 없다면, SetUp_ItemBuffer을 호출시켜 다시 카드를 채워 넣는다.
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
        // 카드에 스탯을 넣기 때문에 SetUp에 PopItem을 호출한다. 그리고 isMine으로 자기자신인지 전달을 해준다. 
        var Card_Object = Instantiate(Card_Prefab, Card_SpawnPoint.position, Utill.QI);
        var Card = Card_Object.GetComponent<Card>();
        Card.SetUp(PopItem(), isMine);
        (isMine ? My_Card : Enemy_Card).Add(Card);

        Set_OriginOrder(isMine);
        Card_Alignment(isMine);
    }

    private void Set_OriginOrder(bool isMine)
    {
        // 이 함수는 order을 정렬해 주기 위한 함수
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


        // isMine이 true라면 My_Card를 정렬하는거고, false라면 Enemy_Card를 정렬한다.
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

        // 카드의 간격
        switch (Obj_Count)
        {
            // 카드가 한 장 일때는 중앙, 두,세 장일 때는 어느정도 카드와 카드 사이가 띄어져 있다.
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

        // 원의 방정식
        for (int i = 0; i < Obj_Count; i++)
        {
            // Obj_Lerps는 0 에서 1 사의 값을 가지므로 [ 0이면 Left_Tr, 0.5이면 둘의 중간값, 1이면 Right_Tr 값이 나온다. ]
            var Target_Pos = Vector3.Lerp(Left_Tr.position, Right_Tr.position, Obj_Lerps[i]);
            var Target_Rot = Utill.QI;

            // 카드의 갯수가 4장 이상일 때 회전을 해야한다.
            if(Obj_Count >= 4)
            {
                float Curve = Mathf.Sqrt(Mathf.Pow(Height, 2) - Mathf.Pow(Obj_Lerps[i] - 0.5f, 2));
                Curve = Height >= 0 ? Curve : -Curve;
                Target_Pos.y += Curve;
                Target_Rot = Quaternion.Slerp(Left_Tr.rotation, Right_Tr.rotation, Obj_Lerps[i]);
            }
            // 만약 카드의 갯수가 4장보다 작은 숫자일 경우 위에 계산을 하지 않고 바로 추가한다.
            Results.Add(new PRS(Target_Pos, Target_Rot, Scale));
            
        }
        return Results;

    }

    // 모든 카드의 마우스로 올리고 내리고 정보를 Card_Manger에게 전달해준다.
    #region My_Card

    public void Card_MouseOver(Card card)
    {
        // 로딩중일 때는 Nothing으로 해둔다.
        if(eCard_State == ECard_State.Nothing)
        {
            return;
        }

        // 마우스를 올려놓은 카드가 Select_Card가 된다.
        Select_Card = card;
        Enlarge_Card(true, card);
    }

    public void Card_MouseExit(Card card)
    {
        Enlarge_Card(false, card);
    }

    public void Card_MouseDown()
    {
        // Can_MousseDrag가 아니면은 return을 해준다.
        if(eCard_State != ECard_State.Can_MouseDrag)
        {
            return;
        }

        // 마우스를 누르고 있을 떄 
        isMy_CardDrag = true;
    }

    public void Card_MouseUp()
    {
        // 마우스를 때고 있을 때
        isMy_CardDrag = false;

        // Can_MouseDrag가 false라면 return을 해준다.
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

        // 카드 드래그 하고 있을 때 
        if(!On_MyCardArea)
        {
            // On_MyCardArea가 false라면 실행한다.
            // 드래그를 해서 필드에 카드가 가있다면 그 위치를 옮겨준다.
            Select_Card.Move_Transform(new PRS(Utill.Mouse_Pos, Utill.QI, Select_Card.Origin_PRS.Scale), false);
            Entity_Manager.Inst.Insert_MyEmptyEntity(Utill.Mouse_Pos.x);
        }
    }

    private void Detect_CardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utill.Mouse_Pos, Vector3.forward);
        // Layer에 만든 MyCardArea Layer를 넣어준다.
        int Layer = LayerMask.NameToLayer("MyCardArea");
        On_MyCardArea = Array.Exists(hits, x => x.collider.gameObject.layer == Layer);
    }

    private void Enlarge_Card(bool isEnlarge, Card card)
    {
        // 마우스를 카드위에 올려놓았을때 확대되는 함수
        // isEnlarge가 true라면 확대이고, false라면 축소이다.    
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
            // 게임이 시작되지도 않았을 때, 즉 isLoading이 true일 동안에는 Nothing으로 해둔다.
            eCard_State = ECard_State.Nothing;
        }

        else if(!Turn_Manager.Inst.My_Turn)
        {
            // 내 턴이 아니라면 마우스만 올릴 수 있다.
            eCard_State = ECard_State.Can_MouseOver;
        }

        else if(Turn_Manager.Inst.My_Turn)
        {
            // 내 턴일 동안에는 카드를 드래그 할 수 있다.
            eCard_State = ECard_State.Can_MouseDrag;
        }
    }

    #endregion
}
