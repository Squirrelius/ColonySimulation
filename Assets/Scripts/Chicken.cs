using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Chicken : MonoBehaviour
{
    private float _moveSpeed = 3.5f;
    public float _detectionRange = 10;
    public float _baseEnergyBurn = 0.25f;
    public float _energyPerSpeed = 0.1f;//How much more energy is burned per _moveSpeed
    public float _energyPerRange = 0.025f;//How much energy is burned per _detectionRange
    public Slider _fullnessSlider;
    public GameObject _chickenPrefab;

    public event Action onDeath;

    [SerializeField] private float _fullness;
    private float _timeSinceLastPregnancy = 0;
    private ParticleSystem _ps;
    private bool _isBurningEnergy = true;

    void Awake()
    {
        _ps = GetComponentInChildren<ParticleSystem>();
        GetComponent<NavMeshAgent>().speed = _MoveSpeed;
    }

    void Update()
    {
        float energyBurnRate = _MoveSpeed * _energyPerSpeed + _detectionRange * _energyPerRange;
        _timeSinceLastPregnancy += Time.deltaTime;
        if (_IsBurningEnergy)
            _Fullness -= Time.deltaTime * (_baseEnergyBurn + energyBurnRate) / 60;
    }

    public void SpawnBabies(Chicken lovePartner)
    {
        _timeSinceLastPregnancy = 0;
        Chicken newChick = Instantiate(_chickenPrefab, transform.position, Quaternion.identity).GetComponent<Chicken>();
        newChick._Fullness = 0.3f;
        //Inherit Stats from Parent and mutate them a bit
        newChick._DetectionRange = _DetectionRange + UnityEngine.Random.Range(-2f, 2f);
        newChick._MoveSpeed = lovePartner._MoveSpeed + UnityEngine.Random.Range(-0.3f, 0.3f);
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
        if (onDeath != null)
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
    public float _MoveSpeed
    {
        get => _moveSpeed;
        set
        {
            _moveSpeed = value;
            GetComponent<NavMeshAgent>().speed = _moveSpeed;
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(1, 1, 1, 0.25f);
        Gizmos.DrawSphere(transform.position, _DetectionRange);
        Gizmos.color = new Color(1, 1, 1, 1);
    }
}
