using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CardIdentification : MonoBehaviour
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
    public int ID;
    [SerializeField] private CardList cardList;
    public TextMeshProUGUI CardTitleText;
    public int BaseStrength;
    private int Strength = int.MinValue;
    public TextMeshProUGUI CardStrengthText;
    private int Rarity;
    public GameObject RarityStar;
    public GameObject StarArea;
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
        SetDisplays();
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
    public void SetDisplays(){
        //Card image
        CardImage.sprite = cardList.GetCard(ID).asset;
        //Card title text
        CardTitleText.text = cardList.GetCard(ID).title;
        //Strength text
        CardStrengthText.text = Strength.ToString();
        if (Strength > BaseStrength)
        {
            CardStrengthText.color = Color.green;
        }
        else if (Strength < BaseStrength)
        {
            CardStrengthText.color = Color.red;
        }
        else
        {
            CardStrengthText.color = Color.white;
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
                Star.transform.SetParent(StarArea.transform, false);
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
        SetDescription();
        
    }
    public void SetDescription(){
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
        CardStrengthText.text = Strength.ToString();
        if (Strength > BaseStrength)
        {
            CardStrengthText.color = Color.green;
        }
        else if (Strength < BaseStrength)
        {
            CardStrengthText.color = Color.red;
        }
        else
        {
            CardStrengthText.color = Color.white;
        }
    }

    public int GetStrength(){
        return Strength;
    }
    public void ChangeStrength(int StrengthChange)
    {
        Strength += StrengthChange;
        CardStrengthText.text = Strength.ToString();
        if (Strength > BaseStrength)
        {
            CardStrengthText.color = Color.green;
        }
        else if (Strength < BaseStrength)
        {
            CardStrengthText.color = Color.red;
        }
        else
        {
            CardStrengthText.color = Color.white;
        }
    }
    public void ChangePotency(int PotencyChange, int ReqAbility){
        if (ReqAbility == Ability){
            Potency += PotencyChange;
            SetDescription();
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
}
