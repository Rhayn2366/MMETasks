using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int scoreCount = 0;
    [SerializeField ]private TextMeshProUGUI scoreCountText;

    private void Start()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        scoreCountText.text = scoreCount.ToString();
    }

    public void IncreaseScore()
    {
        scoreCount++;
        UpdateText();
        Debug.Log(scoreCount);
    }

    internal void DecreaseScore()
    {
        scoreCount--;
        UpdateText();
        Debug.Log(scoreCount);
    }
}