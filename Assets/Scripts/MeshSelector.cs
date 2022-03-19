using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct MeshInfo
{
    public MeshFilter meshFilter;
    public Material material;



    public MeshInfo(MeshFilter filter, Material renderer)
    {
        this.material = renderer;
        this.meshFilter = filter;
    }
}


public class MeshSelector : MonoBehaviour
{
    public static MeshSelector SharedInstance { get; private set; }

    [SerializeField]
    public MeshInfo[] meshes;


    private void Awake()
    {
        SharedInstance = this;
    }


    public void ChangeGameObject(float input, ref GameObject gameObject)
    {
        int index = Mathf.FloorToInt(input * meshes.Length);

        Debug.Log($"The Generated Index is: {index}");

        gameObject.GetComponent<MeshFilter>().mesh = meshes[index].meshFilter.mesh;
        gameObject.GetComponent<Renderer>().material = meshes[index].material;

    }
}
