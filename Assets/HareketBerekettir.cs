using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HareketBerekettir : MonoBehaviour
{
    public float speed = 0;
    static public float getminx = 0, getmaxx = 0;
    public GameObject projectilePrefab;
    public float cooldown = 0;
    float defCooldown = 0;

    void Start()
    {
        float enter;

        var plane = new Plane(Camera.main.transform.forward * -1, this.transform.position);

        Ray rayLeftPlane = Camera.main.ViewportPointToRay(new Vector3(0.0f, 0.5f, 0.0f));
        Ray rayRightPlane = Camera.main.ViewportPointToRay(new Vector3(1.0f, 0.5f, 0.0f));

        defCooldown = cooldown;
        cooldown = 0;

        plane.Raycast(rayLeftPlane, out enter);
        getminx = rayLeftPlane.GetPoint(enter).x;

        plane.Raycast(rayRightPlane, out enter);
        getmaxx = rayRightPlane.GetPoint(enter).x;

    }
    
    void Update()
    {
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
  
        Vector3 direction = new Vector3(horizontalMovement, 0.0f, 0.0f);
        transform.position += direction.normalized * speed * Time.deltaTime;
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, getminx+1, getmaxx-1), transform.position.y, transform.position.z);
        
        if(cooldown > 0)
        {
            cooldown -= Time.deltaTime;
        }

        if(Input.GetButtonDown("Fire1") && cooldown <= 0)
        {
            Instantiate(this.projectilePrefab,
                this.transform.position + Vector3.up * 1.5f,
                this.projectilePrefab.transform.rotation);
            cooldown = defCooldown;
        }
    }

    private void FixedUpdate()
    {
        
    }

    private void Awake()
    {
        
    }
}

///Global
//public int denemeDegisken = 0;
//public GameObject yeniGameObject;
//public Transform yeniTransformObject;
// Start is called before the first frame update

///Start
// Camera.main bıdı bıdı, update içinde kullanılmaması tavisye edilir.

//Vector3 leftPlaneOrigin = Camera.main.ViewportToWorldPoint(new Vector3(0.0f, 0.5f, 0.0f));
//Vector3 rightPlaneOrigin = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, 0.5f, 0.0f));
//Debug.Log(plane.distance);
//plane.distance -= 5;

//Ray rayLeftPlane = Camera.main.ViewportPointToRay(new Vector3(0.0f, 0.5f, 0.0f)); //new Ray(leftPlaneOrigin, Camera.main.transform.forward);
//Ray rayRightPlane = Camera.main.ViewportPointToRay(new Vector3(1.0f, 0.5f, 0.0f)); //new Ray(leftPlaneOrigin, Camera.main.transform.forward);


//Debug.Log(yeniGameObject.name);
//Debug.Log(yeniGameObject.name);
//denemeDegisken += 7;

/// Update
//Debug.Log(Input.GetAxis("Horizontal"));
//transform.position += new Vector3(Input.GetAxis("Horizontal"), 0.0f, 0.0f) * Time.deltaTime * speed;
//Debug.Log(yeniTransformObject.name + " : "  + yeniTransformObject.position);

//float yukaridaNeVar = Input.GetAxis("Vertical");
//Vector3 direction = new Vector3(sagdaHayirVar, yukaridaNeVar, 0.0f).normalized;


//transform.position += Vector3.right * sagdaHayirVar * speed * Time.deltaTime;
//transform.position += Vector3.up * yukaridaNeVar * speed * Time.deltaTime;



//Vector3 direction = Vector3.zero;
//if(Input.GetKey(KeyCode.A))
//    direction.x--;

//if (Input.GetKey(KeyCode.D))
//    direction.x++;

//transform.position += direction * speed * Time.deltaTime;

//float horizontalMovement = Input.GetAxis("Horizontal");