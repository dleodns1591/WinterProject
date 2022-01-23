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
			Debug.Log("100G 지급");
			GoldReferesh();
		}
	}

	public void Purchase()
	{
		if (Gold >= int.Parse(GameObject.Find(EventSystem.current.currentSelectedGameObject.name).transform.GetChild(0).GetComponent<Text>().text.Split('G')[0]))
		{
			Gold -= int.Parse(GameObject.Find(EventSystem.current.currentSelectedGameObject.name).transform.GetChild(0).GetComponent<Text>().text.Split('G')[0]);
			Debug.Log(EventSystem.current.currentSelectedGameObject.name + "을(를) " + int.Parse(GameObject.Find(EventSystem.current.currentSelectedGameObject.name).transform.GetChild(0).GetComponent<Text>().text.Split('G')[0]) + "G에 구입했다");
			GoldReferesh();
		}
		else
		{
			Debug.Log("돈이 부족해!");
		}
		//Debug.Log(GameObject.Find(EventSystem.current.currentSelectedGameObject.name).transform.GetChild(0).GetComponent<Text>().text.Split('G')[0]);
	}

	public void GoldReferesh()
	{
		GoldText.text = Gold.ToString() + "G";
	}
}
