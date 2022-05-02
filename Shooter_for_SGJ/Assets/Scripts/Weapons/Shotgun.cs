using UnityEngine;

public class Shotgun : MonoBehaviour
{
    [SerializeField] private Transform _pellet;

    public int NumberOfPellets;

    public void Shoot()
    {
        for (int i = 0; i < NumberOfPellets; i++)
        {
            var pellet = Instantiate(_pellet);
            pellet.position = transform.position;
            pellet.rotation = transform.rotation;
            

            
            
        }
    }
}
