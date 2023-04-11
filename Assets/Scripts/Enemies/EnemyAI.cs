using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] private float roamChangeDirectionTimer = 2f;

    private enum State
    {
        Roaming
    }
        
    private State state;
    private EnemyPathfinding enemyPathfinding;    
    

    private void Awake()
    {
        state = State.Roaming;
        enemyPathfinding = GetComponent<EnemyPathfinding>();        
    }

    private void Start()
    {
        StartCoroutine(RoamingRoutine());
    }       

    private IEnumerator RoamingRoutine()
    {
        while(state == State.Roaming)
        {
            Vector2 roamingDirection = GetRoamingPosition();
            enemyPathfinding.SetTarget(roamingDirection);
            yield return new WaitForSeconds(roamChangeDirectionTimer);
        }
    }

    private Vector2 GetRoamingPosition()
    {
        return new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }
}
