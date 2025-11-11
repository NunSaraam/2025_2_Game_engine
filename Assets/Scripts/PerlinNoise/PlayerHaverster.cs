using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHaverster : MonoBehaviour
{
    public float rayDistance = 5f;
    public LayerMask hitMask = ~0;                  //가능 한 레이어 전부 다(일단)
    public int toolDamage = 1;
    public float hitCooldown = .15f;

    private float _nextHitTime;
    private Camera _cam;
    private Inventory inventory;

    private void Awake()
    {
        _cam = Camera.main;

        if (inventory == null) inventory = gameObject.AddComponent<Inventory>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && Time.time >= _nextHitTime)
        {
            _nextHitTime = Time.time + hitCooldown;

            Ray ray = _cam.ViewportPointToRay(new Vector3(.5f, .5f, 0));

            if (Physics.Raycast(ray, out var hit, rayDistance, hitMask))
            {
                var block = hit.collider.GetComponent<Block>();

                if (block != null)
                {
                    block.Hit(toolDamage);
                }
            }
        }
    }
}
