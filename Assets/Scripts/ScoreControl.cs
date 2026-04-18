using UnityEngine;

public class ScoreControl : MonoBehaviour
{
    [SerializeField] GameObject scoreBox;
    public static int totalScore = 0;

    void Update()
    {
        scoreBox.GetComponent<TMPro.TMP_Text>().text = "SCORE: " + totalScore;
    }
}
