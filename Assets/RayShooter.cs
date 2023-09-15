using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayShooter : MonoBehaviour

{
    Camera mycam;
    public int shootNumber = 30;
    private AudioSource source;
    private float volLowRange = .5f;
    private float volHighRange = 1.0f;
    public AudioClip shootSound;
    public AudioClip loadSound;
    public AudioClip emptyBullet;
    [SerializeField]private int health = 100;

    // Start is called before the first frame update
    void Start()
    {
        //get my mainCamera
        mycam = GetComponent<Camera>();
        //get the audio source controller
        source = GetComponent<AudioSource>();
        //hide the mouse pointer
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        if(shootNumber==0 || Input.GetMouseButtonDown(1)) {
            shootNumber = 30;
            source.PlayOneShot(loadSound, 1);
        }

            if (Input.GetButtonDown("Fire1")) {//if left button was clicked
            //decrease the shootNumber
            shootNumber--;
            health -= 5;
            //get the screen center
            Vector3 at = new Vector3(mycam.pixelWidth / 2, mycam.pixelHeight / 2, 0);
            //a ray cast from the cam to the screen center
            Ray ray = mycam.ScreenPointToRay(at);
            //play shoot sound (randomm volume)
            float vol = Random.Range(volLowRange, volHighRange);
            source.PlayOneShot(shootSound, vol);
            source.PlayOneShot(emptyBullet);

            //if there is hitted objects then put info about this objet in hit (passed by reference)
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit)) {
               GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                   StartCoroutine( SphereIndicator( hit.point));
                }
               
            }
        }
        
    }
    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        sphere.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
        yield return new WaitForSeconds(1);
        Destroy(sphere);
    }
    void OnGUI()
    {
        int size = 30;
        float posX = mycam.pixelWidth / 2 - size / 4;
        float posY = mycam.pixelHeight / 2 - size / 2;
        GUIStyle style = new GUIStyle();
        style.fontSize=30;
        style.fontStyle = FontStyle.Bold;
        
        GUI.Label(new Rect(posX, posY, size, size), "X",style);
        GUI.Label(new Rect(100, 100, size, size), " "+health+"%", style);


    }
}
