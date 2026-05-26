using System;
using UnityEngine;
using UnityEngine.UI;

public class MeatUI : MonoBehaviour
{
    [SerializeField] private Text _meatCountText;

    private void OnEnable()
    {
        MeatManager.Instance.SubscribeUpdateMeatCount(UpdateMeatCountText);
    }

    private void UpdateMeatCountText(int meatCount)
    {
        _meatCountText.text = meatCount.ToString();
    }
}
