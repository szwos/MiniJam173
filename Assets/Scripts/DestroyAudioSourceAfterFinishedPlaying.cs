using System.Collections;
using UnityEngine;

public class DestroyAduioSourceAfterFinishedPlaying : MonoBehaviour
{
    public AudioSource source;
    private void Awake()
    {
        StartCoroutine(Waiter());
    }

    private IEnumerator Waiter()
    {
        yield return new WaitForSeconds(source.clip.length);
        Destroy(gameObject);
    }
}