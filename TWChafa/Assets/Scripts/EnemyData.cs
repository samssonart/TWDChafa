using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemyData", menuName = "TowerDefense/Enemy Data")]
public class EnemyData : ScriptableObject
{
    public float speed = 2f;
    public int health = 10;
    public int reward = 5;
}