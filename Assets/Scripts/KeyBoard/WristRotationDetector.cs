using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WristRotationDetector : MonoBehaviour
{
    public IMixedRealityHand hand;
    // Start is called before the first frame update
    void Start()
    {
        hand = GetComponent<IMixedRealityHand>();
    }

    // Update is called once per frame
    void Update()
    {
        if (hand != null && hand.TryGetJoint(TrackedHandJoint.Wrist, out MixedRealityPose wristPose))
        {
            Quaternion wristRotation = wristPose.Rotation;
            print(wristRotation);
            // Use wristRotation for further processing or manipulation
        }
    }
}
