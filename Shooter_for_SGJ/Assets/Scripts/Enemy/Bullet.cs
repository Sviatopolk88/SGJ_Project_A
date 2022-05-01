using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage = 10;
    public float Speed = 10f;

    private Rigidbody _rigidbody;
    private float _timer = 2f;
    private Vector3 direction;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
            Destroy(this.gameObject);
        else
            _rigidbody.velocity = transform.forward * Speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        IHittable hitObject = collision.gameObject.GetComponent<IHittable>();
        if (hitObject != null)
        {
            hitObject.HitObject(this.Damage);
        }
        Destroy(this.gameObject);
    }
}
