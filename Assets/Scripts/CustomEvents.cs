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

    public UnityEventVector2 TouchDetected = new UnityEventVector2();

    private void Awake()
    {

        SharedInstance = this;
    }

    private void Update()
    {
        // https://docs.unity3d.com/Manual/MobileInput.html
        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                TouchDetected.Invoke(touch.position);
                
            }
        }
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

[System.Serializable]
public class UnityEventVector2 : UnityEvent<Vector2>
{

}
