using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUI222 : MonoBehaviour
{
    public GameObject rightArrow;
    public GameObject leftArrow;
    public void UI_Move222()
    {
        Main_Number oc = GameObject.Find("�ٱ� ǥ�� �⺻x��ǥ -540 , -1380").GetComponent<Main_Number>();
        if (oc.Spot == 0)
        {
            Debug.Log("UI����");
            rightArrow.SetActive(false); //������ ���� , ��Ȱ��ȭ
            leftArrow.SetActive(true); // ������ Ȱ��ȭ
            oc.countN = 0;
            oc.Spot = 1;
            Debug.Log("UI����2");
        }
        else if (oc.Spot == 1)
        {
            Debug.Log("UI����");
            rightArrow.SetActive(true); //UI���� , Ȱ��ȭ
            leftArrow.SetActive(false); // ������ ��Ȱ��ȭ
            oc.countN = 0;
            oc.Spot = 0;
        }
    }
}
