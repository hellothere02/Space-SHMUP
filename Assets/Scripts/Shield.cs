using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    [SerializeField] float rotationsPerSecond = 0.1f;

    private int levelShow = 0;
    private Material mat;

    private void Start()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        int currLevel = Mathf.FloorToInt(Hero.S.shieldLevel);
        if(levelShow != currLevel)
        {
            levelShow = currLevel;
            mat.mainTextureOffset = new Vector2(0.2f * levelShow, 0);
        }
        float rZ = -(rotationsPerSecond * Time.time * 360) % 360f;
        transform.rotation = Quaternion.Euler(0, 0, rZ);
    }
}
