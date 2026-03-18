using UnityEngine;

/// <summary>
/// Moves the GameObject forward and rotates it on the Y-axis.
/// Rotation speed is derived from movement speed multiplied by a configurable multiplier.
/// </summary>
public class ForwardMoverWithRotation : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private bool isMoving = true;

    [Header("Rotation Settings")]
    [Tooltip("Scales how much moveSpeed affects Y rotation speed. " +
             "E.g. multiplier=10 at moveSpeed=5 → 50 deg/sec rotation.")]
    [SerializeField] private float rotationMultiplier = 10f;

    [Tooltip("Rotates clockwise when positive, counter-clockwise when negative.")]
    [SerializeField] private float rotationDirection = 1f;

    // -----------------------------------------------------------------------
    // Public API
    // -----------------------------------------------------------------------

    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    public void StartMoving() => isMoving = true;
    public void StopMoving()  => isMoving = false;

    // -----------------------------------------------------------------------
    // Unity lifecycle
    // -----------------------------------------------------------------------

    private void Update()
    {
        if (!isMoving) return;

        float delta = Time.deltaTime;

        // Move forward along local Z
        //transform.position += transform.forward * (moveSpeed * delta);

        // Rotate Y proportionally to current move speed
        float yRotationThisFrame = moveSpeed * rotationMultiplier * rotationDirection * delta;
        transform.Rotate(0f, yRotationThisFrame, 0f, Space.Self);
    }
}
