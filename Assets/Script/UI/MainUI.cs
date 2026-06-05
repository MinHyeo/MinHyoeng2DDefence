using UnityEngine;

public class MainUI : UIBase
{
    [Header("치트 버튼")]
    [SerializeField] UIButton cheatButton;

    private void OnEnable()
    {
        cheatButton.BindOnClickButtonEvent(ClearStage);
    }

    private void ClearStage()
    {
        StageManager.Instance.ClearStage();
    }
}
