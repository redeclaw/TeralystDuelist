using UnityEngine;

[CreateAssetMenu(fileName = "New BattleInfo", menuName = "Levels/New BattleInfo")]
public class BattleInfo : ScriptableObject
{
    [SerializeField] private string EnemyName;
    [SerializeField] private int[] EnemyDeck;
    [SerializeField] private int EnemyDamage;
    [SerializeField] private int EnemyHealth;
    public int[] GetEnemyDeck(){
        return EnemyDeck;
    }
    public int GetEnemyDamage(){
        return EnemyDamage;
    }
    public int GetEnemyHealth(){
        return EnemyHealth;
    }
}