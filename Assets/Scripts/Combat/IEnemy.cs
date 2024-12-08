using UnityEngine;

public interface IEnemy
{
    public void TakeDamage(float damage);
    public float Damage { get; }
    public float KnockbackStrength { get; }
}
