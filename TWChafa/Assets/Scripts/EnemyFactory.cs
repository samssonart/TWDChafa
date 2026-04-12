using UnityEngine;

public enum Variations
{
   Normal,
   Fast
    
}

public class EnemyFactory : MonoBehaviour 
{
    public GameObject _normalenemyPrefab;
    public GameObject _fastenemyPrefab;

    public GameObject[] _waypoints;
    public GameObject _goal;

    public GameObject CreateEnemy(Variations variation, Vector3 position)
    {
        GameObject enemyObject;

        switch (variation)
        {
            case Variations.Fast:
                enemyObject = Instantiate(_fastenemyPrefab, position, Quaternion.identity);
                break;

            default:
                enemyObject = Instantiate(_normalenemyPrefab, position, Quaternion.identity);
                break;
        }

        Enemy enemy = enemyObject.GetComponent<Enemy>();

        if (enemy != null)
        {
            enemy.SetPath(_waypoints, _goal);
        }

        return enemyObject;




    }

}
