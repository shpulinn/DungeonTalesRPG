using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    // Logic
    protected bool Collected;

    private const string PlayerName = "Player";

    protected override void OnCollide(Collider2D col)
    {
        if (col.name == PlayerName)
            OnCollect();
    }

    protected virtual void OnCollect()
    {
        Collected = true;
    }
}
