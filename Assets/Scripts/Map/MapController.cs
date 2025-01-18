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
          }else
          {
               Destroy(storyPanel);
               Destroy(storyButton);
          }
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
}
