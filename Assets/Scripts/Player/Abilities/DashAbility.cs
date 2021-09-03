using System.Collections;
using System.Collections.Generic;
using DevilsDen.Events;
using UnityEngine;
namespace DevilsDen.Abilities
{
    public class DashAbility : Ability
    {

        [Header("Settings")]
        [SerializeField] float cooldown = 2f;
        [SerializeField] float duration = 1.5f;
        [SerializeField] float velocity = 50f;
        [SerializeField] float delay = 0.5f;
        [SerializeField] float doubleTapTime = 0.3f;

        string previousKey;
        float previousKeyTime = 0.0f;
        bool isDashIntent
        {
            get
            {
                return (Time.time - previousKeyTime) <= doubleTapTime;
            }
        }

        void Update()
        {
            if (InputManager.instance.GetKeyDown(InputManager.Forward))
            {
                if (previousKey == InputManager.Forward && isDashIntent)
                    Cast();
                previousKey = InputManager.Forward;
                previousKeyTime = Time.time;
            }
            if (InputManager.instance.GetKeyDown(InputManager.Left))
            {
                if (previousKey == InputManager.Left && isDashIntent)
                    Cast();
                previousKey = InputManager.Left;
                previousKeyTime = Time.time;
            }
            if (InputManager.instance.GetKeyDown(InputManager.Backward))
            {
                if (previousKey == InputManager.Backward && isDashIntent)
                    Cast();
                previousKey = InputManager.Backward;
                previousKeyTime = Time.time;
            }
            if (InputManager.instance.GetKeyDown(InputManager.Right))
            {
                if (previousKey == InputManager.Right && isDashIntent)
                    Cast();
                previousKey = InputManager.Right;
                previousKeyTime = Time.time;
            }
        }
        protected override void Cast()
        {
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add(AbilitiesDictionary.dashKey, previousKey);
            args.Add(AbilitiesDictionary.dashVelocity, velocity);
            args.Add(AbilitiesDictionary.dashDelay, delay);
            StartCoroutine(Dash(args));

        }

        IEnumerator Dash(Dictionary<string, object> args)
        {
            EventManager.TriggerEvent(AbilitiesDictionary.dash, args);
            yield return new WaitForSeconds(duration);
            EventManager.TriggerEvent(AbilitiesDictionary.dash, null);
        }

    }
}
