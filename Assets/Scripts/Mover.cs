using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : Fighter
{
    protected BoxCollider2D _boxCollider;
    protected Vector3 _moveDelta;
    protected RaycastHit2D _hit;
    protected float ySpeed = 0.75f;
    protected float xSpeed = 1.0f;

    protected virtual void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        // Reset _moveDelta
        _moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        
        // Swap sprite direction, whether you're going: right or left
        if (_moveDelta.x > 0) 
            transform.localScale = Vector3.one;
        else if (_moveDelta.x < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        
        // Add push vector, if any exists
        _moveDelta += pushDirection;
        
        // Reduce push force by time, based on recovery speed
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);
        
        // check raycast hit
        _hit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(0, _moveDelta.y),
            Mathf.Abs(_moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (_hit.collider == null) 
        {
            // Moving
            transform.Translate(0, _moveDelta.y * Time.deltaTime, 0);
        }
        
        _hit = Physics2D.BoxCast(transform.position, _boxCollider.size, 0, new Vector2(_moveDelta.x, 0),
            Mathf.Abs(_moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (_hit.collider == null) 
        {
            // Moving
            transform.Translate(_moveDelta.x * Time.deltaTime, 0, 0);
        }
    }
}
