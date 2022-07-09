using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int[] damagePoint = {1, 2, 3, 4, 5, 6, 7};
    public float[] pushForce = { 2.0f, 2.2f, 2.4f, 2.8f, 3.0f, 3.4f, 3.8f };
    
    // Upgrade section
    public int weaponLevel = 0;
    private SpriteRenderer _spriteRenderer;

    // Swing section
    [SerializeField] private float cooldown = 0.5f;
    private float _lastSwing;
    private Animator _animator;
    private int _swingTriggerID;
    
    // Constants section
    private const string PlayerName = "Player";
    private const string FighterTag = "Fighter";
    
    protected override void Start()
    {
        base.Start();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
        
        // Assign ANIM ID
        _swingTriggerID = Animator.StringToHash("Swing");
    }

    protected override void Update()
    {
        base.Update();
        if (!Input.GetKeyDown(KeyCode.Space)) return;
        if (Time.time - _lastSwing > cooldown)
        {
            _lastSwing = Time.time;
            Swing();
        }
    }

    protected override void OnCollide(Collider2D col)
    {
        if (col.tag == FighterTag)
        {
            if (col.name == PlayerName) return;
            
            // Create a new damage object, then send it to the fighter we have hit
            Damage dmg = new Damage
            {
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel]
            };
            
            col.SendMessage("ReceiveDamage", dmg);
            
            Debug.Log(col.name);
        }
    }
    
    private void Swing()
    {
        _animator.SetTrigger(_swingTriggerID);
    }

    public void Upgrade()
    {
        weaponLevel++;
        _spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
        
        // Change weapon stats
    }
}
