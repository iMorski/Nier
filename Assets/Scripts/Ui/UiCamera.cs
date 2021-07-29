using UnityEngine;

public class UiCamera : MonoBehaviour
{
    [SerializeField] private Transform Character;
    [SerializeField] private float SmoothTime;

    private Vector3 Position;

    private void Awake()
    {
        Position = transform.position;
    }

    private Vector3 Velocity;
    
    private void Update()
    {
        Vector3 OnCharacterPosition = new Vector3(Position.x + Character.position.x,
            Position.y, Position.z + Character.position.z);

        transform.position = Vector3.SmoothDamp(transform.position,
            OnCharacterPosition, ref Velocity, SmoothTime);
    }
}
