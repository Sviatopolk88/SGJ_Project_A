using UnityEngine;

public class WeaponSwitcher : MonoBehaviour
{
    public int SelectedWeapon = 0;
    void Start()
    {
        SwitchWeapon();
    }

    void Update()
    {
        WeaponCheck();
    }

    private void WeaponCheck()
    {
        int prevSelectedWeapon = SelectedWeapon;
        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            if (SelectedWeapon >= transform.childCount - 1)
            {
                SelectedWeapon = 0;
            }
            else
            {
                SelectedWeapon++;
            }
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            if (SelectedWeapon <= 0)
            {
                SelectedWeapon = transform.childCount - 1;
            }
            else
            {
                SelectedWeapon--;
            }

        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SelectedWeapon = 0;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            SelectedWeapon = 1;
        }
        if (prevSelectedWeapon != SelectedWeapon)
        {
            SwitchWeapon();
        }
    }


    void SwitchWeapon()
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == SelectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}