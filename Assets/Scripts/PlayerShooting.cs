using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public WeaponSO[] weapons;
    public Transform firepoint;

    public WeaponSO currentWeapon;
    Camera cam;

    private void Start()
    {
        currentWeapon = weapons[0];
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (currentWeapon != null && currentWeapon == weapons[0])
            {
                currentWeapon = weapons[1];
            }
            else if (currentWeapon != null && currentWeapon == weapons[1])
            {
                currentWeapon = weapons[0];
            }
        }
    }

    void Shoot()
    {
        //Ray½î±â
        Ray ray =  cam.ScreenPointToRay(Input.mousePosition);

        Vector3 targetPoint;
        targetPoint = ray.GetPoint(50f);

        Vector3 direction = (targetPoint - firepoint.position).normalized;      //¹æÇâ º¤ÅÍ

        //Projcetile »ý¼º
        GameObject proj = Instantiate(currentWeapon.weaponPrefab, firepoint.position, Quaternion.LookRotation(direction));
    }
}
