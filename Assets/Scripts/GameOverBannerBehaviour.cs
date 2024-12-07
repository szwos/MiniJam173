using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverBannerBehaviour : MonoBehaviour
{
    public float TimeToChangeScene = 5f;
    public SpriteRenderer sprite;

    public void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(DimAndChangeScene());
    }

    public void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 1.5f);
    }

    private IEnumerator DimAndChangeScene()
    {
        float t = TimeToChangeScene;
        while (t > 0)
        {
            Color currentColor = sprite.color;
            sprite.color = new Color(currentColor.r, currentColor.g, currentColor.b, 1 - t / TimeToChangeScene);
            t -= Time.deltaTime;
            yield return null;            
        }
        

        SceneManager.LoadScene((int)ScenesEnum.GAME_OVER_SCENE);
    }
}