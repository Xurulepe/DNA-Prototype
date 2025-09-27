using UnityEngine;

public interface IAttackable 
{
    public void ReceiveAttack(AttackType attackType, int damage);
}
