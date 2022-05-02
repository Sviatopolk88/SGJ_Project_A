using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public int Damage = 10;
    public float floatInfrontOfWall;

    public int _bullets = 99;

    //private PlayerUI _playerUI => transform.GetComponent<PlayerUI>();

    [SerializeField] private Transform _bulletHole;

    private Camera _camera;

    void Start()
    {
        _camera = GetComponentInParent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Shooting()
    {
        if (_bullets > 0)
        {
            _bullets--;

            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                GameObject hitObject = hit.transform.gameObject;
                IHittable target = hitObject.GetComponent<IHittable>();
                if (target != null)
                {
                    target.HitObject(Damage);
                    StartCoroutine(HitLocation(hitObject, hit.point)); // Добавить анимацию и префаб попадания во врага
                }
                else
                {
                    Instantiate(_bulletHole, hit.point + hit.normal * floatInfrontOfWall, Quaternion.LookRotation(hit.normal)); // Добавить анимацию и префаб попадания в неживую поверхность
                }
            }
        }
    }

    private void LevelPart(GameObject target)
    {
        Instantiate(_bulletHole, target.transform, true);
    }

    private IEnumerator HitLocation(GameObject target, Vector3 pos)
    {
        Transform hitLocation = Instantiate(_bulletHole, target.transform, true);
        hitLocation.position = pos;

        yield return new WaitForSeconds(1);

        if (target != null)
        {
            Destroy(hitLocation.gameObject);
        }

    }
}
