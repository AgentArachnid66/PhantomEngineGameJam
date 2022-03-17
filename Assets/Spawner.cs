using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private int numSamples = 0;
    [SerializeField] private float radius = 1f;

    [SerializeField] private Vector3[] samples;
    [SerializeField] private LayerMask _mask;
    public float power;

    // Sample a disc of points to find a spawn position
    // poisson disc sampling

    private void Start()
    {
        
    }

    
    [ContextMenu("Test Disc")]
    void TestPoisson()
    {
        PoissonDiscSampling();
    }

    // Snippet from https://github.com/SebLague/Boids/blob/master/Assets/Scripts/BoidHelper.cs
    void PoissonDiscSampling()
    {
        samples = new Vector3[numSamples];

        float goldenRatio = (1 + Mathf.Sqrt(5)) / 2;
        float angleIncrement = Mathf.PI * 2 * goldenRatio;

        for (int i = 0; i < numSamples; i++)
        {
            float t = (float)i / numSamples;
            float inclination = Mathf.Acos(1 - 2 * t);
            float azimuth = angleIncrement * i;

            float x = Mathf.Sin(inclination) * Mathf.Cos(azimuth);
            float y = Mathf.Sin(inclination) * Mathf.Sin(azimuth);
            float z = Mathf.Cos(inclination);
            samples[i] = new Vector3(x, y, z) * radius;
        }
    }


    // Find first best location
    [ContextMenu("Test")]
    void GetBestPoint()
    {
        // Iterate over all points
        for (int i = 0; i < samples.Length; i++)
        {
            // Get midpoint of mesh
            Vector3 mid = samples[i];

            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.position + (new Vector3(samples[i].x, samples[i].y)), 500f, _mask))
                {
                Debug.Log("Raycast Hit");
                Debug.DrawLine(Camera.main.transform.position, Camera.main.transform.position + (new Vector3(samples[i].x, samples[i].y)));
                if (Physics.CheckSphere(mid, radius))
                {
                    Debug.Log("Hit");
                }
            }
            // If length of any hit points are less than predefined value then move on
        }
    }


    // Spawn a well at that location


    // Activate the well to spawn ghosts

    private void OnDrawGizmos()
    {
        for(int i = 0; i < samples.Length; i++)
        {
            Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + (new Vector3(samples[i].x, samples[i].y)));
        }
    }

}
