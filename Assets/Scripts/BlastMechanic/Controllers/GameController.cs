using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : Singleton<GameController>
{
    [Header("References")]
    [SerializeField] private GameGrid _gameGrid;
    [Space(10)]
    
    [Header("UI Elements")]
    [SerializeField] private GameObject UIPanel;
    [SerializeField] private TMP_Text moveText;
    [SerializeField] private TMP_Text pointsText;
    [SerializeField] private GameObject gameOverPanel;

    private int moves;
    
    public Action OnMovesFinished;

    /*[Inject]
    void Construct(GameGrid gameGrid)
    {
        _gameGrid = gameGrid;
    }*/

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
            AudioController.Instance.StopBackgroundMusic();
            AudioController.Instance.PlaySoundEffect(SoundEffects.LevelEnd);
            UIPanel.SetActive(false);
            gameOverPanel.SetActive(true);
        }
        
        moveText.text = moves.ToString();
    }

    public void ReturnMap()
    {
        PlayerPrefs.SetInt("Level",PlayerPrefs.GetInt("Level")+1);
        SceneManager.LoadScene("Main");
    }
}
