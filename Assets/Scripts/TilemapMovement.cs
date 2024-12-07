using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TilemapMovement : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D SelfRb;
    public SpriteRenderer HorizontalDrilllSprite;
    public SpriteRenderer VerticalDrillSprite;
    public GameObject GroundedDetector;
    public Dig DigBehaviour;
    public float DiggingDuration = 1f;
    public float MaxGroundedDistance = 0.15f;
    
    private Vector3 _movementDirection = Vector3.right;
    private bool _canMove = true;
    private bool _horizontalDrillVisible = false;
    private bool _verticalDrillVisible = false;

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
        if(Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            _movementDirection = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _movementDirection = Vector3.right;
        }

        if (Input.GetKey(KeyCode.LeftControl) && IsGrounded && _canMove)
        {
            Vector3? digDestination = DigBehaviour.TryDig(Vector2.right);
            if (digDestination != null)
            {

                StartCoroutine(DigCoroutine(transform.position, digDestination.GetValueOrDefault(), DiggingDuration, Vector2.right));
            }            
        }

        if(Input.GetKey(KeyCode.DownArrow) && IsGrounded && _canMove)
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
            if(_movementDirection == Vector3.left)
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
            }

            if(_movementDirection == Vector3.right) 
            {
                transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
            }

            //TODO: use fuel
            if (Input.GetKey(KeyCode.RightArrow))
            {
                SelfRb.AddForce(new Vector2(250f, 0));
            }

            if (Input.GetKey(KeyCode.LeftArrow))
            {
                SelfRb.AddForce(new Vector2(-250f, 0));
            }
            if (Input.GetKey(KeyCode.Space))
            {
                SelfRb.AddForce(new Vector2(0, 500f));
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

        _canMove = true;
        _horizontalDrillVisible = false;
        _verticalDrillVisible = false;
    }

}
