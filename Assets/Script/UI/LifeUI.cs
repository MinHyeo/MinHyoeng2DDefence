using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    private List<GameObject> _lifeIconList = null;

    private void OnEnable()
    {
        LifeManager.Instance.SubscribeUpdateLifeCount(UpdateLifeIcon);
    }

    private void LoadLifeIcon()
    {
        _lifeIconList = new List<GameObject>();
        var lifeCount = LifeManager.Instance.GetLifeCount();

        var loadObject = Resources.Load<GameObject>("Prefab/UI/MainUI/LifeIcon");
        for (int i = 0; i < lifeCount; i++)
        {
            var iconObject = Instantiate(loadObject, transform);
            _lifeIconList.Add(iconObject);
        }
    }

    private void UpdateLifeIcon(int lifeCount)
    {
        if (_lifeIconList == null)
        {
            LoadLifeIcon();
            return;
        }

        if (_lifeIconList.Count < lifeCount || lifeCount < 0)
            return;

        _lifeIconList[lifeCount].SetActive(false);
    }
}
