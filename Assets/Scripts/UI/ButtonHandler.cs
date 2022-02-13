using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject spawner;
    public GameObject tempSphere;
    [Header("Pause/Play")]
    public GameObject pause;
    public GameObject play;

    public void CreateNewSphere()
    {
        spawner.GetComponent<CreateSphere>().CreateNewSphere(tempSphere);
    }

    public void PauseGameButton()
    {
        GlobalVars.Pause();
    }

    public void ResumeGameButton()
    {
        GlobalVars.Resume();
    }

    private void Update()
    {
        if (GlobalVars.paused)
        {
            pause.SetActive(false);
            play.SetActive(true);
        } else
        {
            play.SetActive(false);
            pause.SetActive(true);
        }
    }

}
