using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomTech;


public class Ghost : MonoBehaviour
{
    public PhantomTech.Navigation.Pathfinding pathfinding;
    

    
    public float _normalisedMagnitude { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        pathfinding.ReachedTarget.AddListener(FindNewTarget);
    }

    private void OnEnable()
    {
        _normalisedMagnitude = transform.localScale.x / GhostObjectPool.SharedInstance.maxScale;
        pathfinding.m_speed = GhostObjectPool.SharedInstance.speedCurve.Evaluate(_normalisedMagnitude);
        StartCoroutine(FadeAway());
    }

    private void OnDisable()
    {
        StopCoroutine(FadeAway());
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FindNewTarget()
    {
        BeginPathfinding(transform.position + Random.onUnitSphere * 10f);
    }

    public void BeginPathfinding(Vector3 _target)
    {
        pathfinding.SetTarget(_target);
    }


    [ContextMenu("Capture Ghost")]
    public void Capture()
    {
        gameObject.SetActive(false);
        CustomEvents.SharedInstance.CapturedGhost.Invoke(_normalisedMagnitude);
        CustomEvents.SharedInstance.GhostNumberChanged.Invoke(-1);
    }


    IEnumerator FadeAway()
    {
        float delay = GhostObjectPool.SharedInstance.cooldownCurve.Evaluate(_normalisedMagnitude);

        Debug.Log($"This Ghosts delay is {delay}");

        yield return new WaitForSecondsRealtime(delay);

        gameObject.SetActive(false);
        CustomEvents.SharedInstance.GhostNumberChanged.Invoke(-1);
    }


}
