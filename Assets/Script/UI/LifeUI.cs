using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    private List<GameObject> _lifeIconList = new List<GameObject>();

    private void Start()
    {
        LoadLifeIcon();
        WaveManager.Instance.SubscribeLifeDecrease(DecreaseLifeIcon);
    }

    private void LoadLifeIcon()
    {
        var lifeCount = WaveManager.Instance.GetStageMaxLifeCount();

        for(int i = 0; i < lifeCount; i++)
        {
            var loadObject = Resources.Load<GameObject>("Prefab/UI/LifeIcon");
            var iconObject = Instantiate(loadObject, transform);
            _lifeIconList.Add(iconObject);
        }
    }

    private void DecreaseLifeIcon(int lifeCount)
    {
        if (_lifeIconList.Count < lifeCount)
            return;

        _lifeIconList[lifeCount].SetActive(false);
    }
}
