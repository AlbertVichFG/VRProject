using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class DmgFlash : MonoBehaviour
{
    public static DmgFlash Instance;

    [SerializeField]    private GameObject flashObject;

    private void Awake()
    {
        Instance = this;

        flashObject.SetActive(false);
    }

    public void ShowFlash()
    {
        StopAllCoroutines();

        StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        flashObject.SetActive(true);

        yield return new WaitForSeconds(0.1f);

        flashObject.SetActive(false);
    }
}
