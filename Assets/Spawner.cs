using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PhantomTech;


public class Spawner : MonoBehaviour
{
    public Rigidbody m_rigidbody;

    [SerializeField] private float radius;
    private Rigidbody[] _rigidbodies = new Rigidbody[2];
    private bool[] _active = new bool[2];

    [SerializeField]private int _currIndex = 0;

    private Vector3 test;

    private void Start()
    {
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            _rigidbodies[i] = Instantiate(m_rigidbody.gameObject).GetComponent<Rigidbody>();
            _rigidbodies[i].gameObject.SetActive(false);
        }


        InvokeRepeating("SpawnSourcesInArea", 3f, 1f);

        /*
        PhantomTech.Spatial.Meshing.MeshGenerated += (ctx =>
        {
          
        }
        }
            );
            */
    }

    [ContextMenu("Spawn Sources around player")]
    private void SpawnSourcesInArea()
    {
            _currIndex = GetRigidbody();

        Rigidbody _currRigidbody = _currIndex >= 0 ? _rigidbodies[_currIndex] : null;
        if (_currRigidbody != null)
        {
            Transform device = PScene.Instance.m_Device.transform;

            Vector3 test = device.position + (Random.onUnitSphere * radius);

            test.y = device.position.y;
            Debug.Log($"Device Position: {device.position} and the Test Position is: {test}");
            Debug.DrawLine(Camera.main.transform.position, test, Color.red);

            _currRigidbody.velocity = Vector3.zero;
            _currRigidbody.angularVelocity = Vector3.zero;
            _currRigidbody.transform.position = test;
            _currRigidbody.gameObject.SetActive(true);

            StartCoroutine(CheckRigidbody(_currRigidbody, _currIndex));
            StartCoroutine(ForceResetAfterTime(4f, _currIndex));

        }

    }


    private void SpawnSource(Vector3 position)
    {
        GameObject newSource = SourceObjectPool.SharedInstance.GetPooledObject();
        if (newSource != null)
        {
            newSource.transform.position = position;
            newSource.transform.rotation = Quaternion.Euler(Vector3.up);
            newSource.SetActive(true);
        }
    }



    private IEnumerator CheckRigidbody(Rigidbody rigidbody, int index)
    {
        yield return new WaitForSeconds(1f);
        bool stopped = false;
        while (!stopped)
        {
            yield return null;
            stopped = rigidbody.IsSleeping();
            _active[index] = !stopped;
        }

        Debug.Log("Rigidbidy Stopped Moving");
        SpawnSource(rigidbody.position);
        _active[index] = false;
        rigidbody.gameObject.SetActive(false);
    }

    private IEnumerator ForceResetAfterTime(float cooldown, int index) {

        yield return new WaitForSecondsRealtime(cooldown);

        if (_active[index])
        {
            StopCoroutine(CheckRigidbody(_rigidbodies[index], index));
            _rigidbodies[index].gameObject.SetActive(false);

        }
    }

    private int GetRigidbody()
    {
        for (int i = 0; i < _rigidbodies.Length; i++)
        {
            if (!_rigidbodies[i].gameObject.activeInHierarchy)
            {
                return i;
            }
        }
        return -1;
    }
}
