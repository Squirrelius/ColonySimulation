using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Chicken : MonoBehaviour
{
    public float _moveSpeed = 1;//causes 1 Energyburnrate per speedpoint
    public float _detectionRange = 10;//causes 0.1 Energyburnrate per meter
    public Slider _fullnessSlider;
    public event Action onDeath;

    private float _fullness;
    private ParticleSystem _ps;

    void Awake()
    {
        _Fullness = 1;
        _ps = GetComponentInChildren<ParticleSystem>();
    }

    void Update()
    {
        float energyBurnRate = _moveSpeed + _detectionRange * 0.1f;

        _Fullness -= Time.deltaTime * energyBurnRate / 60;
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
            _fullness = value;
            _fullnessSlider.value = value;
            if (_fullness <= 0)
                Die();
        }
    }
}
