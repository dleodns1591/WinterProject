using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

// 적 턴이나 내 턴을 관리하는 스크립트이다.
public class Turn_Manager : MonoBehaviour
{
    // 싱글톤을 만들어준다.
    public static Turn_Manager Inst { get; private set; }
    void Awake() => Inst = this;

    // Debelop에 Tootip을 달아줘서 마우스를 올려다 놓으면 Tooltip에 써놓은 글자가 나온다.
    [Header("Develop")]
    [SerializeField] [Tooltip("시작 턴 모드를 정합니다.")] ETurn_Mode eTurn_Mode;
    [SerializeField] [Tooltip("시작 카드 개수를 정합니다.")] int Start_CardCount;
    [SerializeField] [Tooltip("카드 배분이 매우 빨라집니다.")] bool Fast_Mode;

    // Properties는 일반적인 Inspector창에 보인다.
    // 게임이 끝나면 isLoading을 true로 하면 카드와 엔티티 클릭방지 된다.
    [Header("Properties")]
    public bool isLoading; 
    public bool My_Turn;

    // 최초로 시작을 할 때 누구 턴 부터 시작을 할 것인지 정해준다.
    // delay_05에 0.5초 시간을 넣어준다.
    // delay_07에 0.7초 시간을 넣어준다.
    enum ETurn_Mode { My, Enemey }
    WaitForSeconds delay_05 = new WaitForSeconds(0.5f);
    WaitForSeconds delay_07 = new WaitForSeconds(0.7f);

    public static Action<bool> OnAdd_Card;
    public static event Action<bool> OnTurn_Start;

    private void Game_Setup()
    {
        if(Fast_Mode)
        {
            // Fast_Mode를 실행시키면 0.5초였던것이 0.05초로 바뀐다.
            delay_05 = new WaitForSeconds(0.05f);
        }

        switch(eTurn_Mode)
        {
            // 내 턴이라면 My_Trun이 true가 되고, 적 턴이라면 My_Turn이 false가 된다.
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
            yield return delay_05; // 0.5초 대기
            OnAdd_Card?.Invoke(false); // false라면 상대 카드
            yield return delay_05; // 0.5초 대기
            OnAdd_Card?.Invoke(true); // true라면 내 카드
        }
        StartCoroutine(Start_TurnCo());
    }

    private IEnumerator Start_TurnCo()
    {
        // 턴이 시작될 경우 isLoading을 true로 해주고, 최초로 게임이 시작될 때 실행을 시켜주기 위하여 Start_GameCo에 실행시켜준다.
        isLoading = true;

        if(My_Turn)
        {
            // 내 턴일 경우 GameManger Inst를 통해 Notification을 나의 턴이라고 넘겨준다.
            GameManager.Inst.Notification("나의 턴");
        }

        yield return delay_07; // 0.7초 대기
        OnAdd_Card?.Invoke(My_Turn); // 내 턴이라면 카드르 한 장 추가해주고, 상대 턴이라면 상대한테 카드를 한 장 추가해준다.   
        yield return delay_07; // 0.7초 대기
        isLoading = false; // isLoading을 false로 해주면 카드를 클릭할 수 있는 상태로 된다.
        OnTurn_Start?.Invoke(My_Turn);
    }

    public void End_Turn()
    {
        // 턴을 넘겨주기위한 코드
        My_Turn = !My_Turn;
        StartCoroutine(Start_TurnCo());
    }
}
