using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class WeaponController : MonoBehaviour
{
    [SerializeField]
    private bool automaticWeapon;
    private bool isTriggerHeld;

    [SerializeField] private Transform boltCheckPoint;

    [Header("Bullet")]
    [SerializeField]
    private float bulletSpeed;
    [SerializeField]
    private GameObject bulletPrefab;

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
    [SerializeField]
    private MagazineType acceptedMagazineType;
    [SerializeField]
    private SliderController sliderController;


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
    private bool boltPulledBack;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip shootSFX;
    [SerializeField] private AudioClip emptySFX;
    [SerializeField] private AudioClip boltSFX;
    [SerializeField] private AudioClip smgLoopSFX;

    private bool smgSoundPlaying;


    private void Update()
    {
        timePassed += Time.deltaTime;

        if (automaticWeapon && isTriggerHeld)
        {
            Shoot();
        }


        // Slider carregat


        // Ha arribat enrere?
        if (slider.localPosition.z < boltCheckPoint.localPosition.z)
        {
            boltPulledBack = true;
        }

        // Ha tornat endavant després d'anar enrere?
        if (boltPulledBack && slider.localPosition.z > boltCheckPoint.localPosition.z)
        {
            isMagazineIn = true;

            boltPulledBack = false;

            Debug.Log("Zi");
        }
    }

    public void AddMagazine(SelectEnterEventArgs args)
    {
        // VRMagazine newMagazine = args.interactableObject.transform.GetComponent<VRMagazine>();

        VRMagazine newMagazine = args.interactableObject.transform.GetComponentInChildren<VRMagazine>();

        if (newMagazine == null)
            return;

        if (newMagazine.MagazineType != acceptedMagazineType)
        {

            return;
        }



        magazine = newMagazine;

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

        audioSource.PlayOneShot(shootSFX);
        //Debug.Log("Dipara");

        if (magazine == null)
            return;

      //  Debug.Log("MEga");

        if (!isMagazineIn)
            return;

        // Debug.Log("MEgaIN");

        sliderController.Recoil();


        if (magazine.bullets <= 0)
        {
            audioSource.PlayOneShot(emptySFX);
            return;
        }



        if (timePassed < fireRate)
            return;



        GameObject bulletClone = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);

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

    public void StartFiring()
    {
        isTriggerHeld = true;

        if (automaticWeapon && !smgSoundPlaying)
        {
            audioSource.clip = smgLoopSFX;
            audioSource.loop = true;
            audioSource.Play();

            smgSoundPlaying = true;
        }
    }

    public void StopFiring()
    {
        isTriggerHeld = false;

        if (automaticWeapon)
        {
            audioSource.Stop();

            smgSoundPlaying = false;
        }
    }

    public void PlayBoltSound()
    {
        audioSource.PlayOneShot(boltSFX);
    }
}
