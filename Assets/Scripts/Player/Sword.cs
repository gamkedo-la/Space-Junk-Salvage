using UnityEngine;

public class Sword : MonoBehaviour
{
    float InternalChronometer;

    public int Combo;

    public int dam;

    public bool DashAttack;

    public PlayerAttacks myPlayerAttacks;

    private void OnTriggerEnter(Collider other)
    {
        // Check that dam > 0 to see that this is part of an attack and haven't already spent our damage
        if (dam > 0 && other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Health>().TakeDamage(dam);
            dam = 0;
        }
    }
}
