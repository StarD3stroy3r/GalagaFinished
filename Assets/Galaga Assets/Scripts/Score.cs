using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class Score : MonoBehaviour {
    public int score = 0;
    private Text text;
    public int MaxScore;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		text.text = score.ToString();
        if (score >= MaxScore)
        {
            SceneManager.LoadScene("End Game");
        }
	}
}
