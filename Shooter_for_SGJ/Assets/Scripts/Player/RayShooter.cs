using AssemblyCSharp.Assets.Scripts.Player;
using System.Collections;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    public int Damage = 10;
    public float floatInfrontOfWall;

    private PlayerUI _playerUI => transform.GetComponent<PlayerUI>();

    [SerializeField] private Transform _bulletHole;

    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        Shooting();
        GetDamage();
    }

    private void GetDamage()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            _playerUI.SetHP(-25);
        }
    }

    private void Shooting()
    {
        if (_playerUI.Bullets > 0)
        {
            if (_playerUI.GameIsOver == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    _playerUI.SetBullets(_playerUI.Bullets--);
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
