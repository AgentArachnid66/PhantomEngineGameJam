using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SourceObjectPool : MonoBehaviour
{
    public static SourceObjectPool SharedInstance;

    public Source source;
    public int numToPool;
    public Source[] sources;



    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        sources = new Source[numToPool];
        GameObject temp;
        for (int i = 0; i < numToPool; i++)
        {
            temp = Instantiate(source.gameObject);
            temp.SetActive(false);
            sources[i] = temp.GetComponent<Source>();
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < numToPool; i++)
        {
            if (!sources[i].gameObject.activeInHierarchy)
            {
                return sources[i].gameObject;
            }
        }

        return null;
    }

    public Source GetPooledSource()
    {
        for (int i = 0; i < numToPool; i++)
        {
            if (!sources[i].gameObject.activeInHierarchy)
            {
                return sources[i];
            }
        }

        return null;
    }
}
