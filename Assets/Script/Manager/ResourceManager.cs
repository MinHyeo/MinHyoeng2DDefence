using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.AddressableAssets;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private Dictionary<string, AsyncOperationHandle> _handles = new Dictionary<string, AsyncOperationHandle>();

    public void LoadAsset<T>(string address, System.Action<T> callback) where T : UnityEngine.Object
    {
        if(_handles.TryGetValue(address, out AsyncOperationHandle handle))
        {
            callback?.Invoke(handle.Result as T);
            return;
        }

        AsyncOperationHandle<T> loadHandle = Addressables.LoadAssetAsync<T>(address);

        loadHandle.Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                _handles[address] = op;
                callback?.Invoke(op.Result);
            }
            else
            {
                Debug.LogError($"에셋 로드 실패 : {address}");
            }
        };
    }

    public void Instantiate(string address, Transform parent = null, System.Action<GameObject> callback = null)
    {
        Addressables.InstantiateAsync(address, parent).Completed += (op) =>
        {
            if (op.Status != AsyncOperationStatus.Succeeded)
            {
                Debug.LogError($"프리팹 생성 실패 : {address}");
            }
            callback?.Invoke(op.Result);
        };
    }

    public void LoadSprite(string address, System.Action<Sprite> callback)
    {
        if(_handles.TryGetValue(address, out AsyncOperationHandle handle))
        {
            callback?.Invoke(handle.Result as Sprite);
            return;
        }

        AsyncOperationHandle<Sprite> handleOrigin = Addressables.LoadAssetAsync<Sprite>(address);

        handleOrigin.Completed += (op) =>
        {
            if (op.Status == AsyncOperationStatus.Succeeded)
            {
                _handles[address] = op;
                callback?.Invoke(op.Result);
            }
            else
            {
                Debug.LogError($"스프라이트 로드 실패 : {address}");
            }
        };
    }

    public void Release(string address)
    {
        if(_handles.TryGetValue(address, out AsyncOperationHandle handle))
        {
            Addressables.Release(handle);
            _handles.Remove(address);
            Debug.Log($"에셋 메모리 해체 완료 : {address}");
        }
    }
}
