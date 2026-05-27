using System;
using UnityEngine;

public class MeatManager : MonoBehaviour
{
    public static MeatManager Instance;

    private int _meatCount;

    private event Action<int> _onUpdateMeatCount;

    private void Awake()
    {
        Instance = this;
    }

    public void SubscribeUpdateMeatCount(Action<int> callback)
    {
        _onUpdateMeatCount += callback;
    }

    public void SetStartMeatCount(int startMeatCount)
    {
        _meatCount = startMeatCount;
        UpdateMeatCountText();
    }

    public void IncreaseMeatCount(int increaseCount)
    {
        _meatCount += increaseCount;
        UpdateMeatCountText();
    }

    public void DecreaseMeatCount(int decreaseCount)
    {
        _meatCount -= decreaseCount;
        UpdateMeatCountText();
    }
    
    private void UpdateMeatCountText()
    {
        _onUpdateMeatCount?.Invoke(_meatCount);
    }

    public int GetMeatCount()
    {
        return _meatCount;
    }
}
