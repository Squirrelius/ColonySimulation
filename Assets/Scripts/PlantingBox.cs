using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantingBox : MonoBehaviour
{
    public float _bushRespawnCD = 10;
    public GameObject _cultivatedBushPrefab;

    private Bush _currentBush;
    private float _lastBushDeathTime = 0;
    
    void Update()
    {
        if(_currentBush == null && Time.time >= _lastBushDeathTime + _bushRespawnCD)
        {
            _currentBush = Instantiate(_cultivatedBushPrefab, transform.position, Quaternion.identity).GetComponent<Bush>();
            _currentBush.onDie += OnBushDeath;
        }
    }

    private void OnBushDeath(Bush obj)
    {
        _lastBushDeathTime = Time.time;
        _currentBush.onDie -= OnBushDeath;
    }
}
