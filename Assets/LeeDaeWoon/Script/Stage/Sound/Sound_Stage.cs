using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Stage : MonoBehaviour
{
    public int Sound;
    public void Awake()
    {
        Sound = GameObject.Find("AudioSound_test").GetComponent<Test_Sound>().sound;
        this.transform.GetChild(Sound - 1).gameObject.SetActive(true);
    }

    public void Update()
    {

    }

    public void Next_Stage()
    {
        new WaitForSeconds(3f);
        GameObject.Find("AudioSound_test").GetComponent<Test_Sound>().sound += 1;
        Destroy(this.gameObject);
    }
}
