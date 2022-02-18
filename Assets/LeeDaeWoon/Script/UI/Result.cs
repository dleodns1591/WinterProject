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

    public Image Black;
    float time = 0f;
    float F_time = 1f;
    public int Hp;

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
        GameObject.Find("烹钦包府").GetComponent<Player_Control>().Player_HP -= 1;
        if (GameObject.Find("烹钦包府").GetComponent<Player_Control>().Player_HP == 0)
        {
            Destroy(GameObject.Find("烹钦包府"));
        }
        SceneManager.LoadScene(4);
    }

    public void Next()
    {
        if (GameObject.Find("Stage_test").GetComponent<Test_Stage>().Stage == 7)
        {
            Destroy(GameObject.Find("烹钦包府"));
            SceneManager.LoadScene(5);
        }
        SceneManager.LoadScene(4);
    }

    public void Fade()
    {
        StartCoroutine(Fade_Flow());
        Invoke("Next", 2.45f);
    }

    IEnumerator Fade_Flow()
    {
        Black.gameObject.SetActive(true);
        time = 0f;
        Color Alpha = Black.color;
        while (Alpha.a < 1f)
        {
            time += Time.deltaTime / F_time;
            Alpha.a = Mathf.Lerp(0, 1, time);
            Black.color = Alpha;
            yield return null;
        }

        time = 0f;
        yield return new WaitForSeconds(1f);

        while (Alpha.a > 0f)
        {
            time += Time.deltaTime / F_time;
            Alpha.a = Mathf.Lerp(1, 0, time);
            Black.color = Alpha;
            yield return null;
        }
        Black.gameObject.SetActive(false);
        yield return null;
    }

}
