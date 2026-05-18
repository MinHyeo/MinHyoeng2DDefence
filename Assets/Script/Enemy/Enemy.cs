using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;

    private EnemyData _enemyData;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        string _tempId = "enemy_01";
        _enemyData = GameDataManager.Instance.GetEnemyData(_tempId);
    }
    
    public void OnDamaged()
    {
        _spriteRenderer.color = Color.red;

        Invoke("ChangeColor", 1f);
    }

    private void ChangeColor()
    {
        _spriteRenderer.color = Color.white;
    }
}
