using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Experience 
    [SerializeField] private int expValue = 1;
    
    // Logic
    public float triggerLenght = 1;
    public float chaseLenght = 5;
    private bool _isChasing;
    private bool _collidingWithPlayer;
    private Transform _playerTransform;
    private Vector3 _startingPosition;
    
    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D _hitbox;
    private Collider2D[] _hits = new Collider2D[10];
    
    // Constants
    private const string FighterTag = "Fighter";
    private const string PlayerName = "Player";

    protected override void Start()
    {
        base.Start();
        _playerTransform = GameManager.instance.player.transform;
        _startingPosition = transform.position;
        _hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    protected void FixedUpdate()
    {
        // Is the player in trigger range?
        if (Vector3.Distance(_playerTransform.position, _startingPosition) < chaseLenght)
        {
            if (Vector3.Distance(_playerTransform.position, _startingPosition) < triggerLenght)
                _isChasing = true;

            if (_isChasing)
            {
                if (!_collidingWithPlayer)
                {
                    UpdateMotor((_playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                UpdateMotor(_startingPosition - transform.position);
            }
        }
        else
        {
            UpdateMotor(_startingPosition - transform.position);
            _isChasing = false;
        }
        
        // Check for overlaps
        _collidingWithPlayer = false;
        _boxCollider.OverlapCollider(filter, _hits);
        for (int i = 0; i < _hits.Length; i++)
        {
            if (_hits[i] == null)
                continue;

            if (_hits[i].CompareTag(FighterTag) && _hits[i].name == PlayerName)
            {
                _collidingWithPlayer = true;
            }

            // The array is not cleaned up, so we do it ourself
            _hits[i] = null;
        }
    }

    protected override void Death()
    {
        Destroy(gameObject);
        GameManager.instance.GrantExperience(expValue);
        GameManager.instance.ShowText($"+ {expValue} XP!", 30, Color.cyan, transform.position, Vector3.up * 40, 1.0f);
    }
}
