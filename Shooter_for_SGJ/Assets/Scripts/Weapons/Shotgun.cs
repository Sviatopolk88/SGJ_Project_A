using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : MonoBehaviour
{
    public int FirePointRandomMIN = -6;
    public int FirePointRandomMAX = 6;
    public ParticleSystem ShotgunFireEffect;
    public float FireDistance = 50f;
    public int Damage = 400;
    public LayerMask Mask;
    public Transform FirePoint;
    public GameObject HitParticles;

    [SerializeField] private GameObject _playerCamera;

    private float _currentDamage;
    private AudioSource _shotgunFireSound;
    private int _shotgunFireParticalNumber = 2;
    private RaycastHit _hit;
    
    private Transform _firePointStartPosition;

    void Start()
    {
        _shotgunFireSound = GetComponent<AudioSource>();
        _firePointStartPosition = FirePoint;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) Shoot();
    }
    public void Shoot()
    {
        Debug.Log("Выстрел!");
        _shotgunFireSound.Play();
        ShotgunFireEffect.Emit(_shotgunFireParticalNumber);

        float mobDistance;

        for(int bulletCounter = 10; bulletCounter > 0; bulletCounter--)
        {
            FirePoint.localRotation = Quaternion.identity;
            FirePoint.localRotation = Quaternion.Euler(_firePointStartPosition.localRotation.x + Random.Range(FirePointRandomMIN, FirePointRandomMAX), _firePointStartPosition.localRotation.y + Random.Range(FirePointRandomMIN, FirePointRandomMAX), _firePointStartPosition.localRotation.z + Random.Range(FirePointRandomMIN, FirePointRandomMAX));
            Vector3 _forward = FirePoint.TransformDirection(Vector3.forward);
            if (Physics.Raycast (FirePoint.position, _forward, out _hit, FireDistance, Mask))
            {
                Instantiate(HitParticles, _hit.point + _hit.normal * 0.01f, Quaternion.FromToRotation(Vector3.forward, _playerCamera.transform.forward));
                if (_hit.collider.tag == "mob_body")
                {
                    mobDistance = Vector3.Distance(transform.position, _hit.collider.transform.position);
                    _currentDamage = Damage / mobDistance / 10;
                    print("Текущий урон = " + (int)_currentDamage);
                }
            }
        }
    }
}
