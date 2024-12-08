using System;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float damage;
    public float speed;
    public float lifeTime = 5f;
    
    public Vector2 target;
    
    void Start()
    {
        transform.LookAt(target);
        
        Vector3 sourcePosition = transform.position;
        float angle = Mathf.Atan2(target.y - sourcePosition.y, target.x - sourcePosition.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        GetComponent<Rigidbody2D>().linearVelocity = transform.right * speed;
        
        Destroy(gameObject, lifeTime);
    }
    
    private void OnTriggerEnter2D(Collider2D collider2D1)
    {
        if(collider2D1.gameObject.CompareTag("shootTarget") )
        {
            IEnemy enemy = collider2D1.GetComponent<IEnemy>();
            if(enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject);
            }
        
        }
    }
}
