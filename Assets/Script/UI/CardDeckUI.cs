using System.Collections.Generic;
using UnityEngine;

public class CardDeckUI : MonoBehaviour
{
    [SerializeField] private UIButton _cardDrowButton;
    [SerializeField] private RectTransform _cardListTransform;

    private Dictionary<int, GameObject> _cardList = new Dictionary<int, GameObject>();
    private int _cardInstanceId = 0;

    private void OnEnable()
    {
        _cardDrowButton.BindOnClickButtonEvent(DrawCard);
    }

    private void DrawCard()
    {
        string path = $"Prefab/UI/MainUI/CardUI";
        GameObject loadedObj = (GameObject)Resources.Load(path);
        GameObject gObj = Instantiate(loadedObj, _cardListTransform);

        _cardList[_cardInstanceId++] = gObj;

        string randomTowerId = GetRandomTowerId();
        gObj.GetComponent<CardUI>().InitCardUI(_cardInstanceId, randomTowerId);

        //float width = _cardListTransform.sizeDelta.x;
        //float WidthSize = width / _cardList.Count;
    }

    private string GetRandomTowerId()
    {
        List<string> allTowerDataIds = GameDataManager.Instance.GetAllTowerIds();
        int index = Random.Range(0, allTowerDataIds.Count);
        return allTowerDataIds[index];
    }
}