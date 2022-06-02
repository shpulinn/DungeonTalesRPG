using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChestSprite;
    [SerializeField] private int _coinsAmount = 5; // maybe later it should be public !!!!

    private SpriteRenderer _spriteRenderer;

    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnCollect()
    {
        if (Collected) return;
        Collected = true;
        _spriteRenderer.sprite = emptyChestSprite;
        // Grant coins
    }
}
