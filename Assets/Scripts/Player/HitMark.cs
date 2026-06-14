using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitMark : MonoBehaviour
{
    public static HitMark Instance;

    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip hitmarkerSFX;

    [SerializeField]
    private Image hitmarkerImage;

    private void Awake()
    {
        Instance = this;

        hitmarkerImage.gameObject.SetActive(false);
    }

    public void ShowHitmarker()
    {
        audioSource.PlayOneShot(hitmarkerSFX);
        StopAllCoroutines();

        StartCoroutine(HitmarkerRoutine());
    }

    IEnumerator HitmarkerRoutine()
    {
        hitmarkerImage.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        hitmarkerImage.gameObject.SetActive(false);
    }
}
