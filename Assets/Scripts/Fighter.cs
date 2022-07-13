using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    // Public fields
    public int healthPoint;
    public int maxHealthPoint = 10;
    public float pushRecoverySpeed = 0.2f;
    
    // Immunity
    protected float immuneTime = 1.0f;
    protected float lastImmune;
    
    // Push
    protected Vector3 pushDirection;
    
    // All fighters can ReceiveDamage or Dio
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            healthPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;
            
            GameManager.instance.ShowText(dmg.damageAmount.ToString(), 15, Color.red, transform.position, Vector3.zero, 0.5f);

            if (healthPoint <= 0)
            {
                healthPoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {
        
    }
}
