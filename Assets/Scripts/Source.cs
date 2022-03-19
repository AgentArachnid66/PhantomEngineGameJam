using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour
{
    public float spawnCooldown= 1f;
    public int amountToSpawn;
    public Transform spawnPoint;
    public float scaleOffset = 0f;

    private bool _canSpawn = true;
    private Ghost[] _ghosts;
    [SerializeField] private int _currIndex = 0;
    private Ghost _currGhost;

    [Range(1, 20)]
    [SerializeField] private float _radius;

    private void Start()
    {
        _ghosts = new Ghost[amountToSpawn];
    }

    private void OnEnable()
    {
        InvokeRepeating("SpawnGhost", 1f, 2f);
        scaleOffset = Random.Range(-0.25f, 0.25f);
    }

    private void OnDisable()
    {
        CancelInvoke("SpawnGhost");
        _currIndex = 0;
    }

    [ContextMenu("Spawn Ghost")]
    void SpawnGhost()
    {
        if (_canSpawn && _currIndex < amountToSpawn)
        {
            Debug.Log("Spawning Ghost");
            _currGhost = GhostObjectPool.SharedInstance.GetPooledGhost();
            if (_currGhost != null)
            {
                _ghosts[_currIndex] = _currGhost;
                _currGhost.transform.position = spawnPoint.position;
                _currGhost.transform.rotation = spawnPoint.rotation;
                float _scale = GhostObjectPool.SharedInstance.scaleCurve.Evaluate(Mathf.Clamp(Random.Range(0.35f, 1f) + scaleOffset, 0.35f, 1f));
                _currGhost.transform.localScale = new Vector3(_scale, _scale, _scale);
                _currGhost.gameObject.SetActive(true);
                GameObject ghost = _currGhost.gameObject;
                MeshSelector.SharedInstance.ChangeGameObject(_currGhost._normalisedMagnitude, ref ghost);
                _currGhost.BeginPathfinding(spawnPoint.position + Random.onUnitSphere * _radius);
                StartCoroutine(ResetCooldown());
                CustomEvents.SharedInstance.GhostNumberChanged.Invoke(1);

                _currIndex++;
            }
            else
            {
                Debug.Log("No More Ghosts in Pool");
            }
        }
        else
        {
            Debug.LogWarning($"Cannot Spawn Ghost current Index {_currIndex}");

            // If source has spawned all of which it was allocated then it will be turned off
            if(_currIndex >= amountToSpawn)
            {
                gameObject.SetActive(false);
            }
        }
    }


    IEnumerator ResetCooldown()
    {
        _canSpawn = false;

        yield return new WaitForSeconds(spawnCooldown);

        _canSpawn = true;

    }
}