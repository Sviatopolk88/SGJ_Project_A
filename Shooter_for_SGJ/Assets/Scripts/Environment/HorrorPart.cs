using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HorrorPart : MonoBehaviour
{
    private GameObject _flashLight;
    private GameObject _directionalLight;
    private GameObject _directionalLightHorror;
    private Camera _camera;
    private GameObject _audioManager;
    // Start is called before the first frame update
    void Start()
    {
        Initialization();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartHorror()
    {
        _camera.backgroundColor = Color.black;
        _directionalLight.gameObject.SetActive(false);
        _directionalLightHorror.gameObject.SetActive(true);
        _flashLight.gameObject.SetActive(true);
        _audioManager.GetComponent<SoundManager>().StartHorror();
       
    }

    private void Initialization()
    {
        _camera = Camera.main;
        _directionalLight = GameObject.Find("Directional Light");
        _directionalLightHorror = GameObject.Find("Directional Light Horror");
        _flashLight = GameObject.Find("FlashLight");
        _audioManager = GameObject.Find("AudioManager");
        _flashLight.gameObject.SetActive(false);
        _directionalLightHorror.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("HorrorTrigger"))
        {
            other.gameObject.SetActive(false);
            StartHorror();
        }
    }

}
