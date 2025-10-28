using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponThrower : MonoBehaviour
{
    public GameObject weaponInHand;
    public GameObject thrownWeaponPrefab;
    public Slider powerSlider;
    public PlayerBehaviourScript PlayerBehaviour;


    private int maxWeapons = 6;
    private int weaponCount = 6;
    private bool isBless = false;
    private List<GameObject> activeWeapons = new List<GameObject>();

    private void Start()
    {
        if (powerSlider != null)
        {
            powerSlider.maxValue = maxWeapons;
            powerSlider.value = weaponCount;
        }
    }

    void Update()
    {
        if (PlayerBehaviour != null && PlayerBehaviour.isGameOver) return;
        if (Input.GetMouseButtonDown(0) && (weaponCount > 0 || isBless))
        {
            Vector2 mousePosition = GetMouseWorldPosition();
            if (!isBless && weaponCount > 0)
            {
                weaponCount--;
                powerSlider.value = weaponCount;
            }
            ThrowWeapon(mousePosition);
        }
    }

    void ThrowWeapon(Vector2 target)
    {
        if (gameObject.CompareTag("WeaponInHand"))
        {
            GameObject thrownWeapon = Instantiate(thrownWeaponPrefab, weaponInHand.transform.position, weaponInHand.transform.rotation);
            thrownWeapon.transform.localScale = new Vector3(30, 40, 40);

            // Track active weapons
            activeWeapons.Add(thrownWeapon);

            StartCoroutine(MoveWeapon(thrownWeapon, target));
        }
    }

    IEnumerator MoveWeapon(GameObject weapon, Vector2 target)
    {
        float speed = 300f;
        Vector2 direction = (target - (Vector2)weapon.transform.position).normalized;
        RotateWeaponTowards(weapon.transform, target);

        float startTime = Time.time;
        while (Time.time - startTime < 3f)
        {
            if (weapon == null) yield break;
            weapon.transform.position += (Vector3)(direction * speed * Time.deltaTime);
            yield return null;
        }

        if (weapon != null)
        {
            activeWeapons.Remove(weapon);
            Destroy(weapon);

            // Only increment weapon count if not blessed and the weapon was actively tracked
            if (!isBless)
            {
                weaponCount = Mathf.Min(weaponCount + 1, maxWeapons);
                powerSlider.value = weaponCount;
            }
        }
    }

    void RotateWeaponTowards(Transform weaponTransform, Vector2 target)
    {
        Vector2 direction = target - (Vector2)weaponTransform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        weaponTransform.rotation = Quaternion.Euler(0, 0, angle);
    }

    Vector2 GetMouseWorldPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void SetIsBless(bool bless)
    {
        isBless = bless;

        // When bless ends, clear all active weapons to prevent over-counting
        if (!bless)
        {
            StartCoroutine(CleanupActiveWeapons());
        }
    }

    private IEnumerator CleanupActiveWeapons()
    {
        // Wait a frame to ensure all weapons have finished their current update
        yield return null;

        // Make a copy of the list to avoid modification during iteration
        List<GameObject> weaponsToDestroy = new List<GameObject>(activeWeapons);

        foreach (GameObject weapon in weaponsToDestroy)
        {
            if (weapon != null)
            {
                activeWeapons.Remove(weapon);
                Destroy(weapon);
            }
        }

        activeWeapons.Clear();
    }

    public void SetWeaponCount(int count)
    {
        weaponCount = Mathf.Min(count, maxWeapons);
        powerSlider.value = weaponCount;
    }
}