using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private Transform _pellet;
    private AudioSource _shotSound;
    public int NumberOfPellets;

    private void Start()
    {
        _shotSound = GetComponent<AudioSource>();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void Shoot()
    {
        if (!DialogCheck.IsDialog)
        {
            _shotSound.Play();
            for (int i = 0; i < NumberOfPellets; i++)
            {
                var pellet = Instantiate(_pellet);
                pellet.position = transform.position;
                pellet.rotation = transform.rotation;
            }
        }
    }
}
