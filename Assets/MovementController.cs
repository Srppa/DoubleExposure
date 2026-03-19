using Unity.Netcode;
using System.Collections;
using UnityEngine;

public class MovementController : NetworkBehaviour
{
    [Header("Rotation Phase")]
    [Tooltip("Random rotation between 230 and 360 degrees")]
    public float rotationSpeed = 120f;

    [Header("Movement Phase")]
    public float moveSpeed = 2f;
    [Tooltip("Max lateral drift amplitude (world units)")]
    public float lateralDriftAmount = 0.3f;
    [Tooltip("How fast the lateral drift oscillates")]
    public float driftFrequency = 1.5f;

    [Header("Forward Motion Length")]
    public float minMoveDistance = 2f;
    public float maxMoveDistance = 6f;

    [Header("Delays Between Phases (seconds)")]
    public float minDelayAfterRotation = 0.3f;
    public float maxDelayAfterRotation = 1.2f;
    public float minDelayAfterMove = 0.5f;
    public float maxDelayAfterMove = 2f;

    // ── internal state ──────────────────────────────────────────────
    private enum Phase { Idle, Rotating, DelayAfterRotation, Moving, DelayAfterMove }
    private Phase currentPhase = Phase.Idle;
    private bool isPaused = false;

    void Start()
    {
        StartCoroutine(BehaviourLoop());
    }

    public override void OnNetworkSpawn()
    {
        if (IsServer)                             // ← only server drives it
            StartCoroutine(BehaviourLoop());
    }

    // ── PUBLIC API ──────────────────────────────────────────────────

    public void Pause()
    {
        isPaused = true;
    }

    public void Play()
    {
        isPaused = false;
    }

    public bool IsPaused => isPaused;

    // ── HELPERS ─────────────────────────────────────────────────────

    /// Replacement for yield return null — also idles while paused.
    private IEnumerator WaitOneFrame()
    {
        yield return null;
        while (isPaused)
            yield return null;
    }

    /// Replacement for WaitForSeconds — respects pause mid-delay.
    private IEnumerator WaitSeconds(float seconds)
    {
        float elapsed = 0f;
        while (elapsed < seconds)
        {
            yield return null;
            if (!isPaused)
                elapsed += Time.deltaTime;
        }
    }

    // ── MAIN LOOP ───────────────────────────────────────────────────

    IEnumerator BehaviourLoop()
    {
        while (true)
        {
            // ── ROTATION PHASE ──────────────────────────────────────
            currentPhase = Phase.Rotating;
            float targetAngle = Random.Range(230f, 360f);
            float rotated = 0f;
            float direction = Random.value > 0.5f ? 1f : -1f;

            while (rotated < targetAngle)
            {
                yield return StartCoroutine(WaitOneFrame());
                float step = rotationSpeed * Time.deltaTime;
                step = Mathf.Min(step, targetAngle - rotated);
                transform.Rotate(0f, direction * step, 0f, Space.World);
                rotated += step;
            }

            // ── DELAY AFTER ROTATION ────────────────────────────────
            currentPhase = Phase.DelayAfterRotation;
            yield return StartCoroutine(WaitSeconds(Random.Range(minDelayAfterRotation, maxDelayAfterRotation)));

            // ── MOVEMENT PHASE ──────────────────────────────────────
            currentPhase = Phase.Moving;
            float targetDistance = Random.Range(minMoveDistance, maxMoveDistance);
            float travelled = 0f;
            float driftPhaseOffset = Random.Range(0f, Mathf.PI * 2f);

            while (travelled < targetDistance)
            {
                yield return StartCoroutine(WaitOneFrame());
                float dt = Time.deltaTime;

                float forwardStep = moveSpeed * dt;
                forwardStep = Mathf.Min(forwardStep, targetDistance - travelled);

                float driftInput = travelled * driftFrequency + driftPhaseOffset;
                float lateralDelta = Mathf.Sin(driftInput) * lateralDriftAmount * dt;

                Vector3 move = transform.forward * forwardStep
                             + transform.right * lateralDelta;
                transform.position += move;
                travelled += forwardStep;
            }

            // ── DELAY AFTER MOVEMENT ────────────────────────────────
            currentPhase = Phase.DelayAfterMove;
            yield return StartCoroutine(WaitSeconds(Random.Range(minDelayAfterMove, maxDelayAfterMove)));
        }
    }

#if UNITY_EDITOR
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, transform.forward * 2f);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, transform.right * 0.5f);
    }
#endif
}