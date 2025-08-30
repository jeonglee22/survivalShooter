using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public Slider healthSlider;
	public Slider soundSlider;
	public Slider effectSlider;
    public Toggle soundToggle;
    public GameObject menu;

	public SoundManager soundManager;

    public bool IsPaused { get; private set; }
    public bool SoundOnOff { get; private set; }

	private void Awake()
	{
		IsPaused = false;
		menu.SetActive(IsPaused);
	}

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			IsPaused = !IsPaused;
            menu.SetActive(IsPaused);
        }
	}

	public void ClickQuitGame()
    {
		EditorApplication.isPlaying = false;
	}

    public void ClickResume()
    {
		IsPaused = false;
        menu.SetActive(IsPaused);
	}

    public void ToggleSound()
    {
		soundManager.SetMasterVolume(soundToggle.isOn);
	}

	public void SetMusicVolume()
	{
		soundManager.MusicMixerControl(soundSlider.value);
		Debug.Log(soundSlider.value);
	}

	public void SetEffectVolume()
	{
		soundManager.EffectMixerControl(effectSlider.value);
		Debug.Log(effectSlider.value);
	}

	public void SetHealthSlider(float healthPercent)
	{
		healthSlider.value = healthPercent;
	}

	public void SetScoreText(int score)
	{
		scoreText.text = $"Score : {score}";
	}
}
