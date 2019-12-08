using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropdownBehavior : MonoBehaviour
{
    public Dropdown Menu;
    public GameObject spherePrefab;
    public GameObject cubePrefab;
    public GameObject cylinderPrefab;
    private GameObject selectedProjectile;

    public GameObject Controller;
    private RotateArm RotateArm;
    // Start is called before the first frame update
    void Start()
    {
        RotateArm = Controller.GetComponent<RotateArm>();

        Menu.onValueChanged.AddListener(delegate { menuValueChanged(); });
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            Debug.Log(Menu.value + " " + Menu.options.Count);
            if (Menu.options.Count - 1 == Menu.value)
            {
                Menu.value = 0;
            }
            else
            {
                Menu.value++;
            }
            menuValueChanged();
        }
    }

    // When the user selects a new dropdown projectile, set RotateArm's prefab
    // to the appropriate projectile prefab
    void menuValueChanged()
    {
        if (Menu.value == 0)
        {
            selectedProjectile = spherePrefab;
        }
        else if (Menu.value == 1)
        {
            selectedProjectile = cubePrefab;
        }
        else if (Menu.value == 2)
        {
            selectedProjectile = cylinderPrefab;
        }
        changeProjectile();
    }

    // Change current projectile. Replaces any currently attached
    void changeProjectile()
    {
        // Look through all currently existing projectiles and destroy any currently attached to the sling
        GameObject [] tempObjs = GameObject.FindGameObjectsWithTag("Projectile");
        foreach (GameObject obj in tempObjs)
        {
            if (obj.GetComponent<ProjectileAction>().GetAttached() == true &&
                RotateArm.GetState() == "Stationary")
            {
                Destroy(obj);
            }
        }
        // Replace previously attached projectile
        RotateArm.SetProjectilePrefab(selectedProjectile);
    }
}
