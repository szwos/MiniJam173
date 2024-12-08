using UnityEngine;

public class WormAssemblyScript : MonoBehaviour, IEnemy
{
    public float Health = 150f;

    [SerializeField]
    private float _damage = 30;
    public float Damage => _damage;

    [SerializeField]
    private float _knockbackStrength = 25000;
    public float KnockbackStrength => _knockbackStrength;

    public Vector3 Position;

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health < 0)
        {
            Die();
        }
    }

    public void Die() 
    { 
        Destroy(gameObject);
    }

    private void Update()
    {
        Position = transform.position;
    }
}
