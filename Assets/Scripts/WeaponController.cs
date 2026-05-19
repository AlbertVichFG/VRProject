using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections;

public class WeaponController : MonoBehaviour
{

    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletSpawnPoint;
    [SerializeField] 
    private float bulletSpeed;
    [SerializeField]
    private float fireRate;
    private float timePass; //Time since last shot

    [SerializeField]
    private TextMeshProUGUI ammoText;
    private bool isBlinking = false;

    //Charger
    private VRMagazine magazine;
    private bool isMagazineIn = false;

    [Header("Shell")]
    [SerializeField]
    private GameObject shellPrefab;

    [SerializeField]
    private Transform shellSpawnPoint;

    [SerializeField]
    private float shellForce = 2f;

    [SerializeField]
    private Transform slider;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePass += Time.deltaTime;
        if(slider.localPosition.z < -0.020f) //
        {
            
            //Debug.Log("RedytoShoot");
            isMagazineIn = true;
          //  Debug.Log("MagazineBool: " + isMagazineIn);

        }
    }

    public void AddMagazine(SelectEnterEventArgs eventsArgs)
    {
        Debug.Log("Magazine added");
        magazine = eventsArgs.interactableObject.transform.GetComponent<VRMagazine>();
        isMagazineIn = false;
        Debug.Log("MagazineBool: " + isMagazineIn);
        UpdateAmmoUI();
    }

    public void RemoveMagazine(SelectExitEventArgs eventArgs)
    {
        Debug.Log("Magazine removed");
        magazine = null;
        isMagazineIn = false;
        UpdateAmmoUI();
    }

    public void Shoot()
    {
        if (magazine != null && isMagazineIn == true)
        {
            if (fireRate <= timePass && magazine.bullets > 0)
            {
                GameObject bulletClone = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bulletClone.GetComponent<Rigidbody>().linearVelocity = bulletClone.transform.forward * bulletSpeed;
                magazine.bullets--;
                UpdateAmmoUI();
                timePass = 0;
                SpawnShell();   
                Debug.Log("Dispara");
            }
        }
    }

    void UpdateAmmoUI()
    {
        if (magazine != null)
        {
            ammoText.text = magazine.bullets.ToString();

            float ammoPercent = (float)magazine.bullets / 15f;

            ammoText.color = Color.Lerp(Color.red, Color.cyan, ammoPercent);

            // Low ammo warning
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
            ammoText.text = "00/00";
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


    void SpawnShell()
    {
        GameObject shellClone = Instantiate(
            shellPrefab,
            shellSpawnPoint.position,
            shellSpawnPoint.rotation
        );

        Rigidbody rb = shellClone.GetComponent<Rigidbody>();

        // Direcció lateral + una mica amunt
        Vector3 ejectDirection =
            shellSpawnPoint.right +
            shellSpawnPoint.up * 0.5f;

        rb.AddForce(ejectDirection * shellForce, ForceMode.Impulse);

        // Rotació random
        rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);

        Destroy(shellClone, 3f);
    }
}
