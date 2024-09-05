using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipBattle : MonoBehaviour
{
    public BattleManager battleManager;
    public void OnClick(){
        battleManager.GameWin();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
