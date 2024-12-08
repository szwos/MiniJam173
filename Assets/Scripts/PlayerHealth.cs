
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


        IEnemy enemy = collisionGameObject.transform.GetComponent<IEnemy>();
        if (enemy != null)
        {
            if (_isInvincible)
            {
                return;
            }

            PlayerStats.Instance.Health -= enemy.Damage;
            var playerMovement = transform.GetComponent<TilemapMovement>();
            playerMovement.Knockback(collisionGameObject.transform.position, InvicibilityFrameDuration, enemy.KnockbackStrength);
            StartCoroutine(InvicibilityFrameCoroutine(InvicibilityFrameDuration));
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "gasStation")
        {
            if(Input.GetKey(KeyCode.E))
            {
                if(PlayerStats.Instance.Money > 0)
                {
                    PlayerStats.Instance.Money -= 1;
                    if (PlayerStats.Instance.Fuel < PlayerStats.Instance.MaxFuel)
                    {

                        PlayerStats.Instance.Fuel += 2;
                    }
                    PlayerStats.Instance.Health++;
                }
            }
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