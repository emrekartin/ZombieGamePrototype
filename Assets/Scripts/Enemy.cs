using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class Enemy : MonoBehaviour
{
    public Animator EnemyAnimator;
    NavMeshAgent _enemy;
    Transform _playerTransform;
    Transform _tr;
    Vector3 _playerPosition;
    Quaternion _targetRotation;
    public float HealthofEnemy = 1f;

    public Collider[] clds;
    void Awake()
    {
        _enemy = GetComponent<NavMeshAgent>();
        _tr = GetComponent<Transform>();
    }
    void Start()
    {
        _playerTransform = Player.player.transform;
    }
    void Update()
    {
        Move();
        Rotation();
        Attack();
        HealthCheck();
    }
    void Move()
    {
        _enemy.SetDestination(_playerTransform.position);
    }
    void Rotation()
    {
        _playerPosition = _playerTransform.position - _enemy.transform.position;
        _playerPosition.y = 0;
        _targetRotation = Quaternion.LookRotation(_playerPosition);
        _tr.rotation = Quaternion.Lerp(_tr.rotation, _targetRotation, Time.deltaTime * 8f);
    }
    void Attack()
    {
        if (Vector3.Distance(_playerPosition, _enemy.transform.position) < 1.5)
        {
            EnemyAnimator.SetBool("isAttacking",true);
        }
    }
    void HealthCheck()
    {
        if(HealthofEnemy == 0)
        {
            EnemyAnimator.enabled = false;
            for (int i = 0; i< LevelManager.levelManager.stage[0].enemy.Count;i++ )
            {
                if(gameObject == LevelManager.levelManager.stage[0].enemy[i])
                {
                    LevelManager.levelManager.stage[0].enemy.RemoveAt(i);
                }
            }
            GetComponent<Enemy>().enabled = false;
            _enemy.enabled = false;
            
        }
    }
}