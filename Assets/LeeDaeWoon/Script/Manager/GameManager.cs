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
        if (Input.GetKeyDown(KeyCode.Keypad1)) // 1�� Ű�� ������ �� ī�尡 ���´�.
        {
            TurnManager.OnAddCard?.Invoke(true);
        }

        if (Input.GetKeyDown(KeyCode.Keypad3)) // 3�� Ű�� ������ EndTurn�� ȣ���ϵ��� �Ѵ�.
        {
            TurnManager.Inst.EndTurn();
        }
    }

    public void StartGame()
    {
        StartCoroutine(TurnManager.Inst.Start_GameCo()); // TurnManager�� �ִ� Start_GameCo�� �����Ѵ�.
    }

    public void Notification(string message)
    {
        notification_Panel.Show(message);
    }
}
