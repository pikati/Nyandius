
using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    [SerializeField]
    private TMP_Text _hiScoreText;
    public void ChangeScore(int score)
    {
        _scoreText.text = score.ToString("d7");
    }

    public void ChangeHiScore(int hiScore)
    {
        _hiScoreText.text = hiScore.ToString("d7");
    }
}
