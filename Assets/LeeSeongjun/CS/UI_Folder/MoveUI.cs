using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveUI : MonoBehaviour
{
    public int Spot = 0;
    public GameObject rightArrow;
    public GameObject leftArrow;
    
    void Start()
    {
        rightArrow.SetActive(true); //���۽� ������ ����
        leftArrow.SetActive(false); // ������ ��Ȱ��ȭ
    }

    public void UI_Move()
    {
        Main_Number oc = GameObject.Find("�ٱ� ǥ�� �⺻x��ǥ -540 , -1380").GetComponent<Main_Number>();
        if (oc.Spot == 0 )
        {
            Debug.Log("UI����");
            rightArrow.SetActive(false); //������ ���� , ��Ȱ��ȭ
            leftArrow.SetActive(true); // ������ Ȱ��ȭ
            oc.Spot++;
        }
        else if (oc.Spot == 1 )
        {
            Debug.Log("UI����");
            rightArrow.SetActive(true); //UI���� , Ȱ��ȭ
            leftArrow.SetActive(false); // ������ ��Ȱ��ȭ
            oc.Spot = 0;
        }
    }
}