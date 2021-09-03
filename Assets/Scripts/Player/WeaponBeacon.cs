using UnityEngine;

public class WeaponBeacon : MonoBehaviour
{
    public GameObject beacon;
    public Transform cam;
    private CharacterController controller;
    private float baseVelocity = 10f;
    private float holdMultiplier = 8f;
    private float _finalVelocity = 2f;
    private float maxVelocity = 20f;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_finalVelocity >= maxVelocity) return;
            _finalVelocity += holdMultiplier * Time.deltaTime;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            // throw and reset values
            Vector3 posShift = cam.forward.normalized * 5;
            posShift.y = 0f;
            Vector3 pos = controller.transform.position + posShift;
            GameObject newBeacon = Instantiate(beacon, pos, Quaternion.identity);
            newBeacon.GetComponent<Rigidbody>().velocity = cam.forward.normalized * _finalVelocity + (controller.velocity / 2);
            _finalVelocity = baseVelocity;
        }
    }
}
