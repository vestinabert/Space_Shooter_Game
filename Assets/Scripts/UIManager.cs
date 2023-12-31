using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private Image _LivesImage;
    [SerializeField]
    private TextMeshProUGUI _scoreText, _bestText;
    [SerializeField]
    private TextMeshProUGUI _gameOverText;
    [SerializeField]
    private TextMeshProUGUI _restartText;
    public int _bestScore;

    private GameManager _gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _bestScore = PlayerPrefs.GetInt("HighScore", 0);
        _bestText.text = "Best: " + _bestScore;
        _gameOverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

        if (_gameManager == null)
        {
            Debug.LogError("GameManager is null.");
        }
    }

    public void UpdateScore (int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
        _gameOverText.gameObject.SetActive(false);
    }
    public void CheckForBestScore(int playerScore)
    {
        if(playerScore > _bestScore)
        {
            _bestScore = playerScore;
            PlayerPrefs.SetInt("HighScore", _bestScore);
            _bestText.text = "Best: " + _bestScore;
        }
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImage.sprite = _liveSprites[currentLives];

        if(currentLives == 0)
        {
            GameOverSequence();
        }
    }

    private void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);
        StartCoroutine(GameOverFlickerRoutine());
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }

    public void ResumePlay()
    {
        GameManager gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        gm.ResumeGame();
    }
}
