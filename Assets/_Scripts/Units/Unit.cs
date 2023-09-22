using System;
using UnityEngine;
using Utility.UniRx;
using Zenject;

public abstract class Unit : MonoBehaviour
{
    [Inject]
    protected GameManager gameManager;
    [field: SerializeField]
    public float MaxHealth { get; private set; }
    public LimitedFloatReactiveProperty CurrentHealth { get; protected set; } = new();

    [SerializeField]
    protected float speed;

    [SerializeField]
    protected Rigidbody2D rigidbody2d;
    [SerializeField]
    protected Vector2 hitPosition;

    public Vector2 Position => transform.position;
    public Vector2 RectPosition => rectTransform.anchoredPosition;
    public Vector2 HitPosition => RectPosition + hitPosition;
    private RectTransform rectTransform;
    public bool IsAlive => CurrentHealth.Value > 0;

    public virtual void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        InitHealth();
    }

    private void InitHealth()
    {
        CurrentHealth.MaxLimit = true;
        CurrentHealth.MaxValue = MaxHealth;
        CurrentHealth.MinLimit = true;
        CurrentHealth.MinValue = 0;
        CurrentHealth.Value = MaxHealth;
    }

    public void SetManager(GameManager manager)
    {
        gameManager = manager;
    }

    public virtual void TakeHit(int damage)
    {
        CurrentHealth.Value -= damage;
        if (CurrentHealth.Value <= 0)
            UnitDeath();
    }

    protected virtual void UnitDeath()
    {
        Destroy(gameObject);
    }
}
