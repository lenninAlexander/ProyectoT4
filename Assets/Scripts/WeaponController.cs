using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    public static WeaponController sharedInstance;

    public int cantidadMax;
    public List<Weapon> weaponInstance = new List<Weapon>();

    private void Awake()
    {
        sharedInstance = this;
    }

    public void GenerateWeapon()
    {
        int index = Random.Range(0, weaponInstance.Count - 1);
        float x = Random.Range(UpdateLevel.sharedInstance.begin.transform.position.x, UpdateLevel.sharedInstance.end.transform.position.x);

        Weapon currentWeapon = (Weapon)Instantiate(weaponInstance[index], new Vector3(x, 5.0f, 0.0f), weaponInstance[index].transform.rotation);
        currentWeapon.transform.SetParent(this.transform, false);
    }

    public void GenerateWeapons()
    {
        int index = Random.Range(0, cantidadMax);

        for (int i = 0; i < index; i++)
        {
            GenerateWeapon();
        }
    }
}
