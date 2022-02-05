using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Card_Manager : MonoBehaviour
{
    // 싱글톤으로 만들어 준다. 왜냐? Manager는 하나이기 때문이다.
    public static Card_Manager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] Item_So item_So; // Item_So 스크립트를 가져오기 위하여 private로 선언한다.  
    [SerializeField] GameObject Card_Prefab; // 카드를 프리팹으로 만들어 놓은것을 참조하기 위해 써준다.
    [SerializeField] List<Card> My_Card; // 나의 카드를 관리하기 위하여 List로 만들어준다.
    [SerializeField] Transform Card_SpawnPoint; // 카드의 포지션을 정해준다.
    [SerializeField] Transform Card_End_SpawnPoint; // 카드를 버릴 포지션을 정해준다.
    [SerializeField] Transform MyCard_Left; //나의 카드 포지션 왼쪽
    [SerializeField] Transform MyCard_Right; // 나의 카드 포지션 오른쪽
    [SerializeField] ECard_State eCard_State;

    List<Item> itemBuffer;
    Card select_Card; // 선택된 카드를 담는 것
    bool isMyCard_Drag;
    bool onMyCard_Area;
    enum ECard_State { Nothing, CanMouse_Over, CanMouse_Drag } //{ 마우스도 올릴 수 없다. / 마우스만 올릴 수 있다. / 마우스로 드래그 가능하다. }

    void Start()
    {
        Setup_ItemBuffer();
        TurnManager.OnAddCard += AddCard; // 추가
        TurnManager.OffAddCard += ADDCARD; 
    }

    void Update()
    {
        if (isMyCard_Drag) // 만약 isMyCard_Drag가 true라면 Card_Drage함수를 호출한다.
        {
            Card_Drag();
        }

        Detect_CardArea();
        Set_ECard_State();
    }

    private void OnDestroy()
    {
        TurnManager.OnAddCard -= AddCard; // 제거
    }

    private void Setup_ItemBuffer()
    {
        itemBuffer = new List<Item>(100); // 새로운 List 껍데기를 만들어준다.
        for (int i = 0; i < item_So.items.Length; i++) // Item_So스크립트에 현재 8개가 담겨져 있는 배열이 items 이다.
        {
            Item item = item_So.items[i];
            itemBuffer.Add(item);
        }

        for (int i = 0; i < itemBuffer.Count; i++) // 중복 된 카드가 여러장이 나오면 안되므로 랜덤을 돌려준다.
        {
            int rand = Random.Range(i, itemBuffer.Count); // 인덱스가 증가함에 따라
            Item temp = itemBuffer[i]; // 그 인덱스를 마음대로 선택을 하고
            itemBuffer[i] = itemBuffer[rand]; // 대입을 하면 순서가 바뀐다.
            itemBuffer[rand] = temp;
        }
    }

    // 최대 카드가 8개라면, 8개의 카드가 모두 소진됬을 시 다시 Setup해서 Buffer에다가 채워넣습니다.
    public Item PopItem() // 플레이어가 카드를 뽑을 때마다, 
    {
        if (itemBuffer.Count == 0)
        {
            Setup_ItemBuffer();
        }

        Item item = itemBuffer[0]; // 맨 앞에 있는 인덱스를 뽑아서
        itemBuffer.RemoveAt(0); // RemoveAt(0)을 해서
        return item; // return item 으로 뽑아낸다.
    }

    private void AddCard(bool isMine)
    {
        var Card_Object = Instantiate(Card_Prefab, Card_SpawnPoint.position, Utile.QI); // 카드 프리팹을 Instantiate를 했다.
        var Card = Card_Object.GetComponent<Card>(); // Card_Object가 오브젝트 형식이라서 GetComponent로 카드까지 가져온다.
        Card.Setup(PopItem(), isMine); // Card에 Setup을 하면 PopItem이 호출이 된다. 그러면 ItemBuffer에 있는 카드중 한 개를 뽑는다.

        if (isMine == true)
        {
            My_Card.Add(Card); // isMine이 true라면 My_Card에 Add를 한다. 그래서 생성된 카드에 대입을 한다.
        }

        Set_OriginOrder(isMine);
        Card_Ailgnment(isMine);
    }

    private void ADDCARD(bool isMine)
    {
        var Card_Object = Instantiate(Card_Prefab, Card_End_SpawnPoint.position, Utile.QI); // 카드 프리팹을 Instantiate를 했다.
    }

    private void Set_OriginOrder(bool isMine) // Order을 정렬하는 함수
    {
        if (isMine == true)
        {
            int count = My_Card.Count;

            for (int i = 0; i < count; i++)
            {
                var Target_Card = My_Card[i];
                Target_Card?.GetComponent<Order>().Set_OriginOrder(i);
            }
        }
    }

    private void Card_Ailgnment(bool isMine) //카드 정렬하는 함수
    {
        List<PRS> originCard_PRS = new List<PRS>();
        if (isMine)
        {
            originCard_PRS = RoundAlignment(MyCard_Left, MyCard_Right, My_Card.Count, 0.5f, Vector3.one * 1f);
        }

        if (isMine == true)
        {
            var Target_Card = My_Card; // My_Card 를 정렬한다.
            for (int i = 0; i < Target_Card.Count; i++)
            {
                var target_Card = Target_Card[i]; // Target_Card[i] 를 target_Card 라고 만든다.

                target_Card.originPRS = originCard_PRS[i]; // originPRS 에 기본값을 넣어준다.
                target_Card.Move_Transform(target_Card.originPRS, true, 0.7f); // target_Card에 Move_Transform을 통해서 orginPRS 넘져주고, Dotween 을 사용할거지 확인여부 true, Dotween_Time = 0.7f 이다.          
            }
        }
    }

    List<PRS> RoundAlignment(Transform Left_Tr, Transform Right_Tr, int Obj_Count, float Height, Vector3 Scale)
    {
        float[] objLerps = new float[Obj_Count]; // float 배열 objLerps를 만든다.
        List<PRS> results = new List<PRS>(Obj_Count); // List results를 만들어 마지막에 results를 반환한다.

        switch (Obj_Count)
        { //고정값
            case 1:
                objLerps = new float[] { 0.5f }; break; // 카드가 한 장 일때는 중앙
            case 2:
                objLerps = new float[] { 0.27f, 0.73f }; break; // 카드가 두 장 일때는 어느정도 떨어져 있음
            case 3:
                objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break; // 마찬가지로 카드가 세 장 일때는 어느정도 떨어트려 놓는다.
            default: // 카드가 여러장일 경우
                float interval = 1f / (Obj_Count - 1);
                for (int i = 0; i < Obj_Count; i++)
                {
                    objLerps[i] = interval * i;
                }
                break;
        }

        //원의 방정식
        for (int i = 0; i < Obj_Count; i++)
        {
            var Target_Pos = Vector3.Lerp(Left_Tr.position, Right_Tr.position, objLerps[i]); // 각 각의 Target_Pos는 objLerps가 0 ~ 1 사이의 범위의 값을 가지므로, Left_Tr의 포지션과 Right_Tr의 포지션에 Lerp를 해가지고 어떤 위치에 존재하는지 알려준다. [ex) 0 = Left_Tr , 0.5 = 중간값 , 1 = Right_Tr  ]
            var Target_Rot = Utile.QI;
            if (Obj_Count >= 4) // 카드가 4장이상일 경우 회전을 해야한다.
            {
                float curve = Mathf.Sqrt(Mathf.Pow(Height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2)); // Height에 뭘 하든 제곱을 하기 때문에 curve는 양수이다.
                curve = Height >= 0 ? curve : -curve;
                Target_Pos.y += curve;
                Target_Rot = Quaternion.Slerp(Left_Tr.rotation, Right_Tr.rotation, objLerps[i]);
            }
            results.Add(new PRS(Target_Pos, Target_Rot, Scale)); // 카드가 4장 이상 작을 경우 위에 계산을 하지 않고 바로 추가한다. 이유는 카드가 3장 부터는 회전이 필요하지 않기 때문이다.
        }
        return results;
    }

    #region MyCard

    // Card_MouseOver 와 Card_MouseExit 둘 다 매개변수로 Card를 받는다.
    public void Card_MouseOver(Card card)
    {
        if(eCard_State == ECard_State.Nothing) // 로딩중에는 카드를 만질 수 없으므로 Nothing을 해준다.
        {
            return;
        }    

        select_Card = card;
        Enlarge_Card(true, card);
    }

    public void Card_MouseExit(Card card)
    {
        Enlarge_Card(false, card);
    }

    public void Card_MouseDown() // 카드를 누르는 순간 이 함수가 호출 된다.
    {
        if (eCard_State != ECard_State.CanMouse_Drag)
        {
            return;
        }

        isMyCard_Drag = true; // isMyCard_Drag를 true로 한다,
    }

    public void Card_MouseUp() // 카드를 때는 순간 이 함수가 호출 된다.
    {
        isMyCard_Drag = false; // isMyCard_Drag를 false로 한다.

        if(eCard_State != ECard_State.CanMouse_Drag)
        {
            return;
        }
    }

    private void Card_Drag() // 카드를 드래그 중일 때 실행한다.
    {
        if (!onMyCard_Area) // onMyCard_Area가 false라면 드래그를 해서 필드에 카드가 가있다면 실행한다.
        {
            select_Card.Move_Transform(new PRS(Utile.MousePos, Utile.QI, select_Card.originPRS.Scale), false);
        }

        
    }

    private void Detect_CardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utile.MousePos, Vector3.forward); // Physics2D.RaycastAll을 써서 마우스와 충돌한 모든 RaycastHit를 가져온다.
        int layer = LayerMask.NameToLayer("MyCardArea");
        onMyCard_Area = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }

    private void Enlarge_Card(bool isEnlarge, Card card) // 마우스를 가져다 놓았을 때 카드가 확대되는 함수, isEnlarge 가 true라면 확대 false라면 축소 이다.
    {
        if (isEnlarge)
        {
            Vector3 enlarge_Pos = new Vector3(card.originPRS.pos.x, -1.5f, -10f);
            card.Move_Transform(new PRS(enlarge_Pos, Utile.QI, Vector3.one * 1.2f), false);
        }
        else
        {
            card.Move_Transform(card.originPRS, false);
        }

        card.GetComponent<Order>().Set_MostFrontOrder(isEnlarge);
    }

    private void Set_ECard_State()
    {
        if (TurnManager.Inst.isLoading) // isLoading이 true일 때는 Nothing으로 해놓은다.
        {
            eCard_State = ECard_State.Nothing;
        }
        else if (!TurnManager.Inst.MyTurn) // 내 턴이 아닐경우 CanMouse_Over 마우스만 올려놓을 수 있다.
        {
            eCard_State = ECard_State.CanMouse_Over;
        }
        else if(TurnManager.Inst.MyTurn) // 내 턴일 경우 CanMouse_Drag 마우스로 드래그를 할 수 있다.
        {
            eCard_State = ECard_State.CanMouse_Drag;
        }
    }

    #endregion
}
