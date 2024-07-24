using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class VampirismArea : MonoBehaviour
{
    private CircleCollider2D _collider;
    private List<Enemy> enemies = new List<Enemy>();
    public float Range { get; private set; } = 5f;

    public event Action<Enemy> NearestEnemyGeted;

    private void Awake()
    {
        _collider = GetComponent<CircleCollider2D>();
        _collider.radius = Range;
        _collider.isTrigger = true;

        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            Debug.Log("!!!");

            enemies.Add(enemy);

            NearestEnemyGeted?.Invoke(GetNearestEnemy());
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Enemy enemy))
        {
            enemies.Remove(enemy);

            NearestEnemyGeted?.Invoke(GetNearestEnemy());
        }
    }

    private Enemy GetNearestEnemy()
    {
        Enemy nearestEnemy = null;

        if (enemies.Count > 0)
        {
            nearestEnemy = enemies[0];

            if (enemies.Count > 1)
            {
                for (int i = 1; i < enemies.Count; i++)
                {
                    if ((nearestEnemy.transform.position - transform.position).sqrMagnitude > (enemies[i].transform.position - transform.position).sqrMagnitude)
                    {
                        nearestEnemy = enemies[i];
                    }
                }
            }
        }

        return nearestEnemy;
    }
}
