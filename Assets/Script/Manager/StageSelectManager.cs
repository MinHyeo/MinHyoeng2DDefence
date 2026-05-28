using UnityEngine;

public class StageSelectManager : MonoBehaviour
{
    public static StageSelectManager Instance;

    private void Awake()
    {
        Instance = this;
    }
}
