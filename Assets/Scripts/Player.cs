using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Player : Mover
{
    private SpriteRenderer _spriteRenderer;

    private bool _isAlive = true;

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void ReceiveDamage(Damage dmg)
    {
        if (_isAlive == false)
            return;
        
        base.ReceiveDamage(dmg);
        // Update Health bar
        GameManager.instance.OnHealthPointChange();
    }

    protected override void Death()
    {
        _isAlive = false;
        GameManager.instance.deathMenuAnimator.SetTrigger("Show");
    }

    private void FixedUpdate()
    {
        if (_isAlive == false)
            return;
        
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

    public void Respawn()
    {
        Heal(maxHealthPoint);
        _isAlive = true;
        lastImmune = Time.time;
        pushDirection = Vector3.zero;
    }
}
