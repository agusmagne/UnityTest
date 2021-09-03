using System.Collections.Generic;
using UnityEngine;
namespace DevilsDen.Movement
{
    public class MovementHandler : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private CharacterController controller = null;
        private readonly List<IMovementModifier> modifiers = new List<IMovementModifier>();

        public void AddModifier(IMovementModifier mod) => modifiers.Add(mod);
        public bool RemoveModifier(IMovementModifier mod) => modifiers.Remove(mod);

        private void Update() => Move();
        private void Move()
        {
            Vector3 vector = Vector3.zero;
            foreach (IMovementModifier mod in modifiers)
                vector += mod.Value;

            if (vector != Vector3.zero)
                controller.Move(vector * Time.deltaTime);
        }
    }
}
