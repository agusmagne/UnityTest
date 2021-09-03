using System.Collections.Generic;
using UnityEngine;

public class WeaponGrenade : MonoBehaviour
{
    [SerializeField] private List<GameObject> grenadesList;
    public GameObject grenade;
    private int direction = 1;
    // Update is called once per frame
    void Update()
    {
        if (grenadesList.Count >= 0 && grenadesList.Count < 3)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                InstantiateGrenade();
            }
        }

        if (grenadesList.Count > 0 && grenadesList.Count <= 3)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                DestroyGrenade();
            }
        }
    }

    void InstantiateGrenade()
    {
        Vector3 pos = transform.position - transform.forward;
        pos.y = transform.position.y;
        GameObject newGrenade = Instantiate(grenade);
        newGrenade.transform.SetParent(transform);
        newGrenade.transform.position = pos;
        newGrenade.GetComponent<OrbitPlayer>().direction = direction;
        newGrenade.transform.rotation = Quaternion.LookRotation(transform.right * direction);
        grenadesList.Add(newGrenade);
        direction *= -1;
    }

    void DestroyGrenade()
    {
        GameObject g = null;
        int count = 1;
        foreach (GameObject grenade in grenadesList)
        {
            if (count == grenadesList.Count)
            {
                g = grenade;
            }
            count++;
        }
        if (g != null)
        {
            Destroy(g);
            grenadesList.Remove(g);
        }
    }
}
