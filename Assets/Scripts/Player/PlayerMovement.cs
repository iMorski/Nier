using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    /*
    
    private void Start()
    {
        PlayerSphere Sphere = PlayerContainer.Instance.PlayerSphere;
        
        Sphere.CastBegin += OnCastBegin;
        Sphere.CastFinish += OnCastFinish;
    }
    
    */

    private void OnCastBegin() { ChangeState(); }
    private void OnCastFinish() { ChangeState(); }

    private void FixedUpdate()
    {
        ChangePosition(new Vector3(PlayerContainer.Instance.Joystick.Direction.x, 0.0f,
            PlayerContainer.Instance.Joystick.Direction.y));
    }
}
