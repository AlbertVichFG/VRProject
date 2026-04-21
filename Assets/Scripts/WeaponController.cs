using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

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

    //Charger
    private VRMagazine magazine;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timePass += Time.deltaTime;
    }

    public void AddMagazine(SelectEnterEventArgs eventsArgs)
    {
        Debug.Log("Magazine added");
        magazine = eventsArgs.interactableObject.transform.GetComponent<VRMagazine>();
    }

    public void RemoveMagazine(SelectExitEventArgs eventArgs)
    {
        Debug.Log("Magazine removed");
        magazine = null;
    }

    public void Shoot()
    {
        if (magazine != null)
        {
            if (fireRate <= timePass && magazine.bullets > 0)
            {
                GameObject bulletClone = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
                bulletClone.GetComponent<Rigidbody>().linearVelocity = bulletClone.transform.forward * bulletSpeed;
                magazine.bullets--;
                timePass = 0;
                Debug.Log("Dispara");
            }
        }
    }
}
