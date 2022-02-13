using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Simulation : MonoBehaviour
{
    CelestialBody[] bodies;
    VirtualBody[] vBodies;
    static Simulation instance;
    private float timeElapsed;
    [Header("Time")]
    public float sped = 1f;
    public Slider timeSlider;
    public TMP_InputField timeInputField;

    private void OnValidate() {
        UpdateTimeDisplay(sped);
        GlobalVars.timeScale = sped;
        // Time.timeScale = GlobalVars.timeScale;
    }

    public void UpdateTimeDisplay(float targetSpeed)
    {
        if (targetSpeed <= timeSlider.maxValue)
        {
            timeSlider.value = targetSpeed;
        } else
        {
            timeSlider.value = timeSlider.maxValue;
        }
        timeInputField.text = ((int)targetSpeed).ToString();
    }

    void Awake () {
        Time.fixedDeltaTime = GlobalVars.physicsTimeStep;
        // Debug.Log ("Setting fixedDeltaTime to: " + GlobalVars.physicsTimeStep);
    }

    void KeyHandler() {
        if (Input.GetKeyDown(KeyCode.Plus) || Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            GlobalVars.ChangeSpeed(1f);
            UpdateTimeDisplay(GlobalVars.timeScale);
        }
        if (Input.GetKeyDown(KeyCode.Minus) || Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            GlobalVars.ChangeSpeed(-1f);
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
    }

    private void Start()
    {
        // Adds a listener to the main slider and invokes a method when the value changes.
        timeSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        timeInputField.onValueChanged.AddListener(delegate { TextValueChangeCheck(); });
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

    private void Update()
    {
        KeyHandler();
    }

    private void FixedUpdate() {
        // Time.timeScale = GlobalVars.timeScale;
        if (!GlobalVars.paused)
        {
            if (GlobalVars.simulateCelestialBodies)
            {
                UpdateCelestialBodyPhysics();
            }
        }
    }

    private void UpdateCelestialBodyPhysics() {
        bodies = FindObjectsOfType<CelestialBody> (); // TODO Optimize, must be better way then to check all the time.
        for (int i = 0; i < bodies.Length; i++) {
            Vector3 acceleration = CalculateAcceleration (bodies[i].transform.position, bodies[i]);
            bodies[i].UpdateVelocity (acceleration, GlobalVars.physicsTimeStep * GlobalVars.timeScale);
        }

        for (int i = 0; i < bodies.Length; i++) {
            bodies[i].UpdatePosition (GlobalVars.physicsTimeStep * GlobalVars.timeScale);
        }
    }

    public static Vector3 CalculateAcceleration (Vector3 point, CelestialBody ignoreBody) {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in Instance.bodies) {
            if (body != ignoreBody) {
                float sqrDst = (body.transform.position - point).sqrMagnitude;
                if ((sqrDst) < (body.radius + ignoreBody.radius)) {
                    return new Vector3(0,0,0); //TODO Should probs merge or explode here
                }
                Vector3 forceDir = (body.transform.position - point).normalized;
                acceleration += forceDir * GlobalVars.gravitationalConstant * body.mass / sqrDst;
            }
        }

        return acceleration;
    }
    // TODO Too much code is reused, should be able to be merged right?
    public static Vector3 CalculateAcceleration (Vector3 point, VirtualBody ignoreBody) {
        Vector3 acceleration = Vector3.zero;
        foreach (var body in Instance.vBodies) {
            if (body != ignoreBody) {
                float sqrDst = (body.transform.position - point).sqrMagnitude;
                if ((sqrDst) < (body.radius + ignoreBody.radius)) {
                    return new Vector3(0,0,0); //TODO Should probs merge or explode here
                }
                Vector3 forceDir = (body.transform.position - point).normalized;
                acceleration += forceDir * GlobalVars.gravitationalConstant * body.mass / sqrDst;
            }
        }
        return acceleration;
    }

    public static CelestialBody[] Bodies {
        get {
            return Instance.bodies;
        }
    }

    static Simulation Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<Simulation>();
            }
            return instance;
        }
    }
}
