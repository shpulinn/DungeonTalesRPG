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
        
        DontDestroyOnLoad(gameObject);
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
}
