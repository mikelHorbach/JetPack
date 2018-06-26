using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour {

    [SerializeField]
    private GameObject losePanel;

    [SerializeField]
    private GameObject statPanel;

    [SerializeField]
    private Text thisScore;


    [SerializeField]
    private InputField nameField;

    [SerializeField]
    private Text[] names;

    [SerializeField]
    private Text[] scores;

    [SerializeField]
    private Button restartButton;


    void Awake()
    {
        statistics = new LevelStat[5];
        for (int i = 0; i < 5; i++)
        {
            string str = PlayerPrefs.GetString("top" + i, null);
            statistics[i] = JsonUtility.FromJson<LevelStat>(str);
            if (statistics[i] == null)
            {
                statistics[i] = new LevelStat();
            }
        }
    }

    LevelStat[] statistics;

    public void showEndPanel () {
        losePanel.SetActive(true);
    }
	
	public void showStat()
    {
        losePanel.SetActive(false);

        if (statistics[4].score=="")
        {
            statistics[4].name = nameField.text;
            statistics[4].score = thisScore.text;
            BubbleSort(statistics);
        }
        else
        {
            if(compareStrings(thisScore.text, statistics[4].score) >= 0)
            {
                statistics[4].name = nameField.text;
                statistics[4].score = thisScore.text;
                BubbleSort(statistics);
            }
        }

        for (int i = 0; i < 5; i++)
        {
            names[i].text = statistics[i].name;
            scores[i].text = statistics[i].score;
        }

        statPanel.SetActive(true);
    }

 

    void BubbleSort(LevelStat[] A)
    {
        for (int i = 0; i < A.Length; i++)
        {
            for (int j = 0; j < A.Length - i - 1; j++)
            {
                if (compareStrings(A[j].score,A[j + 1].score)<0)
                {
                    LevelStat temp = A[j];
                    A[j] = A[j + 1];
                    A[j + 1] = temp;
                }
            }
        }
    }

    int compareStrings(string s1,string s2)
    {
        if (s1.Length > s2.Length) return 1;
        if (s1.Length < s2.Length) return -1;
        for (int i = 0; i < s1.Length; i++)
            if (s1[i] > s2[i]) return 1;
            else if (s1[i] < s2[i]) return -1;
        return 0;
    }


    public void closeStat()
    {
        statPanel.SetActive(false);
        losePanel.SetActive(false);
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            restartButton.gameObject.SetActive(true);
        }
        else
        {
            Time.timeScale = 1;
        }
        
    }


    public void showStat2()
    {
        Time.timeScale = 0;
        for (int i = 0; i < 5; i++)
        {
            names[i].text = statistics[i].name;
            scores[i].text = statistics[i].score;
        }

        statPanel.SetActive(true);
    }


    private void OnDestroy()
    {
        Debug.Log(statistics);
        for (int i = 0; i < 5; i++)
        {
            string str = JsonUtility.ToJson(statistics[i]);
            PlayerPrefs.SetString("top" + i, str);
            PlayerPrefs.Save();
        }
            
    }




}
