using UnityEngine;

public class DontDestroyManager : MonoBehaviour
{
    public static DontDestroyManager Instance;
    public float MusicTime { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PauseMusic()
    {
        if (GetComponent<AudioSource>().isPlaying)
        {
            MusicTime = GetComponent<AudioSource>().time;
            GetComponent<AudioSource>().Pause();
        }
    }

    public void ResumeMusic()
    {
        var audio = GetComponent<AudioSource>();
        audio.time = MusicTime;
        audio.Play();
    }
}