using UniRx;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Default Weapon")]
public class Weapon : ScriptableObject
{
    public int Damage;

    public int MaxBullets;
    [HideInInspector]
    public IntReactiveProperty Bullets;

    public float ReAttack;
    private float cooldown;

    public Transform bulletsSpawnPoint;
    public BulletLine bulletLine;

    public void Awake()
    {
        Bullets.Value = MaxBullets;
    }

    public void FixedUpdate()
    {
        if (cooldown > 0)
            cooldown -= Time.fixedDeltaTime;
    }

    public void Shot(Vector2 weaponSpawnPosition, Unit unitTarget = null)
    {
        if (cooldown > 0 || Bullets.Value == 0)
            return;

        cooldown += ReAttack;
        Bullets.Value--;

        if (unitTarget != null)
            unitTarget.TakeHit(Damage);

        bulletLine.Enable(unitTarget.HitPosition - weaponSpawnPosition);
    }
}