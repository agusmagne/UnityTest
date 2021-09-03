using UnityEngine;
namespace DevilsDen.Movement
{
    public interface IMovementModifier
    {
        Vector3 Value { get; }
    }
}