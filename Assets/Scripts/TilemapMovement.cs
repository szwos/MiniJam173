using System;
using System.Collections;
using DefaultNamespace;
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
    public ParticleSystem flyParticles;
    public AudioSource AudioSourceBooster;
    public AudioSource AudioSourceDrill;
    public AudioSource AudioSourceAlarm;
    public float DiggingDuration = 1f;
    public float MaxGroundedDistance = 0.15f;
    public float MovementForce = 10000f;
    public float FlyingForce = 30000f;
    public float doubleClickThreshold = 0.3f;
    public float dashCooldown = 1f;
    public float dashStrength = 20f;
    public float dashFuelConsumption = 20f;
    public float AudioFrequency = 10; //50 is once per second
    
    public float inputX;
    
    private Vector3 _movementDirection = Vector3.right;
    private bool _canMove = true;
    private bool _horizontalDrillVisible = false;
    private bool _verticalDrillVisible = false;
    
    private float _lastPressTimeA = 0f;
    private float _lastPressTimeD = 0f;
    
    private float _lastDash = 0f;
    private bool _wasFlying = false;

    private bool _tickAudio = false;
    private int _tickAudioCounter = 0;


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

    void Update()
    {
        if (_canMove)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                if (Time.time - _lastPressTimeA <= doubleClickThreshold)
                {
                    PerformDash(Vector2.left);
                }

                _lastPressTimeA = Time.time;
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                if (Time.time - _lastPressTimeD <= doubleClickThreshold)
                {
                    PerformDash(Vector2.right);
                }

                _lastPressTimeD = Time.time;
            }
        }

        PlayerStats.Instance.Depth = (int)transform.position.y + 10;
    }
    
    // Update is called once per frame
    void FixedUpdate()
    {
        //Audio logic
        
        if(_tickAudioCounter == AudioFrequency)
        {
            _tickAudio = true;
            _tickAudioCounter = 0;
        } else
        {
            _tickAudio = false;
        }
        _tickAudioCounter++;

        //Digging logic
        if (Input.GetKey(KeyCode.LeftControl) && (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)) && IsGrounded && _canMove)
        {
            (Vector3, IDestroyableBlock)? digDestination = DigBehaviour.TryDig(Vector2.right);
            if (digDestination != null)
            {
                Dig(digDestination.GetValueOrDefault().Item1, digDestination.GetValueOrDefault().Item2, Vector2.right);
                // StartCoroutine(DigCoroutine(transform.position, digDestination.GetValueOrDefault().Item1, DiggingDuration, Vector2.right));
            }            
        }

        if(Input.GetKey(KeyCode.S) && IsGrounded && _canMove)
        {
            (Vector3, IDestroyableBlock)? digDestination = DigBehaviour.TryDig(Vector2.down);
            if (digDestination != null)
            {
                Dig(digDestination.GetValueOrDefault().Item1, digDestination.GetValueOrDefault().Item2, Vector2.down);
                // StartCoroutine(DigCoroutine(transform.position, digDestination.GetValueOrDefault().Item1, DiggingDuration, Vector2.down));
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
                var prevFuel = PlayerStats.Instance.Fuel;
                PlayerStats.Instance.Fuel -= PlayerStats.Instance.FuelConsumption;
                if(prevFuel >= 300 && PlayerStats.Instance.Fuel < 300)
                {
                    Instantiate(AudioSourceAlarm, transform);
                }
                if (PlayerStats.Instance.Fuel > 0)
                {
                    
                    if(_tickAudio) Instantiate(AudioSourceBooster, transform);
                    flyParticles.Play();
                    _wasFlying = true;
                    SelfRb.AddForce(new Vector2(0, FlyingForce * Time.fixedDeltaTime));
                }
            }
            else if (_wasFlying)
            {
                flyParticles.Stop();
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

    private void PerformDash(Vector2 direction)
    {
        if (Time.time - _lastDash >= dashCooldown)
        {
            SelfRb.linearVelocityX = direction.x * dashStrength;
            PlayerStats.Instance.Fuel -= dashFuelConsumption;
            _lastDash = Time.time;
        }
    }

    private void Dig(Vector3 digDestination, IDestroyableBlock block, Vector2 direction)
    {
        StartCoroutine(DigCoroutine(transform.position, 
            digDestination, 
            DiggingDuration * (1f/block.MiningSpeedMultiplier) * 1/PlayerStats.Instance.DrillSpeedMultiplier, 
            direction));
    }

    private IEnumerator DigCoroutine(Vector3 diggingStartPosition, Vector2 diggingEndPosition, float duration, Vector2 direction)
    {
        float t = 0f;
        _canMove = false;
        if(direction == Vector2.right) { _horizontalDrillVisible = true; }
        if (direction == Vector2.down) { _verticalDrillVisible = true; }

        
        while (t < duration)
        {
            if(_tickAudio) Instantiate(AudioSourceDrill, transform);
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
