using System.IO;
using UnityEngine;

public class NetworkManager : MonoBehaviour
{
    public static NetworkManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, "SaveData.json");
    }

    //public void RequstSaveData(ScoreViewModel data)
    //{
    //    string json = JsonUtility.ToJson(data, true);
    //    File.WriteAllText(GetPath(), json);
    //    Debug.Log($"저장 완료 : {GetPath()}");
    //}
}
