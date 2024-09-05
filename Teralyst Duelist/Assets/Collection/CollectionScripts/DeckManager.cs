using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckManager : MonoBehaviour
{
    private static DeckManager instance;
    [SerializeField] private GameObject CollectionArea;
    [SerializeField] private GameObject CollectionCard;
    [SerializeField] private GameObject DeckArea;
    [SerializeField] private GameObject DeckCard;
    private List<int> CurrentDeck = new List<int>();
    

    void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        bool[] Collection = LevelManager.Collection;
        for (int i = 0; i < LevelManager.CardNumber; i++){
            if (Collection[i]){
                GameObject collectionCard = Instantiate(CollectionCard, new Vector2(0, 0), Quaternion.identity);
                collectionCard.transform.SetParent(CollectionArea.transform);
                CardIdentification cardIdentification = collectionCard.GetComponent<CardIdentification>();
                cardIdentification.ID = i;
                cardIdentification.CardType = CardIdentification.CardTypes.CollectionCard;
                collectionCard.GetComponent<Selected>().CardType = Selected.CardTypes.CollectionCard;
            }
        }
        for (int i = 0; i < LevelManager.CardNumber; i++){

        }
        //iterate through playerdeck
        for (int i = 0; i < LevelManager.DeckSize; i++){
            //add card to deck list
            CurrentDeck.Add(LevelManager.PlayerDeck[i]);
            //create card in deckarea
            GameObject deckCard = Instantiate(DeckCard, new Vector2(0, 0), Quaternion.identity);
            deckCard.GetComponent<RectTransform>().sizeDelta = new Vector2 (320, 150);
            deckCard.transform.SetParent(DeckArea.transform);
            CardIdentification cardIdentification = deckCard.GetComponent<CardIdentification>();
            cardIdentification.ID = LevelManager.PlayerDeck[i];
            cardIdentification.CardType = CardIdentification.CardTypes.DeckCard;
            deckCard.GetComponent<Selected>().CardType = Selected.CardTypes.DeckCard;
            
            //LevelManager.PlayerDeck[i]
        }
    }
    public static void CardAdded(int ID){
        if (!instance.CurrentDeck.Contains(ID)){
            instance.CurrentDeck.Add(ID);
            //create card in deckarea
            GameObject deckCard = Instantiate(instance.DeckCard, new Vector2(0, 0), Quaternion.identity);
            deckCard.GetComponent<RectTransform>().sizeDelta = new Vector2 (320, 150);
            deckCard.transform.SetParent(instance.DeckArea.transform);
            CardIdentification cardIdentification = deckCard.GetComponent<CardIdentification>();
            cardIdentification.ID = ID;
            cardIdentification.CardType = CardIdentification.CardTypes.DeckCard;
            deckCard.GetComponent<Selected>().CardType = Selected.CardTypes.DeckCard;
        }
    }
    public static void CardRemoved(int ID){
        instance.CurrentDeck.Remove(ID);
    }
    public static void Exit(){
        if (instance.CurrentDeck.Count == LevelManager.DeckSize){
            LevelManager.PlayerDeck = instance.CurrentDeck.ToArray();
            LevelManager.ExitCollection();
        }
    }
}
