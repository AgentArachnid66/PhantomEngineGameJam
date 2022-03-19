using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UI_Controller : MonoBehaviour
{
    [SerializeField]
    public UnityEventInt UpdateNumberFree = new UnityEventInt();
    [SerializeField]
    public UnityEventInt UpdateNumberCaptured = new UnityEventInt();

    private int numFree = 0;
    private int numCaptured = 0;

    private void Start()
    {
        CustomEvents.SharedInstance.CapturedGhost.AddListener(UpdateCaptured);
        CustomEvents.SharedInstance.GhostNumberChanged.AddListener(UpdateNumber);

    }

    public void UpdateNumber(int arg0)
    {
        numFree += arg0;
        UpdateNumberFree.Invoke(numFree);
    }

    public void UpdateCaptured(float arg0)
    {
        numCaptured ++;
        UpdateNumberCaptured.Invoke(numCaptured);
    }
}