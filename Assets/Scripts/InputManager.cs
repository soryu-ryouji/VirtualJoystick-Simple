using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public VirtualJoystick virtualJoystick; // 引用你的虚拟摇杆脚本
    public Image targetImage; // 要移动的 Image 对象
    public float moveSpeed = 20f; // 移动速度

    void Update()
    {
        // 获取虚拟摇杆的输入
        float horizontalInput = virtualJoystick.Horizontal;
        float verticalInput = virtualJoystick.Vertical;

        // 计算移动方向
        // Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0).normalized;
        Vector3 moveDirection = new Vector3(horizontalInput, verticalInput, 0);

        // 移动 Image 对象
        MoveImage(moveDirection);
    }

    void MoveImage(Vector3 moveDirection)
    {
        // 移动的距离，考虑每秒的移动速度
        float moveDistance = moveSpeed * Time.deltaTime;

        // 计算目标位置
        Vector3 targetPosition = targetImage.transform.position + moveDirection * moveDistance;

        // 更新 Image 对象的位置
        targetImage.transform.position = targetPosition;
    }
}
