using UnityEngine;

[CreateAssetMenu(fileName = "New Speaker", menuName = "Levels/Dialogue/New Speaker")]


public class Speaker : ScriptableObject
{
    [SerializeField] private string SpeakerName;
    [SerializeField] private Sprite SpeakerSprite;
    [SerializeField] private bool Nosprite;
    public string GetName(){
        return SpeakerName;
    }
    public Sprite GetSprite(){
        return SpeakerSprite;
    }
    public bool NoSprite(){
        return Nosprite;
    }
    
}
