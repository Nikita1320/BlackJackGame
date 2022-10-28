using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private Bonus[] bonuses;
    [SerializeField] private Bonus currentUsedBonus;

    private void Start()
    {
        for (int i = 0; i < bonuses.Length; i++)
        {
            bonuses[i].exitBonusEvent += ExitBonus;
        }
    }
    public void EntryBonus(int indexBonus)
    {
        Debug.Log("IWork");
        if (currentUsedBonus == bonuses[indexBonus])
        {
            currentUsedBonus.Exit();
            currentUsedBonus = null;
            Debug.Log("DoubleClickOnBonus --> DisActive");
        }
        else
        {
            if (currentUsedBonus != null)
            {
                currentUsedBonus.Exit();
                currentUsedBonus = null;
                Debug.Log("Exit");
            }
            if (bonuses[indexBonus].Entry())
            {
                currentUsedBonus = bonuses[indexBonus];
                Debug.Log("Entry");
            }
        }
    }
    public void ExitBonus()
    {
        currentUsedBonus = null;
    }
    public void Test()
    {
        Debug.Log("Test");
    }
}
