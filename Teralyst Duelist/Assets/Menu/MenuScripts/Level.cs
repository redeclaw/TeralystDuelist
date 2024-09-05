using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Level", menuName = "Levels/New Level")]
public class Level : ScriptableObject
{
    public enum SceneType
    {
        Dialogue,
        PreBattle,
        Battle,
        TutorialBattle
    }
    [SerializeField] private SceneType[] Scenes;
    [SerializeField] private BattleInfo[] BattleInfos;
    [SerializeField] private Conversation[] Conversations;
    [SerializeField] private int NumScenes;
    public SceneType GetScene(int index){
        return Scenes[index];
    }
    public BattleInfo GetBattleInfo(int index){
        return BattleInfos[index];
    }
    public Conversation GetConversation(int index){
        return Conversations[index];
    }
    public int GetNumScenes(){
        return NumScenes;
    }
}
