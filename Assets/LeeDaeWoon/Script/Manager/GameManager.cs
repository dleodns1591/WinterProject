using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//치트, UI, 랭킹, 게임오버
public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }
    void Awake() => Inst = this;

    [SerializeField] TurnSetting turnSetting;

    void Start()
    {
        StartGame();
    }

    void Update()
    {
#if UNITY_EDITOR
        Input_CheatKey();
#endif
    }

    void Input_CheatKey()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1))
        {
            TurnManager.OnAddCard?.Invoke(true);
        }
        //if (Input.GetKeyDown(KeyCode.Keypad2))
        //{
        //    TurnManager.OnAddCard?.Invoke(false);
        //}
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            TurnManager.Inst.EndTurn();
        }
    }

    public void StartGame()
    {
        StartCoroutine(TurnManager.Inst.StartGameCo());
    }

    public void MyTurn_TMP(string message) 
    {
        turnSetting.Show(message);
    }
}
