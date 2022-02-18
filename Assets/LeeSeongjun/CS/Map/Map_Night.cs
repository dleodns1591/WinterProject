using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map_Night : MonoBehaviour
{
    public GameObject Night_1;
    public GameObject Night_2;
    public GameObject Night_3;
    public GameObject Night_4;
    public GameObject Night_5;
    public GameObject Night_6;
    void Update()
    {
        Main_Number oc = GameObject.Find("¹Ù±ù Ç¥Áö ±âº»xÁÂÇ¥ -540 , -1380").GetComponent<Main_Number>();
        switch (oc.Map_Night)
        {
            case 1:
                Night_1.SetActive(true);
                Night_2.SetActive(false);
                Night_3.SetActive(false);
                Night_4.SetActive(false);
                Night_5.SetActive(false);
                Night_6.SetActive(false);
                break;
            case 2:
                Night_1.SetActive(false);
                Night_2.SetActive(true);
                Night_3.SetActive(false);
                Night_4.SetActive(false);
                Night_5.SetActive(false);
                Night_6.SetActive(false);
                break;
            case 3:
                Night_1.SetActive(false);
                Night_2.SetActive(false);
                Night_3.SetActive(true);
                Night_4.SetActive(false);
                Night_5.SetActive(false);
                Night_6.SetActive(false);
                break;
            case 4:
                Night_1.SetActive(false);
                Night_2.SetActive(false);
                Night_3.SetActive(false);
                Night_4.SetActive(true);
                Night_5.SetActive(false);
                Night_6.SetActive(false);
                break;
            case 5:
                Night_1.SetActive(false);
                Night_2.SetActive(false);
                Night_3.SetActive(false);
                Night_4.SetActive(false);
                Night_5.SetActive(true);
                Night_6.SetActive(false);
                break;
            case 6:
                Night_1.SetActive(false);
                Night_2.SetActive(false);
                Night_3.SetActive(false);
                Night_4.SetActive(false);
                Night_5.SetActive(false);
                Night_6.SetActive(true);
                break;
        }
    }
}
