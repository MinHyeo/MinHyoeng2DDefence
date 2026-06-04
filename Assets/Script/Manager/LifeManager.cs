using System;
using UnityEngine;

public class LifeManager
{
    private int _lifeCount;

    private event Action<int> _onUpdateLifeCount;

    public void BindLifeIconUpdate(Action<int> callback)
    {
        _onUpdateLifeCount += callback;
    }

    public void UnBindLifeIconUpdate()
    {
        _onUpdateLifeCount = null;
    }

    public void SetLifeCount(int startLifeCount)
    {
        _lifeCount = startLifeCount;
        UpdateLifeCountIcon();
    }

    public bool DecreaseLifeAndCheckDeath()
    {
        _lifeCount -= 1;
        UpdateLifeCountIcon();
        if (_lifeCount <= 0)
        {
            Debug.Log("게임 실패");
            return true;
        }
        return false;
    }

    private void UpdateLifeCountIcon()
    {
        _onUpdateLifeCount?.Invoke(_lifeCount);
    }

    public int GetLifeCount()
    {
        return _lifeCount;
    }
}