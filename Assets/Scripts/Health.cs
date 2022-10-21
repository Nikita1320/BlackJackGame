using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
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
            cardsConteiners[i].loseLineEvent += LoseLine;
        }
    }
    public void LoseLine()
    {
        healthElement[currentHealth - 1].GetComponent<Image>().sprite = loseHeart;
        currentHealth--;
    }
}
