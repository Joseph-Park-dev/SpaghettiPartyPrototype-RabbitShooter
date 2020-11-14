using System.Collections;
using UnityEngine;

public class MobSpawner : MonoBehaviour
{
    [SerializeField] GameObject _player;
    [SerializeField] GameObject _enemyChaser;
    [SerializeField] int _roundCount;

    private void Start()
    {
        Instantiate(_player, Vector2.zero, Quaternion.identity);
        StartCoroutine(TestMode());
    }

    private IEnumerator TestMode()
    {
        for (int currentRound = 1; currentRound < _roundCount; ++currentRound)
        {
            Debug.Log("Round : " + currentRound);
            SpawnObjectGroup(_enemyChaser, 3);
            yield return new WaitForSeconds(3.0f);
        }
    }

    private void SpawnObjectGroup(GameObject obj, int spawnCount)
    {
        for(int i = 0; i < spawnCount; ++i)
        {
            SpawnObject(obj);
        }
    }


    private void SpawnObject(GameObject obj) // Mob spawner for testing
    {
        Vector2 pos = new Vector2(
            Random.Range(-100f, 100f), 
            Random.Range(-100f,100f)
            );
        Instantiate(obj, pos, Quaternion.identity);
    }
}
