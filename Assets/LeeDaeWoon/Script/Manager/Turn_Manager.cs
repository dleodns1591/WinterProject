using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

// �� ���̳� �� ���� �����ϴ� ��ũ��Ʈ�̴�.
public class Turn_Manager : MonoBehaviour
{
    // �̱����� ������ش�.
    public static Turn_Manager Inst { get; private set; }
    void Awake() => Inst = this;

    // Debelop�� Tootip�� �޾��༭ ���콺�� �÷��� ������ Tooltip�� ����� ���ڰ� ���´�.
    [Header("Develop")]
    [SerializeField] [Tooltip("���� �� ��带 ���մϴ�.")] ETurn_Mode eTurn_Mode;
    [SerializeField] [Tooltip("���� ī�� ������ ���մϴ�.")] int Start_CardCount;
    [SerializeField] [Tooltip("ī�� ����� �ſ� �������ϴ�.")] bool Fast_Mode;

    // Properties�� �Ϲ����� Inspectorâ�� ���δ�.
    // ������ ������ isLoading�� true�� �ϸ� ī��� ��ƼƼ Ŭ������ �ȴ�.
    [Header("Properties")]
    public bool isLoading; 
    public bool My_Turn;

    // ���ʷ� ������ �� �� ���� �� ���� ������ �� ������ �����ش�.
    // delay_05�� 0.5�� �ð��� �־��ش�.
    // delay_07�� 0.7�� �ð��� �־��ش�.
    enum ETurn_Mode { My, Enemey }
    WaitForSeconds delay_05 = new WaitForSeconds(0.5f);
    WaitForSeconds delay_07 = new WaitForSeconds(0.7f);

    public static Action<bool> OnAdd_Card;
    public static event Action<bool> OnTurn_Start;

    private void Game_Setup()
    {
        if(Fast_Mode)
        {
            // Fast_Mode�� �����Ű�� 0.5�ʿ������� 0.05�ʷ� �ٲ��.
            delay_05 = new WaitForSeconds(0.05f);
        }

        switch(eTurn_Mode)
        {
            // �� ���̶�� My_Trun�� true�� �ǰ�, �� ���̶�� My_Turn�� false�� �ȴ�.
            case ETurn_Mode.My:
                My_Turn = true;
                break;
            case ETurn_Mode.Enemey:
                My_Turn = false;
                break;
        }
    }

    public IEnumerator Start_GameCo()
    {
        Game_Setup();
        isLoading = true;

        for( int i = 0; i < Start_CardCount; i++)
        {
            yield return delay_05; // 0.5�� ���
            OnAdd_Card?.Invoke(false); // false��� ��� ī��
            yield return delay_05; // 0.5�� ���
            OnAdd_Card?.Invoke(true); // true��� �� ī��
        }
        StartCoroutine(Start_TurnCo());
    }

    private IEnumerator Start_TurnCo()
    {
        // ���� ���۵� ��� isLoading�� true�� ���ְ�, ���ʷ� ������ ���۵� �� ������ �����ֱ� ���Ͽ� Start_GameCo�� ��������ش�.
        isLoading = true;

        if(My_Turn)
        {
            // �� ���� ��� GameManger Inst�� ���� Notification�� ���� ���̶�� �Ѱ��ش�.
            GameManager.Inst.Notification("���� ��");
        }

        yield return delay_07; // 0.7�� ���
        OnAdd_Card?.Invoke(My_Turn); // �� ���̶�� ī�帣 �� �� �߰����ְ�, ��� ���̶�� ������� ī�带 �� �� �߰����ش�.   
        yield return delay_07; // 0.7�� ���
        isLoading = false; // isLoading�� false�� ���ָ� ī�带 Ŭ���� �� �ִ� ���·� �ȴ�.
        OnTurn_Start?.Invoke(My_Turn);
    }

    public void End_Turn()
    {
        // ���� �Ѱ��ֱ����� �ڵ�
        My_Turn = !My_Turn;
        StartCoroutine(Start_TurnCo());
    }
}
