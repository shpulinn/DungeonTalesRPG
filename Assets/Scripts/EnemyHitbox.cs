using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    // Damage
    public int damage = 1;
    public float pushForce = 3;

    // Constants
    private const string FighterTag = "Fighter";
    private const string PlayerName = "Player";
    
    protected override void OnCollide(Collider2D col)
    {
        if (col.CompareTag(FighterTag) && col.name == PlayerName)
        {
            // Create a new damage object, before sending in on the player
            Damage dmg = new Damage
            {
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce
            };
            
            col.SendMessage("ReceiveDamage", dmg);
        }
    }
}
