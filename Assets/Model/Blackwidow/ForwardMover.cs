using UnityEngine;

/// <summary>
/// Moves the attached GameObject forward at a configurable speed.
/// "Forward" is relative to the object's local transform (its blue Z-axis).
/// </summary>
public class ForwardMover : MonoBehaviour
{
    [Header("Movement Settings")]
    [Tooltip("Movement speed in units per second.")]
    [SerializeField] private float moveSpeed = 5f;

    [Tooltip("Use Time.deltaTime (smooth, frame-rate independent) or Time.fixedDeltaTime for Rigidbody-based movement.")]
    [SerializeField] private bool useFixedUpdate = false;

    [Header("Controls")]
    [Tooltip("Whether the object is currently moving.")]
    [SerializeField] private bool isMoving = true;

    // -----------------------------------------------------------------------
    // Public API
    // -----------------------------------------------------------------------

    /// <summary>Current movement speed (units per second).</summary>
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }

    /// <summary>Starts or resumes forward movement.</summary>
    public void StartMoving() => isMoving = true;

    /// <summary>Pauses forward movement without resetting position.</summary>
    public void StopMoving() => isMoving = false;

    // -----------------------------------------------------------------------
    // Unity lifecycle
    // -----------------------------------------------------------------------

    private void Update()
    {
        if (!useFixedUpdate)
            MoveForward(Time.deltaTime);
    }

    private void FixedUpdate()
    {
        if (useFixedUpdate)
            MoveForward(Time.fixedDeltaTime);
    }

    // -----------------------------------------------------------------------
    // Core logic
    // -----------------------------------------------------------------------

    private void MoveForward(float deltaTime)
    {
        if (!isMoving) return;

        // transform.forward is the object's local +Z axis in world space.
        transform.position += transform.forward * (moveSpeed * deltaTime);
    }
}
