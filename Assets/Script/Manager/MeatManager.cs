using System;
using UnityEngine;

public class MeatManager
{
    private int _meatCount;

    private event Action<int> _onUpdateMeatCount;

    public void BindOndMeatCountUpdate(Action<int> callback)
    {
        _onUpdateMeatCount += callback;
    }

    public void UnBindOndMeatCountUpdate()
    {
        _onUpdateMeatCount = null;
    }

    public void SetStartMeatCount(int startMeatCount)
    {
        _meatCount = startMeatCount;
        UpdateMeatCountText();
    }

    public void UpdateMeatCount(int meatAmount)
    {
        _meatCount += meatAmount;
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
