using UnityEngine;

public class CharacterContainer : MonoBehaviour
{
    public static CharacterContainer Instance;

    public float MoveSpeed;
    public float MoveSmoothTime;

    private void Awake()
    {
        Instance = this;
    }
}
