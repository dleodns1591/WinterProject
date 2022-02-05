using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Card_Manager : MonoBehaviour
{
    // �̱������� ����� �ش�. �ֳ�? Manager�� �ϳ��̱� �����̴�.
    public static Card_Manager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] Item_So item_So; // Item_So ��ũ��Ʈ�� �������� ���Ͽ� private�� �����Ѵ�.  
    [SerializeField] GameObject Card_Prefab; // ī�带 ���������� ����� �������� �����ϱ� ���� ���ش�.
    [SerializeField] List<Card> My_Card; // ���� ī�带 �����ϱ� ���Ͽ� List�� ������ش�.
    [SerializeField] Transform Card_SpawnPoint; // ī���� �������� �����ش�.
    [SerializeField] Transform Card_End_SpawnPoint; // ī�带 ���� �������� �����ش�.
    [SerializeField] Transform MyCard_Left; //���� ī�� ������ ����
    [SerializeField] Transform MyCard_Right; // ���� ī�� ������ ������
    [SerializeField] ECard_State eCard_State;

    List<Item> itemBuffer;
    Card select_Card; // ���õ� ī�带 ��� ��
    bool isMyCard_Drag;
    bool onMyCard_Area;
    enum ECard_State { Nothing, CanMouse_Over, CanMouse_Drag } //{ ���콺�� �ø� �� ����. / ���콺�� �ø� �� �ִ�. / ���콺�� �巡�� �����ϴ�. }

    void Start()
    {
        Setup_ItemBuffer();
        TurnManager.OnAddCard += AddCard; // �߰�
        TurnManager.OffAddCard += ADDCARD; 
    }

    void Update()
    {
        if (isMyCard_Drag) // ���� isMyCard_Drag�� true��� Card_Drage�Լ��� ȣ���Ѵ�.
        {
            Card_Drag();
        }

        Detect_CardArea();
        Set_ECard_State();
    }

    private void OnDestroy()
    {
        TurnManager.OnAddCard -= AddCard; // ����
    }

    private void Setup_ItemBuffer()
    {
        itemBuffer = new List<Item>(100); // ���ο� List �����⸦ ������ش�.
        for (int i = 0; i < item_So.items.Length; i++) // Item_So��ũ��Ʈ�� ���� 8���� ����� �ִ� �迭�� items �̴�.
        {
            Item item = item_So.items[i];
            itemBuffer.Add(item);
        }

        for (int i = 0; i < itemBuffer.Count; i++) // �ߺ� �� ī�尡 �������� ������ �ȵǹǷ� ������ �����ش�.
        {
            int rand = Random.Range(i, itemBuffer.Count); // �ε����� �����Կ� ����
            Item temp = itemBuffer[i]; // �� �ε����� ������� ������ �ϰ�
            itemBuffer[i] = itemBuffer[rand]; // ������ �ϸ� ������ �ٲ��.
            itemBuffer[rand] = temp;
        }
    }

    // �ִ� ī�尡 8�����, 8���� ī�尡 ��� �������� �� �ٽ� Setup�ؼ� Buffer���ٰ� ä���ֽ��ϴ�.
    public Item PopItem() // �÷��̾ ī�带 ���� ������, 
    {
        if (itemBuffer.Count == 0)
        {
            Setup_ItemBuffer();
        }

        Item item = itemBuffer[0]; // �� �տ� �ִ� �ε����� �̾Ƽ�
        itemBuffer.RemoveAt(0); // RemoveAt(0)�� �ؼ�
        return item; // return item ���� �̾Ƴ���.
    }

    private void AddCard(bool isMine)
    {
        var Card_Object = Instantiate(Card_Prefab, Card_SpawnPoint.position, Utile.QI); // ī�� �������� Instantiate�� �ߴ�.
        var Card = Card_Object.GetComponent<Card>(); // Card_Object�� ������Ʈ �����̶� GetComponent�� ī����� �����´�.
        Card.Setup(PopItem(), isMine); // Card�� Setup�� �ϸ� PopItem�� ȣ���� �ȴ�. �׷��� ItemBuffer�� �ִ� ī���� �� ���� �̴´�.

        if (isMine == true)
        {
            My_Card.Add(Card); // isMine�� true��� My_Card�� Add�� �Ѵ�. �׷��� ������ ī�忡 ������ �Ѵ�.
        }

        Set_OriginOrder(isMine);
        Card_Ailgnment(isMine);
    }

    private void ADDCARD(bool isMine)
    {
        var Card_Object = Instantiate(Card_Prefab, Card_End_SpawnPoint.position, Utile.QI); // ī�� �������� Instantiate�� �ߴ�.
    }

    private void Set_OriginOrder(bool isMine) // Order�� �����ϴ� �Լ�
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

    private void Card_Ailgnment(bool isMine) //ī�� �����ϴ� �Լ�
    {
        List<PRS> originCard_PRS = new List<PRS>();
        if (isMine)
        {
            originCard_PRS = RoundAlignment(MyCard_Left, MyCard_Right, My_Card.Count, 0.5f, Vector3.one * 1f);
        }

        if (isMine == true)
        {
            var Target_Card = My_Card; // My_Card �� �����Ѵ�.
            for (int i = 0; i < Target_Card.Count; i++)
            {
                var target_Card = Target_Card[i]; // Target_Card[i] �� target_Card ��� �����.

                target_Card.originPRS = originCard_PRS[i]; // originPRS �� �⺻���� �־��ش�.
                target_Card.Move_Transform(target_Card.originPRS, true, 0.7f); // target_Card�� Move_Transform�� ���ؼ� orginPRS �����ְ�, Dotween �� ����Ұ��� Ȯ�ο��� true, Dotween_Time = 0.7f �̴�.          
            }
        }
    }

    List<PRS> RoundAlignment(Transform Left_Tr, Transform Right_Tr, int Obj_Count, float Height, Vector3 Scale)
    {
        float[] objLerps = new float[Obj_Count]; // float �迭 objLerps�� �����.
        List<PRS> results = new List<PRS>(Obj_Count); // List results�� ����� �������� results�� ��ȯ�Ѵ�.

        switch (Obj_Count)
        { //������
            case 1:
                objLerps = new float[] { 0.5f }; break; // ī�尡 �� �� �϶��� �߾�
            case 2:
                objLerps = new float[] { 0.27f, 0.73f }; break; // ī�尡 �� �� �϶��� ������� ������ ����
            case 3:
                objLerps = new float[] { 0.1f, 0.5f, 0.9f }; break; // ���������� ī�尡 �� �� �϶��� ������� ����Ʈ�� ���´�.
            default: // ī�尡 �������� ���
                float interval = 1f / (Obj_Count - 1);
                for (int i = 0; i < Obj_Count; i++)
                {
                    objLerps[i] = interval * i;
                }
                break;
        }

        //���� ������
        for (int i = 0; i < Obj_Count; i++)
        {
            var Target_Pos = Vector3.Lerp(Left_Tr.position, Right_Tr.position, objLerps[i]); // �� ���� Target_Pos�� objLerps�� 0 ~ 1 ������ ������ ���� �����Ƿ�, Left_Tr�� �����ǰ� Right_Tr�� �����ǿ� Lerp�� �ذ����� � ��ġ�� �����ϴ��� �˷��ش�. [ex) 0 = Left_Tr , 0.5 = �߰��� , 1 = Right_Tr  ]
            var Target_Rot = Utile.QI;
            if (Obj_Count >= 4) // ī�尡 4���̻��� ��� ȸ���� �ؾ��Ѵ�.
            {
                float curve = Mathf.Sqrt(Mathf.Pow(Height, 2) - Mathf.Pow(objLerps[i] - 0.5f, 2)); // Height�� �� �ϵ� ������ �ϱ� ������ curve�� ����̴�.
                curve = Height >= 0 ? curve : -curve;
                Target_Pos.y += curve;
                Target_Rot = Quaternion.Slerp(Left_Tr.rotation, Right_Tr.rotation, objLerps[i]);
            }
            results.Add(new PRS(Target_Pos, Target_Rot, Scale)); // ī�尡 4�� �̻� ���� ��� ���� ����� ���� �ʰ� �ٷ� �߰��Ѵ�. ������ ī�尡 3�� ���ʹ� ȸ���� �ʿ����� �ʱ� �����̴�.
        }
        return results;
    }

    #region MyCard

    // Card_MouseOver �� Card_MouseExit �� �� �Ű������� Card�� �޴´�.
    public void Card_MouseOver(Card card)
    {
        if(eCard_State == ECard_State.Nothing) // �ε��߿��� ī�带 ���� �� �����Ƿ� Nothing�� ���ش�.
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

    public void Card_MouseDown() // ī�带 ������ ���� �� �Լ��� ȣ�� �ȴ�.
    {
        if (eCard_State != ECard_State.CanMouse_Drag)
        {
            return;
        }

        isMyCard_Drag = true; // isMyCard_Drag�� true�� �Ѵ�,
    }

    public void Card_MouseUp() // ī�带 ���� ���� �� �Լ��� ȣ�� �ȴ�.
    {
        isMyCard_Drag = false; // isMyCard_Drag�� false�� �Ѵ�.

        if(eCard_State != ECard_State.CanMouse_Drag)
        {
            return;
        }
    }

    private void Card_Drag() // ī�带 �巡�� ���� �� �����Ѵ�.
    {
        if (!onMyCard_Area) // onMyCard_Area�� false��� �巡�׸� �ؼ� �ʵ忡 ī�尡 ���ִٸ� �����Ѵ�.
        {
            select_Card.Move_Transform(new PRS(Utile.MousePos, Utile.QI, select_Card.originPRS.Scale), false);
        }

        
    }

    private void Detect_CardArea()
    {
        RaycastHit2D[] hits = Physics2D.RaycastAll(Utile.MousePos, Vector3.forward); // Physics2D.RaycastAll�� �Ἥ ���콺�� �浹�� ��� RaycastHit�� �����´�.
        int layer = LayerMask.NameToLayer("MyCardArea");
        onMyCard_Area = Array.Exists(hits, x => x.collider.gameObject.layer == layer);
    }

    private void Enlarge_Card(bool isEnlarge, Card card) // ���콺�� ������ ������ �� ī�尡 Ȯ��Ǵ� �Լ�, isEnlarge �� true��� Ȯ�� false��� ��� �̴�.
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
        if (TurnManager.Inst.isLoading) // isLoading�� true�� ���� Nothing���� �س�����.
        {
            eCard_State = ECard_State.Nothing;
        }
        else if (!TurnManager.Inst.MyTurn) // �� ���� �ƴҰ�� CanMouse_Over ���콺�� �÷����� �� �ִ�.
        {
            eCard_State = ECard_State.CanMouse_Over;
        }
        else if(TurnManager.Inst.MyTurn) // �� ���� ��� CanMouse_Drag ���콺�� �巡�׸� �� �� �ִ�.
        {
            eCard_State = ECard_State.CanMouse_Drag;
        }
    }

    #endregion
}
