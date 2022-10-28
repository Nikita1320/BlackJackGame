using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public delegate void GameLosed();
    public GameLosed loseGameEvent;

    [SerializeField] private CardsConteiner[] cardsConteiners;
    [SerializeField] private List<GameObject> healthElement;
    [SerializeField] private GameObject prefabHealthElement;
    [SerializeField] private Sprite commonHeart;
    [SerializeField] private Sprite loseHeart;
    [SerializeField] private int maxHealth;
    private int currentHealth;
    private void Start()
    {
        currentHealth = maxHealth;
        for (int i = 0; i < maxHealth; i++)
        {
            healthElement.Add(Instantiate(prefabHealthElement, transform));
            healthElement[i].GetComponent<Image>().sprite = commonHeart;
        }
        for (int i = 0; i < cardsConteiners.Length; i++)
        {
            cardsConteiners[i].loseLineEvent += LoseHeart;
        }
    }
    public void LoseHeart()
    {
        healthElement[currentHealth - 1].GetComponent<Image>().sprite = loseHeart;
        currentHealth--;
        if (currentHealth == 0)
        {
            LoseGame();
        }
    }
    private void LoseGame()
    {
        loseGameEvent?.Invoke();
        ResetGame();
    }
    private void ResetGame()
    {
        for (int i = healthElement.Count - 1; i >=0 ; i--)
        {
            Destroy(healthElement[i]);
            healthElement.RemoveAt(i);
        }

        currentHealth = maxHealth;
        for (int i = 0; i < maxHealth; i++)
        {
            healthElement.Add(Instantiate(prefabHealthElement, transform));
            healthElement[i].GetComponent<Image>().sprite = commonHeart;
        }
    }
}
