using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Result : MonoBehaviour
{
    [SerializeField] TMP_Text Result_TMP;
    public Image image;

    void Start() => Scale_Zero();

    [ContextMenu("Scale_One")]
    void Scale_One() => transform.localScale = Vector3.one;

    [ContextMenu("Scale_Zero")]
    void Scale_Zero() => transform.localScale = Vector3.zero;

    public void Show()
    {
        transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.InOutBack);
    }

    public void ReStart()
    {
        SceneManager.LoadScene(4);
    }

    public void Next()
    {
        SceneManager.LoadScene(4);
    }

}
