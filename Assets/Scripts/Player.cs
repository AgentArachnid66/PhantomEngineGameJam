using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class Player : MonoBehaviour
{
    public AnimationCurve scoreCurve;

    [SerializeField]
    private int _score =0;

    [SerializeField]
    private float _maxDist = 10f;

    // Start is called before the first frame update
    void Start()
    {
        if (CustomEvents.SharedInstance != null)
        {
            CustomEvents.SharedInstance.CapturedGhost.AddListener(ctx =>
            {
                Debug.Log("Captured Ghost");
                int eva = Mathf.RoundToInt(scoreCurve.Evaluate(ctx));
                _score += eva;
            });
        }
    }
   

    void AttemptCapture(Vector2 screenPos)
    {
        // Construct a ray from the current touch coordinates
        Ray ray = Camera.main.ScreenPointToRay(screenPos);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, _maxDist))
        {
            Ghost _ghost = hit.rigidbody.GetComponent<Ghost>();
           if (_ghost != null)
            {
                _ghost.Capture();
            }
        }
    }

}
