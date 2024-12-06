using UnityEngine;

public class testMovement : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow)) {
            transform.position += new Vector3(0.01f, 0, 0);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position += new Vector3(-0.01f, 0, 0);
        }
    }
}
