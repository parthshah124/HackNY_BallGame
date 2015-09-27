using UnityEngine;
using System.Collections;

public class StartButton : MonoBehaviour {

    public static bool play = false;
    //public Text ButtonText;

    public void PlayButtonCLickEvent()
    {
        if (play == false)
        {
            Application.LoadLevel(0);
            play = true;
        }
    }

    public void RestartButtonCLickEvent()
    {
        Application.LoadLevel(0);
        play = false;
    }
}
