using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
     [SerializeField] private GameObject panel;
     [SerializeField] private Button storyButton;
     [SerializeField] private List<Sprite> stories;
     
     private int storyIndex = 0;
     private Image panelSpriteRenderer;
     void Awake()
     {
          Debug.Log(PlayerPrefs.GetFloat("playerReadStory"));
          if (PlayerPrefs.GetFloat("playerReadStory") != 1)
          {
               panelSpriteRenderer = panel.GetComponent<Image>();
               panelSpriteRenderer.sprite = stories[storyIndex];  
          }else
          { 
               Destroy(panel);
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
               Destroy(panel);
               Destroy(storyButton);
          }
     }
}
