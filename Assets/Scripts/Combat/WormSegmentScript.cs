using UnityEngine;

public class WormSegmentScript : MonoBehaviour
{
    public GameObject NextSegment;
    public float Speed = 10;
    public float DistanceFromNextSegment = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(transform.position, NextSegment.transform.position);

        if(distance > DistanceFromNextSegment)
        {
            transform.position = Vector2.MoveTowards(transform.position, NextSegment.transform.position, Speed * Time.deltaTime);
        }
        
    }
}
