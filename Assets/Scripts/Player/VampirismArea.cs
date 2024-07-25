using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VampirismArea : MonoBehaviour
{
    private List<Enemy> enemies = new List<Enemy>();
    private string EnemyLayerName = "Enemy";
    private Coroutine _coroutine;
    public float Range { get; private set; } = 5f;

    public event Action<Enemy> NearestEnemyGeted;

    public void Play()
    {
        _coroutine = StartCoroutine(GetEnemiesCoroutine());
    }

    public void Stop()
    {
        StopCoroutine(_coroutine);
    }

    private IEnumerator GetEnemiesCoroutine()
    {
        bool isWork = true;

        while (isWork)
        {
            Collider2D[] enemyInRadius = Physics2D.OverlapCircleAll(transform.position, Range, LayerMask.GetMask(EnemyLayerName));

            enemies = enemyInRadius.Select(collider=>collider.GetComponent<Enemy>()).ToList();

            Debug.Log(enemies.Count);

            NearestEnemyGeted?.Invoke(GetNearestEnemy());

            yield return null;
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
