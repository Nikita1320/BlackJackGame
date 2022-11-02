using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class ScoreManager : MonoBehaviour
{
    public delegate void GameLosedWithScore(int score);
    public GameLosedWithScore gameLosedWithScoreEvent;

    [SerializeField] private int bestScore;
    [SerializeField] private TMP_Text bestScoreText;

    [SerializeField] private int currentScore;
    [SerializeField] private TMP_Text scoreText;

    [SerializeField] private TMP_Text comboText;

    [SerializeField] private int counterCombo;
    [SerializeField] private int[] pointForCombo;
    [SerializeField] private int pointForColectFiveCard;

    [SerializeField] private string[] winningPhrase;
    [SerializeField] private string losePhrase;
    [SerializeField] private string collectFiveCardPhrase;

    [SerializeField] private CardsConteiner[] cardsConteiners;
    [SerializeField] private Health health;
    private SaveAndLoad saveAndLoad;

    [SerializeField] private float timeAnimationScoreText;
    private Quaternion baseRotationComboText;
    private void Start()
    {
        saveAndLoad = new SaveAndLoad();
        bestScore = saveAndLoad.LoadBestScoreBinary(0);
        RenderBestScore();
        baseRotationComboText = comboText.transform.rotation;
        for (int i = 0; i < cardsConteiners.Length; i++)
        {
            cardsConteiners[i].winLineEvent += WinLine;
            cardsConteiners[i].loseLineEvent += LoseLine;
            cardsConteiners[i].addedCardNotAffectEvent += ResetCombo;
        }
        health.loseGameEvent += ResetScore;
    }
    private void WinLine(List<Card> cards)
    {
        if (counterCombo > winningPhrase.Length - 1)
        {
            currentScore += pointForCombo[winningPhrase.Length - 1];
        }
        else
        {
            currentScore += pointForCombo[counterCombo];
        }

        if (cards.Count == 5)
        {
            TweenComboText(collectFiveCardPhrase);
            currentScore += pointForColectFiveCard;
        }
        else
        {
            if (counterCombo > winningPhrase.Length - 1)
            {
                TweenComboText(winningPhrase[winningPhrase.Length - 1]);
            }
            else
            {
                TweenComboText(winningPhrase[counterCombo]);
            }
        }
        RenderCurrentScore();
        counterCombo++;
    }
    private void TweenComboText(string phrase)
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
    private void LoseLine()
    {
        TweenComboText(losePhrase);
        ResetCombo();
    }
    private void ResetCombo()
    {
        counterCombo = 0;
    }
    private void RenderCurrentScore()
    {
        scoreText.text = currentScore.ToString();
    }
    private void RenderBestScore()
    {
        bestScoreText.text = "BS: " + bestScore.ToString();
    }

    public void ResetScore()
    {
        gameLosedWithScoreEvent?.Invoke(currentScore);
        counterCombo = 0;
        if (currentScore > bestScore)
        {
            bestScore = currentScore;
            saveAndLoad.SaveBestScoreBinary(bestScore);
        }
        currentScore = 0;
        RenderCurrentScore();
        RenderBestScore();
    }
}
