using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class CustomEvents : MonoBehaviour
{
    public static CustomEvents SharedInstance;

    [SerializeField]
    public UnityEventFloat CapturedGhost = new UnityEventFloat();

    [SerializeField]
    public UnityEventInt GhostNumberChanged = new UnityEventInt();


    private void Awake()
    {

        SharedInstance = this;
    }
}


[System.Serializable]
public class UnityEventFloat : UnityEvent<float>
{

}

[System.Serializable]
public class UnityEventInt : UnityEvent<int>
{

}