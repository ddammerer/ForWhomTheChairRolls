using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody), typeof(XRGrabInteractable))]
[DefaultExecutionOrder(100)]
public class GrabbedDoorPlayerBlocker : MonoBehaviour
{
    [SerializeField] Collider[] doorColliders;
    [SerializeField] string playerTag = "Player";
    [SerializeField] float releaseAngleBuffer = 2f;

    Rigidbody rb;
    XRGrabInteractable grabInteractable;
    HingeJoint hingeJoint;
    CharacterController playerController;
    JointLimits originalLimits;
    Vector3 lastClearPosition;
    Quaternion lastClearRotation;
    float lastClearAngle;
    bool hasOriginalLimits;
    bool originalUseLimits;
    bool hingeBlocked;
    bool blockedAtUpperLimit;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
        hingeJoint = GetComponent<HingeJoint>();

        if (hingeJoint != null)
        {
            originalLimits = hingeJoint.limits;
            originalUseLimits = hingeJoint.useLimits;
            hasOriginalLimits = true;
        }

        if (doorColliders == null || doorColliders.Length == 0)
            doorColliders = GetComponentsInChildren<Collider>();

        CacheCurrentPose();
    }

    void FixedUpdate()
    {
        if (!grabInteractable.isSelected)
        {
            RestoreHingeLimits();
            CacheCurrentPose();
            return;
        }

        ReleaseHingeBlockIfPulledBack();

        if (playerController == null)
            playerController = FindPlayerController();

        if (playerController == null || !OverlapsPlayer())
        {
            CacheCurrentPose();
            return;
        }

        BlockHingeAtLastClearAngle();
        rb.position = lastClearPosition;
        rb.rotation = lastClearRotation;
#if UNITY_2023_3_OR_NEWER
        rb.linearVelocity = Vector3.zero;
#else
        rb.velocity = Vector3.zero;
#endif
        rb.angularVelocity = Vector3.zero;
    }

    CharacterController FindPlayerController()
    {
        var players = GameObject.FindGameObjectsWithTag(playerTag);
        foreach (var player in players)
        {
            if (player.TryGetComponent(out CharacterController controller))
                return controller;

            controller = player.GetComponentInChildren<CharacterController>();
            if (controller != null)
                return controller;
        }

        return FindFirstObjectByType<CharacterController>();
    }

    bool OverlapsPlayer()
    {
        if (!playerController.enabled)
            return false;

        foreach (var doorCollider in doorColliders)
        {
            if (doorCollider == null || !doorCollider.enabled || doorCollider.isTrigger)
                continue;

            if (Physics.ComputePenetration(
                    doorCollider, doorCollider.transform.position, doorCollider.transform.rotation,
                    playerController, playerController.transform.position, playerController.transform.rotation,
                    out _, out _))
                return true;
        }

        return false;
    }

    void BlockHingeAtLastClearAngle()
    {
        if (hingeJoint == null || !hasOriginalLimits)
            return;

        var limits = originalLimits;
        blockedAtUpperLimit = hingeJoint.angle >= lastClearAngle;

        if (blockedAtUpperLimit)
            limits.max = Mathf.Min(limits.max, lastClearAngle);
        else
            limits.min = Mathf.Max(limits.min, lastClearAngle);

        hingeJoint.limits = limits;
        hingeJoint.useLimits = true;
        hingeBlocked = true;
    }

    void ReleaseHingeBlockIfPulledBack()
    {
        if (!hingeBlocked || hingeJoint == null)
            return;

        var pulledBackFromUpperLimit = blockedAtUpperLimit && hingeJoint.angle < lastClearAngle - releaseAngleBuffer;
        var pulledBackFromLowerLimit = !blockedAtUpperLimit && hingeJoint.angle > lastClearAngle + releaseAngleBuffer;

        if (pulledBackFromUpperLimit || pulledBackFromLowerLimit)
            RestoreHingeLimits();
    }

    void RestoreHingeLimits()
    {
        if (!hingeBlocked || hingeJoint == null || !hasOriginalLimits)
            return;

        hingeJoint.limits = originalLimits;
        hingeJoint.useLimits = originalUseLimits;
        hingeBlocked = false;
    }

    void CacheCurrentPose()
    {
        lastClearPosition = rb.position;
        lastClearRotation = rb.rotation;
        if (hingeJoint != null)
            lastClearAngle = hingeJoint.angle;
    }
}
