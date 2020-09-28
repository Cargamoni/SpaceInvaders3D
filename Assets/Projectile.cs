using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    float speed = 0;
    public int projectileDelete;
    public bool isPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, projectileDelete);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += Vector3.up * speed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        switch(collision.gameObject.tag)
        {
            case "Enemy":
                Destroy(collision.gameObject);
                Destroy(gameObject);
                LevelCreator.score += Mathf.RoundToInt(100.0f * LevelCreator.level);
                break;
            case "Shield":
                if (collision.gameObject.transform.localScale.magnitude <= 0.25f)
                    Destroy(collision.gameObject);
                else
                    collision.gameObject.transform.localScale *= 0.5f;
                Destroy(gameObject);
                break;
            case "Player":
                Destroy(gameObject);
                LevelCreator.instance.die();
                break;
            default:
                break;
        }
    }
}
