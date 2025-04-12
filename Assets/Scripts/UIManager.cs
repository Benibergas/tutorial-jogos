using UnityEngine;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class UIManager : MonoBehaviour
{
    public GameObject endGamePanel;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI countText;
    public TextMeshProUGUI tempoFinalText;
    public TextMeshProUGUI top5Text;

    private float elapsedTime = 0f;
    private int total = 5;
    private bool ended = false;

    void Start()
    {
        GameController.Init();
        UpdateCanudoText();
    }

    void Update()
    {
        if (GameController.gameOver && !ended)
        {
            endGamePanel.SetActive(true);

            // Esconde os textos da HUD
            timerText.gameObject.SetActive(false);
            countText.gameObject.SetActive(false);

            // Salva o tempo no top 5
            HighscoreManager.AddScore(elapsedTime);

            // Mostra o tempo final
            int minutes = Mathf.FloorToInt(elapsedTime / 60f);
            int seconds = Mathf.FloorToInt(elapsedTime % 60f);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);
            tempoFinalText.text = $"Tempo final: {minutes:00}:{seconds:00}:{milliseconds:000}";

            // Mostra o top 5
            top5Text.text = "Melhores Tempos:\n";
            var scores = HighscoreManager.GetScores();
            for (int i = 0; i < scores.Count; i++)
            {
                int minTop5 = Mathf.FloorToInt(scores[i] / 60f);
                int secTop5 = Mathf.FloorToInt(scores[i] % 60f);
                int msTop5= Mathf.FloorToInt((scores[i] * 1000f) % 1000f);
                top5Text.text += $"{i + 1}. {minTop5:00}:{secTop5:00}:{msTop5:000}\n";
            }

            ended = true;
            return;
        }

        if (ended) return;

        elapsedTime += Time.deltaTime;
        int min = Mathf.FloorToInt(elapsedTime / 60f);
        int sec = Mathf.FloorToInt(elapsedTime % 60f);
        int ms = Mathf.FloorToInt((elapsedTime * 1000f) % 1000f);
        timerText.text = $"Tempo: {min:00}:{sec:00}:{ms:000}";

        UpdateCanudoText();
    }

    void UpdateCanudoText()
    {
        int current = total - GameController.collectableCount;
        countText.text = $"Canudos: {current}/{total}";
    }
}
