using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selected : MonoBehaviour
{
    public enum CardTypes{
        BattleCard,
        CollectionCard,
        DeckCard,
        DisplayCard
    }
    public CardTypes CardType;
    public Image CardImage;
    public bool Select = false;
    public bool Selectable = true;
    public BattleManager battleManager;
    [SerializeField] private CardIdentification cardIdentification;
    public void Clear(){
        Destroy(this.gameObject);
    }
    public void SelectedClear(){
        if (Select){
            Destroy(this.gameObject);
        }
    }
    public void OnClick(){
        switch(CardType){
            case CardTypes.BattleCard:
                //make sure card is selectable
                if (Selectable){
                    //check if it has already been selected
                    if (!Select){
                        Select = true;
                        battleManager.CardSelected(transform.GetSiblingIndex(), GetComponent<CardIdentification>().ID);
                        CardImage.color = Color.green;
                    }
                    else{
                        Select = false;
                        battleManager.CardUnselected(transform.GetSiblingIndex(), GetComponent<CardIdentification>().ID);
                        CardImage.color = Color.white;
                    }
                }
                break;
            case CardTypes.CollectionCard:
                DeckManager.CardAdded(cardIdentification.ID);
                break;
            case CardTypes.DeckCard:
                DeckManager.CardRemoved(cardIdentification.ID);
                Destroy(this.gameObject);
                break;
            case CardTypes.DisplayCard:
                break;
            
            default:
            break;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        if (CardType == CardTypes.BattleCard){
            battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        }
    }

}
