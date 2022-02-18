using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Load : MonoBehaviour
{
    public GameObject map_1;
    public GameObject map_2;
    public GameObject map_3;
    public GameObject map_4;
    public GameObject map_5;
    void Update()
    {
        Main_Number oc = GameObject.Find("¹Ù±ù Ç¥Áö ±âº»xÁÂÇ¥ -540 , -1380").GetComponent<Main_Number>();
        switch(oc.Map_Num)
        {
            case 1:
                map_1.SetActive(true);
                map_2.SetActive(false);
                map_3.SetActive(false);
                map_4.SetActive(false);
                map_5.SetActive(false);
                break;
            case 2:
                map_1.SetActive(false);
                map_2.SetActive(true);
                map_3.SetActive(false);
                map_4.SetActive(false);
                map_5.SetActive(false);
                break;
            case 3:
                map_1.SetActive(false);
                map_2.SetActive(false);
                map_3.SetActive(true);
                map_4.SetActive(false);
                map_5.SetActive(false);
                break;
            case 4:
                map_1.SetActive(false);
                map_2.SetActive(false);
                map_3.SetActive(false);
                map_4.SetActive(true);
                map_5.SetActive(false);
                break;
            case 5:
                map_1.SetActive(false);
                map_2.SetActive(false);
                map_3.SetActive(false);
                map_4.SetActive(false);
                map_5.SetActive(true);  
                break;
        }
    }
}
