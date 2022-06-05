using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int damagePoint = 1;
    public float pushForce = 2.0f;
    
    // Upgrade section
    public int weaponLevel = 0;
    private SpriteRenderer _spriteRenderer;

    // Swing section
    [SerializeField] private float _cooldown = 0.5f;
    private float _lastSwing;
    
    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        base.Update();
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (Time.time - _lastSwing > _cooldown)
        {
            _lastSwing = Time.time;
            Swing();
        }
    }

    private void Swing()
    {
        Debug.Log("Swing");
    }
}
