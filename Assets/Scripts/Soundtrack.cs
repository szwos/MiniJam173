using System.Collections;
using UnityEngine;

public class Soundtrack : MonoBehaviour
{
    public AudioSource ost1;
    public AudioSource ost2;
    public AudioSource ost3;


    private bool _musicIsPlaying = false;
    // Update is called once per frame
    void Update()
    {
        if (!_musicIsPlaying)
        {
            SelectMusic();
            //BreakForRandomDuration();
        }
        
    }

    public void SelectMusic()
    {
        int ostNumber = (int)Random.Range(0, 3);
        switch (ostNumber)
        {
            case 0:
                {
                    StartCoroutine(PlayMusic(ost1));
                    break;
                }
            case 1:
                {
                    StartCoroutine(PlayMusic(ost2));
                    break;
                }
            case 2:
                {
                    StartCoroutine(PlayMusic(ost3));
                    break;
                }

        }
    }

    IEnumerator PlayMusic(AudioSource music)
    {
        music.Play();
        _musicIsPlaying = true;
        yield return new WaitForSeconds(music.clip.length);
        _musicIsPlaying = false;
        BreakForRandomDuration();

    }

    private void BreakForRandomDuration()
    {
        float duration = Random.Range(0, 120);
        StartCoroutine(Waiter(duration));

    }

    IEnumerator Waiter(float duration)
    {
        _musicIsPlaying = true;
        yield return new WaitForSeconds(duration);
        _musicIsPlaying = false;
    }
}
