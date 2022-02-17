using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// 치트, UI, 랭킹, 게임오버 등 전반적인 게임관련 된 기능을 이 스크립트에서 관리한다.
public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    void Awake() => Inst = this;

    [Multiline(10)]
    [SerializeField] string Cheat_Info;
    [SerializeField] Notification Notification_Image;
    [SerializeField] Result Result_Image;
    [SerializeField] Result Result_Image_Lose;
    [SerializeField] GameObject EndTurn_Btn;

    WaitForSeconds delay_2 = new WaitForSeconds(2);

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
        if (Input.GetKeyDown(KeyCode.Keypad4))
        {
            Card_Manager.Inst.TryPut_Card(false);
        }
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            Entity_Manager.Inst.Damage_Boss(true, 19);
        }
        if (Input.GetKeyDown(KeyCode.Keypad6))
        {
            Entity_Manager.Inst.Damage_Boss(false, 19);
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

    public IEnumerator GameOver(bool isMyWin)
    {
        Turn_Manager.Inst.isLoading = true;
        EndTurn_Btn.SetActive(false);
        yield return delay_2;

        Turn_Manager.Inst.isLoading = true;
        if (isMyWin)
        {
            Result_Image.Show();
        }
        else
        {
            Result_Image_Lose.Show();
        }
    }

}
