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
    // IsFull_MyEntity�� Entity�� �� á���� �˷��ش�.
    // My_Entity.Count�� MaxEntity_Count �̻��̰� ���� Exist_MyEmpty_Entity�� �������� ���� ���
    public bool IsFull_MyEntity => My_Entity.Count >= MaxEntity_Count && !Exist_MyEmpty_Entity;
    // IsFull_EnemyEntity�� Entity�� �� á���� �˷��ش�.
    // Enemy_Entity���� ��쿡�� ī�带 ����� ���� ��찡 �ƴϱ� ������ Enemy_Entity.Count�� MaxEntity_Count���� �̻��� ��츸 Ȯ�����ش�.
    bool IsFull_EnmeyEntity => Enemy_Entity.Count >= MaxEntity_Count;
    bool Exist_MyEmpty_Entity => My_Entity.Exists(x => x == MyEmpty_Entity);
    int MyEmptyEntity_Index => My_Entity.FindIndex(x => x == MyEmpty_Entity);
    // CanMouse_Input�� �� ���̸鼭 isLoading�� false�� ��� ���콺�� �Է��� ���� �� �ִ�. ��� ������ش�.
    bool CanMouse_Input => Turn_Manager.instance.My_Turn && !Turn_Manager.instance.isLoading;
    // Aim�� null�� �ƴ� ��� Exist_Aim�̶�� �Ѵ�.
    bool Exist_Aim => Target_PickEntity != null;

    // ������ ���� �����ϴ� Entity
    Entity Select_Entity;
    // ���콺�� ����ٰ� ����� �����ϰ� �Ǵ°��� Target_PickEntity �̴�.
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

        // Attack_Able�� true�� ��� Enemy_Entity�� ������ ������ ���´�.
        var Attackers = new List<Entity>(Enemy_Entity.FindAll(x => x.Attack_Able == true));
        for (int i = 0; i < Attackers.Count; i++)
        {
            int rand = Random.Range(i, Attackers.Count);
            Entity Temp = Attackers[i];
            Attackers[i] = Attackers[rand];
            Attackers[rand] = Temp;
        }

        // ������ ������ My_Entity�� �����ϰ� �ð��� �����Ѵ�.
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
        // Entity ���� ���ִ� �Լ�
        // isMine �̸� Target_Y �� -1.7 �ƴϸ��� 1.7 ��ġ�� �־��ش�.
        // Target_Entity�� isMine �̸� My_Entity�� ��������, �ƴϸ��� Enemy_Entity�� �����´�.
        float Target_Y = isMine ? -1.7f : 1.7f;
        var Target_Entity = isMine ? My_Entity : Enemy_Entity;

        for (int i = 0; i < Target_Entity.Count; i++)
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
        if (IsFull_MyEntity)
            return;

        // ���࿡ ���� Entity�� �������� �ʴ´ٸ� My_Entity�� �߰��� ���ش�.
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
        // �� �Լ��� Card_Manager�� Card_MouseUp �Լ��� ȣ�����ش�.
        // ���� �� Entity�� �������� �ʴ´ٸ� return�� ���ش�.
        if (!Exist_MyEmpty_Entity)
            return;

        // MyEmptyEntity_Index�� ���ش�.
        // ������ �� �� �ֵ��� Entity_Alignment�� true�� ���ش�.
        My_Entity.RemoveAt(MyEmptyEntity_Index);
        Entity_Alignment(true);
    }

    public bool Spawn_Entity(bool isMine, Item item, Vector3 Spawn_Pos)
    {
        // bool�� ��ȯ�� ������ ��ƼƼ�� ������ ������ �ߴ��� ���ߴ��� �˰� �ͱ� �����̴�.
        if (isMine)
        {
            // ���� isMine�� ��� �� ��ƼƼ�� �� ���ְų�, �� Empty_Entity�� ���� ��� return false�� ���ش�.
            if (IsFull_MyEntity || !Exist_MyEmpty_Entity)
                return false;
        }
        else
        {
            // ���� isMine�� false�� �� ���� ��ƼƼ�� �� ������ ��� return false�� ���ش�.
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
        // ���� CanMouse_Input�� false��� return�� ���ش�.
        if (!CanMouse_Input)
            return;

        Select_Entity = entity;
    }

    public void Entity_MouseUp()
    {
        if (!CanMouse_Input)
            return;

        // Select_Entity, Target_PickEntity �Ѵ� �����ϸ� Attack_Able�� true���� �����Ѵ�.
        if (Select_Entity && Target_PickEntity && Select_Entity.Attack_Able)
            Attack(Select_Entity, Target_PickEntity);

        // Select_Entity �� Target_PickEntity�� null�� ���ش�.
        Select_Entity = null;
        Target_PickEntity = null;
    }

    public void Entity_MouseDrag()
    {
        if (!CanMouse_Input || Select_Entity == null)
            return;

        // Enemy Ÿ�� ��ƼƼ ã��
        // Ÿ���� �Ѱ��� �����ϴ��� Ȯ�����ֱ� ���� bool�� false�� �����.
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
        // �� �Լ��� Entity_Manager�� �ִ� OnTurn_Start�Լ��� ȣ�� �����ش�. ��, �� ���� �� ȣ���� �ȴ�.
        // isMine�� true��� My_Entity false��� Enemey_Entity�� Target_Entity�� �ִ´�.
        var Target_Entity = isMine ? My_Entity : Enemy_Entity;
        Target_Entity.ForEach(x => x.Attack_Able = true);
    }

    private void Show_Aim(bool isShow)
    {
        Aim.SetActive(isShow);
        if (Exist_Aim)
            // ���� Exist_Aim �� true��� Aim.transform.position�� Target_PickEntity.transform.position�� �״�� �����Ѵ�.
            Aim.transform.position = Target_PickEntity.transform.position;
    }

    private void Attack(Entity Attacke, Entity Defend)
    {
        // �� �Լ��� Entity_Manager�� Entity_MouseUp �Լ��� ȣ���Ų��.
        // Attack�� Defend�� ��ġ�� �̵��ϴ� ���� ��ġ�� �´�, �� �� order�� ����.
        Attacke.Attack_Able = false;
        Attacke.GetComponent<Order>().Set_MostFrontOrder(true);

        Sequence sequence = DOTween.Sequence()
            .Append(Attacke.transform.DOMove(Defend.Origin_Pos, 0.4f)).SetEase(Ease.InSine)
            .AppendCallback(() =>
            {
                Attacke.Damage(Defend.Attack); // ������� ���ݷ¸�ŭ �������� �ش�.
                Defend.Damage(Attacke.Attack); // �������� ���ݷ¸�ŭ �������� �ش�.
                Spawn_Damage(Defend.Attack, Attacke.transform); // �������� ��ġ���� ������� ���ݷ¸�ŭ �������ش�.
                Spawn_Damage(Attacke.Attack, Defend.transform); // ������� ��ġ���� �������� ���ݷ¸�ŭ �������ش�.
            })
            .Append(Attacke.transform.DOMove(Attacke.Origin_Pos, 0.4f)).SetEase(Ease.OutSine)
            .OnComplete(() => Attack_Callback(Attacke, Defend)); // ���� ó��
    }

    private void Spawn_Damage(int Damage, Transform tr)
    {
        var Damage_Component = Instantiate(Damage_Prefab).GetComponent<Damage>();
        Damage_Component.SetUp_Transform(tr);
        Damage_Component.Damaged(Damage);
    }

    private void Attack_Callback(params Entity[] entity)
    {
        // ���� ��� ��� ���� ó��
        entity[0].GetComponent<Order>().Set_MostFrontOrder(false);
        foreach (var Entity in entity)
        {
            if (!Entity.isDie || Entity.isBoss_Empty)
                // ���� isDie�� false �̰ų�, isBoss_Empty�̸��� continue�� ������� �Ʒ� �ִ� �ڵ���� �����Ű�� �ʰ� ���� ��ƼƼ�� �ѱ��.
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
