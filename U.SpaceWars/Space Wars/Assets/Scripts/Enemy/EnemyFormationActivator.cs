using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormationActivator : MonoBehaviour
{
    public static EnemyFormationActivator Instance;

    [SerializeField] GameObject[] enemyFormations;
    [SerializeField] int currentEnemyFormationIndex;
    int childCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        currentEnemyFormationIndex = GameManager.Instance.GetWave() - 1;
        currentEnemyFormationIndex = currentEnemyFormationIndex % enemyFormations.Length;
        enemyFormations[currentEnemyFormationIndex].SetActive(true);

        childCount = enemyFormations[currentEnemyFormationIndex].transform.childCount;

        GameManager.Instance.SetEnemyCount(childCount);
    }

    public int GetFormationCount()
    {
        return enemyFormations.Length;
    }
}
