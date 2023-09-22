using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : Unit
{
    [field: SerializeField]
    public int CollisionDamage { get; private set; }
    [field: SerializeField]
    public float AgroRadius { get; private set; }

    private Player player;
    private bool agro;

    [field: SerializeField]
    public List<ItemScriptable> DropList { get; private set; }

    void Start()
    {
        player = FindObjectOfType<Player>();
    }

    void FixedUpdate()
    {
        if(agro || Vector2.Distance(RectPosition, player.RectPosition) < AgroRadius)
        {
            rigidbody2d.MovePosition((Vector2)transform.position + ToTarget() * speed * Time.fixedDeltaTime);
            LookAtDirection(player.Position.x - Position.x);
        }
    }
    private void LookAtDirection(float value)
    {
        int lookDirection = value > 0 ? 1 : -1;
        transform.localScale = new Vector3(lookDirection, 1f, 1f);
    }
    private Vector2 ToTarget()
    {
        return (player.RectPosition - RectPosition).normalized;
    }

    public override void TakeHit(int damage)
    {
        agro = true;
        base.TakeHit(damage);
    }
    protected override void UnitDeath()
    {
        SpawnDrop();
        gameManager.UnitDeath(this);
        base.UnitDeath();
    }

    private void SpawnDrop()
    {
        ItemScriptable item = DropList[Random.Range(minInclusive: 0, maxExclusive: DropList.Count)];
        gameManager.SpawnDrop(item, transform.position);
    }
}
