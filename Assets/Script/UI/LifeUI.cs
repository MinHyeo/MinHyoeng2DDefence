using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    private List<GameObject> _lifeIconList = null;

    private void OnEnable()
    {
        StageManager.Instance.BindOnLifeIconUpdate(UpdateLifeIcon);
    }

    private void LoadLifeIcon(int lifeCount)
    {
        _lifeIconList = new List<GameObject>();

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
            LoadLifeIcon(lifeCount);
            return;
        }

        if (_lifeIconList.Count < lifeCount || lifeCount < 0)
            return;

        for(int i = 0; i < _lifeIconList.Count; i++)
        {
            _lifeIconList[i].SetActive(i < lifeCount);
        }
    }
}
