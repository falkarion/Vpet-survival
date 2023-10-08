using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillAllEnemies : MonoBehaviour
{
    public bool Kill = false;

    // Update is called once per frame
    void Update()
    {
        if(Kill)
        {
            Enemy[] enemies = GameObject.FindObjectsOfType<Enemy>();
            foreach(Enemy enemy in enemies)
            {
                enemy.TakeDamage(9999);
            }

            Kill = false;
        }
    }
}
