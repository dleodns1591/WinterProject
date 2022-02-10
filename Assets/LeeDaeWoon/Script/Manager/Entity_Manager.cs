using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Manager : MonoBehaviour
{
    public static Entity_Manager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] GameObject Entity_Prefab;
    [SerializeField] List<Entity> My_Entity;
    [SerializeField] List<Entity> Enemy_Entity;
    [SerializeField] Entity MyEmpty_Entity;
    [SerializeField] Entity PlayerBoss_Entity;
    [SerializeField] Entity EnemyBoss_Entity;

    const int MaxEntity_Count = 4;
    // IsFull_MyEntity�� Entity�� �� á���� �˷��ش�.
    // My_Entity.Count�� MaxEntity_Count �̻��̰� ���� Exist_MyEmpty_Entity�� �������� ���� ���
    public bool IsFull_MyEntity => My_Entity.Count >= MaxEntity_Count && !Exist_MyEmpty_Entity;
    // IsFull_EnemyEntity�� Entity�� �� á���� �˷��ش�.
    // Enemy_Entity���� ��쿡�� ī�带 ����� ���� ��찡 �ƴϱ� ������ Enemy_Entity.Count�� MaxEntity_Count���� �̻��� ��츸 Ȯ�����ش�.
    bool IsFull_EnmeyEntity => Enemy_Entity.Count >= MaxEntity_Count;
    bool Exist_MyEmpty_Entity => My_Entity.Exists(x => x == MyEmpty_Entity);
    int MyEmptyEntity_Index => My_Entity.FindIndex(x => x == MyEmpty_Entity);

    WaitForSeconds delay_1 = new WaitForSeconds(1);

    void Start()
    {
        Turn_Manager.OnTurn_Start += OnTurn_Start;
    }

    void Update()
    {
        
    }

    void OnDestroy()
    {
        Turn_Manager.OnTurn_Start -= OnTurn_Start;
    }

    private void OnTurn_Start(bool My_Turn)
    {
        if(!My_Turn)
        {
            StartCoroutine(AI_Co());
        }
    }

    IEnumerator AI_Co()
    {
        Card_Manager.Inst.TryPut_Card(false);
        yield return delay_1;

        // ���ݷ���
        Turn_Manager.Inst.End_Turn();
    }

    private void Entity_Alignment(bool isMine)
    {
        // Entity ���� ���ִ� �Լ�
        // isMine �̸� Target_Y �� -1.7 �ƴϸ��� 1.7 ��ġ�� �־��ش�.
        // Target_Entity�� isMine �̸� My_Entity�� ��������, �ƴϸ��� Enemy_Entity�� �����´�.
        float Target_Y = isMine ? -1.7f : 1.7f;
        var Target_Entity = isMine ? My_Entity : Enemy_Entity;

        for(int i = 0; i< Target_Entity.Count; i++)
        {
            // Target_X�� ���η� �������ش�.
            // Target_Entity.Count�� 1����� X��ǥ�� 0�� �ȴ�. �� �߾��̴�.
            float Target_X = (Target_Entity.Count - 1) * -1.5f + i * 3f;

            var target_Entity = Target_Entity[i];
            target_Entity.Origin_Pos = new Vector3(Target_X, Target_Y, 0);
            target_Entity.Move_Transform(target_Entity.Origin_Pos, true, 0.5f);
            target_Entity.GetComponent<Order>()?.Set_OriginOrder(i);
        }
    }

    public void Insert_MyEmptyEntity(float xPos)
    {
        // �� �Լ��� Card_Manager�� �ִ� Card_Drag �Լ��� ȣ�����ش�.
        // �� �Լ��� ���콺�� �ʵ忡 �巡�׸� ���� �� �� GameObject�� �������� ������ ���ְ� X��ǥ�� ���� ����Ʈ�� ������ �ٲ��ִ� ������ �Ѵ�.
        // IsFull_MyEntity�� true��� return�� ���ش�. ������ �ʵ忡 ���� ī�尡 �� á�� ������ �� �̻� ���� �� ����� �ϱ� ������ return�� ���ش�.
        if(IsFull_MyEntity)
        {
            return;
        }

        // ���࿡ ���� Entity�� �������� �ʴ´ٸ� My_Entity�� �߰��� ���ش�.
        if(!Exist_MyEmpty_Entity)
        {
            My_Entity.Add(MyEmpty_Entity);
        }

        Vector3 Empty_EntityPos = MyEmpty_Entity.transform.position;
        Empty_EntityPos.x = xPos;
        MyEmpty_Entity.transform.position = Empty_EntityPos;

        int Empty_EntityIndex = MyEmptyEntity_Index;
        My_Entity.Sort((Entity_1, Entity_2) => Entity_1.transform.position.x.CompareTo(Entity_2.transform.position.x));
        
        if(MyEmptyEntity_Index != Empty_EntityIndex)
        {
            Entity_Alignment(true);
        }
    }

    public void Remove_MyEmpty_Entity()
    {
        // �� �Լ��� Card_Manager�� Card_MouseUp �Լ��� ȣ�����ش�.
        // ���� �� Entity�� �������� �ʴ´ٸ� return�� ���ش�.
        if(!Exist_MyEmpty_Entity)
        {
            return;
        }

        // MyEmptyEntity_Index�� ���ش�.
        // ������ �� �� �ֵ��� Entity_Alignment�� true�� ���ش�.
        My_Entity.RemoveAt(MyEmptyEntity_Index);
        Entity_Alignment(true);
    }

    public bool Spawn_Entity(bool isMine, Item item, Vector3 Spawn_Pos)
    {
        // bool�� ��ȯ�� ������ ��ƼƼ�� ������ ������ �ߴ��� ���ߴ��� �˰� �ͱ� �����̴�.
        if(isMine)
        {
            // ���� isMine�� ��� �� ��ƼƼ�� �� ���ְų�, �� Empty_Entity�� ���� ��� return false�� ���ش�.
            if(IsFull_MyEntity || !Exist_MyEmpty_Entity)
            {
                return false;
            }
        }
        else
        {
            // ���� isMine�� false�� �� ���� ��ƼƼ�� �� ������ ��� return false�� ���ش�.
            if(IsFull_EnmeyEntity)
            {
                return false;
            }
        }

        var Entity_Object = Instantiate(Entity_Prefab, Spawn_Pos, Utill.QI);
        var Entity = Entity_Object.GetComponent<Entity>();

        if(isMine)
        {
            My_Entity[MyEmptyEntity_Index] = Entity;
        }
        else
        {
            Enemy_Entity.Insert(Random.Range(0, Enemy_Entity.Count), Entity);
        }

        Entity.isMine = isMine;
        Entity.Set_Up(item);
        Entity_Alignment(isMine);

        return true;
    }
}
