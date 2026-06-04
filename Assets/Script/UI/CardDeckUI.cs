using System.Collections.Generic;
using UnityEngine;

public class CardDeckUI : MonoBehaviour
{
    [SerializeField] private UIButton _cardDrawButton;
    [SerializeField] private RectTransform _cardListTransform;

    private Dictionary<int, GameObject> _cardList = new Dictionary<int, GameObject>();
    private int _cardInstanceId = 0;
    private int _cardDrawPrice = 50;

    private void OnEnable()
    {
        _cardDrawButton.BindOnClickButtonEvent(DrawCard);
    }

    private void OnDisable()
    {
        foreach(var pair in _cardList)
        {
            Destroy(pair.Value);
        }
        _cardList.Clear();
    }

    private void DrawCard()
    {
        if (StageManager.Instance.CanDrawCard() == false)
            return;

        string path = $"Prefab/UI/MainUI/CardUI";
        GameObject loadedObj = (GameObject)Resources.Load(path);
        GameObject gObj = Instantiate(loadedObj, _cardListTransform);

        _cardList[_cardInstanceId++] = gObj;

        string randomTowerId = GetRandomTowerId();
        ICardAction iCardAction = gObj.AddComponent<StageCardAction>();
        gObj.GetComponent<CardUI>().InitCardUI(_cardInstanceId, randomTowerId, iCardAction);

        //MeatManager.Instance.DecreaseMeatCount(_cardDrawPrice);
        StageManager.Instance.DecreaseMeatCount(_cardDrawPrice);
    }

    private string GetRandomTowerId()
    {
        List<string> allTowerDataIds = GameDataManager.Instance.GetAllTowerIds();
        int index = Random.Range(0, allTowerDataIds.Count);
        return allTowerDataIds[index];
    }
}