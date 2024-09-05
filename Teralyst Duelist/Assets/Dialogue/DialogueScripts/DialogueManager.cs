using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;
using UnityEngine.Animations;

public class DialogueManager : MonoBehaviour
{
    public int currentIndex;
    public TextMeshProUGUI speakerName, dialogue, navButtonText;
    public Image speakerSprite;
    [SerializeField] private Image background;
    private static DialogueManager instance;
    private Conversation currentConvo;
    private Coroutine typing;
    public CanvasGroup NameArea;
    [SerializeField] Sprite NullSprite;
    [SerializeField] private AudioSource MusicPlayer;
    private bool DialogueFinished;
    private void Awake(){
        if (instance == null){
            instance = this;
        }
        else{
            Destroy(gameObject);
        }
    }
    void Start(){
        DialogueFinished = false;
        currentIndex = 0;
        currentConvo = LevelManager.CurrentConvo;
        speakerName.text = "";
        dialogue.text = "";
        background.sprite = currentConvo.GetBackground();
        speakerSprite.sprite = instance.NullSprite;
        SwitchBackgroundMusic(currentConvo.GetMusic(), currentConvo.GetMusicEdited());
        instance.ReadNext();
    }
    /*
    public static void StartConversation(Conversation convo){
        instance.currentIndex = 0;
        instance.currentConvo = convo;
        instance.speakerName.text = "";
        instance.dialogue.text = "";
        instance.speakerSprite.sprite = instance.NullSprite;
        instance.ReadNext();
    }*/
    public void ReadNext(){
        if(typing == null){
            if(currentIndex > currentConvo.GetLength() - 1){
                if (!DialogueFinished){
                    DialogueFinished = true;
                    LevelManager.ConvoFinished();
                    return;
                }
            }
            speakerName.text = currentConvo.GetLineByIndex(currentIndex).speaker.GetName();
            if(currentConvo.GetLineByIndex(currentIndex).speaker.GetName() == ""){
                NameArea.alpha = 0;
            }
            else{
                NameArea.alpha = 1;
            }
            if (!currentConvo.GetLineByIndex(currentIndex).speaker.NoSprite()){
                speakerSprite.sprite = currentConvo.GetLineByIndex(currentIndex).speaker.GetSprite();
            }
            typing = instance.StartCoroutine(TypeText(currentConvo.GetLineByIndex(currentIndex).Dialogue));
            currentIndex++;
        }
        else{
            instance.StopCoroutine(typing);
            typing = null;
            dialogue.text = currentConvo.GetLineByIndex(currentIndex - 1).Dialogue;
        }
    }

    private IEnumerator TypeText(string text){
        dialogue.text = "";
        bool complete = false;
        int index = 0;
        while (!complete){
            dialogue.text += text[index];
            index++;
            yield return new WaitForSeconds(0.02f);
            if (index == text.Length){
                complete = true;
            }
        }
        typing = null;
    }
    public static void Skip(){
        if (!instance.DialogueFinished){
            instance.DialogueFinished = true;
            LevelManager.ConvoFinished();
            return;
        }
    }
    void Update(){
        if (Input.GetMouseButtonDown(0)){
            ReadNext();
        }
    }
    public void SwitchBackgroundMusic(AudioClip Music, AudioClip MusicEdited){
        MusicPlayer.clip = MusicEdited;
        MusicPlayer.PlayOneShot(Music);
        MusicPlayer.PlayScheduled(AudioSettings.dspTime + Music.length);
    }
}