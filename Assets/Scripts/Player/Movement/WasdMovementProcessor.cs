using System.Collections.Generic;
using DevilsDen.Events;
using UnityEngine;

namespace DevilsDen.Movement
{
    public class WasdMovementProcessor : BaseMovementProcessor
    {
        [Header("References")]
        [SerializeField] Transform cam;

        [Header("Settings")]
        [SerializeField] float velocity = 10f;
        [SerializeField] float rotationVelocity = 0.1f;

        float _currentRotationVelocity;

        void Awake() => EventManager.StartListening(AbilitiesDictionary.dash, OnDash);

        void OnDestroy() => EventManager.StopListening(AbilitiesDictionary.dash, OnDash);

        void Update()
        {
            Vector3 movementVector = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            if (movementVector.magnitude > 0.1f)
            {
                // Mathf.Atan2 returns the angle in radians between the X axis and the vector (y, x). Otherwise from the Y axis and (x, y)
                float targetAngle = Mathf.Atan2(movementVector.x, movementVector.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float nextFrameAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _currentRotationVelocity, rotationVelocity);
                transform.rotation = Quaternion.Euler(0f, nextFrameAngle, 0f);
                movementVector = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward * velocity;
            }
            Value = movementVector;
        }

        void OnDash(Dictionary<string, object> args) => enabled = args == null;
    }
}