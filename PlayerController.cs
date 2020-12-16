using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LadyController : MonoBehaviour
{
    bool isJump = true;
    bool isDead = false;
    bool isSuperJump = false;

    int idMove = 0;
    int superJump = 0;

    Animator anim;

    public AudioClip audioEarnCoint;
    private AudioSource MPEarnCoint;

    public AudioClip audioEarnItems;
    private AudioSource MPEarnItems;

    public AudioClip audioUseItems;
    private AudioSource MPUseItems;

    public AudioClip audioPeluruItems;
    private AudioSource MPPeluruItems;

    public Rigidbody2D rbd;

    // Use this for initialization
    private void Start()
    {
        anim = GetComponent<Animator>();
        Coins.scene = SceneManager.GetActiveScene().buildIndex;

        MPEarnCoint = gameObject.AddComponent<AudioSource>();
        MPEarnCoint.clip = audioEarnCoint;

        MPEarnItems = gameObject.AddComponent<AudioSource>();
        MPEarnItems.clip = audioEarnItems;

        MPUseItems = gameObject.AddComponent<AudioSource>();
        MPUseItems.clip = audioUseItems;

        MPPeluruItems = gameObject.AddComponent<AudioSource>();
        MPPeluruItems.clip = audioPeluruItems;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Jump "+isJump);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            MoveLeft();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            MoveRight();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            Idle();
        }
        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            Idle();
        }
        Move();
        Dead();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // Kondisi ketika menyentuh tanah
        if (isJump)
        {
            anim.ResetTrigger("jump");
            if (idMove == 0) anim.SetTrigger("idle");
            isJump = false;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Kondisi ketika menyentuh tanah
        anim.SetTrigger("jump");
        anim.ResetTrigger("run");
        anim.ResetTrigger("idle");
        isJump = true;
    }

    public void MoveRight()
    {
        idMove = 1;
    }

    public void MoveLeft()
    {
        idMove = 2;
    }

    private void Move()
    {
        if (idMove == 1 && !isDead)
        {
            // Kondisi ketika bergerak ke kekanan
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(1 * Time.deltaTime * 5f, 0, 0);
            transform.localScale = new Vector3(0.5f, 0.4f, 1f);
        }
        if (idMove == 2 && !isDead)
        {
            // Kondisi ketika bergerak ke kiri
            if (!isJump) anim.SetTrigger("run");
            transform.Translate(-1 * Time.deltaTime * 5f, 0, 0);
            transform.localScale = new Vector3(-0.5f, 0.4f, 1f);
        }
    }

    public void Jump()
    {
        if (!isJump)
        {
            // Kondisi ketika Loncat      
            if (isSuperJump == true)
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 400f);
                isSuperJump = false;
            }
            else
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 300f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag.Equals("Coin"))
        {
            Coins.score += 15;
            Destroy(collision.gameObject);
            MPEarnCoint.Play();
        }

        if (collision.transform.tag.Equals("SpecialItem"))
        {
            superJump = 1;
            Destroy(collision.gameObject);
            MPEarnItems.Play();
        }

        if (collision.transform.tag.Equals("Bullet"))
        {
            rbd.velocity = new Vector2(6f, rbd.velocity.y);
            MPPeluruItems.Play();
        }
    }

    public void Idle()
    {
        // kondisi ketika idle/diam
        if (!isJump)
        {
            anim.ResetTrigger("jump");
            anim.ResetTrigger("run");
            anim.SetTrigger("idle");
        }
        idMove = 0;
    }

    private void Dead()
    {
        if (!isDead)
        {
            if (transform.position.y < -10f)
            {
                // kondisi ketika jatuh
                isDead = true;
                SceneManager.LoadScene("GameOver");
            }
        }
    }

    public void useSpecialItem()
    {
        if (superJump > 0)
        {
            isSuperJump = true;
            superJump = 0;
            MPUseItems.Play();
        }
    }

}
