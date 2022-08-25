using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hero : MonoBehaviour
{
    public GameObject fireball;
    Rigidbody2D rigidbody;
    Animator animator;
    float jump_speed = 7.0f;
    float jumptime = 0.0f;
    int jumpn = 0;
    string face = "right";
    string dir = "";
    float speed = 2.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        GameObject o = GameObject.Find("hero_view");
        animator = o.GetComponent<Animator>();
        animator.SetBool("moving",false);
    }

    void FixedUpdate(){

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W)){
            check_jumpn();

            jump();
        }
        
        if(Input.GetKeyDown(KeyCode.A)){
            face = "left";
            check_jumpn();
            move_left();
        }else if(Input.GetKeyDown(KeyCode.D)){
            face = "right";
            check_jumpn();
            move_right();
        }else if(Input.GetKeyDown(KeyCode.Space)){
            fire();
        }else{
            if(dir != "" && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D)){
                stop_move();
            }
        }
    }

    void check_jumpn(){
        if(rigidbody.velocity.y < 0.0001f){
            jumpn = 0;
        }
        if(rigidbody.velocity.x < 0.0001f){
            dir = "";
        }
    }

    void jump()
    {
        if(jumpn >= 2){
            return;
        }
        jumpn++;
        rigidbody.velocity = new Vector2(rigidbody.velocity.x,rigidbody.velocity.y + jump_speed);
    }

    void move_left(){
        if(dir=="left"){
            return;
        }
        dir = "left";
        transform.localScale = new Vector2(-1,1);
        rigidbody.velocity = new Vector2(-speed,rigidbody.velocity.y);
        animator.SetBool("moving",true);
    }

    void move_right(){
        if(dir=="right"){
            return;
        }
        dir = "right";
        transform.localScale = new Vector2(1,1);
        rigidbody.velocity = new Vector2(speed,rigidbody.velocity.y);
        animator.SetBool("moving",true);
    }

    void stop_move(){
        dir = "";
        rigidbody.velocity = new Vector2(0,rigidbody.velocity.y);
        animator.SetBool("moving",false);
    }

    void fire(){
        GameObject o = Instantiate(fireball,
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0),
            new Quaternion(0, 0, 0, 0));
        Rigidbody2D r = o.GetComponent<Rigidbody2D>();
        if(face == "left"){
            r.velocity = new Vector2(-speed * 4,rigidbody.velocity.y);
        }else{
            r.velocity = new Vector2(speed*4,rigidbody.velocity.y);
        }
        Destroy(o,4.0f);
    }
}
