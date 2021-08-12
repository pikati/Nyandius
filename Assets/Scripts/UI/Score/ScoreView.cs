
using UnityEngine;
using TMPro;

public class ScoreView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _scoreText;
    
    public void ChangeScoreText(int score)
    {
        _scoreText.text = score.ToString("d7");
    }
}
