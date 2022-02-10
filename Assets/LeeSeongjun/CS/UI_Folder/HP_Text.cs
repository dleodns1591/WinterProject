using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HP_Text : MonoBehaviour
{

	public Image hpbar;
	public Text HpText;
	public static float health;
	private void Update()
	{
		PlayerHPbar();
	}

	public void PlayerHPbar()

	{
		Player_Control oc = GameObject.Find("통합관리").GetComponent<Player_Control>();
		hpbar.fillAmount = oc.Player_HP / 100f;
		HpText.text = string.Format("HP {0}/100", oc.Player_HP);
	}
}

