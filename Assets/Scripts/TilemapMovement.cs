using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TilemapMovement : MonoBehaviour //TODO: rename o just CharacterController or MovementController
{
    [SerializeField]
    public Rigidbody2D SelfRb;
    public Collider2D collider1;
    public SpriteRenderer HorizontalDrilllSprite;
    public SpriteRenderer VerticalDrillSprite;
    public GameObject GroundedDetector;
    public Dig DigBehaviour;
    public float DiggingDuration = 1f;
    public float MaxGroundedDistance = 0.15f;
    public float MovementForce = 10000f;
    public float FlyingForce = 30000f;
    public float doubleClickThreshold = 0.3f;

    public float inputX;
    
    private Vector3 _movementDirection = Vector3.right;
    private bool _canMove = true;
    private bool _horizontalDrillVisible = false;
    private bool _verticalDrillVisible = false;
    
    private float _lastPressTimeA = 0f;
    private float _lastPressTimeD = 0f;

    public bool IsGrounded
    {
        get
        {
            RaycastHit2D hit = Physics2D.Raycast(GroundedDetector.transform.position, Vector2.down, MaxGroundedDistance, LayerMask.GetMask("Ground"));
            Debug.DrawRay(GroundedDetector.transform.position, transform.TransformDirection(Vector3.down) * MaxGroundedDistance, Color.green);
            if(hit.distance > 0)
            {
                return true;
            }
            return false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Digging logic
        if (Input.GetKey(KeyCode.LeftControl) && IsGrounded && _canMove)
        {
            Vector3? digDestination = DigBehaviour.TryDig(Vector2.right);
            if (digDestination != null)
            {

                StartCoroutine(DigCoroutine(transform.position, digDestination.GetValueOrDefault(), DiggingDuration, Vector2.right));
            }            
        }

        if(Input.GetKey(KeyCode.S) && IsGrounded && _canMove)
        {
            Vector3? digDestination = DigBehaviour.TryDig(Vector2.down);
            if (digDestination != null)
            {
                StartCoroutine(DigCoroutine(transform.position, digDestination.GetValueOrDefault(), DiggingDuration, Vector2.down));
            }
        }


        //Movement logic
        if (_canMove)
        {
            inputX = Input.GetAxis("Horizontal");

            transform.rotation = inputX switch
            {
                < -0.001f => Quaternion.Euler(new Vector3(0, 180, 0)),
                > 0.001f => Quaternion.Euler(new Vector3(0, 0, 0)),
                _ => transform.rotation
            };

            SelfRb.AddForce(new Vector2(inputX * MovementForce * Time.fixedDeltaTime, 0f));
            if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
            {
                PlayerStats.Instance.Fuel -= PlayerStats.Instance.FuelConsumption;
                if(PlayerStats.Instance.Fuel > 0)
                    SelfRb.AddForce(new Vector2(0, FlyingForce * Time.fixedDeltaTime));
            }
            
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (Time.time - _lastPressTimeA <= doubleClickThreshold)
                {
                    HandleDoubleClick(Vector2.left);
                }
                _lastPressTimeA = Time.time;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (Time.time - _lastPressTimeD <= doubleClickThreshold)
                {
                    HandleDoubleClick(Vector2.right);
                }
                _lastPressTimeD = Time.time;
            }
        }

        //Animation logic
        if (_horizontalDrillVisible)
        {
            HorizontalDrilllSprite.enabled = true;
        }
        else 
        { 
            HorizontalDrilllSprite.enabled = false; 
        }

        if (_verticalDrillVisible)
        {
            VerticalDrillSprite.enabled = true;
        }
        else
        {
            VerticalDrillSprite.enabled = false;
        }

    }

    private void HandleDoubleClick(Vector2 direction)
     {
        //throw new NotImplementedException();
    }

    private IEnumerator DigCoroutine(Vector3 diggingStartPosition, Vector2 diggingEndPosition, float duration, Vector2 direction)
    {
        float t = 0f;
        _canMove = false;
        if(direction == Vector2.right) { _horizontalDrillVisible = true; }
        if (direction == Vector2.down) { _verticalDrillVisible = true; }

        while (t < duration)
        {
            transform.position = Vector3.Lerp(diggingStartPosition, diggingEndPosition, t / duration);
            t += Time.deltaTime;

            yield return null;
        }

        SelfRb.linearVelocityY = 0f;
        _canMove = true;
        _horizontalDrillVisible = false;
        _verticalDrillVisible = false;
    }

    public void Knockback(Vector3 knockbackSource, float invicibilityFrameDuration, float knockbackStrength)
    {
        StartCoroutine(InvicibilityVisualEffectCoroutine(invicibilityFrameDuration));
        if (knockbackSource.x > transform.position.x)
        {
            SelfRb.AddForce(new Vector2(-1, 0.7f) * knockbackStrength);
        } else
        {
            SelfRb.AddForce(new Vector2(1, 0.7f) * knockbackStrength);
        }
        
    }

    private IEnumerator InvicibilityVisualEffectCoroutine(float duration)
    {
        float t = 0;
        ApplyColorAlphaToAllChildrenAndSelf(transform, 0.3f);
        GetComponent<Collider2D>().excludeLayers |= LayerMask.GetMask("Enemy");
        while (t < duration)
        {
            yield return null;
            t += Time.deltaTime;
        }
        ApplyColorAlphaToAllChildrenAndSelf(transform, 1f);
        GetComponent<Collider2D>().excludeLayers &= ~LayerMask.GetMask("Enemy");
    }

    private void ApplyColorAlphaToAllChildrenAndSelf(Transform transform, float alpha)
    {
        int childCount = transform.childCount;

        for (int i = 0; i < childCount; i++)
        {
            Transform child = transform.GetChild(i);

            if (child != null)
            {
                SpriteRenderer spriteRenderer;
                if(child.TryGetComponent<SpriteRenderer>(out spriteRenderer))
                {
                    spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, alpha);
                }

                if (child.childCount != 0)
                {
                    ApplyColorAlphaToAllChildrenAndSelf(child.transform, alpha);
                }
            }
        }

        SpriteRenderer renderer;
        if (transform.TryGetComponent<SpriteRenderer>(out renderer))
        {
            renderer.color = new Color(renderer.color.r, renderer.color.g, renderer.color.b, alpha);
        }

    }
}
