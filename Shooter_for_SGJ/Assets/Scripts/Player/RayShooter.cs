using System.Collections;
using UnityEngine;

public class RayShooter : MonoBehaviour
{
    public int Damage = 10;

    [SerializeField] private Transform _hitLocation;
    private Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(_camera.pixelWidth / 2, _camera.pixelHeight / 2, 0);
            Ray ray = _camera.ScreenPointToRay(point);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
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
                    StartCoroutine(HitLocation(hitObject, hit.point)); // Добавить анимацию и префаб попадания в неживую поверхность
                }
            }
        }
    }

    private IEnumerator HitLocation(GameObject target, Vector3 pos)
    {
        Transform hitLocation = Instantiate(_hitLocation, target.transform, true);
        hitLocation.position = pos;

        yield return new WaitForSeconds(1);
        
        Destroy(hitLocation.gameObject);
    }
}
