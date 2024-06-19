using UnityEngine;
using UnityEngine.EventSystems;

public class VirtualJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickRect;
    public float radius = 100f;
    public float Horizontal => InputDirection.x;
    public float Vertical => InputDirection.z;

    private Vector3 InputDirection { get; set; }
    private Vector2 inputVector;
    private Vector2 joystickCenter;

    private void Start()
    {
        InputDirection = Vector3.zero;
        joystickCenter = joystickRect.anchoredPosition;
    }

    public void OnDrag(PointerEventData ped)
    {
        var parent = joystickRect.parent as RectTransform;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(parent, ped.position, ped.pressEventCamera, out Vector2 pos))
        {
            // 计算拖动点相对于摇杆中心的偏移量
            Vector2 offset = pos - joystickCenter;

            // 限制偏移量在圆形范围内
            if (offset.magnitude > radius)
            {
                offset = offset.normalized * radius;
            }

            // 计算输入向量
            inputVector = offset / radius;

            // 重定位摇杆图像
            joystickRect.anchoredPosition = joystickCenter + offset;
        }

        InputDirection = new Vector3(inputVector.x, 0, inputVector.y);
    }

    public void OnPointerDown(PointerEventData ped)
    {
        OnDrag(ped);
    }

    public void OnPointerUp(PointerEventData ped)
    {
        inputVector = Vector2.zero;
        joystickRect.anchoredPosition = joystickCenter;
        InputDirection = Vector3.zero;
    }
}
