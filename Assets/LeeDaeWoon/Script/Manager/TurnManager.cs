using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Inst { get; private set; } //�̱������� ������ݴϴ�.
    void Awake() => Inst = this;

    [Header("Develop")] // Develop���� Tooltip�� �޾��༭ ���콺�� �÷������� �� �� ���ڰ� �߰� �Ѵ�.
    [SerializeField] [Tooltip("���� �� ��带 ���մϴ�.")] ETurnMode eTurnMode;
    [SerializeField] [Tooltip("ī�� ����� �ſ� �������ϴ�.")] bool fastMode; // fastMode�� ī�� ����� �ſ� ��������.
    [SerializeField] [Tooltip("���� ī�� ������ ���մϴ�.")] int Start_CardCount; // Start_CardCount�� ���� ī�� ������ ���Ѵ�.

    [Header("Properties")] // Properties�� �Ϲ����� �ν����Ϳ� ���̴� Properties �̴�.
    public bool isLoading; // ������ ������ isLoading�� true�� �ϸ� ī��� ��ƼƼ Ŭ������
    public bool MyTurn;

    enum ETurnMode { My, Other} // ������ ���� �������� �ƴϸ�, ������ ���� ��������
    WaitForSeconds delay05 = new WaitForSeconds(0.5f); // WaitForSeconds 0.5�ʸ� delay05 ��� �����Ѵ�.
    WaitForSeconds delay07 = new WaitForSeconds(0.7f); // WaitForSeconds 0.7�ʸ� delay07 ��� �����Ѵ�.

    public static Action<bool> OnAddCard; // Action�� ����ϱ� ���ؼ��� 'using System'�� ����� �ش�.
    public static Action<bool> OffAddCard;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void Game_Setup()
    {
        if(fastMode)
        {
            delay05 = new WaitForSeconds(0.05f); // fastMode �� true ��� 0.5f => 0.05f �� �ٲ��.
        }

        switch (eTurnMode)
        {
            case ETurnMode.My: // �� ���̶�� MyTurn�� Ture�� �Ѵ�.
                MyTurn = true; break;
            case ETurnMode.Other: // �� ���̶�� MyTurn�� False�� �Ѵ�.
                MyTurn = false; break;
        }
    }

    public IEnumerator Start_GameCo()
    {
        Game_Setup(); //Game_Setup�� ȣ���Ѵ�.
        isLoading = true;

        for (int i = 0; i < Start_CardCount; i++)
        {
            yield return delay05; // 0.5�ʸ� ����ϰ� OnAddCard�� ȣ���Ѵ�.
            OnAddCard?.Invoke(false); // ��� ��
            yield return delay05; // 0.5�ʸ� ����ϰ� OnAddCard�� ȣ���Ѵ�.
            OnAddCard?.Invoke(true); // �� ī��
        }
        StartCoroutine(Start_TurnCo()); // Start_GameCo���� ī�� ����� ������ Start_TurnCo�� �����Ѵ�.
    }

    private IEnumerator Start_TurnCo()
    {
        isLoading = true;
        
        if(MyTurn)
        {
            GameManager.Inst.Notification("���� ��"); // �� ���̶��, GameManager Inst�� ���� Notification�� ���� ���̶�� �Ѱ��ش�.
        }
        yield return delay07;
        OffAddCard?.Invoke(MyTurn);

        // �� ī�� 5�� �߰�
        yield return delay07; // 0.7�ʸ� ��� �ϰ� OnAddCard�� ȣ���Ѵ�.
        OnAddCard?.Invoke(MyTurn);
        yield return delay07; // 0.7�ʸ� ��� �ϰ� OnAddCard�� ȣ���Ѵ�.
        OnAddCard?.Invoke(MyTurn);
        yield return delay07; // 0.7�ʸ� ��� �ϰ� OnAddCard�� ȣ���Ѵ�.
        OnAddCard?.Invoke(MyTurn);
        yield return delay07; // 0.7�ʸ� ��� �ϰ� OnAddCard�� ȣ���Ѵ�.
        OnAddCard?.Invoke(MyTurn);
        yield return delay07; // 0.7�ʸ� ��� �ϰ� OnAddCard�� ȣ���Ѵ�.
        OnAddCard?.Invoke(MyTurn);

        isLoading = false;


    }

    public void EndTurn()
    {
        MyTurn = !MyTurn; // MyTurn�� �����´�.
        StartCoroutine(Start_TurnCo()); // Start_TurnCo �Լ��� �����Ѵ�.
    }
}
