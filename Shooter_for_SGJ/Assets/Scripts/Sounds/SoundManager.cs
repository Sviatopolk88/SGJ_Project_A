using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _figthMusic;
    [SerializeField] private AudioClip[] _horrorMusic;
    [SerializeField] private AudioClip[] _arenaMusic;
    [SerializeField] private AudioClip[] _idleMusic;

    public AudioClip devil;
    void Start()
    {
        _audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
        StartIdle();
    }

    public void StartFight()
    {
        if (!FindObjectOfType<Arena>().IsFighting)
        {
            _audioSource.clip = _figthMusic[Random.Range(0, _figthMusic.Length)];
            _audioSource.Play();
        }
    }

    public void StartHorror()
    {
        _audioSource.clip = _horrorMusic[0];
        _audioSource.Play();
    }

    public void StartIdle()
    {
        _audioSource.clip = _idleMusic[0];
        _audioSource.Play();
    }

    public void StopPlayer()
    {
        if (!FindObjectOfType<Arena>().IsFighting)
        {
            _audioSource.Stop();
        }
        else
        {
            
        }
            
    }

    public void Devil()
    {
        _audioSource.clip = devil;
        _audioSource.Play();
        _audioSource.loop = false;
    }

    public void StartArenaFight()
    {
        _audioSource.clip = _arenaMusic[Random.Range(0, _arenaMusic.Length)];
        _audioSource.Play();
    }
}
