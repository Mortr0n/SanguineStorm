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
    }

    private void Start()
    {
        DrawUpgradeCards();
    }

    public void DrawUpgradeCards()
    {
        int drawCount = baseCardCount;
        if (UnityEngine.Random.value < chanceForFourth) drawCount++;
        if (UnityEngine.Random.value < chanceForFifth) drawCount++;

        List<UpgradeCard_SO> pool = BuildWeightedPool();
        List<UpgradeCard_SO> chosen = new List<UpgradeCard_SO>();

        for (int i = 0; i < drawCount; i++)
        {
            var chosenCard = pool[UnityEngine.Random.Range(0, pool.Count)];
            chosen.Add(chosenCard);
            pool.RemoveAll(x => x == chosenCard); // no dupes
        }

        foreach (var card in chosen)
        {
            var cardGO = Instantiate(cardSlotPrefab, cardContainer);
            var button = cardGO.GetComponentInChildren<UpgradeCardButton>();

            button.SetCard(card);
        }
    }



    private List<UpgradeCard_SO> BuildWeightedPool()
    {
        List<UpgradeCard_SO> pool = new();
        foreach (var card in allUpgradeCards)
        {
            //bool ownsWeapon = PlayerOwnsWeapon(card.weaponIdentifier);
            bool ownsWeapon = true; //FIXME: for testing purposes, replace with actual logic above!
            int weight = 1;

            if (ownsWeapon)
                weight = UnityEngine.Random.Range(0, 100) < 50 ? 5 : 2; // random chance to favor owned weapons

            for (int i = 0; i < weight; i++)
                pool.Add(card);
        }

        return pool;
    }





}
