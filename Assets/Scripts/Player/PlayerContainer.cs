using UnityEngine;

public class PlayerContainer : MonoBehaviour
{
    public static PlayerContainer Instance;

    public UiJoystick Joystick;

    private void Awake()
    {
        Instance = this;
    }
}
