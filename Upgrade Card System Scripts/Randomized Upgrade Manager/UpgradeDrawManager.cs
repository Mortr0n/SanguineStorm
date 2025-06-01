using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeDrawManager : MonoBehaviour
{
    [SerializeField] private List<UpgradeCard_SO> allUpgradeCards;
    [SerializeField] public Transform cardContainer;
    [SerializeField] private GameObject cardSlotPrefab;
    [SerializeField] private int baseCardCount = 3;
    [SerializeField] private float chanceForFourth = 0.2f;
    [SerializeField] private float chanceForFifth = 0.05f;

    public static UpgradeDrawManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this)
        {
            Debug.LogWarning("Multiple instances of UpgradeDrawManager detected! Destroying duplicate.");
            Destroy(gameObject);
        }
    }

    private UpgradeCard_SO GetRandomEquipCard()
    {
        List<UpgradeCard_SO> equipCards = allUpgradeCards.FindAll(card => card.isWeaponEquipCard && !VS_PlayerController.instance.HasWeapon(card.weaponIdentifier));
        if (equipCards.Count == 0)
        {
            Debug.LogWarning("No equip cards available in the pool.");
            return null;
        }
        return equipCards[UnityEngine.Random.Range(0, equipCards.Count)];
    }

    public void DrawUpgradeCards()
    {
        var needsEquipCard = VS_PlayerController.instance.NeedsWeapon();

        Debug.Log("Drawing upgrade cards...");
        int drawCount = baseCardCount;
        if (UnityEngine.Random.value < chanceForFourth) drawCount++;
        if (UnityEngine.Random.value < chanceForFifth) drawCount++;

        List<UpgradeCard_SO> pool = BuildWeightedPool();
        List<UpgradeCard_SO> chosen = new List<UpgradeCard_SO>();

        if (needsEquipCard)
        {
            // if we need an equip card, ensure we have at least one in the pool
            UpgradeCard_SO equipCard = GetRandomEquipCard();
            if (equipCard != null)
            {
                Debug.Log("Adding equip card to pool.");
                chosen.Add(equipCard);
                pool.Remove(equipCard); // remove from pool to avoid duplicates
                drawCount--;
            }
            else
            {
                Debug.LogWarning("No equip cards available in the pool.");
            }
        }

        drawCount = Mathf.Clamp(drawCount, 3, 5); // ensure we don't draw less than 1 or more than available cards
        for (int i = 0; i < drawCount; i++)
        {
            Debug.Log($"Drawing card {i + 1}/{drawCount} from pool of {pool.Count} cards.");
            var chosenCard = pool[UnityEngine.Random.Range(0, pool.Count)];
            chosen.Add(chosenCard);
            pool.Remove(chosenCard); // no dupes
        }

        foreach (var card in chosen)
        {
            var cardGO = Instantiate(cardSlotPrefab, cardContainer);
            var button = cardGO.GetComponent<UpgradeCardButton>();

            button.SetCard(card);
        }
    }



    private List<UpgradeCard_SO> BuildWeightedPool()
    {
        List<UpgradeCard_SO> pool = new();
        VS_PlayerController playerController = VS_PlayerController.instance;
        foreach (var card in allUpgradeCards)
        {
            bool isEquipCard = card.isWeaponEquipCard;
            bool weaponUnlocked = playerController.IsWeaponUnlocked(card.weaponIdentifier);
            bool isEquipped = playerController.HasWeapon(card.weaponIdentifier);

            int weight = 1;
            if (isEquipCard)
            {
                if (!weaponUnlocked && !isEquipped)
                {
                    pool.Add(card); // add equip cards only if the weapon is not unlocked or equipped
                }
                continue;
            }
            if (isEquipped)
            {
                weight = UnityEngine.Random.Range(0, 100) < 50 ? 5 : 2; // random chance to favor owned weapons

                for (int i = 0; i < weight; i++)
                {
                    pool.Add(card);
                }
                    
            }
                
        }

        // fallback in case pool is empty we can add equip cards
        if (pool.Count == 0)
        {
            Debug.LogWarning("No upgrade cards available in pool, adding equip cards.");
            foreach (var card in allUpgradeCards)
            {
                if (card.isWeaponEquipCard && !playerController.HasWeapon(card.weaponIdentifier));
                {
                    pool.Add(card);
                }
            }
        }

        return pool;
    }
}
