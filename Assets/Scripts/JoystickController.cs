using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class JoystickController : MonoBehaviour
{
    public static JoystickController joystick;
    public FloatingJoystick floatingJoystick;
    public Vector3 moveVelocity;
    public NavMeshAgent _player;
    public float movingSpeed;
    private void Awake()
    {
        joystick = this;
        _player = GetComponent<NavMeshAgent>();
    }
    public void FixedUpdate()
    {
        moveVelocity = floatingJoystick.Direction;
        moveVelocity.z = moveVelocity.y;
        moveVelocity.y = 0;
        _player.velocity = moveVelocity * _player.speed;
        movingSpeed = Mathf.Sqrt(Mathf.Pow(_player.velocity.x, 2) + Mathf.Pow(_player.velocity.z, 2)) / 2f;
    }
}