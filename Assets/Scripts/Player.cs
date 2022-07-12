using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : Mover
{
    private SpriteRenderer _spriteRenderer;

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        base.ReceiveDamage(dmg);
        // Update Health bar
        GameManager.instance.OnHealthPointChange();
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        UpdateMotor(new Vector3(x, y, 0));
    }

    public void SwapSprite(int spriteID)
    {
        _spriteRenderer.sprite = GameManager.instance.playerSprites[spriteID];
    }

    public void PlayerLevelUp()
    {
        // On level up, player got more Maximum HP and restore current HP.
        maxHealthPoint++;
        healthPoint = maxHealthPoint;
        // Update Health bar
        GameManager.instance.OnHealthPointChange();
    }

    public void SetLevel(int level)
    {
        if (GameManager.instance.GetCurrentLevel() <= 1)
            return;
        for (int i = 0; i < level; i++)
        {
            PlayerLevelUp();
        }
    }

    public void Heal(int healingAmount)
    {
        // If HP is maximum already
        if (healthPoint == maxHealthPoint)
            return;
        
        healthPoint += healingAmount;
        // If HP after adding become more that allowed
        if (healthPoint > maxHealthPoint)
            healthPoint = maxHealthPoint;
        
        // Update Health bar
        GameManager.instance.OnHealthPointChange();
        // Show UI text
       GameManager.instance.ShowText("+" + healingAmount.ToString() + " HP!", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
    }
}
