using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GamePlay : MonoBehaviour {

    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI gemsUI;

    public GameObject collectedGem;

    private int previousGems;
    private int previousScore;
    
    private void OnEnable()
    {

        previousGems = GameManager.instance.gems;
        previousScore = 0;

    }

    void Update () {

        UpdateUI();

	}

    void UpdateUI()
    {

        int score = GameManager.instance.score;
        int gems = GameManager.instance.gems;

        UpdateScoreUi(score);
        UpdateGemsUi(gems);

    }

    void UpdateScoreUi(int score)
    {

        scoreUI.text = score.ToString();

        if (score > previousScore)
        {

            Animator anim = scoreUI.GetComponent<Animator>();
            anim.SetTrigger("TextTrigger");

        }

        previousScore = score;

    }

    void UpdateGemsUi(int gems)
    {

        gemsUI.text = gems.ToString();

        if (gems > previousGems)
        {

            Animator anim = gemsUI.GetComponent<Animator>();
            anim.SetTrigger("TextTrigger");

            Animator gemAnim = collectedGem.GetComponent<Animator>();
            gemAnim.SetTrigger("Show");

        }

        previousGems = gems;

    }

}
