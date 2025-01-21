using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class MapController : MonoBehaviour
{
     #region Privates
     
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
     [FormerlySerializedAs("buildings")]
     [Space(10)]
     
     [Header("Buildings")]
     [SerializeField] private List<GameObject> ruinedBuildings;
     [SerializeField] private List<GameObject> restoredBuildings;
     
     private int storyIndex = 0;
     private Image panelSpriteRenderer;
     private AudioSource _audioSource;
     private string levelName;
     
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
               playButton.SetActive(true);
          }

          for (int i = 1; i < PlayerPrefs.GetInt("Level")+1; i++)
          {
               Debug.Log("for " +PlayerPrefs.GetInt("Level"));
               if (i !=  PlayerPrefs.GetInt("Level")) 
               {
                    Debug.Log("i: " +i);
                    var restoredBuilding = Instantiate(restoredBuildings[0]);
                    restoredBuilding.transform.position = ruinedBuildings[i-1].transform.position;
                    ruinedBuildings[i-1].SetActive(false);
               }
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
     
     #endregion
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
          }else
          {
               PlayerPrefs.SetFloat("playerReadStory", 1);
               Destroy(storyPanel);
               Destroy(storyButton);
               playButton.SetActive(true);
          }
     }
}
