using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardList : MonoBehaviour
{
    public List<Card> cards = new List<Card>();
    public Sprite[] cardFaces;
    public List<string> AbilityList = new List<string>();
    public List<string> CondList = new List<string>();
    public List<string> RoundAbilList = new List<string>();
    //public string[] AbilityList;
    // Start is called before the first frame update
    void Awake()
    {
       BuildCardDatabase();
       BuildAbilityDatabase();
       BuildConditionsDatabase();
       BuildRoundAbilityDatabase();
    }
    public Card GetCard(int id)
    {
        return cards.Find(card => card.id == id);
    }
    public Card GetCard(string title)
    {
        return cards.Find(card => card.title == title);
    }
    void BuildCardDatabase()
    {
        cards = new List<Card>()
        {
            new Card(0, "Peasant", "\"While most knew the kingdom of Almeria by their knights and wizards, peasants made up the vast majority of Almeria's army.\"", cardFaces[0],
            new Dictionary<string, int>
            {
                {"strength", 1},
                {"rarity", 1},
                {"ability", 1},
                {"potency", 2}
            }),
            new Card(1, "Farmer", "\"While they were not trained for combat, callused bodies and skilled use of weaponry made farmers capable fighters.\"", cardFaces[1],
            new Dictionary<string, int>
            {
                {"strength", 2},
                {"rarity", 1},
                {"ability", 2},
                {"potency", 3}
            }),
            new Card(2, "Rogue", "\"Rogues originally preyed on the common people, using their skills to threaten and steal. But when the kingdom of Almeria fell into peril, many of them utilized their skills for good.\"", cardFaces[2],
            new Dictionary<string, int>
            {
                {"strength", 3},
                {"rarity", 2},
                {"ability", 1},
                {"potency", 5}
            }),
            new Card(3, "Barbarian", "\"A fierce barbarian\"", cardFaces[3],
            new Dictionary<string, int>
            {
                {"strength", 4},
                {"rarity", 2},
                {"ability", 1},
                {"potency", 3}
            }),
            new Card(4, "Pirate", "\"A wild pirate\"", cardFaces[4],
            new Dictionary<string, int>
            {
                {"strength", 5},
                {"rarity", 3},
                {"ability", 1},
                {"potency", 4}
            }),
            new Card(5, "Archer", "\"A sharp-eyed archer\"", cardFaces[5],
            new Dictionary<string, int>
            {
                {"strength", 4},
                {"rarity", 4},
                {"ability", 4},
                {"potency", 1}
            }),
            new Card(6, "Boar", "\"Almerian hunters regularly ventures into the woods in search of meat, but even trained hunters could meet their end at the tusks of an Almerian boar.\"", cardFaces[6],
            new Dictionary<string, int>
            {
                {"strength", 8},
                {"rarity", 3},
                {"ability", 0}
            }),
            new Card(7, "Bear", "\"There are numerous treacheries Almerian civilians face, but a rule that even children know is to never get between a bear cub and its mother.\"", cardFaces[7],
            new Dictionary<string, int>
            {
                {"strength", 6},
                {"rarity", 3},
                {"ability", 1},
                {"potency", 3}
            }),
            new Card(8, "Giant", "\"A huge giant\"", cardFaces[8],
            new Dictionary<string, int>
            {
                {"strength", 9},
                {"rarity", 3},
                {"ability", 0},
            }),
            new Card(9, "Knight", "\"Not only did knights have expert training and top-quality armour, but their flame-enchanted swords ensured one hit would be lethal.\"", cardFaces[9],
            new Dictionary<string, int>
            {
                {"strength", 5},
                {"rarity", 4},
                {"ability", 1},
                {"potency", 7},
                {"condition", 1},
                {"requirement", 7}
            }),
            new Card(10, "Wizard", "\"The magic of the wizards of Almeria stems from a rare recessive gene carried within a very small portion of the population. \"", cardFaces[10],
            new Dictionary<string, int>
            {
                {"strength", 5},
                {"rarity", 4},
                {"ability", 1},
                {"potency", 10},
                {"condition", 2},
                {"requirement", 2}
            }),
            new Card(11, "Herbalist", "\"The vast majority of citizens in Almeria lived within Silvria, the capital city. The wilds outside the city were far too dangerous for most.\"", cardFaces[23],
            new Dictionary<string, int>
            {
                {"strength", 3},
                {"rarity", 2},
                {"ability", 2},
                {"potency", 5}
            }),
            new Card(12, "Wisp", "\"It's said that those who lived unfulfilling lives in Almeria were doomed to become wisps on death, pursuing the life they wished they had.", cardFaces[12],
            new Dictionary<string, int>
            {
                {"strength", 1},
                {"rarity", 1},
                {"ability", 0}
            }),
            new Card(13, "Bard", "\"An inspiring bard\"", cardFaces[13],
            new Dictionary<string, int>
            {
                {"strength", 3},
                {"rarity", 4},
                {"ability", 3},
                {"potency", 2}
            }),
            new Card(14, "Geowyn the Wise", "\"Geowyn pioneered the practice of enchanting soldier's equipment, leading the kingdom of Almeria to many victories.\"", cardFaces[14],
            new Dictionary<string, int>
            {
                {"strength", 1},
                {"rarity", 5},
                {"ability", 7},
                {"potency", 1}
            }),
            new Card(15, "Street Urchin", "\"Due to the war, many Almerian children were abandoned or orphaned, forcing them to beg on the street to get by.\"", cardFaces[15],
            new Dictionary<string, int>
            {
                {"strength", 2},
                {"rarity", 1},
                {"ability", 0}
            }),
            new Card(16, "Spy", "\"Despite their relatively primitive technology, the Almerians still made use of espionage to gain insight into their enemies' plans.\"", cardFaces[16],
            new Dictionary<string, int>
            {
                {"strength", 0},
                {"rarity", 4},
                {"ability", 1},
                {"potency", 10}
            }),
            new Card(17, "Mermaid", "\"The waters of the Almerian sea were treacherous for many reasons. Not only were there raging storms, but they also beguiling mermaids who would lead them off course.\"", cardFaces[17],
            new Dictionary<string, int>
            {
                {"strength", 4},
                {"rarity", 3},
                {"ability", 5}
            }),
            new Card(18, "Wolf", "\"The vast majority of citizens in Almeria lived within Silvria, the capital city. The wilds outside the city were far too dangerous for most.\"", cardFaces[18],
            new Dictionary<string, int>
            {
                {"strength", 3},
                {"rarity", 3},
                {"ability", 1},
                {"potency", 10},
                {"condition", 3},
                {"requirement", 20}
            }),
            new Card(19, "Cleric", "\"The vast majority of citizens in Almeria lived within Silvria, the capital city. The wilds outside the city were far too dangerous for most.\"", cardFaces[19],
            new Dictionary<string, int>
            {
                {"strength", 5},
                {"rarity", 4},
                {"ability", 2},
                {"potency", 7}
            }),
            new Card(20, "Druid", "\"The vast majority of citizens in Almeria lived within Silvria, the capital city. The wilds outside the city were far too dangerous for most.\"", cardFaces[20],
            new Dictionary<string, int>
            {
                {"strength", 3},
                {"rarity", 3},
                {"ability", 1},
                {"potency", 5}
            }),
            new Card(21, "Shaman", "\"The vast majority of citizens in Almeria lived within Silvria, the capital city. The wilds outside the city were far too dangerous for most.\"", cardFaces[21],
            new Dictionary<string, int>
            {
                {"strength", 3},
                {"rarity", 3},
                {"ability", 1},
                {"potency", 5}
            }),
            new Card(22, "Monk", "\"The vast majority of citizens in Almeria lived within Silvria, the capital city. The wilds outside the city were far too dangerous for most.\"", cardFaces[22],
            new Dictionary<string, int>
            {
                {"strength", 3},
                {"rarity", 3},
                {"ability", 1},
                {"potency", 4},
                {"roundability", 1},
                {"roundpotency", 3}
            }),
            new Card(23, "Radan the Raider", "\"The vast majority of citizens in Almeria lived within Silvria, the capital city. The wilds outside the city were far too dangerous for most.\"", cardFaces[23],
            new Dictionary<string, int>
            {
                {"strength", 3},
                {"rarity", 3},
                {"ability", 1},
                {"potency", 5}
            }),
            new Card(24, "Horseman", "\"While Almeria's military used magic to transport their troops around, bandits of the area were happy to make use of the extra horses.\"", cardFaces[24],
            new Dictionary<string, int>
            {
                {"strength", 12},
                {"rarity", 4},
                {"ability", 0}
            }),
            new Card(25, "Alator the Dragon", "\"In a time of great need, the kingdom of Almeria forged an alliance with Alator the dragon, recruiting his strength in exchange for a great reserve of treasure.\"", cardFaces[25],
            new Dictionary<string, int>
            {
                {"strength", 13},
                {"rarity", 5},
                {"ability", 0}
            }),
            new Card(26, "Vampire", "\n\"Vampires often disguised themselves within the nobility of Almeria, leading the poor and desperate to their estates to feed.\"", cardFaces[26],
            new Dictionary<string, int>
            {
                {"strength", 0},
                {"rarity", 5},
                {"ability", 5}
            }),
            new Card(27, "Flag Bearer", "\"Flag bearers played a key role in the Almerian army, boosting troop morale and coordinating rallies.\"", cardFaces[27],
            new Dictionary<string, int>
            {
                {"strength", 3},
                {"rarity", 3},
                {"ability", 8},
                {"potency", 2}
            }),
        };
    }
    //percent will be replaced by potency
    public void BuildAbilityDatabase()
    {
        AbilityList.Add("This card has no ability");
        AbilityList.Add("Your opponent takes $ damage");
        AbilityList.Add("You heal $ HP");
        AbilityList.Add("Cards in your hand gain $ strength");
        AbilityList.Add("Cards in your opponent's hand lose $ strength");
        AbilityList.Add("If your opponent plays a card on top of this, disable its ability.");
        AbilityList.Add("This card has the current card on the field's strength + $");
        AbilityList.Add("Cards in your deck gain $ Power");
        AbilityList.Add("Increase the damage dealing effects of cards in your hand by $");
        AbilityList.Add("Rush X:\nIf this is the first round, gain $ strength");
        //AbilityList = Abilities.ToArray();
    }
    
    public void BuildConditionsDatabase(){
        CondList.Add("");
        CondList.Add("If this card has $ or more strength, ");
        CondList.Add("If $ or more rounds have been played, ");
        CondList.Add("If your opponent has less than $ health, ");
    }

    public void BuildRoundAbilityDatabase(){
        RoundAbilList.Add("");
        RoundAbilList.Add("This card gains $ strength for each round that has been played\n");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
