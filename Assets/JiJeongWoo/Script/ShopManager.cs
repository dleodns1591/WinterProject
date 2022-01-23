using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
	public int Gold;
	public Text GoldText;
	void Start()
	{

	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Q))
		{
			Gold += 100;
			Debug.Log("100G ����");
			GoldReferesh();
		}
	}

	public void Purchase()
	{
		if (Gold >= int.Parse(GameObject.Find(EventSystem.current.currentSelectedGameObject.name).transform.GetChild(0).GetComponent<Text>().text.Split('G')[0]))
		{
			Gold -= int.Parse(GameObject.Find(EventSystem.current.currentSelectedGameObject.name).transform.GetChild(0).GetComponent<Text>().text.Split('G')[0]);
			Debug.Log(EventSystem.current.currentSelectedGameObject.name + "��(��) " + int.Parse(GameObject.Find(EventSystem.current.currentSelectedGameObject.name).transform.GetChild(0).GetComponent<Text>().text.Split('G')[0]) + "G�� �����ߴ�");
			GoldReferesh();
		}
		else
		{
			Debug.Log("���� ������!");
		}
		//Debug.Log(GameObject.Find(EventSystem.current.currentSelectedGameObject.name).transform.GetChild(0).GetComponent<Text>().text.Split('G')[0]);
	}

	public void GoldReferesh()
	{
		GoldText.text = Gold.ToString() + "G";
	}
}
