using UnityEngine;

[CreateAssetMenu(fileName = "New Conversation", menuName = "Levels/Dialogue/New Conversation")]
public class Conversation : ScriptableObject
{
    [SerializeField] private Sprite Background;
    [SerializeField] private AudioClip Music;
    [SerializeField] private AudioClip MusicEdited;

    [SerializeField] private DialogueLine[] allLines;
    public DialogueLine GetLineByIndex(int index){
        return allLines[index];
    }
    public int GetLength(){
        return allLines.Length;
    }
    public Sprite GetBackground(){
        return Background;
    }
    public AudioClip GetMusic(){
        return Music;
    }
    public AudioClip GetMusicEdited(){
        return MusicEdited;
    }
}
