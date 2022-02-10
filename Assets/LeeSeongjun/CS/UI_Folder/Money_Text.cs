using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money_Text : MonoBehaviour
{
    public Text ScriptTxt;
    void Update()
    {
        Player_Control oc = GameObject.Find("통합관리").GetComponent<Player_Control>();
        ScriptTxt.text = "" + oc.Player_Money + "G";
    }
}
