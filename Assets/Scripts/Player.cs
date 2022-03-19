using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public AnimationCurve scoreCurve;

    [SerializeField]
    private int _score =0;

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
