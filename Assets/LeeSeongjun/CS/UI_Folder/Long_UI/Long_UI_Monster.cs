using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Long_UI_Monster : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    public void UI_Monster_Click()
    {
        Long_UI_Main oc = GameObject.Find("�丷��_UI ����").GetComponent<Long_UI_Main>();
        oc.Long_UI_Level = 4;
    }
}
