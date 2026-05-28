using UnityEngine;

public enum UIRootType : byte
{
    None = 0,
    BackgroundUI,
    MainUI,
    ContentUI,
    PopupUI,
    VeryFrontUI
}

public enum UIType : byte
{
    LoadingUI,
    MainUI,
    HudUI,
    StageSelectUI,

    DNSimplePopup,
    DNMyProfilePopup, // 신규UI추가 1) 새로운 UIType을 추가한다
    DNInventory,
    DNDialogueUI,
    DNInfoBookUI
}

public static class UIManagerExtension
{
    public static string GetUIPath(this UIManager uiManager, UIRootType uiRootType, UIType uiType)
    {
        string path = string.Empty; // "" == string.Empty

        // 신규UI추가 2) Resources.Load를 할 경로를 직접 명시한다
        // 해당 경로는 프로젝트창에서 Resources/Prefabs/UI폴더 내에 있는 RootType 폴더명과 UIType 프리팹 이름과 동일해야 한다! (ex. ContentUI/DNMyProfilePopup)
        path = $"Prefab/UI/{uiRootType}/{uiType}";
        return path;
    }

    public static void ShowStartupUIOnGameStart(this UIManager uiManager)
    {
        //uiManager.OpenLoadingUI();
        //uiManager.OpenUI(UIRootType.MainUI, UIType.MainUI);
        //uiManager.OpenUI(UIRootType.MainUI, UIType.HudUI);
        uiManager.OpenUI(UIRootType.ContentUI, UIType.StageSelectUI);

        // 게임 로비 UI를 여기서 오픈해주자 -> uiManager.
        // MainUI도
    }

    public static void AddHudSlot(this UIManager uiManager, int instanceId, Transform targetTransform)
    {
        var uiBase = uiManager.GetOpenedUI(UIRootType.MainUI, UIType.HudUI);
        if (uiBase == null)
            return;

        if(uiBase is HudUI hudUI)
        {
            hudUI.AddHudSlot(instanceId, targetTransform);
        }
    }

    public static void RemoveHudSlot(this UIManager uiManager, int instanceId)
    {
        var uiBase = uiManager.GetOpenedUI(UIRootType.MainUI, UIType.HudUI);
        if (uiBase == null)
            return;

        if (uiBase is HudUI hudUI)
        {
            hudUI.RemoveHudSlot(instanceId);
        }
    }
}

