using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCreator : MonoBehaviour
{
    static GameObject[] alienPrefabs;
    public GameObject shieldPrefab;
    static GameObject[] test;

    public GameObject[,] alienInstance;

    public Text curScore;
    public Text bestScore;

    public Material[] alienMaterials;

    public int width, height, speed;

    static public int score;

    // Start is called before the first frame update
    void Start()
    {
        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        if (alienPrefabs == null)
        {
            alienPrefabs = Resources.LoadAll<GameObject>("Prefabs/Aliens");
        }


        Vector3 origin = new Vector3(width / 2, 0, 0);
        alienInstance = new GameObject[width, height];
        for(int y= 0; y < height; y++)
        {
            GameObject prefab = alienPrefabs[Random.Range(0, alienPrefabs.Length)];
            Material material = alienMaterials[Random.Range(0, alienMaterials.Length)];
            prefab.GetComponent<MeshRenderer>().sharedMaterial = material;
            for (int x = 0; x < width; x++)
            {
                Vector3 offset = new Vector3(x * 1.5f, y * 1.25f, 0.0f);
                alienInstance[x,y]= Instantiate(prefab,
                                            transform.position + offset - origin,
                                            prefab.transform.rotation,
                                            transform);
            }
        }

        //for (int y = -height; y <= height; y++)
        //{
        //    GameObject prefab = alienPrefabs[Random.Range(0, alienPrefabs.Length)];
        //    Material material = alienMaterials[Random.Range(0, alienMaterials.Length)];
        //    prefab.GetComponent<MeshRenderer>().sharedMaterial = material;
        //    for(int x = -width; x <= width; x++)
        //    {
        //        Vector3 offset = new Vector3(x * 1.5f, y * 1.25f, 0.0f);
        //        Instantiate(prefab, 
        //            this.transform.position + offset,
        //            prefab.transform.rotation, 
        //            this.transform);
        //    }
        //}

        for (int x = -2; x <= 2; x++)
        {
            Vector3 offset = new Vector3(x * 5, -height * 3.25f, 0.0f);
            Instantiate(this.shieldPrefab,
                this.transform.position + offset,
                this.shieldPrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach (Transform child in transform)
        {
            if ((child.position.x > HareketBerekettir.getmaxx && speed > 0 ) || 
                (child.position.x < HareketBerekettir.getminx && speed < 0))
            {
                transform.Translate(0.0f, -0.2f, 0.0f);
                speed = -speed;
                break;
            }

            if(child.position.y < 0.5f)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        test = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject item in test)
        {
            count++;
        }

        if(count <=0 )
            SceneManager.LoadScene("GameOver");

        Debug.Log(count);

        curScore.text = score.ToString();

        //if(speed > 0)
        //{
        //    foreach (Transform child in transform)
        //    {
        //        if (child.position.x > HareketBerekettir.getmaxx)
        //        {
        //            speed = -speed;
        //            break;
        //        }

        //    }
        //}
        //else
        //{
        //    foreach (Transform child in transform)
        //    {
        //        if (child.position.x < HareketBerekettir.getminx)
        //        {
        //            speed = -speed;
        //            break;
        //        }
        //    }
        //}

        transform.Translate(speed * Time.deltaTime, 0.0f, 0.0f);
    }
}
