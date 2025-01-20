using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class GameController : Singleton<GameController>
{
    [Header("References")]
    [SerializeField] private GameGrid _gameGrid;
    private LevelController _levelController;
    [Space(10)]
    
    [Header("UI Elements")]
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private TMP_Text moveText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private GameObject gameOverPanel;

    private int moves;
    
    public Action OnMovesFinished;
    public int points;

    private AudioController _audioController;

    [Inject]
    void Construct(AudioController audioController, LevelController levelController)
    {
        //_gameGrid = gameGrid;
        _audioController = audioController;
        _levelController = levelController;
    }

    private void Start()
    {
        moves = _gameGrid._moveCount;
        moveText.text = moves.ToString();
    } 
    public async Task DecreaseMovesAsync()
    {
        moves--;

        if (moves <= 0)
        {
            GetComponent<InputController>().enabled = false;
            moves = 0;
            moveText.text = moves.ToString();
            await Task.Delay(TimeSpan.FromSeconds(1));
            OnMovesFinished?.Invoke();
            _audioController.StopBackgroundMusic();
            _audioController.PlaySoundEffect(SoundEffects.LevelEnd);
            UIPanel.SetActive(false);
            gameOverPanel.SetActive(true);
        }
        
        moveText.text = moves.ToString();
    }

    public void ReturnMap()
    {
        PlayerPrefs.SetInt("Level",PlayerPrefs.GetInt("Level")+1);
        if (true) //kazandıysa
        {
            _levelController.levelDataFromScriptableObject.didWin = true;
        }
        SceneManager.LoadScene("Main");
    }
}
