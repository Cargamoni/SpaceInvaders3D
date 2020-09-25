using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //deneme
    public GameObject enemyProjectile;
    private float nextAttack;
    // Start is called before the first frame update
    void Start()
    {
        nextAttack = Time.time + Random.Range(2, 5);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextAttack)
        {
            var origin = transform.position + Vector3.down;
            if(!Physics.Raycast(origin, Vector3.down, out RaycastHit hit) 
                || !hit.collider.CompareTag("Enemy"))
            {
                Instantiate(enemyProjectile,
                origin * 1.5f,
                enemyProjectile.transform.rotation);

                nextAttack = Time.time + Random.Range(2, 5);
            }
        }
    }
}
