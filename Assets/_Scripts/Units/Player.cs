using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : Unit
{
    [Inject]
    private VisibleFloatingJoystick joystick;
    [field: SerializeField]
    public Weapon Weapon { get; private set; }
    [SerializeField]
    private Transform bulletsSpawnPoint;


    public override void Awake()
    {
        base.Awake();
        if (Weapon != null)
        {
            Weapon.bulletsSpawnPoint = bulletsSpawnPoint;
            Weapon.bulletLine = GetComponentInChildren<BulletLine>();
            Weapon.Awake();
        }
    }
    private void FixedUpdate()
    {
        if (joystick.Direction != Vector2.zero)
        {
            OnMove();
            LookAtDirection(joystick.Direction.x);
        }
        Weapon.FixedUpdate();
    }

    private void LookAtDirection(float value)
    {
        int lookDirection = value > 0 ? 1: -1;
        transform.localScale = new Vector3(lookDirection, 1f, 1f);
    }

    public void OnMove()
    {
        Vector2 unitPosition = transform.position;

        rigidbody2d.MovePosition(unitPosition + joystick.Direction * speed * Time.fixedDeltaTime);
    }

    public void Attack()
    {
        Unit target = NearestEnemyInSight();
        if (target != null)
        {
            LookAtDirection(target.Position.x - Position.x);
            Weapon.Shot(RectPosition + bulletsSpawnPoint.GetComponent<RectTransform>().anchoredPosition, target);
        }
    }

    private Unit NearestEnemyInSight()
    {
        Unit result = null;
        float minDistanse = float.MaxValue;

        LayerMask hitMask = LayerMask.GetMask("Enemy", "Wall");
        foreach (Unit enemy in gameManager.Enemyes)
        {
            Vector2 direction = enemy.Position - (Vector2)bulletsSpawnPoint.position;
            RaycastHit2D hit = Physics2D.Raycast(bulletsSpawnPoint.position, direction, distance: float.MaxValue, layerMask: hitMask);
            if (hit 
                && LayerMask.LayerToName(hit.collider.gameObject.layer) == "Enemy"
                && hit.collider.TryGetComponent(out Enemy target))
            {
                float distance = Vector2.Distance(Position, enemy.Position);
                if(distance < minDistanse)
                {
                    result = enemy;
                    minDistanse = distance;
                }
            }
        }
        return result;
    }
}
