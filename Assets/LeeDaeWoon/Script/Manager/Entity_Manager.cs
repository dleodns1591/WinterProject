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
    // IsFull_MyEntity는 Entity가 꽉 찼는지 알려준다.
    // My_Entity.Count가 MaxEntity_Count 이상이고 또한 Exist_MyEmpty_Entity가 존재하지 않을 경우
    public bool IsFull_MyEntity => My_Entity.Count >= MaxEntity_Count && !Exist_MyEmpty_Entity;
    // IsFull_EnemyEntity는 Entity가 꽉 찼는지 알려준다.
    // Enemy_Entity같은 경우에는 카드를 끌어다 놓는 경우가 아니기 때문에 Enemy_Entity.Count가 MaxEntity_Count보다 이상일 경우만 확인해준다.
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

        // 공격로직
        Turn_Manager.Inst.End_Turn();
    }

    private void Entity_Alignment(bool isMine)
    {
        // Entity 정렬 해주는 함수
        // isMine 이면 Target_Y 가 -1.7 아니면은 1.7 위치를 넣어준다.
        // Target_Entity는 isMine 이면 My_Entity를 가져오고, 아니면은 Enemy_Entity를 가져온다.
        float Target_Y = isMine ? -1.7f : 1.7f;
        var Target_Entity = isMine ? My_Entity : Enemy_Entity;

        for(int i = 0; i< Target_Entity.Count; i++)
        {
            // Target_X는 가로로 정렬해준다.
            // Target_Entity.Count가 1개라면 X좌표는 0이 된다. 즉 중앙이다.
            float Target_X = (Target_Entity.Count - 1) * -1.5f + i * 3f;

            var target_Entity = Target_Entity[i];
            target_Entity.Origin_Pos = new Vector3(Target_X, Target_Y, 0);
            target_Entity.Move_Transform(target_Entity.Origin_Pos, true, 0.5f);
            target_Entity.GetComponent<Order>()?.Set_OriginOrder(i);
        }
    }

    public void Insert_MyEmptyEntity(float xPos)
    {
        // 이 함수는 Card_Manager에 있는 Card_Drag 함수에 호출해준다.
        // 이 함수는 마우스를 필드에 드래그를 했을 때 빈 GameObject가 없으면은 생성을 해주고 X좌표에 따라 리스트에 순서를 바꿔주는 역할을 한다.
        // IsFull_MyEntity가 true라면 return을 해준다. 이유는 필드에 놓을 카드가 꽉 찼기 때문에 더 이상 놓을 수 없어야 하기 때문에 return을 해준다.
        if(IsFull_MyEntity)
        {
            return;
        }

        // 만약에 나의 Entity가 존재하지 않는다면 My_Entity에 추가를 해준다.
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
        // 이 함수는 Card_Manager에 Card_MouseUp 함수에 호출해준다.
        // 만약 내 Entity가 존재하지 않는다면 return을 해준다.
        if(!Exist_MyEmpty_Entity)
        {
            return;
        }

        // MyEmptyEntity_Index를 빼준다.
        // 정렬이 될 수 있도록 Entity_Alignment를 true로 해준다.
        My_Entity.RemoveAt(MyEmptyEntity_Index);
        Entity_Alignment(true);
    }

    public bool Spawn_Entity(bool isMine, Item item, Vector3 Spawn_Pos)
    {
        // bool을 반환한 이유는 엔티티가 스폰을 성공을 했는지 못했는지 알고 싶기 때문이다.
        if(isMine)
        {
            // 만약 isMine일 경우 내 엔티티가 꽉 차있거나, 내 Empty_Entity가 없을 경우 return false를 해준다.
            if(IsFull_MyEntity || !Exist_MyEmpty_Entity)
            {
                return false;
            }
        }
        else
        {
            // 만약 isMine이 false일 때 적의 엔티티가 꽉 차있을 경우 return false를 해준다.
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
