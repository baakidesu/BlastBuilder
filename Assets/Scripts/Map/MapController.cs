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
     [Space(10)]
     
     [Header("Buildings")]
     [SerializeField] private List<GameObject> buildings;
     #endregion
     

     #region Privates
     
     private int storyIndex = 0;
     private Image panelSpriteRenderer;
     private AudioSource _audioSource;
     private string levelName;
     
     #endregion
     void Awake()
     {
          PlayerPrefs.SetInt("Level", 1);
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
               playButton.SetActive(true);
          }

          for (int i = 1; i < buildings.Count+1; i++)
          {
               levelName = "Level" + i;
               Debug.Log(i);
               if (Resources.Load<LevelScriptableObject>("Levels/" + levelName).didWin)
               {
                   //buildings[i].; sprite değiştir.
               }else
               {
                 PlayerPrefs.SetInt("Level", i);   
               }
          }
     }

     private void Start()
     {
          Debug.Log(PlayerPrefs.GetInt("Level"));
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
     public void OnClickPanelOpener()
     {
          OnBuildingClick(PlayerPrefs.GetInt("Level")+1);
          playButton.SetActive(false);
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
               playButton.SetActive(true);
          }
     }
}
