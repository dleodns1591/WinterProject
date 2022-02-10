using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 치트, UI, 랭킹, 게임오버 등 전반적인 게임관련 된 기능을 이 스크립트에서 관리한다.
public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] Notification Notification_Image;

    void Start()
    {
        Start_Game();
    }

    void Update()
    {
#if UNITY_EDITOR
        Cheat_Key();
#endif
    }

    private void Cheat_Key()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            Turn_Manager.OnAdd_Card?.Invoke(true);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            Turn_Manager.OnAdd_Card?.Invoke(false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            Turn_Manager.Inst.End_Turn();
        }
    }

    private void Start_Game()
    {
        // Turn_Manager에 있는 Start_GameCo를 실행시킨다.
        StartCoroutine(Turn_Manager.Inst.Start_GameCo());
    }

    public void Notification(string Message)
    {
        Notification_Image.Show(Message);
    }
}
