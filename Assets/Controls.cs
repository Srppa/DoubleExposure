using System;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public GameObject MenuReference;
    

    [SerializeField] private OVRHand leftHand;
    private bool wasPinching = false;

    void Start()
    {
      
    }

    void Update()
    {
        if (leftHand == null) return;
        if (!leftHand.IsTracked) return;

        bool isPinching = leftHand.GetFingerIsPinching(OVRHand.HandFinger.Index);

        // Only fire on the frame pinch STARTS
        if (isPinching && !wasPinching)
        {
            OnPinch();
        }

        wasPinching = isPinching;
    }

    void OnPinch()
    {
        MenuReference.SetActive(!MenuReference.activeSelf);
        // Your activation logic here
    }
}