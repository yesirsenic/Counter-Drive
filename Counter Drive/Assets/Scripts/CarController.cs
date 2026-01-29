using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float moveSensitivity = 8f;
    [SerializeField] private float maxX = 3.5f;
    [SerializeField] private float moveSmooth = 12f;

    [Header("Tilt")]
    [SerializeField] private float tiltAngle = 36f;
    [SerializeField] private float tiltSmooth = 10f;

    private float targetX;
    private float currentTilt;

    // Input System
    private Vector2 pointerStartPos;
    private float startX;
    private bool isDragging;

    void Start()
    {
        targetX = transform.position.x;
    }

    void Update()
    {
        HandlePointerInput();
        ApplyMovement();
        ApplyTilt();
    }

    // =============================
    // New Input System (2026)
    // =============================
    void HandlePointerInput()
    {
        if (Pointer.current == null)
            return;

        if (Pointer.current.press.wasPressedThisFrame)
        {
            isDragging = true;
            pointerStartPos = Pointer.current.position.ReadValue();
            startX = targetX;
        }
        else if (Pointer.current.press.isPressed && isDragging)
        {
            Vector2 currentPos = Pointer.current.position.ReadValue();
            float deltaX = (currentPos.x - pointerStartPos.x) / Screen.width;

            targetX = startX + deltaX * moveSensitivity;
            targetX = Mathf.Clamp(targetX, -maxX, maxX);
        }
        else if (Pointer.current.press.wasReleasedThisFrame)
        {
            isDragging = false;
        }
    }

    // =============================
    // Movement
    // =============================
    void ApplyMovement()
    {
        Vector3 pos = transform.position;
        pos.x = Mathf.Lerp(pos.x, targetX, Time.deltaTime * moveSmooth);
        transform.position = pos;
    }

    void ApplyTilt()
    {
        float delta = targetX - transform.position.x;
        float targetTilt = -delta * tiltAngle;

        currentTilt = Mathf.Lerp(currentTilt, targetTilt, Time.deltaTime * tiltSmooth);
        transform.rotation = Quaternion.Euler(0f, -currentTilt, 0f);
    }
}