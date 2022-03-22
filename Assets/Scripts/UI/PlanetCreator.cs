using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlanetCreator : MonoBehaviour
{
    [Header("Planet Name")]
    public TMP_InputField planet_name_input_field;
    public string planet_name;

    private void Start()
    {
        planet_name_input_field.onValueChanged.AddListener(delegate { PlanetNameChange(); });
    }

    private void PlanetNameChange()
    {
        planet_name = planet_name_input_field.text;
    }

    public void ClosePlanetCreator()
    {
        gameObject.SetActive(false);
    }
}
