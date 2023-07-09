using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TextMeshProUGUI text = GetComponent<TextMeshProUGUI>();

        var correctAnswers = GameManager.instance.answeredCorrectly;
        var totalAnswers = GameManager.instance.GetLevelCount();

        text.text = correctAnswers.ToString() + "/" + totalAnswers.ToString();
        
    }
}
