using UnityEngine;

public class TestButtonObject : MonoBehaviour
{
    private Camera _mainCamera;
    private bool _isDragging = false;
    private Vector3 _offset;

    void Awake()
    {
        // 메인 카메라 미리 가져오기 (성능 최적화)
        _mainCamera = Camera.main;
    }

    // 1. 마우스로 이 오브젝트의 Collider를 클릭했을 때 최초 1회 호출
    void OnMouseDown()
    {
        _isDragging = true;

        // 마우스 클릭 위치와 오브젝트 중심 위치 사이의 거리(오차)를 계산합니다.
        // 이 처리를 안 해주면 클릭하는 순간 오브젝트 중심점이 마우스 커서로 '툭' 하고 순간이동합니다.
        _offset = transform.position - GetMouseWorldPosition();
    }

    // 2. 마우스를 누른 채로 움직일 때 매 프레임 호출
    void OnMouseDrag()
    {
        if (_isDragging)
        {
            // 현재 마우스 위치에 최초 클릭 시 계산한 오차(Offset)를 더해 자연스럽게 이동시킵니다.
            transform.position = GetMouseWorldPosition() + _offset;
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // 마우스의 현재 스크린 위치 (X, Y)
        Vector3 mouseScreenPos = Input.mousePosition;

        // 카메라는 Z축 뒤에 멀리 떨어져 있으므로, 2D 공간의 깊이(Z) 값을 카메라와의 거리만큼 보정해 줍니다.
        mouseScreenPos.z = _mainCamera.WorldToScreenPoint(transform.position).z;

        // 최종 월드 좌표 변환
        return _mainCamera.ScreenToWorldPoint(mouseScreenPos);
    }

    private void OnMouseUp()
    {
        _isDragging = false;

        if (TowerManager.Instance.CanPlaceTower(transform.position))
        {
            //transform.position = TowerManager.Instance.GetCellCenterWolrd();
            TowerManager.Instance.SpawnTower(transform.position);
            gameObject.SetActive(false);
        }
    }
}
