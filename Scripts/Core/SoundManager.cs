using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    private AudioSource soundSource;
    private AudioSource musicSource;

    private void Awake()
    {
        soundSource = GetComponent<AudioSource>();
        musicSource = transform.GetChild(0).GetComponent<AudioSource>();

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != null && instance != this)
            Destroy(gameObject);

        float savedMusicVol = PlayerPrefs.GetFloat("musicVolume", 1);
        float savedSoundVol = PlayerPrefs.GetFloat("soundVolume", 1);

        musicSource.volume = savedMusicVol * 0.3f;
        soundSource.volume = savedSoundVol * 1f;
    }

    public void PlaySound(AudioClip _sound)
    {
        soundSource.PlayOneShot(_sound);
    }
    public void PlaySound(AudioClip clip, Vector3 sourcePosition)
{
    if (clip == null) return;

    Transform player = GameObject.FindGameObjectWithTag("Player")?.transform;
    if (player == null)
    {
        soundSource.PlayOneShot(clip);
        return;
    }

    float distance = Vector3.Distance(player.position, sourcePosition);
    float maxDistance = 12f;

    if (distance > maxDistance) return;

    float volume = 1f - (distance / maxDistance);
    soundSource.PlayOneShot(clip, volume * soundSource.volume);
}


    public void ChangeSoundVolume(float _change)
    {
        ChangeSourceVolume(1f, "soundVolume", _change, soundSource);
    }
    public void ChangeMusicVolume(float _change)
    {
        ChangeSourceVolume(0.3f, "musicVolume", _change, musicSource);
    }

    private void ChangeSourceVolume(float baseVolume, string volumeName, float change, AudioSource source)
    {
        float currentVolume = PlayerPrefs.GetFloat(volumeName, 1);
        currentVolume += change;

        currentVolume = Mathf.Round(currentVolume * 10) / 10;

        if (currentVolume > 1)
            currentVolume = 0;
        else if (currentVolume < 0)
            currentVolume = 1;

        float finalVolume = currentVolume * baseVolume;
        source.volume = finalVolume;

        PlayerPrefs.SetFloat(volumeName, currentVolume);
    }
}
