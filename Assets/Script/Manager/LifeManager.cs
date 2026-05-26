using System;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;

    private int _lifeCount;

    private event Action<int> _updateLifeCount;

    private void Awake()
    {
        Instance = this;
    }

    public void SubscribeUpdateLifeCount(Action<int> callback)
    {
        _updateLifeCount += callback;
    }

    public void SetLifeCount(int startLifeCount)
    {
        _lifeCount = startLifeCount;
        UpdateLifeCountIcon();
    }

    public void DecreaseLifeCount()
    {
        _lifeCount -= 1;
        UpdateLifeCountIcon();
    }

    private void UpdateLifeCountIcon()
    {
        _updateLifeCount?.Invoke(_lifeCount);
    }

    public int GetLifeCount()
    {
        return _lifeCount;
    }
}