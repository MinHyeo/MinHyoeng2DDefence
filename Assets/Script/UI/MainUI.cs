using UnityEngine;

public class MainUI : UIBase
{
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
