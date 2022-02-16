using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HP_Heart : MonoBehaviour
{
    public GameObject One_Heart_1;
    public GameObject Two_Heart_1;
    public GameObject Three_Heart_1;
    public GameObject Four_Heart_1;
    public GameObject Five_Heart_1;

    public GameObject One_Heart_0;
    public GameObject Two_Heart_0;
    public GameObject Three_Heart_0;
    public GameObject Four_Heart_0;
    public GameObject Five_Heart_0;

    public GameObject UI_Object;
    void Update()
    {
        Player_Control oc = GameObject.Find("통합관리").GetComponent<Player_Control>();
        switch (oc.Player_HP)
        {
            case 0:
                SceneManager.LoadScene("Dead_Scene"); 
                Destroy(UI_Object);

                break;

            case 1:
                One_Heart_1.SetActive(true);
                Two_Heart_1.SetActive(false);
                Three_Heart_1.SetActive(false);
                Four_Heart_1.SetActive(false);
                Five_Heart_1.SetActive(false);

                One_Heart_0.SetActive(false);
                Two_Heart_0.SetActive(true);
                Three_Heart_0.SetActive(true);
                Four_Heart_0.SetActive(true);
                Five_Heart_0.SetActive(true);
                break;

            case 2:
                One_Heart_1.SetActive(true);
                Two_Heart_1.SetActive(true);
                Three_Heart_1.SetActive(false);
                Four_Heart_1.SetActive(false);
                Five_Heart_1.SetActive(false);

                One_Heart_0.SetActive(false);
                Two_Heart_0.SetActive(false);
                Three_Heart_0.SetActive(true);
                Four_Heart_0.SetActive(true);
                Five_Heart_0.SetActive(true);
                break;

            case 3:
                One_Heart_1.SetActive(true);
                Two_Heart_1.SetActive(true);
                Three_Heart_1.SetActive(true);
                Four_Heart_1.SetActive(false);
                Five_Heart_1.SetActive(false);

                One_Heart_0.SetActive(false);
                Two_Heart_0.SetActive(false);
                Three_Heart_0.SetActive(false);
                Four_Heart_0.SetActive(true);
                Five_Heart_0.SetActive(true);
                break;
                
            case 4:
                One_Heart_1.SetActive(true);
                Two_Heart_1.SetActive(true);
                Three_Heart_1.SetActive(true);
                Four_Heart_1.SetActive(true);
                Five_Heart_1.SetActive(false);

                One_Heart_0.SetActive(false);
                Two_Heart_0.SetActive(false);
                Three_Heart_0.SetActive(false);
                Four_Heart_0.SetActive(false);
                Five_Heart_0.SetActive(true);
                break;

            case 5:
                One_Heart_1.SetActive(true);
                Two_Heart_1.SetActive(true);
                Three_Heart_1.SetActive(true);
                Four_Heart_1.SetActive(true);
                Five_Heart_1.SetActive(true);

                One_Heart_0.SetActive(false);
                Two_Heart_0.SetActive(false);
                Three_Heart_0.SetActive(false);
                Four_Heart_0.SetActive(false);
                Five_Heart_0.SetActive(false);
                break;
        }
    }
}
