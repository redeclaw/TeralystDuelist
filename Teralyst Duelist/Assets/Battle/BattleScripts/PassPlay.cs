using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassPlay : MonoBehaviour
{
    public BattleManager battleManager;
    public void OnClick(){
        battleManager.StartCoroutine(battleManager.EndTurn());
    }
    // Start is called before the first frame update
    void Start()
    {
        battleManager = GameObject.Find("BattleManager").GetComponent<BattleManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
