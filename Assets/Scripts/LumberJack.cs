using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemTypes
{
    None,
    Wood,
    Meat
}

public class LumberJack : MonoBehaviour
{
    public Slider _fullnessSlider;
    public Image _carriedItemIcon;
    public Sprite _WoodSprite;
    public Sprite _MeatSprite;
    private ItemTypes _carriedItem;

    [SerializeField] private float _energyBurnRate = 0.5f;//Determines how many % fullness is lost per minute
    private float _fullness;

    private void Awake()
    {
        _Fullness = 1;
    }

    private void Update()
    {
        _Fullness -= Time.deltaTime * _energyBurnRate / 60;
    }

    public void Die()
    {
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

    public ItemTypes _CarriedItem
    {
        get => _carriedItem;
        set
        {
            _carriedItem = value;
            if (_carriedItem == ItemTypes.None)
                _carriedItemIcon.enabled = false;
            else
            {
                _carriedItemIcon.enabled = true;

                if (_carriedItem == ItemTypes.Wood)
                    _carriedItemIcon.sprite = _WoodSprite;
                else if (_carriedItem == ItemTypes.Meat)
                    _carriedItemIcon.sprite = _MeatSprite;
            }
        }
    }
}
