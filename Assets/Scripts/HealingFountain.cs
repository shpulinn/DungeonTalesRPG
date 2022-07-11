using UnityEngine;

public class HealingFountain : Collidable
{
    [SerializeField]
    private int healingAmount = 1;
    [SerializeField]
    private float healCooldown = 1.0f;
    
    private float _lastHeal;

    protected override void OnCollide(Collider2D col)
    {
        if (col.name != "Player")
            return;
        
        if (Time.time - _lastHeal > healCooldown)
        {
            _lastHeal = Time.time;
            GameManager.instance.player.Heal(healingAmount);
        }
    }
}
