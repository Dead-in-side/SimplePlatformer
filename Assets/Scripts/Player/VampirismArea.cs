using System;
using System.Collections.Generic;
using UnityEngine;

public class VampirismArea : MonoBehaviour
{
    private float _range = 5f;

    public float Range => _range;

    public List<Enemy> GetEnemies()
    {
        List<Enemy> enemies = new List<Enemy>();

        Collider2D[] collidersInside = Physics2D.OverlapCircleAll(transform.position, _range);

        foreach(Collider2D collider in collidersInside)
        {
            if(collider.TryGetComponent(out Enemy enemy))
            {
                enemies.Add(enemy);
            }
        }

        return enemies;
    }
}
