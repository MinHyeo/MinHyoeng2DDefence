using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MeatUI : MonoBehaviour
{
    [Header("고기 재화 텍스트")]
    [SerializeField] private TextMeshProUGUI _meatCountText;

    private void OnEnable()
    {
        StageManager.Instance.BindOndMeatCountUpdate(UpdateMeatCountText);
        //MeatManager.Instance.Bind(UpdateMeatCountText);
    }

    private void UpdateMeatCountText(int meatCount)
    {
        _meatCountText.text = meatCount.ToString();
    }
}
