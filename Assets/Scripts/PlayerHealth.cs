
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    
    public SpriteRenderer DimmPrefab;
    public GameObject Camera;

    private void Update()
    {
        if(PlayerStats.Instance.Fuel < 0)
        {
            Instantiate(DimmPrefab, Camera.transform);
            Destroy(this); //If adding some Destroy animation, remember to lock above Instantiate, so that it doesnt execute multiple times
        }
    }
}