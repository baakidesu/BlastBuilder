using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    #region Privates

    [Header("Story Panel Elements")] [SerializeField]
    private GameObject storyPanel;

    [SerializeField] private Button storyButton;

    [Space(10)] [Header("Play Game Panel Elements")] [SerializeField]
    private GameObject playGamePanel;

    [Space(10)] [Header("To be continued Panel Elements")] [SerializeField]
    private GameObject toBeContinuedPanel;

    [Space(10)] [Header("Story Sprites")] [SerializeField]
    private List<Sprite> stories;

    [Space(10)] [Header("Play Button")] [SerializeField]
    private GameObject playButton;

    [SerializeField] private AudioClip buttonClickSound;

    [FormerlySerializedAs("buildings")] [Space(10)] [Header("Buildings")] [SerializeField]
    private List<GameObject> ruinedBuildings;

    [SerializeField] private GameObject restoredBuilding;

    private int storyIndex;
    private int maxLevel;
    private Image panelSpriteRenderer;
    private AudioSource _audioSource;
    private string levelName;

    #endregion
    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = buttonClickSound;
        maxLevel = ruinedBuildings.Count;
        if (PlayerPrefs.GetFloat("playerReadStory") != 1) //Play Story
        {
            playButton.SetActive(false);
            storyPanel.SetActive(true);
            panelSpriteRenderer = storyPanel.GetComponent<Image>();
            panelSpriteRenderer.sprite = stories[storyIndex];
            PlayerPrefs.SetInt("Level", 1);
        }
        else //Don't play story
        {
            Destroy(storyPanel);
            Destroy(storyButton);
        }

        for (var i = 1; i < PlayerPrefs.GetInt("Level") + 1; i++)
        {
            if (i != PlayerPrefs.GetInt("Level"))
            {
                var _restoredBuilding = Instantiate(restoredBuilding);
                _restoredBuilding.transform.position = ruinedBuildings[i - 1].transform.position;
                ruinedBuildings[i - 1].SetActive(false);
            }

            if (i > maxLevel)
            {
                toBeContinuedPanel.SetActive(true);
                playButton.SetActive(false);
            }
        }
    }

    private void ButtonClickSoundPlay()
    {
        _audioSource.Play();
    }

    public void OnClickPanelOpener()
    {
        playGamePanel.SetActive(true);
        playButton.SetActive(false);
    }

    public void OnStoryClick()
    {
        storyIndex++;
        if (stories.Count > storyIndex)
        {
            panelSpriteRenderer.sprite = stories[storyIndex];
        }
        else
        {
            PlayerPrefs.SetFloat("playerReadStory", 1);
            Destroy(storyPanel);
            Destroy(storyButton);
            playButton.SetActive(true);
        }
    }

    public void OnPlayGameClick()
    {
        ButtonClickSoundPlay();
        SceneManager.LoadScene("Blast");
    }

    public void OnReturnMapClick()
    {
        ButtonClickSoundPlay();
        playGamePanel.SetActive(false);
        playButton.SetActive(true);
    }

    public void RestartWholeGameButton()
    {
        PlayerPrefs.SetInt("Level", 1);
        PlayerPrefs.SetFloat("playerReadStory", 0);
        SceneManager.LoadScene("Main");
    }

    public void ExitGameButton()
    {
        Application.Quit();
    }
}