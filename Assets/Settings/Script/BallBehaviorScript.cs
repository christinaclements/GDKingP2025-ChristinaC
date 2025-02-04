using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class BallBehaviorScript : MonoBehaviour {
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float minX = -8.01f;
    public float minY = -4.16f;
    public float maxX = 7.8f;
    public float maxY = 4.34f;
    public float minSpeed;
    public float maxSpeed;
    public Vector2 targetPosition;
    public GameObject target;
    public float minLaunchSpeed;
    public float maxLaunchSpeed;
    public float minTimeToLaunch;
    public float maxTimeToLaunch;
    public float cooldown;
    public bool launching;
    public float launchDuration;
    public float timeLastLaunch;
    public float timeSinceLastLaunch;
    public int secondsToMaxSpeed;
    public float timeLaunchStart;
    public Rigidbody2D body;
    public bool rerouting;


    void Start(){
        //secondsToMaxSpeed = 30;
        //minSpeed = .001f;
        //maxSpeed = 2.0f;
        targetPosition = getRandomPosition();
    }

    // Update is called once per frame
    void FixedUpdate(){
        if (onCoolDown() == false) {
            if (launching == true){
                float currentLaunchTime = Time.time - timeLaunchStart;
                if (currentLaunchTime > launchDuration){
                    startCooldown();
                }
            }
            else {
                launch();
            }
        }
        body = GetComponent<Rigidbody2D>();
        Vector2 currentPos = body.position;
        float distance = Vector2.Distance(currentPos, targetPosition);
        if (distance > 0.1){
            float difficulty = getDifficultyPercentage();
            float currentSpeed;
            if (launching == true){
                float launchingForHowLong = Time.time - timeLaunchStart;
                if (launchingForHowLong > launchDuration){
                    startCooldown();
                }
                currentSpeed = Mathf.Lerp(minLaunchSpeed, maxLaunchSpeed, difficulty);
            }
            else {
                currentSpeed = Mathf.Lerp(minSpeed, maxSpeed, difficulty);
            }
            currentSpeed = currentSpeed * Time.deltaTime;
            Vector2 newPosition = Vector2.MoveTowards(currentPos, targetPosition, currentSpeed);
            body.MovePosition(newPosition);
        }
        else { // You are at the target
            if (launching == true) {
                startCooldown();
            } 
            targetPosition = getRandomPosition();
        }
    }
    public Vector2 getRandomPosition() {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        Vector2 v = new Vector2(randomX, randomY);
        return v;
    }
    public float getDifficultyPercentage() {
        float difficulty = Mathf.Clamp01(Time.timeSinceLevelLoad / secondsToMaxSpeed);
        return difficulty;
    }
    public void launch() {
        Rigidbody2D targetBody = target.GetComponent<Rigidbody2D>();
        targetPosition = targetBody.position;
        if (launching == false) {
            timeLaunchStart = Time.time;
            launching = true;
        }
    }
    public bool onCoolDown() {
        bool result = false;
        float timeSinceLastLaunch = Time.time - timeLastLaunch;
        if (timeSinceLastLaunch < cooldown){
            result = true;
        }
        else {
            Debug.Log("result: " + result);
        }
        return result;
    }
    public void startCooldown(){
        timeLastLaunch = Time.time;
        launching = false;
    }
    private void OnCollisionEnter2D(Collision2D collision) {
        Debug.Log(this + " Collided with: " + collision.gameObject.name);
        if (collision.gameObject.tag == "Wall"){
            targetPosition = getRandomPosition();
        }
        if (collision.gameObject.tag == "Ball"){
            Reroute(collision);
        }
    }
    
    public void initialPosition(){
        body = GetComponent<Rigidbody2D>();
        body.position = getRandomPosition();
        targetPosition = getRandomPosition();
        launching = false;
        rerouting = true;
    }
     public void Reroute(Collision2D collision){
        GameObject otherBall = collision.gameObject;
        if (rerouting == true){
            otherBall.GetComponent<BallBehaviorScript>().rerouting = false;
            Rigidbody2D ballBody = otherBall.GetComponent<Rigidbody2D>();
            Vector2 contact = collision.GetContact(0).normal;
            targetPosition = Vector2.Reflect(targetPosition, contact).normalized;
            launching = false;
            float seperationDistance = 0.1f;
            ballBody.position += contact * seperationDistance;
        } else{
            rerouting = true;
        }
    }  
}
