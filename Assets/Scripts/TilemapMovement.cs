using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapMovement : MonoBehaviour
{
    [SerializeField]
    public Rigidbody2D SelfRb;
    public GameObject GroundedDetector;
    public Dig DigBehaviour;
    public float DiggingDuration = 1f;
    public float MaxGroundedDistance = 0.15f;
    
    private Vector3 _movementDirection = Vector3.right;
    private bool _canMove = true;

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
        Debug.Log(_canMove);
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
                StartCoroutine(DigCoroutine(transform.position, digDestination.GetValueOrDefault(), DiggingDuration));
            }            
        }

        if(Input.GetKey(KeyCode.DownArrow) && IsGrounded && _canMove)
        {
            Vector3? digDestination = DigBehaviour.TryDig(Vector2.down);
            if (digDestination != null)
            {
                StartCoroutine(DigCoroutine(transform.position, digDestination.GetValueOrDefault(), DiggingDuration));
            }
        }


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

    }

    private IEnumerator DigCoroutine(Vector3 diggingStartPosition, Vector3 diggingEndPosition, float duration)
    {
        float t = 0f;
        _canMove = false;

        while(t < duration)
        {
            transform.position = Vector3.Lerp(diggingStartPosition, diggingEndPosition, t / duration);
            t += Time.deltaTime;

            yield return null;
        }

        _canMove = true;
    }

}
