using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card
{
    public int id;
    public string title;
    public string description;
    public Sprite asset;
    public Dictionary<string, int> stats = new Dictionary<string, int>();
    public Card(int id, string title, string description, Sprite asset, Dictionary<string, int> stats)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.asset = asset;
        this.stats = stats;
    }
    public Card(Card card)
    {
        this.id = card.id;
        this.title = card.title;
        this.description = card.description;
        this.asset = card.asset;
        this.stats = card.stats;
    }
}
