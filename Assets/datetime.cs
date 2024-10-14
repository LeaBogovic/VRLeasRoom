using UnityEngine;
using System;
using System.Globalization;

public class datetime : MonoBehaviour
{
    [SerializeField]
    public Transform _hourHand;
    [SerializeField]
    public Transform _minuteHand;
    [SerializeField]
    public Transform _secondHand;

    public int _previousSeconds;
    public int _timeInSeconds;

    private void Update()
    {
        ConvertTimeToSeconds();
        RotateClockHands();

    }
    public int ConvertTimeToSeconds()
    {
        int currectSeconds = DateTime.Now.Second;
        int currentMinute = DateTime.Now.Minute;
        int currentHour = DateTime.Now.Hour;

        if (currentHour >= 12)
        {
            currentHour -= 12;
        }
        _timeInSeconds = currectSeconds + (currentMinute * 60) + (currentHour * 60 * 60);
        return _timeInSeconds;
    }
    void RotateClockHands()
    {
        float secondhandPerSecond = 360f / 60f;
        float minutehandPerSecond = 360f / (60f * 60f);
        float hourhandPerSecond = 360f / (60f * 60f * 12f);

        if (_timeInSeconds != _previousSeconds)
        {
            Debug.Log(_timeInSeconds);
            _secondHand.localRotation = Quaternion.Euler(_timeInSeconds * secondhandPerSecond, 0, 0);
            _minuteHand.localRotation = Quaternion.Euler(_timeInSeconds * minutehandPerSecond, 0, 0);
            _hourHand.localRotation = Quaternion.Euler(_timeInSeconds * hourhandPerSecond, 0, 0);
        }
        _previousSeconds = _timeInSeconds;
    }
}

