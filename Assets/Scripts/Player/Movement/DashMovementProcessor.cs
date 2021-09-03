using System.Collections;
using System.Collections.Generic;
using DevilsDen.Events;
using UnityEngine;

namespace DevilsDen.Movement
{
    public class DashMovementProcessor : BaseMovementProcessor
    {
        Transform cam;
        string key;
        Vector3 direction;
        float velocity;
        Vector3 vector = Vector3.zero;

        void Awake() => EventManager.StartListening(AbilitiesDictionary.dash, OnDash);
        void OnDestroy() => EventManager.StopListening(AbilitiesDictionary.dash, OnDash);

        void Start() => cam = Camera.main.transform;

        void Update()
        {
            vector = Vector3.Slerp(vector, Vector3.zero, 5 * Time.deltaTime);
            Value = vector;
        }

        void OnDash(Dictionary<string, object> args)
        {
            if (args == null)
                enabled = false;
            else
            {
                StartCoroutine(Dash(args));
            }
        }
        IEnumerator Dash(Dictionary<string, object> args)
        {
            var delay = (float)args[AbilitiesDictionary.dashDelay];
            yield return new WaitForSeconds(delay);

            enabled = true;
            key = args[AbilitiesDictionary.dashKey] as string;
            velocity = (float)args[AbilitiesDictionary.dashVelocity];
            switch (key)
            {
                case InputManager.Forward:
                    direction = cam.forward;
                    break;
                case InputManager.Left:
                    direction = -cam.right;
                    break;
                case InputManager.Backward:
                    direction = -cam.forward;
                    break;
                case InputManager.Right:
                    direction = cam.right;
                    break;
            }
            direction.y = 0f;
            vector = direction * velocity;
        }

    }
}