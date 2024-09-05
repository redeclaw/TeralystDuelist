using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardIdentification2 : MonoBehaviour
{
    //public GameObject CardOutline;
    //public GameObject CardArea;
    public enum CardTypes{
        BattleCard,
        CollectionCard,
        DeckCard,
        DisplayCard
    }
    public CardTypes CardType;
    public Image CardImage;
    public GameObject PlayerCard;
    public int ID;
    [SerializeField] private CardList cardList;
    private string Title;
    public GameObject TextBackground;
    public TextMeshProUGUI CardTitleText;
    public int BaseStrength;
    private int Strength = int.MinValue;
    public TextMeshProUGUI CardStrengthText;
    private int Rarity;
    public GameObject RarityStar;
    public int Ability;
    public int Potency;
    public int Condition;
    public int Requirement;
    public int RoundAbility;
    public int RoundPotency;
    public string Description;
    public bool PlayerSide;
    //true if card belongs to player, false if not.
    private BattleManager battleManager;
    /*
    public Sprite[] ElementImages;
    public int Element;
    public GameObject ElementDisplay;
    */
    //should only be true when used in collection scene
    // should only be true when used in collection scene AND as part of deck;

    void Start()
    {
        //clear children from duplicated cards, will reinstantiate later on
        foreach (Transform child in transform){
            Destroy(child.gameObject);
        }
        //outline stuff
        //CardArea = GameObject.Find("CardArea");
        /*GameObject Cardoutline = Instantiate(CardOutline, new Vector2(PlayerCard.transform.position.x, PlayerCard.transform.position.y), Quaternion.identity);
        Cardoutline.transform.SetParent(CardArea.transform, true);
        Cardoutline.transform.position = new Vector2(PlayerCard.transform.position.x, PlayerCard.transform.position.y);
        */
        //caching required components
        if (CardType == CardTypes.BattleCard){
            battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
            StartSetStrength();
        }
        else{
            BaseStrength = cardList.GetCard(ID).stats["strength"];
            Strength = BaseStrength;
        }
        CreateDisplays();
        SetBonusStats();
        if (CardType == CardTypes.BattleCard){
            TriggerRoundAbility();
        }
    }
    public void StartSetStrength(){
        BaseStrength = cardList.GetCard(ID).stats["strength"];
        //check if strength has been set yet
        if(Strength == int.MinValue){
            //Debug.Log($"default strength! {Strength}");
            if (PlayerSide){
                Strength = BaseStrength + battleManager.PlayerStrengthMod;
            }
            else{
                Strength = BaseStrength + battleManager.EnemyStrengthMod;
            }
        }
    }
    public void CreateDisplays(){
        //Sets sprite image
        CardImage.sprite = cardList.GetCard(ID).asset;
        //Background for text
        GameObject Background = Instantiate(TextBackground, new Vector2(0, -13), Quaternion.identity);
        Background.transform.SetParent(PlayerCard.transform, false);
        //Card title text
        Title = cardList.GetCard(ID).title;
        TextMeshProUGUI TitleText = Instantiate(CardTitleText, new Vector2(0, 0), Quaternion.identity);
        TitleText.transform.SetParent(PlayerCard.transform, false);
        TitleText.text = Title;
        //Strength text
        TextMeshProUGUI StrengthText = Instantiate(CardStrengthText, new Vector2(30, -40), Quaternion.identity);
        StrengthText.transform.SetParent(PlayerCard.transform, false);
        StrengthText.text = Strength.ToString();
        if (Strength > BaseStrength)
        {
            StrengthText.color = Color.green;
        }
        else if (Strength < BaseStrength)
        {
            StrengthText.color = Color.red;
        }
        else
        {
            StrengthText.color = Color.white;
        }
        if (CardType == CardTypes.BattleCard){
            /*
            //element
            GameObject elementDisplay = Instantiate(ElementDisplay, new Vector2 (-30, -50), Quaternion.identity);
            Image ElementImage = elementDisplay.GetComponent<Image>();
            ElementImage.sprite = ElementImages[Element];
            elementDisplay.transform.SetParent(PlayerCard.transform , false);
            */
        }
        //Rarity stars
        Rarity = cardList.GetCard(ID).stats["rarity"];
        for (int i = 0; i < Rarity; i++)
            {
                GameObject Star = Instantiate(RarityStar, new Vector2 (0, 0), Quaternion.identity);
                Star.transform.SetParent(PlayerCard.transform, false);
            }
    }
    public void SetBonusStats(){
        //setting all other stats
        Ability = cardList.GetCard(ID).stats["ability"];
        if (cardList.GetCard(ID).stats.ContainsKey("potency"))
        {
            Potency = cardList.GetCard(ID).stats["potency"];
        }
        else{
            Potency = 1;
        }
        if (cardList.GetCard(ID).stats.ContainsKey("condition")){
            Condition = cardList.GetCard(ID).stats["condition"];
        }
        else{
            Condition = 0;
        }
        if (cardList.GetCard(ID).stats.ContainsKey("requirement")){
            Requirement = cardList.GetCard(ID).stats["requirement"];
        }
        else{
            Requirement = 0;
        }
        if (cardList.GetCard(ID).stats.ContainsKey("roundability")){
            RoundAbility = cardList.GetCard(ID).stats["roundability"];
        }
        else{
            RoundAbility = 0;
        }
        if (cardList.GetCard(ID).stats.ContainsKey("roundpotency")){
            RoundPotency = cardList.GetCard(ID).stats["roundpotency"];
        }
        else{
            RoundPotency = 1;
        }
        //replace placeholder stuff in string with actual values
        string AbilityDesc = cardList.AbilityList[Ability];
        for(int i = 0; i < AbilityDesc.Length; i++){
            if (AbilityDesc[i] == 36){
                AbilityDesc = AbilityDesc.Remove(i, 1);
                AbilityDesc = AbilityDesc.Insert(i, Potency.ToString());
            }
        }
        string ReqDesc = cardList.CondList[Condition];
        for(int i = 0; i < ReqDesc.Length; i++){
            if (ReqDesc[i] == 36){
                ReqDesc = ReqDesc.Remove(i, 1);
                ReqDesc = ReqDesc.Insert(i, Requirement.ToString());
            }
        }
        string RoundAbilDesc = cardList.RoundAbilList[RoundAbility];
        for(int i = 0; i < RoundAbilDesc.Length; i++){
            if (RoundAbilDesc[i] == 36){
                RoundAbilDesc = RoundAbilDesc.Remove(i, 1);
                RoundAbilDesc = RoundAbilDesc.Insert(i, RoundPotency.ToString());
            }
        }
        //string interpolation for desc
        Description = $"{RoundAbilDesc}{ReqDesc}{AbilityDesc}\n{cardList.GetCard(ID).description}";
    }

    public void TriggerRoundAbility(){
        switch (RoundAbility){
            case 1:
            ChangeStrength(battleManager.RoundCount * RoundPotency);
            break;

            default:
            break;
        }
    }



    public void SetStrength(int NewStrength)
    {
        Strength = NewStrength;
        TextMeshProUGUI StrengthText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        StrengthText.text = Strength.ToString();
        if (Strength > BaseStrength)
        {
            StrengthText.color = Color.green;
        }
        else if (Strength < BaseStrength)
        {
            StrengthText.color = Color.red;
        }
        else
        {
            StrengthText.color = Color.white;
        }
    }

    public int GetStrength(){
        return Strength;
    }
    public void ChangeStrength(int StrengthChange)
    {
        Strength += StrengthChange;
        TextMeshProUGUI StrengthText = transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        StrengthText.text = Strength.ToString();
        if (Strength > BaseStrength)
        {
            StrengthText.color = Color.green;
        }
        else if (Strength < BaseStrength)
        {
            StrengthText.color = Color.red;
        }
        else
        {
            StrengthText.color = Color.white;
        }
    }

    public double GetPriority(){
        double StrengthMod = Strength * -0.2;
        switch (Condition){
            case 1:
            if (Strength < Requirement){
                return StrengthMod;
            }
            else{
                break;
            }

            default:
            break;
        }
        int[] AbilityVals = new int[7] {0, 1, 1, 5, 5, 3, 0};
        return AbilityVals[Ability] * Potency + StrengthMod;
    }
    // Update is called once per frame
    public void Return()
    {
        if (PlayerSide)
        {
            //gameObject.transform.SetParent(PlayerArea.transform, false);
            PlayerCard.layer = 6;
            //battleManager.UpdatePlayerScore(Strength * -1);
            //DragDrop dragDrop = gameObject.GetComponent<DragDrop>();
            //dragDrop.inPlay = false;
        }
        if (PlayerSide == false)
        {
            //gameObject.transform.SetParent(EnemyArea.transform, false);
            //battleManager.UpdateEnemyScore(Strength * -1);
        }
    }
}