using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    static public Main S;
    static protected Dictionary<WeaponType, WeaponDefinition> WEAP_DICT;

    [SerializeField] private GameObject[] prefabEnemies;
    [SerializeField] private float enemySpawnPerSecond = 0.5f;
    [SerializeField] private float enemyDefaultPadding = 1.5f;

    public WeaponDefinition[] weaponDefinitions;

    private BoundsCheck bndCheck;

    private void Awake()
    {
        S = this;
        bndCheck = GetComponent<BoundsCheck>();
        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);

        WEAP_DICT = new Dictionary<WeaponType, WeaponDefinition>();
        foreach (WeaponDefinition def in weaponDefinitions)
        {
            WEAP_DICT[def.type] = def;
        }
    }

    public void SpawnEnemy()
    {
        int ndx = Random.Range(0, prefabEnemies.Length);
        GameObject go = Instantiate<GameObject>(prefabEnemies[ndx]);

        float enemyPaddding = enemyDefaultPadding;
        if(go.GetComponent<BoundsCheck>() != null)
        {
            enemyPaddding = Mathf.Abs(go.GetComponent<BoundsCheck>().radius);
        }

        Vector3 pos = Vector3.zero;
        float xMin = -bndCheck.camWidth + enemyPaddding;
        float xMax = bndCheck.camWidth - enemyPaddding;
        pos.x = Random.Range(xMin, xMax);
        pos.y = bndCheck.camHeight + enemyPaddding;
        go.transform.position = pos;

        Invoke("SpawnEnemy", 1f / enemySpawnPerSecond);
    }

    public void DelayedRestart (float delay)
    {
        Invoke("Restart", delay);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>
    /// статическа€ фунци€, возвращающа€ WD из пол€ WEAP_DICT
    /// </summary>
    /// <returns>
    /// экземл€р WD или, если нет такого определени€ дл€ указанного WT, возращает новый экземпл€р с типом none.
    /// </returns>
    /// <param name="wt">тип оружи€ WT дл€ которого требуетс€ получить WD
    /// </param>
    static public WeaponDefinition GetWeaponDefinition(WeaponType wt)
    {
        if(WEAP_DICT.ContainsKey(wt))
        {
            return (WEAP_DICT[wt]);
        }
        return (new WeaponDefinition());
    }
}
