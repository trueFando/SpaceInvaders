using UnityEngine;

public class DamageDealer : MonoBehaviour
{
    [SerializeField] private int damage;
    public int Damage => damage;

    public void Hit()
    {
        Destroy(gameObject);
    }
}
