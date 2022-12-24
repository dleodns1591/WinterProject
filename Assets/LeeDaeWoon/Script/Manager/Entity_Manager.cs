using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Entity_Manager : MonoBehaviour
{
    public static Entity_Manager instance { get; private set; }
    void Awake() => instance = this;

    [SerializeField] GameObject Entity_Prefab;
    [SerializeField] GameObject Damage_Prefab;
    [SerializeField] List<Entity> My_Entity;
    [SerializeField] List<Entity> Enemy_Entity;
    [SerializeField] Entity MyEmpty_Entity;
    [SerializeField] Entity MyBoss_Entity;
    [SerializeField] Entity EnemyBoss_Entity;
    [SerializeField] GameObject Aim;

    const int MaxEntity_Count = 4;
    // IsFull_MyEntity는 Entity가 꽉 찼는지 알려준다.
    // My_Entity.Count가 MaxEntity_Count 이상이고 또한 Exist_MyEmpty_Entity가 존재하지 않을 경우
    public bool IsFull_MyEntity => My_Entity.Count >= MaxEntity_Count && !Exist_MyEmpty_Entity;
    // IsFull_EnemyEntity는 Entity가 꽉 찼는지 알려준다.
    // Enemy_Entity같은 경우에는 카드를 끌어다 놓는 경우가 아니기 때문에 Enemy_Entity.Count가 MaxEntity_Count보다 이상일 경우만 확인해준다.
    bool IsFull_EnmeyEntity => Enemy_Entity.Count >= MaxEntity_Count;
    bool Exist_MyEmpty_Entity => My_Entity.Exists(x => x == MyEmpty_Entity);
    int MyEmptyEntity_Index => My_Entity.FindIndex(x => x == MyEmpty_Entity);
    // CanMouse_Input은 내 턴이면서 isLoading이 false일 경우 마우스의 입력을 받을 수 있다. 라고 만들어준다.
    bool CanMouse_Input => Turn_Manager.instance.My_Turn && !Turn_Manager.instance.isLoading;
    // Aim이 null이 아닐 경우 Exist_Aim이라고 한다.
    bool Exist_Aim => Target_PickEntity != null;

    // 공격할 것을 선택하는 Entity
    Entity Select_Entity;
    // 마우스로 끌어다가 대상을 선택하게 되는것이 Target_PickEntity 이다.
    Entity Target_PickEntity;
    WaitForSeconds delay_1 = new WaitForSeconds(1);
    WaitForSeconds delay_2 = new WaitForSeconds(2);

    void Start()
    {
        Turn_Manager.OnTurn_Start += OnTurn_Start;
    }

    void Update()
    {
        Show_Aim(Exist_Aim);
    }

    void OnDestroy()
    {
        Turn_Manager.OnTurn_Start -= OnTurn_Start;
    }

    private void OnTurn_Start(bool My_Turn)
    {
        Attack_AbleReset(My_Turn);

        if (!My_Turn)
            StartCoroutine(AI_Co());
    }

    IEnumerator AI_Co()
    {
        Card_Manager.instnace.TryPut_Card(false);
        yield return delay_1;

        // Attack_Able이 true인 모든 Enemy_Entity를 가져와 순서를 섞는다.
        var Attackers = new List<Entity>(Enemy_Entity.FindAll(x => x.Attack_Able == true));
        for (int i = 0; i < Attackers.Count; i++)
        {
            int rand = Random.Range(i, Attackers.Count);
            Entity Temp = Attackers[i];
            Attackers[i] = Attackers[rand];
            Attackers[rand] = Temp;
        }

        // 보스를 포함한 My_Entity를 랜덤하게 시간차 공격한다.
        foreach (var Attacker in Attackers)
        {
            var Defend = new List<Entity>(My_Entity);
            Defend.Add(MyBoss_Entity);
            int rand = Random.Range(0, Defend.Count);
            Attack(Attacker, Defend[rand]);

            if (Turn_Manager.instance.isLoading)
                yield break;

            yield return delay_2;
        }

        Turn_Manager.instance.End_Turn();
    }

    private void Entity_Alignment(bool isMine)
    {
        // Entity 정렬 해주는 함수
        // isMine 이면 Target_Y 가 -1.7 아니면은 1.7 위치를 넣어준다.
        // Target_Entity는 isMine 이면 My_Entity를 가져오고, 아니면은 Enemy_Entity를 가져온다.
        float Target_Y = isMine ? -1.7f : 1.7f;
        var Target_Entity = isMine ? My_Entity : Enemy_Entity;

        for (int i = 0; i < Target_Entity.Count; i++)
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
        if (IsFull_MyEntity)
            return;

        // 만약에 나의 Entity가 존재하지 않는다면 My_Entity에 추가를 해준다.
        if (!Exist_MyEmpty_Entity)
            My_Entity.Add(MyEmpty_Entity);

        Vector3 Empty_EntityPos = MyEmpty_Entity.transform.position;
        Empty_EntityPos.x = xPos;
        MyEmpty_Entity.transform.position = Empty_EntityPos;

        int Empty_EntityIndex = MyEmptyEntity_Index;
        My_Entity.Sort((Entity_1, Entity_2) => Entity_1.transform.position.x.CompareTo(Entity_2.transform.position.x));

        if (MyEmptyEntity_Index != Empty_EntityIndex)
            Entity_Alignment(true);
    }

    public void Remove_MyEmpty_Entity()
    {
        // 이 함수는 Card_Manager에 Card_MouseUp 함수에 호출해준다.
        // 만약 내 Entity가 존재하지 않는다면 return을 해준다.
        if (!Exist_MyEmpty_Entity)
            return;

        // MyEmptyEntity_Index를 빼준다.
        // 정렬이 될 수 있도록 Entity_Alignment를 true로 해준다.
        My_Entity.RemoveAt(MyEmptyEntity_Index);
        Entity_Alignment(true);
    }

    public bool Spawn_Entity(bool isMine, Item item, Vector3 Spawn_Pos)
    {
        // bool을 반환한 이유는 엔티티가 스폰을 성공을 했는지 못했는지 알고 싶기 때문이다.
        if (isMine)
        {
            // 만약 isMine일 경우 내 엔티티가 꽉 차있거나, 내 Empty_Entity가 없을 경우 return false를 해준다.
            if (IsFull_MyEntity || !Exist_MyEmpty_Entity)
                return false;
        }
        else
        {
            // 만약 isMine이 false일 때 적의 엔티티가 꽉 차있을 경우 return false를 해준다.
            if (IsFull_EnmeyEntity)
                return false;
        }

        var Entity_Object = Instantiate(Entity_Prefab, Spawn_Pos, Utill.QI);
        var Entity = Entity_Object.GetComponent<Entity>();

        if (isMine)
            My_Entity[MyEmptyEntity_Index] = Entity;
        else
            Enemy_Entity.Insert(Random.Range(0, Enemy_Entity.Count), Entity);

        Entity.isMine = isMine;
        Entity.Set_Up(item);
        Entity_Alignment(isMine);

        return true;
    }

    public void Entity_MouseDown(Entity entity)
    {
        // 만약 CanMouse_Input이 false라면 return을 해준다.
        if (!CanMouse_Input)
            return;

        Select_Entity = entity;
    }

    public void Entity_MouseUp()
    {
        if (!CanMouse_Input)
            return;

        // Select_Entity, Target_PickEntity 둘다 존재하며 Attack_Able이 true여야 공격한다.
        if (Select_Entity && Target_PickEntity && Select_Entity.Attack_Able)
            Attack(Select_Entity, Target_PickEntity);

        // Select_Entity 와 Target_PickEntity를 null로 해준다.
        Select_Entity = null;
        Target_PickEntity = null;
    }

    public void Entity_MouseDrag()
    {
        if (!CanMouse_Input || Select_Entity == null)
            return;

        // Enemy 타겟 엔티티 찾기
        // 타겟이 한개라도 존재하는지 확인해주기 위해 bool로 false로 만든다.
        bool Exist_Target = false;
        foreach (var Hit in Physics2D.RaycastAll(Utill.Mouse_Pos, Vector3.forward))
        {
            Entity entity = Hit.collider?.GetComponent<Entity>();
            if (entity != null && !entity.isMine && Select_Entity.Attack_Able)
            {
                Target_PickEntity = entity;
                Exist_Target = true;
                break;
            }
        }

        if (!Exist_Target)
            Target_PickEntity = null;
    }

    public void Attack_AbleReset(bool isMine)
    {
        // 이 함수는 Entity_Manager에 있는 OnTurn_Start함수에 호출 시켜준다. 즉, 내 턴일 때 호출이 된다.
        // isMine이 true라면 My_Entity false라면 Enemey_Entity를 Target_Entity에 넣는다.
        var Target_Entity = isMine ? My_Entity : Enemy_Entity;
        Target_Entity.ForEach(x => x.Attack_Able = true);
    }

    private void Show_Aim(bool isShow)
    {
        Aim.SetActive(isShow);
        if (Exist_Aim)
            // 만약 Exist_Aim 이 true라면 Aim.transform.position을 Target_PickEntity.transform.position에 그대로 대입한다.
            Aim.transform.position = Target_PickEntity.transform.position;
    }

    private void Attack(Entity Attacke, Entity Defend)
    {
        // 이 함수는 Entity_Manager에 Entity_MouseUp 함수에 호출시킨다.
        // Attack이 Defend의 위치로 이동하다 원래 위치로 온다, 이 때 order가 높다.
        Attacke.Attack_Able = false;
        Attacke.GetComponent<Order>().Set_MostFrontOrder(true);

        Sequence sequence = DOTween.Sequence()
            .Append(Attacke.transform.DOMove(Defend.Origin_Pos, 0.4f)).SetEase(Ease.InSine)
            .AppendCallback(() =>
            {
                Attacke.Damage(Defend.Attack); // 방어자의 공격력만큼 데미지를 준다.
                Defend.Damage(Attacke.Attack); // 공격자의 공격력만큼 데미지를 준다.
                Spawn_Damage(Defend.Attack, Attacke.transform); // 공격자의 위치에는 방어자의 공격력만큼 대입해준다.
                Spawn_Damage(Attacke.Attack, Defend.transform); // 방어자의 위치에는 공격자의 공격력만큼 대입해준다.
            })
            .Append(Attacke.transform.DOMove(Attacke.Origin_Pos, 0.4f)).SetEase(Ease.OutSine)
            .OnComplete(() => Attack_Callback(Attacke, Defend)); // 죽음 처리
    }

    private void Spawn_Damage(int Damage, Transform tr)
    {
        var Damage_Component = Instantiate(Damage_Prefab).GetComponent<Damage>();
        Damage_Component.SetUp_Transform(tr);
        Damage_Component.Damaged(Damage);
    }

    private void Attack_Callback(params Entity[] entity)
    {
        // 죽을 사람 골라서 죽음 처리
        entity[0].GetComponent<Order>().Set_MostFrontOrder(false);
        foreach (var Entity in entity)
        {
            if (!Entity.isDie || Entity.isBoss_Empty)
                // 만약 isDie가 false 이거나, isBoss_Empty이면은 continue를 실행시켜 아래 있는 코드들을 실행시키지 않고 다음 엔티티로 넘긴다.
                continue;

            if (Entity.isMine)
                My_Entity.Remove(Entity);
            else
                Enemy_Entity.Remove(Entity);

            Sequence sequence = DOTween.Sequence()
                .Append(Entity.transform.DOShakePosition(1.3f))
                .Append(Entity.transform.DOScale(Vector3.zero, 0.3f)).SetEase(Ease.OutCirc)
                .OnComplete(() =>
                {
                    Entity_Alignment(Entity.isMine);
                    Destroy(Entity.gameObject);
                });
        }
        StartCoroutine(Chack_BossDie());
    }

    IEnumerator Chack_BossDie()
    {
        yield return delay_2;
        if(MyBoss_Entity.isDie)
            StartCoroutine(GameManager.instnace.GameOver(false));

        if (EnemyBoss_Entity.isDie)
            StartCoroutine(GameManager.instnace.GameOver(true));
    }

    public void Damage_Boss(bool isMIne, int Damage)
    {
        var Target_BossEntity = isMIne ? MyBoss_Entity : EnemyBoss_Entity;
        Target_BossEntity.Damage(Damage);
        StartCoroutine(Chack_BossDie());
    }

}
