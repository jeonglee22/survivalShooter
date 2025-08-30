using System.Collections;
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
	public Image panelImage;

	public SoundManager soundManager;

    public bool IsPaused { get; private set; }
    public bool SoundOnOff { get; private set; }

	public int blinkingCount = 4;
	public float blinkingTimeTotal = 0.5f;
	private float alpha = 100f / 255f;

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

			Time.timeScale = IsPaused ? 0 : 1;
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
	}

	public void SetEffectVolume()
	{
		soundManager.EffectMixerControl(effectSlider.value);
	}

	public void SetHealthSlider(float healthPercent)
	{
		healthSlider.value = healthPercent;
	}

	public void SetScoreText(int score)
	{
		scoreText.text = $"Score : {score}";
	}

	public void PanelFliking()
	{
		panelImage.gameObject.SetActive(true);
		StartCoroutine(Blinking());
	}

	private IEnumerator Blinking()
	{
		for(int i = 0; i < blinkingCount; i++)
		{
			Color color = panelImage.color;
			color.a = alpha - panelImage.color.a;
			panelImage.color = color;
			yield return new WaitForSeconds(blinkingTimeTotal / blinkingCount);
		}
		panelImage.gameObject.SetActive(false);
	}
}
