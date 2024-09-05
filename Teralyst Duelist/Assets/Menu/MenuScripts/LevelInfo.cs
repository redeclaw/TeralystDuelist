using UnityEngine;

[CreateAssetMenu(fileName = "New LevelInfo", menuName = "Levels/New LevelInfo")]
public class LevelInfo : ScriptableObject
{
    [SerializeField] private string LevelName;
    [SerializeField] private int[] PlaceRewards;
    [SerializeField] private int NumBattles;
    [SerializeField] private int EntryFee;
    public string GetLevelName(){
        return LevelName;
    }
    public int[] GetPlaceRewards(){
        return PlaceRewards;
    }
    public int GetNumBattles(){
        return NumBattles;
    }
    public int GetEntryFee(){
        return EntryFee;
    }
}