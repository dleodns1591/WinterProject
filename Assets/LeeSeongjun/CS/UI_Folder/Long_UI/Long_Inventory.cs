using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Long_Inventory : MonoBehaviour
{

    void Start()
    {
        
    }

    // Update is called once per frame
    public void UI_Inventory_Click()
    {
        Long_UI_Main oc = GameObject.Find("�丷��_UI ����").GetComponent<Long_UI_Main>();
        oc.Long_UI_Level = 1;
    }
}
