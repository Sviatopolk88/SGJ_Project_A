using System.Collections;
using UnityEngine;

public class EnemyBase : MonoBehaviour, IHittable
{
    public int Health = 100;
    public int Damage = 10;
    public bool isDead => Health <= 0;
    public void HitObject(int damage)
    {
        this.Health -= damage;
        
        if (isDead)
            Destroy(gameObject); // Добавить анимацию смерти
    }
}
