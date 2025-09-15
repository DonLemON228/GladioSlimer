using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Video;

[System.Serializable]
public class Tutorial
{
    public string name;
    public VideoClip clip;
    public string description;
    public bool m_isEndTutorial;
    public GameObject m_startButton;
}

public class TutorialMenu : MonoBehaviour
{
    [SerializeField] private List<Tutorial> tutorials;

    [SerializeField] private VideoPlayer videoPlayer;

    [SerializeField] private TextMeshProUGUI text_Name;
    [SerializeField] private TextMeshProUGUI text_Description;

    [SerializeField] private int currentIndex = 0;
    [SerializeField] private int maxIndex;

    private void Start()
    {
        maxIndex = tutorials.Count;
        Tutorial selectedTutorial = tutorials[currentIndex];
        videoPlayer.clip = selectedTutorial.clip;
        text_Name.text = selectedTutorial.name;
        text_Description.text = selectedTutorial.description;
    }

    public void SetTutorial(int index)
    {
        if (index == 0)
        {
            currentIndex--;
        }
        else
        {
            currentIndex++;
        }

        if (currentIndex >= maxIndex)
        {
            currentIndex = 0;
        }

        if (currentIndex < 0)
        {
            currentIndex = maxIndex - 1;
        }

        Tutorial selectedTutorial = tutorials[currentIndex];
        videoPlayer.clip = selectedTutorial.clip;
        text_Name.text = selectedTutorial.name;
        text_Description.text = selectedTutorial.description;
        if(selectedTutorial.m_isEndTutorial)
            selectedTutorial.m_startButton.SetActive(true);
    }

}