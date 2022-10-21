using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text comboText;
    [SerializeField] private int countCombo;
    [SerializeField] private int[] pointForCombo;
    [SerializeField] private int pointForColectFiveCard;
    [SerializeField] private string[] winningPhrase;
    [SerializeField] private string losePhrase;
    [SerializeField] private string collectFiveCardPhrase;
    [SerializeField] private CardsConteiner[] cardsConteiners;
    [SerializeField] private float timeAnimationScoreText;
    private Quaternion baseRotationComboText;
    private void Start()
    {
        baseRotationComboText = comboText.transform.rotation;
        for (int i = 0; i < cardsConteiners.Length; i++)
        {
            cardsConteiners[i].winLineEvent += WinLine;
            cardsConteiners[i].loseLineEvent += LoseLine;
            cardsConteiners[i].addedCardNotAffectEvent += ResetCombo;
        }
    }
    public void WinLine(List<Card> cards)
    {
        if (countCombo > winningPhrase.Length - 1)
        {
            score += pointForCombo[winningPhrase.Length - 1];
        }
        else
        {
            score += pointForCombo[countCombo];
        }

        if (cards.Count == 5)
        {
            TweenComboText(collectFiveCardPhrase);
            score += pointForColectFiveCard;
        }
        else
        {
            if (countCombo > winningPhrase.Length - 1)
            {
                TweenComboText(winningPhrase[winningPhrase.Length - 1]);
            }
            else
            {
                TweenComboText(winningPhrase[countCombo]);
            }
        }
        RenderScore();
        countCombo++;
    }
    public void TweenComboText(string phrase)
    {
        bool completeFirstTween = false;
        bool completeSecondTween = false;
        comboText.text = phrase;
        comboText.enabled = true;
        comboText.transform.DORotateQuaternion(Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 5), timeAnimationScoreText / 2)
              .OnComplete(() =>
              {
                  if (completeFirstTween == false)
                  {
                      completeFirstTween = true;
                      comboText.transform.DORotateQuaternion(baseRotationComboText, timeAnimationScoreText / 2).OnComplete(() =>
                      {
                          if (completeSecondTween == false)
                          {
                              completeSecondTween = true;
                              comboText.enabled = false;
                          }
                      });
                  }
              });
    }
    public void LoseLine()
    {
        TweenComboText(losePhrase);
    }
    public void ResetCombo()
    {
        countCombo = 0;
    }
    public void RenderScore()
    {
        scoreText.text = score.ToString();
    }
}
