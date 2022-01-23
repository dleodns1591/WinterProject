using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Long_UI_Main : MonoBehaviour
{
    public GameObject Long_UI_Inventory;
    public GameObject Long_UI_Card;
    public GameObject Long_UI_Map;
    public GameObject Long_UI_Monster;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Main_Number oc = GameObject.Find("¹Ù±ù Ç¥Áö ±âº»xÁÂÇ¥ -540 , -1380").GetComponent<Main_Number>();
        switch (oc.Long_UI_Number)
        {
            case 1:
                break;
            case 2:
                break;
        }
    }
}
