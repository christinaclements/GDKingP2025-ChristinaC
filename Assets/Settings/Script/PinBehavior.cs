using System.Collections;
using UnityEngine;

public class PinBehavior : MonoBehaviour{

    public float speed = 2.0f;
    public Vector2 newPosition;
    public Vector3 mousePosG;
    Camera cam;
    public Rigidbody2D body;
    public float dashSpeed = 5.0f;
    public float baseSpeed = 2.0f;
    public bool dashing;
    public float dashDuration;
    public float timeDashStart;
    //cooldown variable
    public static float cooldownRate = 5.0f;
    public static float cooldown;
    public float timeLastDashEnded;
    public float start;
    public AudioSource[] audioSources;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        cam = Camera.main;
        dashing = false;
        audioSources = GetComponents<AudioSource>();
    }
    private void Update(){
        Dash();
    }
    // Update is called once per frame
    void FixedUpdate(){
        body = GetComponent<Rigidbody2D>();
        Vector2 currentPosition = body.position;
        mousePosG = cam.ScreenToWorldPoint(Input.mousePosition);
        newPosition = Vector2.MoveTowards(body.position, mousePosG, speed * Time.fixedDeltaTime);
        body.MovePosition(newPosition);

    }
    private void OnCollisionEnter2D(Collision2D collision){
        string collided = collision.gameObject.tag;
        Debug.Log("Collided with " + collided);
        if ( collided == "Ball" || collided == "Wall"){
            UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
            StartCoroutine(WaitForSoundAndTransition());
        }
    }
    private IEnumerator WaitForSoundAndTransition() {
        audioSources[0].Play();
        yield return new WaitForSeconds(audioSources[0].clip.length);
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }
    private void Dash(){
        if (dashing == true){
            float currentTime = Time.time;
            
            float howLong = Time.time - timeDashStart;
            if (howLong > dashDuration) { 
                dashing = false; 
                speed = baseSpeed; 
                timeLastDashEnded = Time.time;
                cooldown = cooldownRate;
            }
        }
        else{
            cooldown = cooldown - Time.deltaTime;
            if (cooldown < 0.0) { 
                cooldown = 0.0f;
            }
            if (Input.GetMouseButtonDown(0) == true && cooldown == 0.0){
                dashing = true;
                speed = dashSpeed;
                timeDashStart = Time.time;
                if (audioSources[1].isPlaying) { 
                    audioSources[1].Stop();
                }
                audioSources[1].Play();
            }

        }
    }
}
