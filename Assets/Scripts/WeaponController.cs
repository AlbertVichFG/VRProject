using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class WeaponController : MonoBehaviour
{
    [Header("Bullet")]
    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private float fireRate = 0.2f;

    [SerializeField]
    private Transform bulletSpawnPoint;

    private float timePassed;

    [Header("Ammo UI")]
    [SerializeField]
    private TextMeshProUGUI ammoText;

    [Header("Magazine")]
    private VRMagazine magazine;

    private bool isMagazineIn = false;

    [SerializeField]
    private Transform slider;

    [Header("Shell")]
    [SerializeField]
    private GameObject shellPrefab;

    [SerializeField]
    private Transform shellSpawnPoint;

    [SerializeField]
    private float shellForce = 2f;

    private bool isBlinking = false;

    private void Update()
    {
        timePassed += Time.deltaTime;

        // Slider carregat
        if (slider.localPosition.z < -0.020f)
        {
            isMagazineIn = true;
        }
    }

    public void AddMagazine(SelectEnterEventArgs args)
    {
        magazine =
            args.interactableObject.transform.GetComponent<VRMagazine>();

        isMagazineIn = false;

        UpdateAmmoUI();
    }

    public void RemoveMagazine(SelectExitEventArgs args)
    {
        magazine = null;

        isMagazineIn = false;

        UpdateAmmoUI();
    }

    public void Shoot()
    {
        if (magazine == null)
            return;

        if (!isMagazineIn)
            return;

        if (magazine.bullets <= 0)
            return;

        if (timePassed < fireRate)
            return;

        // Bullet Pool
        GameObject bulletClone =
            BulletPool.Instance.GetBullet();

        bulletClone.transform.position =
            bulletSpawnPoint.position;

        bulletClone.transform.rotation =
            bulletSpawnPoint.rotation;

        bulletClone.SetActive(true);

        Rigidbody rb =
            bulletClone.GetComponent<Rigidbody>();

        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        rb.linearVelocity = bulletClone.transform.forward * bulletSpeed;

        // Shell
        SpawnShell();

        // Ammo
        magazine.bullets--;

        UpdateAmmoUI();

        timePassed = 0;
    }

    void SpawnShell()
    { 
        Debug.Log("Spawn Shell");
        GameObject shellClone = Instantiate(
            shellPrefab,
            shellSpawnPoint.position,
            shellSpawnPoint.rotation
        );


        Rigidbody rb = shellClone.GetComponent<Rigidbody>();

        Vector3 ejectDirection =
            shellSpawnPoint.right * 0.5f +
            shellSpawnPoint.up * 0.2f;

        rb.AddForce(
            ejectDirection.normalized * shellForce,
            ForceMode.Impulse
        );

        rb.AddTorque(
            Random.insideUnitSphere * 5f,
            ForceMode.Impulse
        );

        Destroy(shellClone, 5f);
    }

    void UpdateAmmoUI()
    {
        if (magazine != null)
        {
            ammoText.text = magazine.bullets.ToString();

            float ammoPercent =
                (float)magazine.bullets / 15f;

            ammoText.color =
                Color.Lerp(Color.red, Color.cyan, ammoPercent);

            if (magazine.bullets <= 3)
            {
                if (!isBlinking)
                {
                    StartCoroutine(BlinkAmmo());
                }
            }
        }
        else
        {
            ammoText.text = "0";
            ammoText.color = Color.red;
        }
    }

    IEnumerator BlinkAmmo()
    {
        isBlinking = true;

        while (magazine != null && magazine.bullets <= 3)
        {
            ammoText.enabled = false;

            yield return new WaitForSeconds(0.15f);

            ammoText.enabled = true;

            yield return new WaitForSeconds(0.15f);
        }

        ammoText.enabled = true;

        isBlinking = false;
    }
}
