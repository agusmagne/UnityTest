using System.Collections.Generic;
using DevilsDen.Events;
using UnityEngine;

namespace DevilsDen.Abilities
{
    public class GrenadeAbility : Ability
    {
        [Header("Settings")]
        [SerializeField] GameObject grenade;
        [SerializeField] float grenadeVelocity = 15f;
        [SerializeField] float spawnCooldown = 3f;

        float lastSpawnTime = 0f;

        List<GameObject> currentGrenades = new List<GameObject>();

        void Awake()
        {
            Physics.IgnoreLayerCollision(3, 6);
        }

        protected override void Cast()
        {
            if (currentGrenades.Count == 3) lastSpawnTime = Time.time;
            currentGrenades = currentGrenades.FindAll(x => true);
            Dictionary<string, object> args = new Dictionary<string, object>();
            args.Add(AbilitiesDictionary.grenadeVelocity, grenadeVelocity);
            args.Add(AbilitiesDictionary.grenadeObject, currentGrenades[0]);
            Debug.Log(grenadeVelocity);
            currentGrenades.RemoveAt(0);
            EventManager.TriggerEvent(AbilitiesDictionary.grenade, args);
        }

        void Update()
        {
            if (currentGrenades.Count < 3)
            {
                if ((Time.time - spawnCooldown) > lastSpawnTime)
                {
                    lastSpawnTime = Time.time;
                    SpawnGrenade();
                }
            }

            if (InputManager.instance.GetKeyDown(InputManager.Grenade) && currentGrenades.Count > 0)
            {
                Cast();
            }
            else
            {

            }

        }

        void SpawnGrenade()
        {
            GameObject newGrenade = Instantiate(grenade);
            newGrenade.transform.SetParent(transform);

            Vector3 spawnPosition = transform.position - transform.forward;
            spawnPosition.y = 0f;
            newGrenade.transform.position = spawnPosition;

            int spawnDirection = GetSpawnDirection();
            OrbitPlayer comp = newGrenade.GetComponent<OrbitPlayer>();
            comp.direction = spawnDirection;
            newGrenade.transform.rotation = Quaternion.LookRotation(transform.right * spawnDirection);
            currentGrenades.Add(newGrenade);
        }

        int GetSpawnDirection()
        {
            switch (currentGrenades.Count)
            {
                case 0:
                    return 1;
                case 1:
                    return -1;
                case 2:
                    return 1;
                default:
                    return -1;
            }
        }
    }
}