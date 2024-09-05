using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public BattleManager battleManager;
    [SerializeField] private GameObject PlayerArea;
    [SerializeField] private GameObject TutorialTextBackgorund;
    [SerializeField] private TextMeshProUGUI TutorialText;
    [SerializeField] private GameObject ZoomCanvas;
    [SerializeField] private Button NextButton;
    [SerializeField] private CanvasGroup MainCanvasGroup;
    private static TutorialManager instance;
    private bool TutorialNext;
    // Start is called before the first frame update
    void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
            return;
        }
    }
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void NextTutorial(){
        instance.TutorialNext = true;
        return;
    }
    public IEnumerator StartTutorial(){
        MainCanvasGroup.blocksRaycasts = false;
        TutorialNext = false;
        Debug.Log("peepee");
        while(!TutorialNext){
            yield return null;
        }
        TutorialText.text = "Your deck template consists of seven unique cards. Your deck is made out of four copies of each card in your template, for a total of 28 cards. ";
        TutorialNext = false;
        while(!TutorialNext){
            yield return null;
        }
        Queue<int> PlayerDeck = battleManager.PlayerDeckIdentifiers;
        Queue<int> EnemyDeck = battleManager.EnemyDeckIdentifiers;
        PlayerDeck.Enqueue(0);
        PlayerDeck.Enqueue(1);
        PlayerDeck.Enqueue(2);
        PlayerDeck.Enqueue(1);
        PlayerDeck.Enqueue(3);
        PlayerDeck.Enqueue(2);
        PlayerDeck.Enqueue(1);
        EnemyDeck.Enqueue(1);
        EnemyDeck.Enqueue(0);
        EnemyDeck.Enqueue(0);
        EnemyDeck.Enqueue(11);
        EnemyDeck.Enqueue(3);
        EnemyDeck.Enqueue(15);
        EnemyDeck.Enqueue(15);
        battleManager.StartCoroutine(battleManager.FlipCoin(9));
        TutorialText.text = "At the start of each round, both players draw 7 cards. This game, you'll go first. Since your opponent is going second, they get a +1 boost to the cards in their hand this round.";
        TutorialNext = false;
        while(!TutorialNext){
            yield return null;
        }
        TutorialText.text = "You can see what each of your cards do by hovering over them. Try playing the peasant by clicking it, and then clicking the play button.";
        TutorialNext = false;
        NextButton.interactable = false;
        MainCanvasGroup.blocksRaycasts = true;
        while(!TutorialNext){
            yield return null;
        }
        TutorialText.text = "The number in the top left of each card is its strength. In order to play a card, it must have higher strength than the one on the field. Play your barbarian.";
        TutorialNext = false;
        while(!TutorialNext){
            yield return null;
        }
        TutorialText.text = "Since you don't have any cards you can play, you must pass. You can pass if you cannot or don't want to play any cards.";
        TutorialNext = false;
        while(!TutorialNext){
            yield return null;
        }
        MainCanvasGroup.blocksRaycasts = false;
        NextButton.interactable = true;
        TutorialText.text = "When you pass, you give your opponent initiative. When you have initiative, you can play any multiple of identical cards in your hand. Since your opponent had initiative, they played the pair of peasants they had in their hand.";
        TutorialNext = false;
        while(!TutorialNext){
            yield return null;
        }
        MainCanvasGroup.blocksRaycasts = true;
        NextButton.interactable = false;
        TutorialText.text = "In order to play on a pair, you must play a higher strength pair on top of it. Play both of your rogues.";
        TutorialNext = false;
        while(!TutorialNext){
            yield return null;
        }
        TutorialText.text = "Your opponent passed, giving you initiative. You can now play all three of your farmers!";
        TutorialNext = false;
        while(!TutorialNext){
            yield return null;
        }
        MainCanvasGroup.blocksRaycasts = false;
        NextButton.interactable = true;
        TutorialText.text = "When a player runs out of cards, the round ends. Both players draw a hand of seven new cards.";
        TutorialNext = false;
        while(!TutorialNext){
            yield return null;
        }
        TutorialText.text = "Players take damage for any remaining cards they had in hand at the end of the round. You currently deal 2 damage for each remaining card, so your opponent took 6 damage for their 3 remaining cards.";
        TutorialNext = false;
        while(!TutorialNext){
            yield return null;
        }
        TutorialText.text = "That's the end of the tutorial. Use your skills to defeat your opponent!";
        TutorialNext = false;
        while(!TutorialNext){
            yield return null;
        }
        MainCanvasGroup.blocksRaycasts = true;
        Destroy(this);
    }
    public IEnumerator CheckSelected(){
        bool Selected = false;
        while(!Selected){
            yield return null;
            foreach (Transform card in PlayerArea.transform){
                if (card.GetComponent<Selected>().Select){
                    Selected = true;
                }
            }
        }
        Debug.Log("poopoo");
    }
    void OnDestroy(){
        Destroy(TutorialTextBackgorund);
    }
}
