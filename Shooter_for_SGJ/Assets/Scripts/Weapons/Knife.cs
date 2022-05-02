using UnityEngine;

public class Knife : MonoBehaviour
{
    public int Damage = 40;
    public Vector3 DistanceAttack = Vector3.one;
    public LayerMask MaskEnemy;

    void Start()
    {
    }

    public void HitKnife()
    {
        if (!DialogCheck.IsDialog)
        {
            // Анимация


            // Поиск всех столкновений
            Collider[] Enemies = Physics.OverlapBox(transform.position, DistanceAttack, Quaternion.identity, MaskEnemy);
            
            // Нанесение урона
            foreach(Collider enemy in Enemies)
            {
                IHittable target = enemy.transform.GetComponent<IHittable>();
                if (target != null)
                {
                    target.HitObject(Damage);
                }
            }
        }
    }
    // Прорисовка области поражения ножа
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, DistanceAttack);
    }
}
