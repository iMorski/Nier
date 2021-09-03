using System;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float SmoothTime;
    
    [NonSerialized] public Rigidbody Rigidbody;
    [NonSerialized] public Animator Animator;
    
    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Animator = GetComponentInChildren<Animator>();
    }
    
    private bool OnCast;

    public void ChangeState()
    {
        if (!OnCast) Animator.CrossFade("Movement-Cast", SmoothTime);
        else Animator.CrossFade("Movement-Free", SmoothTime);
        
        OnCast = !OnCast;
    }

    private Vector3 Position;

    public void ChangePosition(Vector3 Direction, float Distance)
    {
        if (Direction != new Vector3())
        {
            Position = Vector3.Normalize(new Vector3(
                Direction.x, .0f, Direction.z));

            SmoothRotation();
        }
        else
        {
            SmoothStop();
        }
        
        SmoothAnimatorSpeed(Distance);

        Rigidbody.velocity = Position * SmoothSpeed();
    }
    
    private Vector3 PositionVelocity;
    
    public void SmoothStop()
    {
        if (Position != new Vector3()) Position = 
            Vector3.SmoothDamp(Position, new Vector3(),
                ref PositionVelocity, SmoothTime);
    }

    private Vector3 RotationDirection;
    private Vector3 RotationVelocity;

    public void SmoothRotation()
    {
        if (RotationDirection != Position) RotationDirection = 
            Vector3.SmoothDamp(RotationDirection, Position,
                ref RotationVelocity,SmoothTime);
        
        transform.rotation = Quaternion.LookRotation(RotationDirection);
    }
    
    private float AnimatorSpeed;
    private float AnimatorVelocity;

    public void SmoothAnimatorSpeed(float Value)
    {
        if (AnimatorSpeed != Value) AnimatorSpeed = 
            Mathf.SmoothDamp(AnimatorSpeed, Value,
                ref AnimatorVelocity, SmoothTime);
        
        Animator.SetFloat("Speed", AnimatorSpeed);
    }
    
    private float SpeedCurrent;
    private float SpeedVelocity;
    
    public float SmoothSpeed()
    {
        if (OnCast && SpeedCurrent > Speed / 2.0f) SpeedCurrent =
            Mathf.SmoothDamp(SpeedCurrent, Speed / 2.0f, ref SpeedVelocity, SmoothTime);
        
        else if (!OnCast && SpeedCurrent < Speed) SpeedCurrent =
            Mathf.SmoothDamp(SpeedCurrent, Speed, ref SpeedVelocity, SmoothTime);

        return SpeedCurrent;
    }
}