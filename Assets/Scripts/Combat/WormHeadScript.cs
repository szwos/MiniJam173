using DefaultNamespace;
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
    public TerrainMananger TerrainManager;
    public Transform WormAssemblyTransform;

    public void Awake()
    {
        if (Player == null)
        {
            Player = FindFirstObjectByType<TilemapMovement>().transform;
        }

        if (TerrainManager == null)
        {
            TerrainManager = FindFirstObjectByType<TerrainMananger>();
        }
    }

    private void FixedUpdate()
    {
        float distance = Vector3.Distance(transform.position, Player.position);
       
        Vector2 direction = Player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Vector3.forward * angle), Time.deltaTime * distance * 0.25f);


        SelfRb.AddForce(this.transform.right * Speed);

    }

    private void Update()
    {        
        Vector3 worldPosition = transform.localPosition + WormAssemblyTransform.localPosition;
        TerrainManager.SetBlock(worldPosition, BlockRegistry.Air);

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Player.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
    }
}
