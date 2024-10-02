using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]

public class UnitSpawnData
{
    [SerializeField]
    public EnemyUnitScriptableObject EnemyUnitSO;
    [SerializeField]
    [Range(0f, 1f)]
    public float SpawnWeight;
}

public class SpawnScript : MonoBehaviour
{
    [Header("Light")]
    [SerializeField]
    private Light[] _light;
    [SerializeField]
    [Range(1, 5)]
    private int _lightIndex;
    [Header("Movement")]
    [SerializeField]
    [Range(0f, 100f)]
    private float _distanceField = 15f;
    [SerializeField]
    [Range(0f, 10f)]
    private float _speed = 4f;
    [Header("Units Data")]
    [SerializeField]
    private GameObject _castomPrefab;
    [SerializeField]
    private bool _useCastomPrefab = false;
    [Space]
    [SerializeField]
    [Range(1, 3)]
    private int _unitNumber = 3;
    [Space]
    [SerializeField]
    public UnitSpawnData[] unitSpawnData;
    private GameObject _enemyUnit;
    void Awake() 
   {
        float totalWeight = 0f;

        foreach (UnitSpawnData data in unitSpawnData)
            totalWeight += data.SpawnWeight;

        EnemyUnitStats[] enemyUnitStats = GeneratRandomInstansesOfUnits(totalWeight);

        if (_useCastomPrefab && _castomPrefab == null) 
            _enemyUnit = Instantiate(_castomPrefab, transform.position, transform.rotation, transform);
        else
            _enemyUnit = Instantiate(enemyUnitStats[0].EnemyUnitPrefab, transform.position, transform.rotation, transform);

        EnemyScript enemyUnitScript = _enemyUnit.AddComponent<EnemyScript>();

        enemyUnitScript.EnemyUnits = enemyUnitStats;
        enemyUnitScript.DistanceField = _distanceField;
        enemyUnitScript.Speed = _speed;

        //Light 
        Instantiate(_light[_lightIndex - 1], transform.position, transform.rotation, transform);
    }
    private EnemyUnitStats[] GeneratRandomInstansesOfUnits(float totalWeight)
    {
        int enemyCount = Random.Range(1, _unitNumber + 1);
        EnemyUnitStats[] EnemyUnits = new EnemyUnitStats[enemyCount];

        int i = 0;
        foreach (EnemyUnitStats Unity in EnemyUnits)
        {
            float randomValue = Random.value * totalWeight;
            float cumulativeWeight = 0f;

            foreach (UnitSpawnData data in unitSpawnData)
            {
                cumulativeWeight += data.SpawnWeight;

                if (randomValue <= cumulativeWeight)
                {
                    EnemyUnits[i] = data.EnemyUnitSO.GenerateRandomStats();
                } 
                else continue; 
            }
            i++;
        }

        EnemyUnits = EnemyUnits
            .OrderByDescending(x => x.IsElite ? 1 : 0)
            .ThenByDescending(x => x.UnitLevel).ToArray();

        return EnemyUnits;
    }

}
