using UnityEngine;

public class Sword : MonoBehaviour
{
    public int Combo;

    public int dam;

    public bool DashAttack;

    private int Damage =>
        dam +
        (int) (dam * (Combo - 1) * .1f) +
        (DashAttack
            ? (int) (dam * .3f)
            : 0);

    private void OnTriggerEnter(Collider other)
    {
        // Check that dam > 0 to see that this is part of an attack and haven't already spent our damage
        if (dam > 0 && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(Damage);
            dam = 0;
        }
    }
}