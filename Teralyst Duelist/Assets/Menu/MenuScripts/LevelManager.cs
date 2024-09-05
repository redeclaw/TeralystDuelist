using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int DeckSize = 8;
    public static int CardNumber = 19;
    public static int[] PlayerDeck;
    public static int[] EnemyDeck;
    public static bool[] Collection;
    public static Conversation CurrentConvo;
    private static LevelManager instance;
    public static int PlayerDamage;
    public static int PlayerHealth;
    public static int EnemyDamage;
    public static int EnemyHealth;
    public static bool TutorialBattle;
    public Level[] Levels;
    public int SceneIndex;
    public int DialogueIndex;
    public int BattleIndex;
    public Level CurrentLevel;
    private static bool Paused;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null){
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else{
            Destroy(gameObject);
            return;
        }


        Collection = new bool[CardNumber];
        for (int i = 0; i < CardNumber; i++){
            Collection[i] = false;
        }
        for (int i = 0; i < CardNumber; i++){
            Collection[i] = true;
        }
        PlayerDeck = new int[8] {0, 1, 2, 3, 4, 5, 6, 27};
        PlayerDamage = 2;
        PlayerHealth = 50;
    }

    public static void NewGame(){
        /*
        Collection = new bool[CardNumber];
        for (int i = 0; i < CardNumber; i++){
            Collection[i] = false;
        }
        for (int i = 0; i < 13; i++){
            Collection[i] = true;
        }
        PlayerDeck = new int[7] {14, 2, 11, 10, 15, 23, 20};
        */
        instance.StartLevel(0);        
    }
    public static void ConvoFinished(){
        instance.ContinueLevel();
    }
    public void StartLevel(int index){
        CurrentLevel = Levels[index];
        SceneIndex = 0;
        DialogueIndex = 0;
        BattleIndex = 0;
        ContinueLevel();
    }

    public void ContinueLevel(){
        if (SceneIndex > CurrentLevel.GetNumScenes() - 1){
            //end level
            Debug.Log("Out of levels!");
            return;
        }
        switch(CurrentLevel.GetScene(SceneIndex)){
            case Level.SceneType.Dialogue:
            StartConvo(CurrentLevel.GetConversation(DialogueIndex));
            DialogueIndex++;
            break;
            
            case Level.SceneType.PreBattle:
            //add prelevel
            break;

            case Level.SceneType.Battle:
            StartBattle(CurrentLevel.GetBattleInfo(BattleIndex));
            BattleIndex++;
            break;

            case Level.SceneType.TutorialBattle:
            StartTutorialBattle(CurrentLevel.GetBattleInfo(BattleIndex));
            BattleIndex++;
            break;

            default:
            break;
        }
        SceneIndex++;
    }

    public void StartConvo(Conversation convo){
        CurrentConvo = convo;
        LevelChanger.FadeToLevel(2);
        //DialogueManager.StartConversation(convo);
    }
    public void StartBattle(BattleInfo battleInfo){
        EnemyDeck = battleInfo.GetEnemyDeck();
        EnemyDamage = battleInfo.GetEnemyDamage();
        EnemyHealth = battleInfo.GetEnemyHealth();
        TutorialBattle = false;
        LevelChanger.FadeToLevel(1);
    }
    public void StartTutorialBattle(BattleInfo battleInfo){
        EnemyDeck = battleInfo.GetEnemyDeck();
        EnemyDamage = battleInfo.GetEnemyDamage();
        EnemyHealth = battleInfo.GetEnemyHealth();
        TutorialBattle = true;
        LevelChanger.FadeToLevel(1);
    }
    public static void BattleWin(){
        instance.ContinueLevel();
    }
    public static void BattleLoss(){

    }
    public static void Continue(){
        LevelChanger.FadeToLevel(4);
    }
    public static void Options(){
        LevelChanger.FadeToLevel(3);
    }
    public static void ExitCollection(){
        LevelChanger.FadeToLevel(0);
    }
    public static void Pause(){
        if (LevelManager.Paused){
            Time.timeScale = 1;
            LevelManager.Paused = false;
        }
        else{
            Time.timeScale = 0;
            LevelManager.Paused = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }
        if (Input.GetKeyDown(KeyCode.P)){
            LevelManager.Pause();
        }
    }
}
