using UnityEngine;
public enum EnemyType
{
    Normal,
    Fast,
    Tank
}
public class EnemyFactory : MonoBehaviour
{
    public GameObject normalEnemyPrefab;
    public GameObject fastEnemyPrefab;
    public GameObject tankEnemyPrefab;

    public Enemy CreateEnemy(EnemyType type, Vector3 position)
    {
        GameObject prefab = GetPrefab(type);

        GameObject enemyGO = Instantiate(prefab, position, Quaternion.identity);
        Enemy enemy = enemyGO.GetComponent<Enemy>();

        return enemy;
    }

    GameObject GetPrefab(EnemyType type)
    {
        switch (type)
        {
            case EnemyType.Fast:
                return fastEnemyPrefab;
            case EnemyType.Tank:
                return tankEnemyPrefab;
            default:
                return normalEnemyPrefab;
        }
    }

}