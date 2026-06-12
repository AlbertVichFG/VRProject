using UnityEngine;

public class VRMagazine : MonoBehaviour
{
    [SerializeField]
    private MagazineType magazineType;

    public MagazineType MagazineType => magazineType;

    public int bullets;


}
