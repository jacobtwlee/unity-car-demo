using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CarController : MonoBehaviour
{
    private Vector2 input;

    public WheelCollider frontLeftWheelCollider, frontRightWheelCollider, backLeftWheelCollider, backRightWheelCollider;
    public Transform frontLeftWheelTransform, frontRightWheelTransform, backLeftWheelTransform, backRightWheelTransform;
    public float maxSteerAngle;
    public float maxTorque;
    public bool hasAllWheelDrive;
    public Rigidbody carBody;

    void Start() {
        carBody.centerOfMass = new Vector3(0,-0.5f,0);
    }

    void FixedUpdate() {
        float torque = maxTorque * input.magnitude;

        frontLeftWheelCollider.motorTorque = torque;
        frontRightWheelCollider.motorTorque = torque;

        if (hasAllWheelDrive) {
            backLeftWheelCollider.motorTorque = torque;
            backRightWheelCollider.motorTorque = torque;
        }

        Vector2 forward = new Vector2(this.transform.forward.x, this.transform.forward.z);

        float steerAngle = Vector2.SignedAngle(input, forward);
        steerAngle = Mathf.Clamp(steerAngle, -maxSteerAngle, maxSteerAngle);

        frontLeftWheelCollider.steerAngle = steerAngle;
        frontRightWheelCollider.steerAngle = steerAngle;

        UpdateWheelPose(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPose(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPose(backLeftWheelCollider, backLeftWheelTransform);
        UpdateWheelPose(backRightWheelCollider, backRightWheelTransform);

        if (input == Vector2.zero) {
            frontLeftWheelCollider.brakeTorque = 100;
            frontRightWheelCollider.brakeTorque = 100;
            backLeftWheelCollider.brakeTorque = 100;
            backRightWheelCollider.brakeTorque = 100;
        } else {
            frontLeftWheelCollider.brakeTorque = 0;
            frontRightWheelCollider.brakeTorque = 0;
            backLeftWheelCollider.brakeTorque = 0;
            backRightWheelCollider.brakeTorque = 0;
        }
    }

    private void UpdateWheelPose(WheelCollider collider, Transform transform) {
        Vector3 position = transform.position;
        Quaternion rotation = transform.rotation;

        collider.GetWorldPose(out position, out rotation);

        transform.position = position;
        transform.rotation = rotation;
    }

    public void Move(InputAction.CallbackContext context) {
        input = context.ReadValue<Vector2>();
    }
}
