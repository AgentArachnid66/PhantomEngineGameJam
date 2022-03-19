using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostObjectPool : MonoBehaviour
{
    public static GhostObjectPool SharedInstance;

    public Ghost ghost;
    public int numToPool;
    public Ghost[] ghosts;
    public float maxScale=2f;
    public AnimationCurve speedCurve;
    public AnimationCurve cooldownCurve;
    public AnimationCurve scaleCurve;



    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        maxScale = scaleCurve.keys[scaleCurve.length - 1].value;
        ghosts = new Ghost[numToPool];
        GameObject temp;
        for (int i = 0; i < numToPool; i++)
        {
            temp = Instantiate(ghost.gameObject);
            temp.SetActive(false);
            ghosts[i] = temp.GetComponent<Ghost>();
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < numToPool; i++)
        {
            if (!ghosts[i].gameObject.activeInHierarchy)
            {
                return ghosts[i].gameObject;
            }
        }

        return null;
    }

    public Ghost GetPooledGhost()
    {
        for (int i = 0; i < numToPool; i++)
        {
            if (!ghosts[i].gameObject.activeInHierarchy)
            {
                return ghosts[i];
            }
        }

        return null;
    }
}
