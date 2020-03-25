using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chicken : MonoBehaviour
{
    public float _moveSpeed = 1;//causes .5 Energyburnrate per speedpoint
    public float _detectionRange = 10;//causes 0.1 Energyburnrate per meter
    public Slider _fullnessSlider;
    public GameObject _chickenPrefab;

    public event Action onDeath;

    private float _fullness;
    private float _timeSinceLastPregnancy = 0;
    private ParticleSystem _ps;
    private bool _isBurningEnergy;

    void Awake()
    {
        _Fullness = 0.25f;
        _ps = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        float energyBurnRate = _moveSpeed * 0.25f + _detectionRange * 0.05f;
        _timeSinceLastPregnancy += Time.deltaTime;
        if(_IsBurningEnergy)
            _Fullness -= Time.deltaTime * energyBurnRate / 60;
    }

    public void SpawnBabies()
    {
        _timeSinceLastPregnancy = 0;
        Instantiate(_chickenPrefab, transform.position, Quaternion.identity);
        _Fullness -= 0.3f;
    }

    public void PlaySexyParticle(float duration)
    {
        _ps.Play();
        Invoke("StopSexyParticle", duration);
    }

    public void StopSexyParticle()
    {
        _ps.Stop();
    }

    public void Die()
    {
        if(onDeath != null)
            onDeath();
        Destroy(gameObject);
    }

    public float _Fullness
    {
        get => _fullness;
        set
        {
            _fullness = Mathf.Clamp(value, 0, 1);
            _fullnessSlider.value = _fullness;
            if (_fullness <= 0)
                Die();
        }
    }

    public bool _IsBurningEnergy
    {
        get => _isBurningEnergy;
        set => _isBurningEnergy = value;
    }

    public float _DetectionRange
    {
        get => _detectionRange;
        set => _detectionRange = value;
    }

    public float _TimeSinceLastPregnancy
    {
        get => _timeSinceLastPregnancy;
        set => _timeSinceLastPregnancy = value;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1,1,1,0.15f);
        Gizmos.DrawSphere(transform.position, _DetectionRange);
        Gizmos.color = new Color(1, 1, 1, 1);
    }
}
