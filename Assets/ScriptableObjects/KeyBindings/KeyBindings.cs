using UnityEngine;

[CreateAssetMenu(fileName = "KeyBindings", menuName = "KeyBindings")]
public class KeyBindings : ScriptableObject
{
    public KeyCode Forward, Backward, Left, Right, GrenadeReload, GrenadeThrow, BeaconThrow, Grenade;
    public KeyCode GetKeyBinding(string key)
    {
        switch (key)
        {
            case InputManager.Forward:
                return Forward;
            case InputManager.Backward:
                return Backward;
            case InputManager.Left:
                return Left;
            case InputManager.Right:
                return Right;
            case InputManager.GrenadeReload:
                return GrenadeReload;
            case InputManager.GrenadeThrow:
                return GrenadeThrow;
            case InputManager.BeaconThrow:
                return BeaconThrow;
            case InputManager.Grenade:
                return Grenade;
            default:
                return KeyCode.None;
        }
    }
}
