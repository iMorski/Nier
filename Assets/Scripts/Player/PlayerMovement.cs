using UnityEngine;

public class PlayerMovement : CharacterMovement
{
    [SerializeField] private Joystick Joystick;
    
    private void Update()
    {
        var Direction = new Vector3();
        var Distance = .0f;

        if (Joystick.Direction != new Vector2())
        {
            Direction = new Vector3(Joystick.Direction.x,
                .0f, Joystick.Direction.y);

            Distance = 1.0f; // Joystick.Distance
        }
        else
        {
            Direction = new Vector3(Input.GetAxisRaw("Horizontal"),
                .0f, Input.GetAxisRaw("Vertical"));

            Distance = (Direction != new Vector3()) ? 1.0f : .0f;
        }
        
        ChangePosition(Direction, Distance);
    }
}
