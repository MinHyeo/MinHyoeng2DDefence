using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    private Dictionary<int, GameObject> _createdGameObjectContainer = new Dictionary<int, GameObject>();
    //private Dictionary<int, FieldObject> _fieldObjectContainer = new Dictionary<int, FieldObject>();

    public static GameObjectManager Instance;

    private int _objectInstanceKeyGenerator = 0;

    private void Awake()
    {
        Instance = this;
    }

    //public void CreateFieldObject(string fieldObjectDataId, Transform spawnSpot)
    //{
    //    var fieldObject = GameDataManager.Instance.GetFieldObjectData(fieldObjectDataId);
    //    if (fieldObject == null)
    //        return;

    //    ResourceManager.Instance.Instantiate(fieldObject.PrefabPath, this.transform, (createdObject) =>
    //    {
    //        Debug.Log("2222");
    //        createdObject.transform.position = spawnSpot.position;
    //        AddFieldObjectOnCreated(createdObject, fieldObjectDataId);
    //    });
    //}

    //private void AddFieldObjectOnCreated(GameObject createdObject, string fieldObjectDataId)
    //{
    //    _objectInstanceKeyGenerator++;
    //    var generatedInstanceId = _objectInstanceKeyGenerator;
    //    var fieldObject = createdObject.GetComponent<FieldObject>();

    //    if (fieldObject != null)
    //    {
    //        _fieldObjectContainer.Add(generatedInstanceId, fieldObject);
    //        fieldObject.InitFieldObjectInfoOnCreated(generatedInstanceId, fieldObjectDataId);
    //    }
    //}

    //public void RequestDestroyFieldObject(int instanceId)
    //{
    //    var fieldObjectComponent = GetFieldObjectByInstanceId(instanceId);
    //    if (fieldObjectComponent == null)
    //    {
    //        return;
    //    }

    //    // 요청된 필드 오브젝트를 제거함
    //    _fieldObjectContainer.Remove(instanceId);
    //    Destroy(fieldObjectComponent.gameObject);
    //}

    //public FieldObject GetFieldObjectByInstanceId(int fieldObjectInstanceId)
    //{
    //    if (_fieldObjectContainer.ContainsKey(fieldObjectInstanceId) == false)
    //    {
    //        Debug.LogError($"{fieldObjectInstanceId} 찾으려는 필드 오브젝트가 유효하지 않습니다");
    //        return null;
    //    }

    //    return _fieldObjectContainer[fieldObjectInstanceId];
    //}
}
