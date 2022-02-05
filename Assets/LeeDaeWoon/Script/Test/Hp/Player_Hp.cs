using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

namespace CCGKit
{
    /// <summary>
    /// Player_Hp 와 방어막을 표시하는 스크립트
    /// </summary>

    public class Player_Hp : MonoBehaviour
    {
#pragma warning disable 649 // 649번째의 오류를 표시하지 않는다.
        [SerializeField] private Image Hp_Bar;
        [SerializeField] private Image Hp_BarBackground;
        [SerializeField] private TextMeshProUGUI Hp_Text;
        //[SerializeField] private GameObject Shield_Group;
        //[SerializeField] private TextMeshProUGUI Shield_Text;
#pragma warning disable 649 // 649번째의 오류를 표시하지 않는다.

        private int Max_Value;
        
        public void Initialize(IntVariable Hp, int Max, IntVariable Shield)
        {
            Max_Value = Max;

        }

        private void SetUp(int Value, bool animateChange = true)
        {
            var newValue = Value / (float)Max_Value;
            if(animateChange)
            {
                Hp_Bar.DOFillAmount(newValue, 0.2f).SetEase(Ease.InSine);
                var seq = DOTween.Sequence();
                seq.AppendInterval(0.5f);
                seq.Append(Hp_BarBackground.DOFillAmount(newValue, 0.2f));
                seq.SetEase(Ease.InSine);
            }
            else
            {
                Hp_Bar.fillAmount = newValue;
                Hp_BarBackground.fillAmount = newValue;
            }

            Hp_Text.text = $"{Value.ToString()}/{Max_Value.ToString()}";
        }

        //private void Set_Shield(int Value)
        //{
        //    Shield_Text.text = $"{Value.ToString()}";
        //    Set_ShieldActive(Value > 0);
        //}

        //private void Set_ShieldActive(bool shield_Active)
        //{
        //    Shield_Group.SetActive(shield_Active);
        //}

        public void OnHp_Changed(int Value)
        {
            SetUp(Value);
            if (Value <= 0)
            {
                gameObject.SetActive(false);
            }
        }

        //public void OnShield_Changed(int Value)
        //{
        //    Set_Shield(Value);
        //}
    }
}


