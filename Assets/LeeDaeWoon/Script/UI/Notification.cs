using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class Notification : MonoBehaviour
{
    [SerializeField] TMP_Text Notification_TMP;
    void Start() => Scale_Zero();


    void Update()
    {
        
    }

    public void Show(string Message)
    {
        Notification_TMP.text = Message;
        Sequence sequence = DOTween.Sequence()
            // 내 턴을 알리기 위한 알림창의 크기를 1로 한다음, 0.9초 대기하고 크기를 0으로 줄여준다.
            .Append(transform.DOScale(Vector3.one, 0.3f).SetEase(Ease.InOutQuad))
            .AppendInterval(0.9f)
            .Append(transform.DOScale(Vector3.zero, 0.3f).SetEase(Ease.InOutQuad));
    }


    // Inspector창에서 제어를 하기위해 ContextMeneu를 사용한다.
    [ContextMenu("Scale_One")]
    // Scale_One일 경우 크기를 1로한다.
    private void Scale_One() => transform.localScale = Vector3.one;

    [ContextMenu("Scale_Zero")]
    // Scale_Zero일 경우 크기를 0으로 한다.
    public void Scale_Zero() => transform.localScale = Vector3.zero;
}
