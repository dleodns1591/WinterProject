using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopManager : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Purchase()
	{
        Debug.Log(GameObject.Find(EventSystem.current.currentSelectedGameObject.name).transform.GetChild(0).GetComponent<Text>().text.Split('G')[0]);
    }
}
