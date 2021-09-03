using UnityEngine;
public class InputManager : MonoBehaviour
{
    public const string Forward = "Forward";
    public const string Backward = "Backward";
    public const string Left = "Left";
    public const string Right = "Right";
    public const string GrenadeReload = "GrenadeReload";
    public const string GrenadeThrow = "GrenadeThrow";
    public const string BeaconThrow = "BeaconThrow";
    public const string Grenade = "Grenade";
    public static InputManager instance;
    public KeyBindings keyBindings;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(this);
        }
        DontDestroyOnLoad(this);
    }

    public bool GetKeyDown(string key)
    {
        if (Input.GetKeyDown(keyBindings.GetKeyBinding(key)))
        {
            return true;
        }
        return false;
    }
}
