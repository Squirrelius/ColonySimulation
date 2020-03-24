using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Storage : MonoBehaviour
{
    public Text _woodAmt;
    public Text _meatAmt;

    private int _wood = 0;
    private int _meat = 0;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public int _Wood
    {
        get => _wood;
        set
        {
            _wood = value;
            _woodAmt.text = value.ToString();
        }
    }
    public int _Meat
    {
        get => _meat;
        set
        {
            _meat = value;
            _meatAmt.text = value.ToString();
        }
    }
}
