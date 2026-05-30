using System;
using UnityEngine;

public class LifeManager : MonoBehaviour
{
    public static LifeManager Instance;

    private int _lifeCount;

    private event Action<int> _onUpdateLifeCount;

    private void Awake()
    {
        Instance = this;
    }

    public void SubscribeUpdateLifeCount(Action<int> callback)
    {
        _onUpdateLifeCount += callback;
    }

    public void SetLifeCount(int startLifeCount)
    {
        _lifeCount = startLifeCount;
        UpdateLifeCountIcon();
    }

    public void DecreaseLifeCount()
    {
        if (_lifeCount <= 0)
            return;

        _lifeCount -= 1;
        UpdateLifeCountIcon();
        if (_lifeCount <= 0)
        {
            Debug.Log("게임 실패");
            StageManager.Instance.FailStage();
        }
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