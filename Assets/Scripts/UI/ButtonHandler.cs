using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ButtonHandler : MonoBehaviour
{
    public GameObject spawner;
    public GameObject tempSphere;
    [Header("Pause/Play")]
    public GameObject pause;
    public GameObject play;
    public GameObject esc_menu;

    [Header("Time")]
    public float sped = 0f;
    public Slider timeSlider;
    public TMP_InputField timeInputField;

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

    public void EscContinue()
    {
        esc_menu.SetActive(false);
    }

    public void EscQuit()
    {
        Application.Quit();
    }

    private void Update()
    {
        KeyHandler();

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

    void KeyHandler()
    {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus) || Input.GetKeyDown(KeyCode.P))
        {
            GlobalVars.ChangeSpeed(10f);
            UpdateTimeDisplay(GlobalVars.timeScale);
        }
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            GlobalVars.ChangeSpeed(-10f);
            UpdateTimeDisplay(GlobalVars.timeScale);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (GlobalVars.paused)
            {
                GlobalVars.Resume();
            }
            else
            {
                GlobalVars.Pause();
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            esc_menu.SetActive(!esc_menu.activeSelf);
        }
    }

    private void OnValidate()
    {
        UpdateTimeDisplay(sped);
        GlobalVars.timeScale = sped;
        // Time.timeScale = GlobalVars.timeScale;
    }

    public void UpdateTimeDisplay(float targetSpeed)
    {
        if (targetSpeed <= timeSlider.maxValue)
        {
            timeSlider.value = targetSpeed;
        }
        else
        {
            timeSlider.value = timeSlider.maxValue;
        }
        timeInputField.text = ((int)targetSpeed).ToString();
    }

    private void Start()
    {
        // Adds a listener to the main slider and invokes a method when the value changes.
        timeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        timeInputField.onValueChanged.AddListener(delegate { TextValueChangeCheck(); });

        // InitialVelocity();
    }

    public void ValueChangeCheck()
    {
        GlobalVars.timeScale = timeSlider.value;
        UpdateTimeDisplay(GlobalVars.timeScale);
    }

    public void TextValueChangeCheck()
    {
        GlobalVars.timeScale = int.Parse(timeInputField.text);
        if (GlobalVars.timeScale < 1f)
        {
            GlobalVars.timeScale = 1f;
        }
        UpdateTimeDisplay(GlobalVars.timeScale);
    }
}
