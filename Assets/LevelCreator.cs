using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCreator : MonoBehaviour
{
    static GameObject[] alienPrefabs;
    public GameObject shieldPrefab;
    public GameObject turretPrefab;
    static GameObject[] test;
    public GameObject enemyProjectilePrefab;

    static public float level = 1;
    
    public static LevelCreator instance;

    List<GameObject> availableAliens = new List<GameObject>();
    
    static public GameObject[,] alienInstance;

    public Text curScore;
    public Text bestScore;
    static public bool bizDostuz;

    public Material[] alienMaterials;

    public int lives;
    public int width, height;
    public RawImage[] hearts;
    public float step;
    private float nextAttack;

    static public int score;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        nextAttack = 0;
        bizDostuz = true;
        bestScore.text = PlayerPrefs.GetInt("BestScore", 0).ToString();

        if (alienPrefabs == null)
        {
            alienPrefabs = Resources.LoadAll<GameObject>("Prefabs/Aliens");
        }

        float marginX = 1.5f;
        float marginY = 1.25f;
        Vector3 origin = new Vector3((float)(width * marginX) / 2f, 0f, 0f);
        alienInstance = new GameObject[width, height];
        for(int y = 0; y < height; y++)
        {
            GameObject prefab = alienPrefabs[Random.Range(0, alienPrefabs.Length)];
            Material material = alienMaterials[Random.Range(0, alienMaterials.Length)];
            prefab.GetComponent<MeshRenderer>().sharedMaterial = material;
            for (int x = 0; x < width; x++)
            {
                Vector3 offset = new Vector3(x * marginX, y * marginY, 0.0f);
                alienInstance[x, y] = Instantiate(prefab,
                    transform.position + offset - origin,
                    prefab.transform.rotation,
                    transform
                );
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
            Vector3 offset = new Vector3(x * 5, -2.5f, 0.0f);
            Instantiate(this.shieldPrefab,
               offset,
                this.shieldPrefab.transform.rotation);
        }

    }

    [ContextMenu("kill all")]
    void killThemAll()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void die() 
    {

        if(--lives == 0) {
            SceneManager.LoadScene("GameOver");
            return;
        }

        for (int i = 0; i < 3; i++) {
            hearts[i].enabled = i < lives; 
        }

        turretPrefab.SetActive(false);
        StartCoroutine(wait()); 

    }

    private IEnumerator wait()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1);
        Time.timeScale = 1;
        turretPrefab.SetActive(true);
    }

    void attack() {

        if (Time.time > nextAttack && !bizDostuz)
        {
            nextAttack = Time.time + Random.Range(1, 2);

            //uygun enemy seç
            availableAliens.Clear();
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    var alien = alienInstance[x, y];
                    if (alien != null) {
                        availableAliens.Add(alien);
                        break;
                    }
                }
            }

            var enemy = availableAliens[Random.Range(0, availableAliens.Count)];

            Instantiate(enemyProjectilePrefab,
                enemy.transform.position + Vector3.down,
                enemyProjectilePrefab.transform.rotation
            );

        }
    }


    // Update is called once per frame
    void Update()
    {
        int count = 0;
        foreach (Transform child in transform)
        {
            if ((child.position.x > HareketBerekettir.getmaxx && step > 0 ) || 
                (child.position.x < HareketBerekettir.getminx && step < 0))
            {
                transform.Translate(0.0f, -0.2f, 0.0f);
                step = -step;
                break;
            }

            if(child.position.y < 0.3f)
            {
                if (bizDostuz)
                    SceneManager.LoadScene("EasterEgg");
                else
                    SceneManager.LoadScene("GameOver");
            }
        }

        test = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject item in test)
        {
            count++;
        }

        if (count <= 0 && lives > 0)
        {
            level *= 2.0f;
            SceneManager.LoadScene("PlayGame");
        }

        else if (count <= 0 && lives == 0)
        {
            level = 1.0f;
            SceneManager.LoadScene("GameOver");
        }
            

        //Debug.Log(count);

        curScore.text = score.ToString();


        //if(Time.frameCount % 90 == 0)
            transform.Translate(step * Time.deltaTime * level, 0.0f, 0.0f);

        attack();

    }
}
