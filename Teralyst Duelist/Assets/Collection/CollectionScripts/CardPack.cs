using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class CardPack : MonoBehaviour
{
    [SerializeField] private int[] Contents;
    [SerializeField] private GameObject Card;
    [SerializeField] private GameObject DisplayArea;
    private List<int> RemainingContents = new();
    // Start is called before the first frame update
    void Start(){
        for (int i = 0; i < Contents.Length; i++){
            if(LevelManager.Collection[Contents[i]] == false){
                RemainingContents.Add(Contents[i]);
            }
        }
        for (int i = 0; i < RemainingContents.Count; i++){
            Debug.Log(RemainingContents[i]);
        }
    }
    public void OnClick(){
        System.Random rand = new();
        for (int i = 0; i < 3; i++){
            if (RemainingContents.Count > 0){
                int RandRange = Random.Range(0, RemainingContents.Count);
                int ID = RemainingContents[RandRange];
                RemainingContents.RemoveAt(RandRange);
                GameObject NewCard = Instantiate(Card, new Vector2(0, 0), Quaternion.identity);
                CardIdentification cardIdentification = NewCard.GetComponent<CardIdentification>();
                cardIdentification.ID = ID;
                cardIdentification.CardType = CardIdentification.CardTypes.DisplayCard;
                NewCard.transform.SetParent(DisplayArea.transform, false);
                NewCard.GetComponent<Selected>().CardType = Selected.CardTypes.DisplayCard;
                LevelManager.Collection[ID] = true;
                Debug.Log(ID);
            }
        }
    }

    // Update is called once per frame
}
