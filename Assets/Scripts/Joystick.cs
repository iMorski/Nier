using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [Range(0.0f, 1.0f)][SerializeField] private float NullDistance;
    
    [SerializeField] private GameObject HandleArea;
    [SerializeField] private GameObject Handle;
    
    [NonSerialized] public Vector2 Direction;
    [NonSerialized] public float Distance;
    
    private float HandleAreaRadius;
    private void Start()
    {
        Rect HandleAreaRect = HandleArea.GetComponent<RectTransform>().rect;
        
        HandleAreaRadius = Math.Max(HandleAreaRect.width, HandleAreaRect.height) / 4.0f *
                           GetComponentInParent<Canvas>().scaleFactor;
    }

    private Vector2 BeginMousePosition;
    
    public void OnPointerDown(PointerEventData Data)
    {
        if (!InScreen(Data)) return;
        
        BeginMousePosition = MousePosition(Data);

        HandleArea.transform.position = BeginMousePosition;
        Handle.transform.position = BeginMousePosition;
    }

    public void OnDrag(PointerEventData Data)
    {
        Vector2 DirectionInPixel = MousePosition(Data) - BeginMousePosition;
        Vector2 DirectionInPixelInCircle = Vector2.ClampMagnitude(DirectionInPixel, HandleAreaRadius);

        float DragDistance = Vector2.Distance(MousePosition(Data), BeginMousePosition);

        if (DragDistance > HandleAreaRadius ||
            DragDistance > HandleAreaRadius)
        {
            BeginMousePosition = BeginMousePosition + (DirectionInPixel - DirectionInPixelInCircle);
        }
        
        HandleArea.transform.position = BeginMousePosition;
        Handle.transform.position = MousePosition(Data);
        
        Distance = Vector2.Distance(new Vector2(), DirectionInPixelInCircle) / HandleAreaRadius;
        
        if (Distance > NullDistance) Direction = new Vector2(
            DirectionInPixelInCircle.x / HandleAreaRadius,
            DirectionInPixelInCircle.y / HandleAreaRadius);
        else Direction = new Vector2();
    }

    public void OnPointerUp(PointerEventData Data)
    {
        Direction = new Vector2();
        Distance = 0.0f;
        
        HandleArea.transform.localPosition = new Vector3();
        Handle.transform.localPosition = new Vector3();
    }

    private bool InScreen(PointerEventData Data)
    {
        return Data.hovered.Contains(gameObject);
    }

    private Vector2 MousePosition(PointerEventData Data)
    {
        return Data.position;
    }
}