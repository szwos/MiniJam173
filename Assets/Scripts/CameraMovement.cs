using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    public Transform PlayerPosition = null;
    public float speed = 2.0f;

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, PlayerPosition.transform.position, speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
}
