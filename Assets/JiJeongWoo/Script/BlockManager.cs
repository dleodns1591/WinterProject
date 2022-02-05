using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlockManager : MonoBehaviour
{
	void Start()
	{

	}

	void Update()
	{

	}

	public void MapInteraction()
	{
		if (gameObject.GetComponent<Image>().color.r == 1)
		{
			if(gameObject.GetComponent<Image>().color.g == 1)
			{
				if (gameObject.GetComponent<Image>().color.b == 1)
				{
					Debug.Log("white");
				}
				else
				{
					Debug.Log("yellow");
				}
			}
			else
			{
				if (gameObject.GetComponent<Image>().color.b == 1)
				{
					Debug.Log("purple");
				}
				else
				{
					Debug.Log("red");
				}
			}
		}
		else
		{
			if (gameObject.GetComponent<Image>().color.g == 1)
			{
				if (gameObject.GetComponent<Image>().color.b == 1)
				{
					Debug.Log("skyblue");
				}
				else
				{
					Debug.Log("green");
				}
			}
			else
			{
				if (gameObject.GetComponent<Image>().color.b == 1)
				{
					Debug.Log("blue");
				}
				else
				{
					Debug.Log("black");
				}
			}
		}
	}
}
