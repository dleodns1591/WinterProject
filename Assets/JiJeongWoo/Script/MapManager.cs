using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
	public GameObject Block;
	public int[,] MapDesign = new int[8, 2];
	public GameObject BlockCanvas;

	void Start()
	{
		MapCreate();
	}

	void Update()
	{

	}

	void MapCreate()
	{
		for (int i = 0; i < 8; i++)
		{
			for (int j = 0; j < 2; j++)
			{
				if (MapDesign[i, j] != 6)
				{
					MapDesign[i, j] = Random.Range(1, 6);
				}
				if (MapDesign[i, j] == 5)
				{
					if (j == 1)
					{
						MapDesign[i, j] = Random.Range(1, 5);
					}
					else
					{
						MapDesign[i, j + 1] = 6;
						//Debug.Log("aaaa" + i + "/" + j);
					}
				}
				//Debug.Log(MapDesign[i, j]);
				GameObject BlockSet = Instantiate(Block, new Vector2((-4.0f + i) * 108, (3.0f - j) * 108), Quaternion.identity);
				BlockSet.transform.SetParent(BlockCanvas.transform, false);
				if (MapDesign[i, j] == 1)
				{
					BlockSet.GetComponent<Image>().color = new Color(1, 0, 0);
				}
				else if (MapDesign[i, j] == 2)
				{
					BlockSet.GetComponent<Image>().color = new Color(1, 1, 0);
				}
				else if (MapDesign[i, j] == 3)
				{
					BlockSet.GetComponent<Image>().color = new Color(0, 1, 0);
				}
				else if (MapDesign[i, j] == 4)
				{
					BlockSet.GetComponent<Image>().color = new Color(0, 1, 1);
				}
				else if (MapDesign[i, j] == 5)
				{
					BlockSet.GetComponent<Image>().color = new Color(0, 0, 1);
				}
				else if (MapDesign[i, j] == 6)
				{
					BlockSet.GetComponent<Image>().color = new Color(0, 0, 1);
				}
			}
		}
		GameObject BlockSet1 = Instantiate(Block, new Vector2(4.0f * 108, 3.0f * 108), Quaternion.identity);
		BlockSet1.transform.SetParent(BlockCanvas.transform, false);
		BlockSet1.GetComponent<Image>().color = new Color(1, 1, 1);
		GameObject BlockSet2 = Instantiate(Block, new Vector2(4.0f * 108, 2.0f * 108), Quaternion.identity);
		BlockSet2.transform.SetParent(BlockCanvas.transform, false);
		BlockSet2.GetComponent<Image>().color = new Color(1, 1, 1);
	}
}
