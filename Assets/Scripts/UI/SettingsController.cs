using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class SettingsController : MonoBehaviour
{
    [SerializeField] Button volumeButton;
    [SerializeField] Button screenMotionButton;

    const string VolumeKey = "WanderingBlade.VolumePercent";
    const string MotionKey = "WanderingBlade.ReducedMotion";

    AudioSource audioSource;
    AudioClip testClip;
    int volumePercent;
    bool reducedScreenMotion;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
        audioSource.ignoreListenerPause = true;

        // A short test tone, generated without an external audio asset.
        const int sampleRate = 44100;
        int sampleCount = Mathf.RoundToInt(sampleRate * 0.15f);
        float[] samples = new float[sampleCount];

        for (int i = 0; i < sampleCount; i++)
        {
            float envelope =
                Mathf.Sin(Mathf.PI * i / (sampleCount - 1));

            samples[i] =
                Mathf.Sin(2f * Mathf.PI * 660f * i / sampleRate)
                * envelope * 0.12f;
        }

        testClip = AudioClip.Create(
            "MenuTestTone", sampleCount, 1, sampleRate, false);

        testClip.SetData(samples, 0);
    }

    void Start()
    {
        volumePercent = Mathf.Clamp(
            PlayerPrefs.GetInt(VolumeKey, 75), 0, 100);

        reducedScreenMotion =
            PlayerPrefs.GetInt(MotionKey, 0) == 1;

        ApplyVolume();
        RefreshLabels();
    }

    public void CycleVolume()
    {
        volumePercent = (volumePercent + 25) % 125;

        PlayerPrefs.SetInt(VolumeKey, volumePercent);
        PlayerPrefs.Save();

        ApplyVolume();
        RefreshLabels();
        PlayTestSound();
    }

    public void ToggleScreenMotion()
    {
        reducedScreenMotion = !reducedScreenMotion;

        PlayerPrefs.SetInt(
            MotionKey, reducedScreenMotion ? 1 : 0);

        PlayerPrefs.Save();
        RefreshLabels();
    }

    public void PlayTestSound()
    {
        audioSource.PlayOneShot(testClip);
    }

    void ApplyVolume()
    {
        AudioListener.volume = volumePercent / 100f;
    }

    void RefreshLabels()
    {
        volumeButton.GetComponentInChildren<TMP_Text>(true).text =
            $"VOLUME: {volumePercent}%";

        screenMotionButton
            .GetComponentInChildren<TMP_Text>(true).text =
            reducedScreenMotion
                ? "SCREEN MOTION: OFF"
                : "SCREEN MOTION: ON";
    }

    void OnDestroy()
    {
        if (testClip != null)
            Destroy(testClip);
    }
}