using UnityEngine;
using UnityEngine.UI;

public class Game_Over : MonoBehaviour
{
    private float fDestroyTime = 0.2f;
    private float fTickTime;

    public Color Color;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject goImage = GameObject.Find("Canvas/Image");
        Color color = goImage.GetComponent<Image>().color;
        fTickTime += Time.deltaTime;
        if (fTickTime >= fDestroyTime)
        {
            color.a += 0.005f;
            goImage.GetComponent<Image>().color = color;
        }
    }
}
