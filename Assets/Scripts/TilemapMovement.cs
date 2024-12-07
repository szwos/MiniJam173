using System;
using UnityEngine;
using UnityEngine.Tilemaps;

//TOOD: flipowanie postaci
//TOOD: stweakowaæ lerpa, bo po kopaniu sie jakos na chwile zatrzymuje zanim zacznie kopac nastepny (ale to nie jest wazne bardzo)
//TODO: posprzatac tu moze, chociaz wsm wyjebane, to jest gamejam xd

public class TilemapMovement : MonoBehaviour
{
    [SerializeField]
    public Tilemap Tilemap;
    public Rigidbody2D SelfRb;
    public GameObject GroundedDetector;
    public Dig DigBehaviour;
    public float DiggingSpeed;
    public float MaxGroundedDistance = 0.1f;
    

    private Vector3 _diggingStartPosition;
    private Vector3 _diggingEndPosition;
    private Vector3 _movementDirection = Vector3.right;
    private float _diggingStartTime;
    private float _diggingDistance;
    private bool _canMove = true;

    public int IsDigging
    {
        //0 - not digging
        //1 - horizontaly
        //2 - vertically
        get
        {
            if (Input.GetKey(KeyCode.LeftControl))
            {
                return 1;
            }
            else if(Input.GetKey(KeyCode.DownArrow))
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
    }

    public bool IsGrounded
    {
        get
        {
            RaycastHit2D hit = Physics2D.Raycast(GroundedDetector.transform.position, Vector2.down, MaxGroundedDistance, LayerMask.GetMask("Ground"));
            Debug.DrawRay(GroundedDetector.transform.position, transform.TransformDirection(Vector3.down) * 1, Color.green);
            if(hit.distance > 0)
            {
                return true;
            }
            return false;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DigBehaviour.Digging += OnDigging;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow)) 
        {
            _movementDirection = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _movementDirection = Vector3.right;
        }


        if (_canMove)
        {


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
        } else
        {
            float distanceCovered = (Time.time - _diggingStartTime) * DiggingSpeed;
            float fractionOfMovement = distanceCovered / _diggingDistance;
            transform.position = Vector3.Lerp(_diggingStartPosition, _diggingEndPosition, fractionOfMovement);
            if(distanceCovered >= _diggingDistance - 0.01f)
            {
                _canMove = true;
            }
        }

    }

    private void OnDigging(object sender, Vector3 diggingDestination)
    {
        //transform.position = diggingDestination;
        DigToPosition(diggingDestination);
    }
    
    public void DigToPosition(Vector3 destination)
    {
        _diggingStartPosition = transform.position;
        _diggingEndPosition = destination;
        _diggingStartTime = Time.time;
        _diggingDistance = Vector3.Distance(_diggingStartPosition, _diggingEndPosition);
        _canMove = false;
    }

}
