using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreText : MonoBehaviour
{
    private PlayerScore playerScore;
    // Start is called before the first frame update
    void Start()
    {
        playerScore = PlayerScore.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = string.Format("Score: {0}", playerScore.score);
    }
}
