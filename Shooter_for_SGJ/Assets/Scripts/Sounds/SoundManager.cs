using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _figthMusic;
    [SerializeField] private AudioClip[] _horrorMusic;
    [SerializeField] private AudioClip[] _idleMusic;
    void Start()
    {
        _audioSource = GameObject.Find("Player").GetComponent<AudioSource>();
    }

    public void StartFight()
    {
        _audioSource.clip = _figthMusic[Random.Range(0,_figthMusic.Length)];
        _audioSource.Play();
    }

    public void StartHorror()
    {
        _audioSource.clip = _horrorMusic[0];
        _audioSource.Play();
    }

    public void IdleMusic()
    {
        _audioSource.clip = _idleMusic[0];
        _audioSource.Play();
    }

    public void StopPlayer()
    {
        _audioSource.Stop();
    }
}
