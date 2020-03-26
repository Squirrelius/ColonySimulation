using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChickenStatsUI : MonoBehaviour
{
    public Text _chickenCntTxt;
    public Text _avgSpeedTxt;
    public Text _avgDetectionRangeTxt;

    void FixedUpdate()
    {
        Chicken[] chickens = FindObjectsOfType<Chicken>();
        float speedSum = 0;
        float detectionRangeSum = 0;
        foreach(Chicken chicken in chickens)
        {
            speedSum += chicken._MoveSpeed;
            detectionRangeSum += chicken._DetectionRange;
        }

        _chickenCntTxt.text = chickens.Length.ToString();
        _avgSpeedTxt.text = (speedSum / chickens.Length).ToString();
        _avgDetectionRangeTxt.text = (detectionRangeSum / chickens.Length).ToString();
    }
}
