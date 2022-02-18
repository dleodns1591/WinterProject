using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    public static Sound Instance;
    public int Sound_Audio = 1;

    public AudioSource one_Stage;
    public AudioSource two_Stage;
    public AudioSource three_Stage;
    public AudioSource four_Stage;
    public AudioSource five_Stage;
    public AudioSource Boss_Stage;

    public void Awake()
    {
        if (Instance != null)
        {
            Sound_Audio = Instance.GetComponent<Sound>().Sound_Audio;
            Destroy(this.gameObject);
            return;
        }
        Instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void Update()
    {
        if (Sound_Audio == 1)
        {
            one_Stage.Play();
        }
        if (Sound_Audio == 2)
        {
            one_Stage.Stop();
            two_Stage.Play();
        }
    }

    public void Audio()
    {
        Sound_Audio += 1;

    }
}
