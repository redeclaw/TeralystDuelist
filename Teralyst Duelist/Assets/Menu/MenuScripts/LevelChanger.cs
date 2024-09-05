using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelChanger : MonoBehaviour
{
    private bool Paused;
    public Animator animator;
    private int LevelToLoad;
    public static LevelChanger instance;
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
    }
    /*
    void Update() {
        if (Input.GetMouseButtonDown(0)){
            FadeToLevel(1);
        }
    }
    */
    public static void FadeToLevel(int levelIndex){
        instance.LevelToLoad = levelIndex;
        instance.animator.SetTrigger("FadeOut");
    }
    public void OnFadeComplete(){
        SceneManager.LoadScene(LevelToLoad);
        instance.animator.SetTrigger("FadeIn");

    }
}
