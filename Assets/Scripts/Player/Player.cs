using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float sensitivity = 0.05f;
    [SerializeField] private Transform headTransform;
    [SerializeField] private float movementSpeed = 1f;
    
    private Vector2 direction;
    private Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        EvaluateMovement();
    }
    
    public void Move(Vector2 _direction)
    {
        direction = _direction;
    }

    public void Look(Vector2 delta)
    {
        if (headTransform.eulerAngles.x - delta.y < 270f && headTransform.eulerAngles.x >= 270f)
        {
            var angles = headTransform.eulerAngles;
            angles.x = 270f;
            headTransform.eulerAngles = angles;
        }

        if (headTransform.eulerAngles.x - delta.y > 90f && headTransform.eulerAngles.x <= 90f)
        {
            var angles = headTransform.eulerAngles;
            angles.x = 90f;
            headTransform.eulerAngles = angles;
        }
        
        headTransform.RotateAround(headTransform.position, Vector3.up, delta.x * sensitivity);
        headTransform.RotateAround(headTransform.position, headTransform.right, -delta.y * sensitivity);
    }

    private void EvaluateMovement()
    {
        var forward = headTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        var right = headTransform.right;
        right.y = 0f;
        right.Normalize();

        rb.linearVelocity = (forward * direction.y + right * direction.x) * movementSpeed;
    }
}