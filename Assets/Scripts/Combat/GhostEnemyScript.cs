using UnityEditor;
using UnityEngine;

public class GhostEnemyScript : MonoBehaviour, IEnemy
{
    public GameObject Player;
    public float Health = 30f;
    public float speed = 3f;

    [SerializeField]
    private float _knockbackStrength = 100f;
    public float KnockbackStrength
    {
        get
        {
            return _knockbackStrength;
        }
    }

    [SerializeField]
    private float _damage = 20f;
    public float Damage 
    { 
        get
        {
            return _damage;
        } 
    }

    public void Awake()
    {
        if (Player == null) 
        {
            Player = FindFirstObjectByType<TilemapMovement>().transform.gameObject;
        }
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Die();
        }
    }

    public void Die()
    {
        //TODO: death Animation 
        Destroy(gameObject);
    }

    public void FixedUpdate()
    {
        //float distance = Vector3.Distance(transform.position, Player.transform.position);
        //Vector2 direction = Player.transform.position - transform.position;        
        //direction.Normalize();
        //float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.position = Vector2.MoveTowards(transform.position, Player.transform.position, speed * Time.deltaTime);
        //transform.rotation = Quaternion.Euler(Vector3.forward * angle);

        if(Player.transform.position.x < transform.position.x) 
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
        } else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        }
    }
}
