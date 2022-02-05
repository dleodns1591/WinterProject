using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] Notification_Panel notification_Panel;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
#if UNITY_EDITOR
        CheatKey();
#endif
    }

    private void CheatKey()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) // 1번 키를 누르면 내 카드가 나온다.
        {
            TurnManager.OnAddCard?.Invoke(true);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3)) // 3번 키를 누르면 EndTurn을 호출하도록 한다.
        {
            TurnManager.Inst.EndTurn();
        }
    }

    public void StartGame()
    {
        StartCoroutine(TurnManager.Inst.Start_GameCo()); // TurnManager에 있는 Start_GameCo를 실행한다.
    }

    public void Notification(string message)
    {
        notification_Panel.Show(message);
    }
}
