using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    [SerializeField] private GameObject DropZone;
    [SerializeField] private GameObject PlayerCard;
    [SerializeField] private GameObject PlayerArea;
    [SerializeField] private GameObject EnemyArea;
    [SerializeField] private GameObject CoinCanvas;
    [SerializeField] private GameObject DarkEffect;
    [SerializeField] private GameObject Coin;
    public int[] StartingDeck;
    public Queue<int> PlayerDeckIdentifiers;
    //public Queue<int> PlayerDeckElements;
    public int[] EnemyStarting;
    public Queue<int> EnemyDeckIdentifiers;
    //public Queue<int> EnemyDeckElements;
    public List<int> SelectedCards;
    [SerializeField] private Button PassButton;
    [SerializeField] private TextMeshProUGUI PassText;
    [SerializeField] private HealthBar healthBarPlayer;
    [SerializeField] private HealthBar healthBarEnemy;
    public int PlayerMaxHealth;
    public int PlayerCurrentHealth;
    private int PlayerDamage;
    public int EnemyMaxHealth;
    public int EnemyCurrentHealth;
    private int EnemyDamage;
    private bool GameFinished;
    [SerializeField] private TextMeshProUGUI PlayerHealthDisplay;
    [SerializeField] private TextMeshProUGUI EnemyHealthDisplay;
    [SerializeField] private TextMeshProUGUI EnemyCardDisplay;
    [SerializeField] private TextMeshProUGUI RoundDisplay;
    [SerializeField] private Image PlayerImage;
    [SerializeField] private Image EnemyImage;
    public int PlayerStrengthMod;
    public int EnemyStrengthMod;
    public int RoundCount;
    private bool PlayerCardDisabled;
    private bool EnemyCardDisabled;
    [SerializeField] private TutorialManager tutorialManager;
    [SerializeField] private AudioSource SoundEffectSource;
    [SerializeField] private AudioClip CardDrawSound;
    [SerializeField] private AudioClip ButtonPressSound;

    void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    void Start(){
        if (LevelManager.TutorialBattle){
            StartTutorial();
        }
        else{
            StartGame();
            Destroy(tutorialManager);
        }
    }
    /*
    public static void StartBattle(){
        instance.RoundCount = -1;
        instance.PlayerCurrentHealth = instance.PlayerMaxHealth;
        instance.healthBarPlayer.SetMaxHealth(instance.PlayerMaxHealth);
        instance.PlayerHealthDisplay.text = $"{instance.PlayerCurrentHealth}/{instance.PlayerMaxHealth}";
        instance.EnemyCurrentHealth = instance.EnemyMaxHealth;
        instance.healthBarEnemy.SetMaxHealth(instance.EnemyMaxHealth);
        instance.EnemyHealthDisplay.text = $"{instance.EnemyCurrentHealth}/{instance.EnemyMaxHealth}";
        instance.PlayerDeckIdentifiers = new Queue<int>();
        instance.PlayerDeckElements = new Queue<int>();
        instance.EnemyDeckIdentifiers = new Queue<int>();
        instance.EnemyDeckElements = new Queue<int>();
        instance.StartingDeck = LevelManager.PlayerDeck;
        instance.EnemyStarting = LevelManager.EnemyDeck;
        System.Random random = new System.Random();
        instance.StartCoroutine(instance.StartRound(true));
    }
    */
    public void StartGame()
    {
        GameFinished = false;
        RoundCount = -1;
        //get info from levelmanager
        PlayerMaxHealth = LevelManager.PlayerHealth;
        EnemyMaxHealth = LevelManager.EnemyHealth;
        StartingDeck = LevelManager.PlayerDeck;
        EnemyStarting = LevelManager.EnemyDeck;
        PlayerDamage = LevelManager.PlayerDamage;
        EnemyDamage = LevelManager.EnemyDamage;
        //set player health
        PlayerCurrentHealth = PlayerMaxHealth;
        healthBarPlayer.SetMaxHealth(PlayerMaxHealth);
        PlayerHealthDisplay.text = $"{PlayerCurrentHealth}/{PlayerMaxHealth}";
        //set enemy health
        EnemyCurrentHealth = EnemyMaxHealth;
        healthBarEnemy.SetMaxHealth(EnemyMaxHealth);
        EnemyHealthDisplay.text = $"{EnemyCurrentHealth}/{EnemyMaxHealth}";
        //initialize deck parts
        PlayerDeckIdentifiers = new Queue<int>();
        //PlayerDeckElements = new Queue<int>();
        EnemyDeckIdentifiers = new Queue<int>();
        //EnemyDeckElements = new Queue<int>();
        //System.Random random = new();
        //StartCoroutine(FlipCoin(random.Next(8, 12)));
        StartCoroutine(FlipCoin(Random.Range(8, 12)));
    }
    public void StartTutorial(){
        GameFinished = false;
        RoundCount = -1;
        //get info from levelmanager
        PlayerMaxHealth = LevelManager.PlayerHealth;
        EnemyMaxHealth = LevelManager.EnemyHealth;
        StartingDeck = LevelManager.PlayerDeck;
        EnemyStarting = LevelManager.EnemyDeck;
        PlayerDamage = LevelManager.PlayerDamage;
        EnemyDamage = LevelManager.EnemyDamage;
        //set player health
        PlayerCurrentHealth = PlayerMaxHealth;
        healthBarPlayer.SetMaxHealth(PlayerMaxHealth);
        PlayerHealthDisplay.text = $"{PlayerCurrentHealth}/{PlayerMaxHealth}";
        //set enemy health
        EnemyCurrentHealth = EnemyMaxHealth;
        healthBarEnemy.SetMaxHealth(EnemyMaxHealth);
        EnemyHealthDisplay.text = $"{EnemyCurrentHealth}/{EnemyMaxHealth}";
        //initialize deck parts
        PlayerDeckIdentifiers = new Queue<int>();
        //PlayerDeckElements = new Queue<int>();
        EnemyDeckIdentifiers = new Queue<int>();
        //EnemyDeckElements = new Queue<int>();
        tutorialManager.StartCoroutine(tutorialManager.StartTutorial());
        //StartCoroutine(FlipCoin(8));

    }
    public IEnumerator FlipCoin(int NumFlips){
        GameObject darkEffect = Instantiate(DarkEffect, new Vector2(0, 0), Quaternion.identity);
        darkEffect.transform.SetParent(CoinCanvas.transform, false);
        GameObject coin = Instantiate(Coin, new Vector2(0, 0), Quaternion.identity);
        coin.transform.SetParent(CoinCanvas.transform, false);
        coin.GetComponent<CoinFlip>().NumFlips = NumFlips;
        yield return new WaitForSeconds(4);
        Destroy(darkEffect);
        if (NumFlips % 2 == 0){
            StartCoroutine(StartRound(false, true));
        }
        else{
            StartCoroutine(StartRound(true, true));
        }
    }
    public void SplitDeck(ref Queue<int> Identifiers, int[] deck){
        //PlayerDeckIdentifiers = new Queue<int>();
        //PlayerDeckElements = new Queue<int>();
        //Identifiers = new Queue<int>();
        //Elements = new Queue<int>();
        int[] cardIdentifiers = new int[LevelManager.DeckSize * 4];
        //int[] cardElements = new int[LevelManager.DeckSize * 4];
        for (int i = 0; i < LevelManager.DeckSize; i++){
            for (int j = 0; j < 4; j++){
                //cardIdentifiers[i*4+j] = StartingDeck[i];
                cardIdentifiers[i*4+j] = deck[i];
                //cardElements[i*4+j] = j;
                //temp[i*4+j].Key = StartingDeck[i];

            }
        }
        Shuffle(cardIdentifiers);
        //int[] tempdeck = shuffle(StartingDeck);
        for (int i = 0; i < LevelManager.DeckSize*4; i++){
            //Debug.Log(i);
            //PlayerDeckIdentifiers.Enqueue(cardIdentifiers[i]);
            //PlayerDeckElements.Enqueue(cardElements[i]);
            Identifiers.Enqueue(cardIdentifiers[i]);
            //Elements.Enqueue(cardElements[i]);
        }
        

    }
    public void SplitDeck(ref Queue<int> Identifiers, ref Queue<int> Elements, int[] deck){
        //PlayerDeckIdentifiers = new Queue<int>();
        //PlayerDeckElements = new Queue<int>();
        //Identifiers = new Queue<int>();
        //Elements = new Queue<int>();
        int[] cardIdentifiers = new int[LevelManager.DeckSize * 4];
        int[] cardElements = new int[LevelManager.DeckSize * 4];
        for (int i = 0; i < LevelManager.DeckSize; i++){
            for (int j = 0; j < 4; j++){
                //cardIdentifiers[i*4+j] = StartingDeck[i];
                cardIdentifiers[i*4+j] = deck[i];
                cardElements[i*4+j] = j;
                //temp[i*4+j].Key = StartingDeck[i];

            }
        }
        Shuffle(cardIdentifiers);
        //int[] tempdeck = shuffle(StartingDeck);
        for (int i = 0; i < LevelManager.DeckSize*4; i++){
            //Debug.Log(i);
            //PlayerDeckIdentifiers.Enqueue(cardIdentifiers[i]);
            //PlayerDeckElements.Enqueue(cardElements[i]);
            Identifiers.Enqueue(cardIdentifiers[i]);
            Elements.Enqueue(cardElements[i]);
        }
        

    }
    public IEnumerator StartRound(bool PlayerStart, bool FirstRound){
        RoundCount++;
        EnemyCardDisabled = false;
        RoundDisplay.text = $"Rounds passed: {RoundCount}";
        if (PlayerDeckIdentifiers.Count == 0){
            SplitDeck(ref PlayerDeckIdentifiers, StartingDeck);
            SplitDeck(ref EnemyDeckIdentifiers, EnemyStarting);
        }
        for(int i = 0; i < LevelManager.DeckSize; i++){
            //Debug.Log(i);
            DrawCard(true);
            DrawCard(false);
            yield return new WaitForSeconds(0.25f);
        }
        EnemyCardDisplay.text = $"Enemy Cards Left: {LevelManager.DeckSize}";
        if (PlayerStart){
            if (FirstRound){
                foreach (Transform card in EnemyArea.transform){
                    card.GetComponent<CardIdentification>().ChangeStrength(1);
                }
            }
            StartTurn();
        }
        else{
            if (FirstRound){
                foreach (Transform card in PlayerArea.transform){
                    card.GetComponent<CardIdentification>().ChangeStrength(1);
                }
            }
            yield return new WaitForSeconds(1);
            StartCoroutine(EnemyTurn());
        }
    }
    public void EndRound(bool NoPlayerCards){
        foreach(Transform card in DropZone.transform){
            Destroy(card.gameObject);
        }
        DropZone.transform.DetachChildren();
        StartCoroutine(ChangeHealth(PlayerArea.transform.childCount * EnemyDamage, true));
        StartCoroutine(ChangeHealth(EnemyArea.transform.childCount * PlayerDamage, false));
        foreach(Transform card in PlayerArea.transform){
            Destroy(card.gameObject);
        }
        foreach(Transform card in EnemyArea.transform){
            Destroy(card.gameObject);
        }
        StartCoroutine(StartRound(NoPlayerCards, false));
    }
    public void StartTurn(){
        PassButton.interactable = true;
        PassText.text = "Pass";
        //determine which cards selectable based on whats on field
        SetSelectableEmpty();
    }
    public void SetSelectableEmpty(){
        switch (DropZone.transform.childCount)
        {
            //if none, easy. Just set all to selectable
            case 0:
            //pass button shouldnt be interactable, no reason to pass on open board
            PassButton.interactable = false;
            //Debug.Log("zero");
            foreach(Transform card in PlayerArea.transform){
                card.GetComponent<Selected>().Selectable = true;
            }
            break;

            //if one, still easy. Set all with greater strength to selectable
            case 1:
            //Debug.Log("one");
            int ReqStrength = DropZone.transform.GetChild(0).GetComponent<CardIdentification>().GetStrength();
            foreach(Transform card in PlayerArea.transform){
                if (card.GetComponent<CardIdentification>().GetStrength() > ReqStrength){
                    card.GetComponent<Selected>().Selectable = true;
                }
                else{
                    card.GetComponent<Selected>().Selectable = false;
                }
            }
            break;

            //if 2, only pairs with greater strength selectable
            case 2:
            //Debug.Log("two");
            ReqStrength = DropZone.transform.GetChild(0).GetComponent<CardIdentification>().GetStrength();
            Dictionary<int, int> IdDict = new();
            foreach (Transform card in PlayerArea.transform){
                int CurrentID = card.GetComponent<CardIdentification>().ID;
                if (!IdDict.ContainsKey(CurrentID)){
                    IdDict[CurrentID] = 1;
                }
                else{
                    IdDict[CurrentID]++;
                }
            }
            foreach(Transform card in PlayerArea.transform){
                CardIdentification CardInfo = card.GetComponent<CardIdentification>();
                if (CardInfo.GetStrength() > ReqStrength && IdDict[CardInfo.ID] > 1){
                    card.GetComponent<Selected>().Selectable = true;
                }
                else{
                    card.GetComponent<Selected>().Selectable = false;
                }
            }
            break;

            //if 3, only triples with greater strength selectable
            case 3:
            //Debug.Log("three");
            ReqStrength = DropZone.transform.GetChild(0).GetComponent<CardIdentification>().GetStrength();
            IdDict = new Dictionary<int, int>();
            foreach(Transform card in PlayerArea.transform){
                int CurrentID = card.GetComponent<CardIdentification>().ID;
                if (!IdDict.ContainsKey(CurrentID)){
                    IdDict[CurrentID] = 1;
                }
                else{
                    IdDict[CurrentID]++;
                }
            }
            foreach(Transform card in PlayerArea.transform){
                CardIdentification CardInfo = card.GetComponent<CardIdentification>();
                if (CardInfo.GetStrength() > ReqStrength && IdDict[CardInfo.ID] > 2){
                    card.GetComponent<Selected>().Selectable = true;
                }
                else{
                    card.GetComponent<Selected>().Selectable = false;
                }
            }
            break;

            //if 4, only quads with greater strength selectable
            case 4:
            //Debug.Log("four");
            ReqStrength = DropZone.transform.GetChild(0).GetComponent<CardIdentification>().GetStrength();
            IdDict = new Dictionary<int, int>();
            foreach(Transform card in PlayerArea.transform){
                int CurrentID = card.GetComponent<CardIdentification>().ID;
                if (!IdDict.ContainsKey(CurrentID)){
                    IdDict[CurrentID] = 1;
                }
                else{
                    IdDict[CurrentID]++;
                }
            }
            foreach(Transform card in PlayerArea.transform){
                CardIdentification CardInfo = card.GetComponent<CardIdentification>();
                if (CardInfo.GetStrength() > ReqStrength && IdDict[CardInfo.ID] > 3){
                    card.GetComponent<Selected>().Selectable = true;
                }
                else{
                    card.GetComponent<Selected>().Selectable = false;
                }
            }
            break;
            
            default:
            break;
        }
    }
    public IEnumerator EnemyTurn(){
        PlayerCardDisabled = false;
        //logic for enemy turn
        //first, generate dictionary of ids: basically finds singles, pairs, trips and quads.
        Dictionary<int, int> IdDict = new();
        foreach(Transform card in EnemyArea.transform){
            int CurrentID = card.GetComponent<CardIdentification>().ID;
            if (!IdDict.ContainsKey(CurrentID)){
                IdDict[CurrentID] = 1;
            }
            else{
                IdDict[CurrentID]++;
            }
        }
        //Debug.Log(DropZone.transform.childCount);
        switch(DropZone.transform.childCount){
            //board empty
            case 0:
            //if one card play it 
            if (EnemyArea.transform.childCount == 1){
                EnemyPlayCard(0);
            }
            else{
                if(!TryPlayLowestMultiple(4, 4, int.MinValue, IdDict)){
                    if(!TryPlayLowestMultiple(3, 3, int.MinValue, IdDict)){
                        if(!TryPlayLowestMultiple(2, 2, int.MinValue, IdDict)){
                            TryPlayLowestMultiple(1, 1, int.MinValue, IdDict);
                        }
                    }
                }
            }
            break;

            case 1:
            int ReqStrength = DropZone.transform.GetChild(0).GetComponent<CardIdentification>().GetStrength();
            if(!TryPlayLowestMultiple(1, 1, ReqStrength, IdDict)){
                if(!TryPlayLowestMultiple(1, 2, ReqStrength, IdDict)){
                    if(!TryPlayLowestMultiple(1, 3, ReqStrength, IdDict)){
                        if(!TryPlayLowestMultiple(1, 4, ReqStrength, IdDict)){
                            EnemyPass();
                        }
                    }
                }
            }
            break;
            case 2:
            ReqStrength = DropZone.transform.GetChild(0).GetComponent<CardIdentification>().GetStrength();
            if(!TryPlayLowestMultiple(2, 2, ReqStrength, IdDict)){
                if(!TryPlayLowestMultiple(2, 3, ReqStrength, IdDict)){
                    if(!TryPlayLowestMultiple(2, 4, ReqStrength, IdDict)){
                        EnemyPass();
                    }
                }
            }
            break;
            case 3:
            ReqStrength = DropZone.transform.GetChild(0).GetComponent<CardIdentification>().GetStrength();
            if(!TryPlayLowestMultiple(3, 3, ReqStrength, IdDict)){
                if(!TryPlayLowestMultiple(3, 4, ReqStrength, IdDict)){
                    EnemyPass();
                }
            }
            break;

            case 4:
            ReqStrength = DropZone.transform.GetChild(0).GetComponent<CardIdentification>().GetStrength();
            if(!TryPlayLowestMultiple(4, 4, ReqStrength, IdDict)){
                EnemyPass();
            }
            break;

            default:
            break;
        }
        //sloppy workaround so cards get deleted before child count is checked
        yield return null;
        EnemyCardDisplay.text = $"Enemy Cards Left: {EnemyArea.transform.childCount}";
        //yield return new WaitForSeconds(1);
        //end round if no cards left
        if (EnemyArea.transform.childCount == 0){
            yield return new WaitForSeconds(1);
            EndRound(false);
        }
        else{
            //EnemyCardDisplay.text = $"Enemy Cards Left: {EnemyArea.transform.childCount}";
            StartTurn();
        }
    }
    public void EnemyPass(){
        //put new cards on top
        foreach (Transform card in DropZone.transform){
            Destroy(card.gameObject);
        }
        //must detach children as childcount is called within the same frame and destroy occurs next frame
        DropZone.transform.DetachChildren();
        //StartCoroutine(RefillHand(false));
    }
    
    public bool TryPlayLowestMultiple(int ReqGroupSize, int DesiredGroupSize, int ReqStrength, Dictionary<int, int> IdDict){
        //check for quads
        List<int> groups = new();
        foreach (KeyValuePair<int, int> group in IdDict)
        {
            //Debug.Log(group.Key);
            if (group.Value == DesiredGroupSize){
                groups.Add(group.Key);
            }
            }
        //if group exist, attempt play lowest quad
        if (groups.Count > 0){
            int LowestStrength = int.MaxValue;
            int lowestIndex = int.MaxValue;
            int currentStrength;
            for (int i = 0; i < groups.Count; i++){
                foreach(Transform card in EnemyArea.transform){
                    if (card.gameObject.GetComponent<CardIdentification>().ID == groups[i]){
                        currentStrength = card.gameObject.GetComponent<CardIdentification>().GetStrength();
                        if (currentStrength < LowestStrength && currentStrength > ReqStrength){
                            lowestIndex = i;
                            LowestStrength = currentStrength;
                        }
                        break;
                    }
                }
            }
            //if lowestIndex = int.maxvalue, all too low, return false
            if (lowestIndex == int.MaxValue){
                return false;
            }
            //find location of all cards and play them
            List<int> tracker = new();
            foreach(Transform card in EnemyArea.transform){
                if (card.gameObject.GetComponent<CardIdentification>().ID == groups[lowestIndex]){
                        tracker.Add(card.GetSiblingIndex());
                }
            }
            switch(ReqGroupSize){
                case 1:
                //StartCoroutine(ChangeHealth(1, true, true));
                EnemyPlayCard(tracker[0]);
                break;
                case 2:
                //StartCoroutine(ChangeHealth(4, true, true));
                EnemyPlayCard(tracker[0], tracker[1]);
                break;
                case 3:
                //StartCoroutine(ChangeHealth(9, true, true));
                EnemyPlayCard(tracker[0], tracker[1], tracker[2]);
                break;
                case 4:
                //StartCoroutine(ChangeHealth(16, true, true));
                EnemyPlayCard(tracker[0], tracker[1], tracker[2], tracker[3]);
                break;
                default:
                break;
            }
            return true;
        }
        return false;
    }
    public void AddCard(bool Player, int Index){
        GameObject CurrentCard;
        if(Player){
            CurrentCard = PlayerArea.transform.GetChild(Index).gameObject;
        }
        else{
            CurrentCard = EnemyArea.transform.GetChild(Index).gameObject;
            //select set to true to keep in line with player cards
            CurrentCard.GetComponent<Selected>().Select = true;
        }
        ProcessAbility(Player, CurrentCard.GetComponent<CardIdentification>());
        GameObject NewCard = Instantiate(CurrentCard, new Vector2(0, 0), Quaternion.identity);
        NewCard.transform.SetParent(DropZone.transform);
        NewCard.GetComponent<CardIdentification>().CardImage.color = Color.white;
        NewCard.GetComponent<CardIdentification>().SetStrength(CurrentCard.GetComponent<CardIdentification>().GetStrength());
    }
    public void ProcessAbility(bool player, CardIdentification cardIdentification){
        if (player){
            if (PlayerCardDisabled){
                return;
            }
        }
        else{
            if (EnemyCardDisabled){
                return;
            }
        }
        switch (cardIdentification.Condition){
            case 1:
            if (cardIdentification.GetStrength() < cardIdentification.Requirement){
                return;
            }
            else{
                break;
            }
            case 2:
            if (RoundCount < cardIdentification.Requirement){
                return;
            }
            else{
                break;
            }
            case 3:
            if (player){
                if (cardIdentification.Requirement > EnemyCurrentHealth){
                    break;
                }
                else{
                    return;
                }
            }
            else{
                if (cardIdentification.Requirement > PlayerCurrentHealth){
                    break;
                }
                else{
                    return;
                }
            }
            default:
            break;
        }
        switch (cardIdentification.Ability)
        {
            case 0:
            break;
            
            case 1:
            StartCoroutine(ChangeHealth(cardIdentification.Potency, !player));
            break;

            case 2:
            StartCoroutine(ChangeHealth(-1 * cardIdentification.Potency, player));
            break;

            case 3:
            if (player){
                foreach (Transform card in PlayerArea.transform){
                    if (card.GetComponent<Selected>().Select == false){
                        card.GetComponent<CardIdentification>().ChangeStrength(cardIdentification.Potency);
                    }
                }
            }
            else{
                foreach (Transform card in EnemyArea.transform){
                    if (card.GetComponent<Selected>().Select == false){
                        card.GetComponent<CardIdentification>().ChangeStrength(cardIdentification.Potency);
                    }
                }
            }
            break;

            case 4:
            if (player){
                foreach (Transform card in EnemyArea.transform){
                    card.GetComponent<CardIdentification>().ChangeStrength(-1 * cardIdentification.Potency);
                }
            }
            else {
                foreach (Transform card in PlayerArea.transform){
                    card.GetComponent<CardIdentification>().ChangeStrength(-1 * cardIdentification.Potency);
                }
            }
            break;

            case 5:
            if (player){
                EnemyCardDisabled = true;
            }
            else{
                PlayerCardDisabled = true;
            }
            break;

            case 7:
            if (player){
                PlayerStrengthMod += cardIdentification.Potency;
            }
            else{
                EnemyStrengthMod += cardIdentification.Potency;
            }
            break;

            case 8:
            if (player){
                foreach (Transform card in PlayerArea.transform){
                    card.GetComponent<CardIdentification>().ChangePotency(cardIdentification.Potency, 1);
                }
                break;
            }
            else{
                foreach (Transform card in EnemyArea.transform){
                    card.GetComponent<CardIdentification>().ChangePotency(cardIdentification.Potency, 1);
                }
                break;
            }



            default:
            break;
        }
    }
    public void EnemyPlayCard(int CardIndex){
        foreach (Transform card in DropZone.transform){
            Destroy(card.gameObject);
        }
        //must detach children as childcount is called within the same frame and destroy occurs next frame
        DropZone.transform.DetachChildren();
        //DropZone.BroadcastMessage("Clear", SendMessageOptions.DontRequireReceiver);
        AddCard(false, CardIndex);
        //remove gameobjects from enemy area
        Destroy(EnemyArea.transform.GetChild(CardIndex).gameObject);
    }
    public void EnemyPlayCard(int Index1, int Index2){
        foreach (Transform card in DropZone.transform){
            Destroy(card.gameObject);
        }
        //must detach children as childcount is called within the same frame and destroy occurs next frame
        DropZone.transform.DetachChildren();
        //DropZone.BroadcastMessage("Clear", SendMessageOptions.DontRequireReceiver);
        AddCard(false, Index1);
        AddCard(false, Index2);
        //remove gameobjects from enemy area
        GameObject delete1 = EnemyArea.transform.GetChild(Index1).gameObject;
        GameObject delete2 = EnemyArea.transform.GetChild(Index2).gameObject;
        Destroy(delete1);
        Destroy(delete2);
    }
    public void EnemyPlayCard(int Index1, int Index2, int Index3){
        foreach (Transform card in DropZone.transform){
            Destroy(card.gameObject);
        }
        //must detach children as childcount is called within the same frame and destroy occurs next frame
        DropZone.transform.DetachChildren();
        //DropZone.BroadcastMessage("Clear", SendMessageOptions.DontRequireReceiver);
        AddCard(false, Index1);
        AddCard(false, Index2);
        AddCard(false, Index3);
        //remove gameobjects from enemy area
        GameObject delete1 = EnemyArea.transform.GetChild(Index1).gameObject;
        GameObject delete2 = EnemyArea.transform.GetChild(Index2).gameObject;
        GameObject delete3 = EnemyArea.transform.GetChild(Index3).gameObject;
        Destroy(delete1);
        Destroy(delete2);
        Destroy(delete3);

    }
    public void EnemyPlayCard(int Index1, int Index2, int Index3, int Index4){
        foreach (Transform card in DropZone.transform){
            Destroy(card.gameObject);
        }
        //must detach children as childcount is called within the same frame and destroy occurs next frame
        DropZone.transform.DetachChildren();
        //DropZone.BroadcastMessage("Clear", SendMessageOptions.DontRequireReceiver);
        AddCard(false, Index1);
        AddCard(false, Index2);
        AddCard(false, Index3);
        AddCard(false, Index4);
        //remove gameobjects from enemy area
        GameObject delete1 = EnemyArea.transform.GetChild(Index1).gameObject;
        GameObject delete2 = EnemyArea.transform.GetChild(Index2).gameObject;
        GameObject delete3 = EnemyArea.transform.GetChild(Index3).gameObject;
        GameObject delete4 = EnemyArea.transform.GetChild(Index4).gameObject;
        Destroy(delete1);
        Destroy(delete2);   
        Destroy(delete3);
        Destroy(delete4);
    }
    public IEnumerator EndTurn(){
        SoundEffectSource.PlayOneShot(ButtonPressSound);
        PassButton.interactable = false;
        //put new cards on top
        foreach (Transform card in DropZone.transform){
            Destroy(card.gameObject);
        }
        //must detach children as childcount is called within the same frame and destroy occurs next frame
        DropZone.transform.DetachChildren();
        //DropZone.BroadcastMessage("Clear", SendMessageOptions.DontRequireReceiver);
        foreach (int card in SelectedCards){
            AddCard(true, card);
        }
        //remove gameobjects from playerarea
        PlayerArea.BroadcastMessage("SelectedClear", SendMessageOptions.DontRequireReceiver);
        //change health by cards played
        //StartCoroutine(ChangeHealth((int)Mathf.Pow(SelectedCards.Count, 2), false, true));
        if(SelectedCards.Count > 0){
            yield return new WaitForSeconds(1);
        }
        /*else{
            //if no cards played, refill hand
            StartCoroutine(RefillHand(true));
        }*/
        //remove cards from list
        SelectedCards.Clear();
        yield return new WaitForSeconds(1);
        //end round if no cards left
        if (PlayerArea.transform.childCount == 0){
            yield return new WaitForSeconds(1);
            EndRound(true);
        }
        else{
            StartCoroutine(EnemyTurn());
        }
    }
    
    public void CardSelected(int childIndex, int ID){
        SoundEffectSource.PlayOneShot(ButtonPressSound);
        SelectedCards.Add(childIndex);
        if (DropZone.transform.childCount != 0){
            foreach(Transform card in PlayerArea.transform){
                if (card.GetComponent<CardIdentification>().ID != ID){
                    card.GetComponent<Selected>().Selectable = false;
                }
            }
            if (DropZone.transform.childCount == SelectedCards.Count()){
                PassButton.interactable = true;
                PassText.text = "Play";
                foreach(Transform card in PlayerArea.transform){
                    if (!card.GetComponent<Selected>().Select){
                        card.GetComponent<Selected>().Selectable = false;
                    }
                }
            }
            else{
                PassButton.interactable = false;
            }
        }
        else{
            PassButton.interactable = true;
            PassText.text = "Play";
            foreach(Transform card in PlayerArea.transform){
                if (card.GetComponent<CardIdentification>().ID != ID){
                    card.GetComponent<Selected>().Selectable = false;
                }
            }
        }
    }
    public void CardUnselected(int childIndex, int ID){
        SoundEffectSource.PlayOneShot(ButtonPressSound);
        SelectedCards.Remove(childIndex);
        if (SelectedCards.Count == 0){
            PassButton.interactable = true;
            SetSelectableEmpty();
            PassText.text = "Pass";
        }
        else if (DropZone.transform.childCount > 0){
            PassButton.interactable = false;
        }
    }
    public IEnumerator RefillHand(bool Player){
        if (Player){
            while (PlayerArea.transform.childCount < LevelManager.DeckSize){
                DrawCard(true);
                yield return new WaitForSeconds(0.25f);
            }
        }
        else{
            while (EnemyArea.transform.childCount < LevelManager.DeckSize){
                DrawCard(false);
            }
        }
    }
    public void DrawCard(bool Player){
        if (Player){
            SoundEffectSource.PlayOneShot(CardDrawSound);
            if(PlayerDeckIdentifiers.Count > 0){
                GameObject Playercard = Instantiate(PlayerCard, new Vector2(0, 0), Quaternion.identity);
                Playercard.transform.SetParent(PlayerArea.transform, false);
                //Playercard.name = (PlayerDeckIdentifiers.Peek().ToString() + PlayerDeckElements.Peek().ToString());
                CardIdentification cardIdentification = Playercard.GetComponent<CardIdentification>();
                cardIdentification.ID = PlayerDeckIdentifiers.Peek();
                cardIdentification.PlayerSide = true;
                //cardIdentification.Element = PlayerDeckElements.Peek();
                cardIdentification.CardType = CardIdentification.CardTypes.BattleCard;
                PlayerCard.GetComponent<Selected>().CardType = Selected.CardTypes.BattleCard;
                //removingfrom deck
                PlayerDeckIdentifiers.Dequeue();
                //PlayerDeckElements.Dequeue();
            }
            else{
                Debug.Log("player deck empty!");
            }
        }
        else{
            if(EnemyDeckIdentifiers.Count > 0){
            GameObject Enemycard = Instantiate(PlayerCard, new Vector2(0, 0), Quaternion.identity);
            Enemycard.transform.SetParent(EnemyArea.transform, false);
            //Enemycard.name = (EnemyDeckIdentifiers.Peek().ToString() + EnemyDeckElements.Peek().ToString());
            CardIdentification cardIdentification = Enemycard.GetComponent<CardIdentification>();
            cardIdentification.ID = EnemyDeckIdentifiers.Peek();
            cardIdentification.PlayerSide = false;
            //cardIdentification.Element = EnemyDeckElements.Peek();
            cardIdentification.CardType = CardIdentification.CardTypes.BattleCard;
            Enemycard.GetComponent<Selected>().CardType = Selected.CardTypes.BattleCard;
            //removing from deck
            EnemyDeckIdentifiers.Dequeue();
            //EnemyDeckElements.Dequeue();
        }
        else{
            Debug.Log("enemy deck empty!");
        }
        }
    }
    
    public int FindLowestCard(GameObject parent){
        int Lowest = int.MaxValue;
        int lowestIndex = 0;
        foreach (Transform child in parent.transform){
            int CurrentStrength = child.GetComponent<CardIdentification>().GetStrength();
            if (Lowest > CurrentStrength){
                Lowest = CurrentStrength;
                lowestIndex = child.GetSiblingIndex();
            }
        }
        return lowestIndex;
    }
    public static void Shuffle(int[] deck)
    {
        for(int i = 0; i < deck.Length; i++)
        {
            int j = Random.Range(i, deck.Length);
            (deck[j], deck[i]) = (deck[i], deck[j]);
        }
    }
    
    public static void Shuffle(int[] deck1, int[] deck2)
    {
        for(int i = 0; i < deck1.Length; i++)
        {
            int j = Random.Range(i, deck1.Length);
            (deck1[i], deck1[j]) = (deck1[j], deck1[i]);
            (deck2[i], deck2[j]) = (deck2[j], deck2[i]);
        }
    }
    public void GameWin(){
        if (!GameFinished){
            LevelManager.BattleWin();
            GameFinished = true;
        }
    }
    public void GameLoss(){
        LevelManager.BattleLoss();
    }
    public IEnumerator ChangeHealth(int change, bool player){
        //todo: make green if positive change
        if (player){
            PlayerCurrentHealth -= change;
            if (PlayerCurrentHealth > PlayerMaxHealth){
                PlayerCurrentHealth = PlayerMaxHealth;
            }
            healthBarPlayer.SetHealth(PlayerCurrentHealth);
            PlayerHealthDisplay.text = $"{PlayerCurrentHealth}/{PlayerMaxHealth}";
            if (PlayerCurrentHealth <= 0){
                GameLoss();
            }
        }
        else{
            EnemyCurrentHealth -= change;
            if (EnemyCurrentHealth > EnemyMaxHealth){
                EnemyCurrentHealth = EnemyMaxHealth;
            }
            healthBarEnemy.SetHealth(EnemyCurrentHealth);
            EnemyHealthDisplay.text = $"{EnemyCurrentHealth}/{EnemyMaxHealth}";
            if (EnemyCurrentHealth <= 0){
                GameWin();
            }

        }
        if (player && change > 0){
            PlayerImage.color = Color.red;
            yield return new WaitForSeconds(1);
            PlayerImage.color = Color.white;
        }
        else if (!player && change > 0){ 
            EnemyImage.color = Color.red;
            yield return new WaitForSeconds(1);
            EnemyImage.color = Color.white;
        }
        //green
        else if (player && change < 0){
            PlayerImage.color = Color.green;
            yield return new WaitForSeconds(1);
            PlayerImage.color = Color.white;
        }
        else if (!player && change < 0){
            EnemyImage.color = Color.green;
            yield return new WaitForSeconds(1);
            EnemyImage.color = Color.white;
        }
    }

   
}
