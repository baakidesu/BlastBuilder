using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MapController : MonoBehaviour
{
     #region Publics
     
     [Header("Story Panel Elements")]
     [SerializeField] private GameObject storyPanel;
     [SerializeField] private Button storyButton;
     [Space(10)]
     
     [Header("Play Game Panel Elements")]
     [SerializeField] private GameObject playGamePanel;
     [Space(10)]

     [Header("Story Sprites")]
     [SerializeField] private List<Sprite> stories;
     [Space(10)]
     
     [Header("Play Button")]
     [SerializeField] private GameObject playButton;

     [SerializeField] private AudioClip buttonClickSound;
     
     //[Header("Buildings")]
     #endregion
     

     #region Privates
     
     private int storyIndex = 0;
     private Image panelSpriteRenderer;
     private AudioSource _audioSource;
     
     #endregion
     void Awake()
     {
           _audioSource = GetComponent<AudioSource>();
           _audioSource.clip = buttonClickSound;
          if (PlayerPrefs.GetFloat("playerReadStory") != 1)
          {
               panelSpriteRenderer = storyPanel.GetComponent<Image>();
               panelSpriteRenderer.sprite = stories[storyIndex];
               PlayerPrefs.SetInt("Level",1);
          }else
          {
               Destroy(storyPanel);
               Destroy(storyButton);
          }
     }

     private void Start()
     {
          Debug.Log(PlayerPrefs.GetInt("Level"));
     }

     public void OnStoryClick()
     {
          storyIndex++;
          if (stories.Count > storyIndex)
          {
               panelSpriteRenderer.sprite = stories[storyIndex];
          }else
          {
               PlayerPrefs.SetFloat("playerReadStory", 1);
               Destroy(storyPanel);
               Destroy(storyButton);
          }
     }

     #region PlayGame Panel Functions

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

     public void OnBuildingClick(int buildingIndex)
     {
          ButtonClickSoundPlay();
          PlayerPrefs.SetInt("Level", buildingIndex);
          playGamePanel.SetActive(true);
     }

     #endregion

     private void ButtonClickSoundPlay()
     {
          _audioSource.Play();
     }

     public void OnPlayButton()
     {
          OnBuildingClick(PlayerPrefs.GetInt("Level")+1);
          playButton.SetActive(false);
     }
}
