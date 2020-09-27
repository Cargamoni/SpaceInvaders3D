using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    GameObject enemyProjectilePrefab;
    GameObject shooterAlien;
    private float nextAttack;
    private int fighterRow;
    int width, height;
    // Start is called before the first frame update
    void Start()
    {
        nextAttack = Time.time + Random.Range(2, 5);
        enemyProjectilePrefab = Resources.Load<GameObject>("Prefabs/AlienProjectile");
        width = LevelCreator.alienInstance.GetUpperBound(0);
        height = LevelCreator.alienInstance.GetUpperBound(1);
        fighterRow = 0;
    }

    bool isFightersDead()
    {
        for(int i = 0; i<= width; i++)
        {
            if (LevelCreator.alienInstance[i, fighterRow] != null)
            {
                Debug.Log(LevelCreator.alienInstance[i, fighterRow]);
                return false;
            }
        }
        fighterRow = fighterRow < height ? fighterRow + 1 : fighterRow;
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        //updateLastRow(lastRawAliens);
        if(Time.time > nextAttack)
        {
            int randomEnemy = Random.Range(0, width);
            shooterAlien = LevelCreator.alienInstance[randomEnemy, fighterRow];
            if (!isFightersDead() && shooterAlien != null)
            {
                var origin = shooterAlien.transform.position + Vector3.down;
                Instantiate(enemyProjectilePrefab,
                                origin * 0.5f,
                                enemyProjectilePrefab.transform.rotation);
                nextAttack = Time.time + Random.Range(2, 7);
                Debug.Log(width + ":" + height + ":" + isFightersDead() + ":" + fighterRow);
            }
        }

        //if (Time.time > nextAttack)
        //{
        //    var origin = transform.position + Vector3.down;
        //    if(!Physics.Raycast(origin, Vector3.down, out RaycastHit hit) 
        //        || !hit.collider.CompareTag("Enemy"))
        //    {
        //        Instantiate(enemyProjectile,
        //        origin * 1.5f,
        //        enemyProjectile.transform.rotation);

        //        nextAttack = Time.time + Random.Range(2, 5);
        //    }
        //}


    }
}
