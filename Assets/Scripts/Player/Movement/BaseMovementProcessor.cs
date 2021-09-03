using UnityEngine;
namespace DevilsDen.Movement
{
    public abstract class BaseMovementProcessor : MonoBehaviour, IMovementModifier
    {
        [Header("References")]
        [SerializeField] protected MovementHandler movementHandler = null;

        public Vector3 Value { get; protected set; }
        private void OnEnable() => movementHandler.AddModifier(this);
        private void OnDisable() => movementHandler.RemoveModifier(this);
    }
}