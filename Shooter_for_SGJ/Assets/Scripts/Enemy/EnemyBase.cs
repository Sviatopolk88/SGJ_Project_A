using UnityEngine;

public class EnemyBase : MonoBehaviour, IHittable
{
    public float Health = 100f;
    public bool isDead => Health <= 0;
    public void HitObject(int damage)
    {
        this.Health -= damage;
        Debug.Log($"{transform.name} health: {Health}");
        if (isDead)
            Destroy(gameObject);
    }
}
