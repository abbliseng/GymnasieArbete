using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonHandler : MonoBehaviour
{
    public GameObject spawner;
    public GameObject tempSphere;

    public void CreateNewSphere()
    {
        spawner.GetComponent<CreateSphere>().CreateNewSphere(tempSphere);
    }
}
