using UnityEngine;

public class EnemyMelee : EnemyBase
{
    [Header("Melee Settings")]
    [SerializeField] private Transform _meleeAttackPoint;
    [SerializeField] private Vector3 _meleeRange;
    [SerializeField] private int _meleeDamage = 10;
    [SerializeField] private LayerMask _playerLayer;

    protected override void Attack()
    {
        base.Attack();

        Collider[] hitPlayers = Physics.OverlapBox(_meleeAttackPoint.position, _meleeRange * 0.5f, Quaternion.identity, _playerLayer);

        foreach (Collider hitPlayer in hitPlayers)
        {
            if (hitPlayer.TryGetComponent(out Player player))
            {
                Debug.Log("Player Hit by Melee Attack");
                player.ReceiveAttack(AttackType.Melee, _meleeDamage);
            }
        }
    }

    protected override void Die()
    {
        GameObject healthRegen = HealthRegenPool.Instance.GetPooledObject();

        if (healthRegen != null)
        {
            healthRegen.transform.position = transform.position;
            healthRegen.transform.rotation = Quaternion.identity;
            healthRegen.SetActive(true);
        }

        base.Die();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(_meleeAttackPoint.position, _meleeRange);
    }
}
