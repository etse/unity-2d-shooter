using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScoreText : MonoBehaviour
{
    private PlayerState playerScore;
    // Start is called before the first frame update
    void Start()
    {
        playerScore = PlayerState.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Text>().text = string.Format("Score: {0}", playerScore.score);
    }
}
