using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player player;
    Transform _targetPoint;
    Transform _tr;
    public Enemy currentEnemyTarget;
    public Animator PlayerAnimation;
    public Transform targetEnemy;
    Vector3 _enemyPosition;
    Quaternion _targetRotation;
    public ParticleSystem ShootParticle;
    bool OnlyOne;
    private void Awake()
    {
        player = this;
    }
    private void Start()
    {
        _tr = transform;
        _targetPoint = JoystickController.joystick.transform;
    }
    private void Update()
    {
        Move(); 
        MoveDirection();
        FindClosestEnemy();
        EnemyDistance();
    }
    void Move()
    {
        _tr.position = Vector3.Lerp(_tr.position, _targetPoint.position, Time.deltaTime * 8f);
    }
    void MoveDirection()
    {
        if (_targetPoint.GetComponent<JoystickController>().moveVelocity != Vector3.zero)
        {
            _tr.rotation = Quaternion.Lerp(_tr.rotation, Quaternion.LookRotation(_targetPoint.GetComponent<JoystickController>().moveVelocity), Time.deltaTime * 8);
            PlayerAnimation.SetFloat("Blend", Mathf.Abs(_targetPoint.GetComponent<JoystickController>().movingSpeed));

        }
        else
        {
            PlayerAnimation.SetFloat("Blend", Mathf.Lerp(PlayerAnimation.GetFloat("Blend"), 0, Time.deltaTime * 8f));
        }
    }
    void EnemyDistance()
    {
        if (_targetPoint.GetComponent<JoystickController>().moveVelocity == Vector3.zero)
        {
            if(targetEnemy != null)
            if (Vector3.Distance(_tr.position, targetEnemy.position) < 3f)
            {
                if (!OnlyOne)
                {
                    OnlyOne = true;
                    StartCoroutine(shoott());
                }
                _enemyPosition = targetEnemy.position - _tr.position;
                _enemyPosition.y = 0;
                _targetRotation = Quaternion.LookRotation(_enemyPosition);
                _tr.rotation = Quaternion.Lerp(_tr.rotation, _targetRotation, Time.deltaTime * 8f);

            }
        }
    }
    void Shoot()
    {
        ShootParticle.Play();
        targetEnemy.gameObject.GetComponent<Enemy>().HealthofEnemy = 0;
    }
    IEnumerator shoott()
    {
        yield return new WaitForSeconds(1);
        OnlyOne = false;
        Shoot();
    }
    
    public Enemy FindClosestEnemy()
    {
        Enemy _enemy = null;
        float _closestEnemyDistance = Mathf.Infinity;
        Enemy _currentEnemy;
        if (LevelManager.levelManager.stage[0].enemy.Count > 0)
        {
            for (int i = 0; i < LevelManager.levelManager.stage[0].enemy.Count; i++)
            {
                _currentEnemy = LevelManager.levelManager.stage[0].enemy[i];
                float distanceToEnemy = Vector3.Distance(_currentEnemy.transform.position,_tr.position);
                if (distanceToEnemy < _closestEnemyDistance)
                {
                    _closestEnemyDistance = distanceToEnemy;
                    _enemy = _currentEnemy;
                    targetEnemy = _currentEnemy.transform;
                }
            }
            if (targetEnemy.GetComponent<Enemy>().HealthofEnemy == 0)
            {
                targetEnemy = null;
                return _enemy;
            }
            return _enemy;
        }
        else
        {
            targetEnemy = null;
            return _enemy;
        }
        
    }
}
