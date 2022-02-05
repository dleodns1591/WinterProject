using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TurnManager : MonoBehaviour
{
    public static TurnManager Inst { get; private set; } //싱글톤으로 만들어줍니다.
    void Awake() => Inst = this;

    [Header("Develop")] // Develop에는 Tooltip을 달아줘서 마우스를 올려놓았을 때 저 글자가 뜨게 한다.
    [SerializeField] [Tooltip("시작 턴 모드를 정합니다.")] ETurnMode eTurnMode;
    [SerializeField] [Tooltip("카드 배분이 매우 빨라집니다.")] bool fastMode; // fastMode는 카드 배분이 매우 빨라진다.
    [SerializeField] [Tooltip("시작 카드 개수를 정합니다.")] int Start_CardCount; // Start_CardCount는 시작 카드 개수를 정한다.

    [Header("Properties")] // Properties는 일반적인 인스펙터에 보이는 Properties 이다.
    public bool isLoading; // 게임이 끝나면 isLoading을 true로 하면 카드와 엔티티 클릭방지
    public bool MyTurn;

    enum ETurnMode { My, Other} // 나부터 턴을 진행할지 아니면, 적부터 턴을 진행할지
    WaitForSeconds delay05 = new WaitForSeconds(0.5f); // WaitForSeconds 0.5초를 delay05 라고 지정한다.
    WaitForSeconds delay07 = new WaitForSeconds(0.7f); // WaitForSeconds 0.7초를 delay07 라고 지정한다.

    public static Action<bool> OnAddCard; // Action을 사용하기 위해서는 'using System'을 사용해 준다.
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
            delay05 = new WaitForSeconds(0.05f); // fastMode 가 true 라면 0.5f => 0.05f 로 바뀐다.
        }

        switch (eTurnMode)
        {
            case ETurnMode.My: // 내 턴이라면 MyTurn을 Ture로 한다.
                MyTurn = true; break;
            case ETurnMode.Other: // 적 턴이라면 MyTurn을 False로 한다.
                MyTurn = false; break;
        }
    }

    public IEnumerator Start_GameCo()
    {
        Game_Setup(); //Game_Setup을 호출한다.
        isLoading = true;

        for (int i = 0; i < Start_CardCount; i++)
        {
            yield return delay05; // 0.5초를 대기하고 OnAddCard를 호출한다.
            OnAddCard?.Invoke(false); // 상대 턴
            yield return delay05; // 0.5초를 대기하고 OnAddCard를 호출한다.
            OnAddCard?.Invoke(true); // 내 카드
        }
        StartCoroutine(Start_TurnCo()); // Start_GameCo에서 카드 배분이 끝나면 Start_TurnCo를 실행한다.
    }

    private IEnumerator Start_TurnCo()
    {
        isLoading = true;
        
        if(MyTurn)
        {
            GameManager.Inst.Notification("나의 턴"); // 내 턴이라면, GameManager Inst를 통해 Notification을 나의 턴이라고 넘겨준다.
        }
        yield return delay07;
        OffAddCard?.Invoke(MyTurn);

        // 총 카드 5장 추가
        yield return delay07; // 0.7초를 대기 하고 OnAddCard를 호출한다.
        OnAddCard?.Invoke(MyTurn);
        yield return delay07; // 0.7초를 대기 하고 OnAddCard를 호출한다.
        OnAddCard?.Invoke(MyTurn);
        yield return delay07; // 0.7초를 대기 하고 OnAddCard를 호출한다.
        OnAddCard?.Invoke(MyTurn);
        yield return delay07; // 0.7초를 대기 하고 OnAddCard를 호출한다.
        OnAddCard?.Invoke(MyTurn);
        yield return delay07; // 0.7초를 대기 하고 OnAddCard를 호출한다.
        OnAddCard?.Invoke(MyTurn);

        isLoading = false;


    }

    public void EndTurn()
    {
        MyTurn = !MyTurn; // MyTurn을 뒤집는다.
        StartCoroutine(Start_TurnCo()); // Start_TurnCo 함수를 실행한다.
    }
}
