
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    public SpriteRenderer DimmPrefab;
    public GameObject Camera;

    public float InvicibilityFrameDuration = 3f;

    private bool _isInvincible = false;

    private void Update()
    {
        if(PlayerStats.Instance.Fuel < 0 || PlayerStats.Instance.Health <= 0)
        {
            Instantiate(DimmPrefab, Camera.transform);
            Destroy(this); //If adding some Destroy animation, remember to lock above Instantiate, so that it doesnt execute multiple times
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleCollision(collision.gameObject);
    }

    private void HandleCollision(GameObject collisionGameObject)
    {
        if (_isInvincible)
        {
            return;
        }

        IEnemy enemy = collisionGameObject.transform.GetComponent<IEnemy>();
        if (enemy != null)
        {
            PlayerStats.Instance.Health -= enemy.Damage;
            var playerMovement = transform.GetComponent<TilemapMovement>();
            playerMovement.Knockback(collisionGameObject.transform.position, InvicibilityFrameDuration, enemy.KnockbackStrength);
            StartCoroutine(InvicibilityFrameCoroutine(InvicibilityFrameDuration));
        }
    }



    private IEnumerator InvicibilityFrameCoroutine(float duration)
    {
        float t = 0;
        _isInvincible = true;
        while (t < duration) 
        {
            yield return null;
            t += Time.deltaTime;
        }
        _isInvincible = false;
    }




}