using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private AudioClip startSong;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameManager gameMan;
    [SerializeField] private GameObject fadeGuy;

    private bool hitPlay = false;
    private bool actualPlay = false;

    // Update is called once per frame
    void Update()
    {
        if (hitPlay && !audioSource.isPlaying)
        {
            audioSource.PlayOneShot(startSong);
            actualPlay = true;
            hitPlay = false;
        }
        if (actualPlay && !audioSource.isPlaying)
        {
            gameMan.PlayGame();
        }
    }

    public void MusicStart()
    {
        audioSource.loop = false;
        hitPlay = true;
        fadeGuy.SetActive(true);
    }
}
