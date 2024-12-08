using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    public Transform PlayerTransform;
    public GameObject WormPrefab;
    public GameObject BatPrefab;
   

    //Spawns per minute
    public float WormBaseSpawnRate;
    public float BatBaseSpawnRate;

    public float MinimumRangeToSpawn;
    public float MaximumRangeToSpawn;


    //FixedUpdate is 50 Hz by default
    private void FixedUpdate()
    {
        if((int)Random.Range(0, 50 * 60 * BatBaseSpawnRate) == 0)
        {
            SpawnMob(BatPrefab);
        }

        if((int)Random.Range(0, 50 * 60 * WormBaseSpawnRate) == 0)
        {
            SpawnMob(WormPrefab);
        }
    }

    private void SpawnMob(GameObject prefab)
    {
        float x = Random.Range(MinimumRangeToSpawn, MaximumRangeToSpawn);
        float y = Random.Range(MinimumRangeToSpawn, MaximumRangeToSpawn);
        if((int)Random.Range(0, 2) == 0)
        {
            x = -x;
        }
        if ((int)Random.Range(0, 2) == 0)
        {
            y = -y;
        }

        Vector3 positionAroundPlayer = new Vector3(x, y, 0);
        positionAroundPlayer = positionAroundPlayer + PlayerTransform.transform.position;

        GameObject spawnedBat = Instantiate(prefab, positionAroundPlayer, transform.rotation);
        Debug.Log(positionAroundPlayer);

        
    }
}
