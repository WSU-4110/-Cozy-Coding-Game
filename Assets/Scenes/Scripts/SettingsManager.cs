using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private Toggle muteToggle;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private AudioSource musicSource;   // background music source

    private void Start()
    {
        // Load mute preference
        int savedMute = PlayerPrefs.GetInt("mute_all", 0);
        bool isMuted = savedMute == 1;
        muteToggle.isOn = isMuted;
        ApplyMute(isMuted);

        // Load SFX volume (default 1.0f)
        float savedSfxVolume = PlayerPrefs.GetFloat("sfx_volume", 1f);
        sfxVolumeSlider.value = savedSfxVolume;
        ApplySfxVolume(savedSfxVolume);

        // Load Music volume (default 1.0f)
        float savedMusicVolume = PlayerPrefs.GetFloat("music_volume", 1f);
        musicVolumeSlider.value = savedMusicVolume;
        ApplyMusicVolume(savedMusicVolume);

        // Hook listeners
        muteToggle.onValueChanged.AddListener(OnMuteChanged);
        sfxVolumeSlider.onValueChanged.AddListener(OnSfxVolumeChanged);
        musicVolumeSlider.onValueChanged.AddListener(OnMusicVolumeChanged);

        // Hide panel at start
        settingsPanel.SetActive(false);
    }

    public void TogglePanel()
    {
        settingsPanel.SetActive(!settingsPanel.activeSelf);
    }

    private void OnMuteChanged(bool isOn)
    {
        ApplyMute(isOn);
        PlayerPrefs.SetInt("mute_all", isOn ? 1 : 0);
    }

    private void OnSfxVolumeChanged(float value)
    {
        ApplySfxVolume(value);
        PlayerPrefs.SetFloat("sfx_volume", value);
    }

    private void OnMusicVolumeChanged(float value)
    {
        ApplyMusicVolume(value);
        PlayerPrefs.SetFloat("music_volume", value);
    }

    private void ApplyMute(bool mute)
    {
        AudioListener.pause = mute;
    }

    private void ApplySfxVolume(float value)
    {
        if (SFXManager.Instance != null)
        {
            SFXManager.Instance.SetVolume(value);
        }
    }



    private void ApplyMusicVolume(float value)
    {
        if (musicSource != null)
        {
            musicSource.volume = value;
        }
    }
}
