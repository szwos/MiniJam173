using UnityEngine;

public class EnemyScript : MonoBehaviour, IEnemy
{
    [SerializeField]
    private float _knockbackStrength = 10000f;
    public float KnockbackStrength
    {
        get
        {
            return _knockbackStrength;
        }
    }

    [SerializeField]
    private float _damage = 10f;
    public float Damage
    {
        get
        {
            return _damage;
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log(damage + " AUUU");
    }
}
