using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class GameController : Singleton<GameController>
{
    #region Privates

    private AudioController _audioController;
    private int moves;
    private GameGrid _gameGrid;
    private LevelController _levelController;
    
    [Header("UI Elements")]
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private TMP_Text moveText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private TMP_Text pointsToGatherText;
    [Space(10)]

    [Header("Win Panel")]
    [SerializeField] private GameObject winPanel;
    [Space(10)]
    
    [Header("Lose Panel")]
    [SerializeField] private GameObject gameOverPanel;
    
    public Action OnMovesFinished;
    [HideInInspector] public int points = 0;

    #endregion

    #region Injections

    [Inject]
    void Construct(AudioController audioController, LevelController levelController, GameGrid gameGrid)
    {
        _gameGrid = gameGrid;
        _audioController = audioController;
        _levelController = levelController;
    }

    #endregion
    private void Start()
    {
        moves = _gameGrid._moveCount;
        moveText.text = moves.ToString();
        pointsToGatherText.text = _levelController.levelDataFromScriptableObject.pointToGather.ToString();
        Debug.Log("Level: " + PlayerPrefs.GetInt("Level"));
    } 
    public async Task DecreaseMovesAsync()
    {
        moves--;
        pointsText.text = points.ToString();
        
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

            if (points >= _levelController.levelDataFromScriptableObject.pointToGather) //win
            {
                winPanel.SetActive(true);
                _levelController.levelDataFromScriptableObject.didWin = true;
                PlayerPrefs.SetInt("Level",PlayerPrefs.GetInt("Level")+1);
                Debug.Log("Level ending: "+ PlayerPrefs.GetInt("Level"));
            }else
            {
                gameOverPanel.SetActive(true);
            }
        }
        
        moveText.text = moves.ToString();
    } 
    public void ReturnMap()
    {
        SceneManager.LoadScene("Main");
    } 
    public void RestartLevel()
    {
        SceneManager.LoadScene("Blast");
    }
}
