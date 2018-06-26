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
    private Text scoreLabel;

    [SerializeField]
    private InputField nameField;

    [SerializeField]
    private Text[] names;

    [SerializeField]
    private Text[] scores;

    [SerializeField]
    private Button restartButton;

    int am =0;

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
            else am++;
        }
    }

    LevelStat[] statistics;

    public void showEndPanel () {
        losePanel.SetActive(true);
    }
	
	public void showStat()
    {
        losePanel.SetActive(false);

        for(int i = 0; i < am; i++)
        {
            if (System.String.Compare(statistics[i].score, thisScore.text, true)<=0)
            {
               
                for (int j = i + 1; j < am; j++)
                {
                    statistics[j].name = statistics[j - 1].name;
                    statistics[j].score = statistics[j-1].score;
                }
                statistics[i].name = nameField.text;
                statistics[i].score = thisScore.text;
                break;
            }
        }

        if (am < 5 && statistics[am].name=="")
        {
            statistics[am].name = nameField.text;
            statistics[am].score = thisScore.text;
            am++;
        }

        for (int i = 0; i < am; i++)
        {
            names[i].text = statistics[i].name;
            scores[i].text = statistics[i].score;
        }

        statPanel.SetActive(true);
    }


    public void closeStat()
    {
        statPanel.SetActive(false);
        restartButton.gameObject.SetActive(true);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < am; i++)
        {
            string str = JsonUtility.ToJson(statistics[i]);
            PlayerPrefs.SetString("top" + i, str);
            PlayerPrefs.Save();
        }
            
    }

}
