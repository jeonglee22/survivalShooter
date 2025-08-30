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

	public GameObject gameOverPanel;
	public TextMeshProUGUI gameOverText;

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
		Time.timeScale = IsPaused ? 0 : 1;
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
		Color color = panelImage.color;
		color.a = alpha;
		panelImage.color = color;

		yield return new WaitForSeconds(blinkingTimeTotal);

		color = panelImage.color;
		color.a = 0;
		panelImage.color = color;

		panelImage.gameObject.SetActive(false);
	}

	public void SetGameOverUI(bool isGameOver)
	{
		if(isGameOver)
		{
			gameOverPanel.gameObject.SetActive(true);
			panelImage.gameObject.SetActive(false);
			StartCoroutine(TextScaling(0f, 1f));
		}
		else
		{
			gameOverPanel.gameObject.SetActive(false);
			gameOverText.transform.localScale = Vector3.zero;
		}
	}

	public IEnumerator TextScaling(float scaleMin, float scaleMax)
	{
		var percent = 0f;
		while (percent > 2f)
		{
			percent += 0.2f;
			gameOverText.transform.localScale = new Vector3(1f,1f,1f) * Mathf.Lerp(scaleMin, scaleMax, percent);
			yield return new WaitForSeconds(0.2f);
		}
	}
}
