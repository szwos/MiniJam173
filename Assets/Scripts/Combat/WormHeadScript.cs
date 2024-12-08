using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class WormHeadScript : MonoBehaviour
{
    public Rigidbody2D SelfRb;
    public float Speed;
    [Range(0f, 1f)]
    //public float RotationSpeed;
    public float MinDistance;
    public Transform Player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, Player.position);
        //
            //rotate slowly
            Vector2 direction = Player.transform.position - transform.position;
            direction.Normalize();
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;


        /*float rotationSpeed = distance / (1 + distance);
    Debug.Log(rotationSpeed);
        transform.rotation = Quaternion.Euler(Vector3.forward * (angle * rotationSpeed));            */

        //}
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.forward * angle), Time.deltaTime * distance * 0.25f);


        SelfRb.AddForce(this.transform.right * Speed);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Player.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }
}
