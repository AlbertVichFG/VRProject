using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance;

    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioClip simonMusic;
    [SerializeField] private AudioClip zombieMusic;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        PlaySimonMusic();
    }

    public void PlaySimonMusic()
    {
        musicSource.clip = simonMusic;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayZombieMusic()
    {
        musicSource.clip = zombieMusic;
        musicSource.loop = true;
        musicSource.Play();
    }
}
