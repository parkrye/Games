using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Animator animator;
    SpriteRenderer spriteRenderer;
    new Rigidbody2D rigidbody;
    RaycastHit2D rayHit;
    Vector3 inCameraPos;
    public AudioSource[] audios;

    public GameObject failCanvas;

    public int playerNum;       // ĳ���� �ѹ�
    public float move_Speed;    // �̵� �ӵ�
    public float jump_Power;    // ���� ����
    bool isGround;              // ���� ���� ����ִ���
    bool breakBox;              // ���� �ڽ��� �ν�����

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();

        isGround = false;
        breakBox = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!DataManager.Manager.GetMenu())
        {
            OpenMenu();
            CheckGround();
            PlayerMove();
            PlayerJump();
        }
    }

    // �÷��̾� �̵�
    void PlayerMove()
    {
        if(playerNum == 0)
        {
            if (Input.GetKey("a") && !Input.GetKey("d"))
            {
                spriteRenderer.flipX = false;
                animator.SetBool("Move", true);
                rigidbody.velocity = new Vector2(-move_Speed, rigidbody.velocity.y);
            }
            else if (Input.GetKey("d") && ! Input.GetKey("a"))
            {
                spriteRenderer.flipX = true;
                animator.SetBool("Move", true);
                rigidbody.velocity = new Vector2(move_Speed, rigidbody.velocity.y);
            }
            else
            {
                if (isGround)
                {
                    animator.SetBool("Move", false);
                }
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
        }
        else
        {
            if (Input.GetKey("left") && !Input.GetKey("right"))
            {
                spriteRenderer.flipX = false;
                animator.SetBool("Move", true);
                rigidbody.velocity = new Vector2(-move_Speed, rigidbody.velocity.y);
            }
            else if (Input.GetKey("right") && !Input.GetKey("left"))
            {
                spriteRenderer.flipX = true;
                animator.SetBool("Move", true);
                rigidbody.velocity = new Vector2(move_Speed, rigidbody.velocity.y);
            }
            else
            {
                if (isGround)
                {
                    animator.SetBool("Move", false);
                }
                rigidbody.velocity = new Vector2(0, rigidbody.velocity.y);
            }
        }

        // ī�޶� ������ ���� �� ����
        inCameraPos = Camera.main.WorldToViewportPoint(transform.position);
        if (inCameraPos.x < 0f) inCameraPos.x = 0f;
        if (inCameraPos.x > 1f) inCameraPos.x = 1f;
        transform.position = Camera.main.ViewportToWorldPoint(inCameraPos);
    }

    // �÷��̾� ����
    void PlayerJump()
    {
        if (isGround)
        {
            if (playerNum == 0)
            {
                if (Input.GetKeyDown(KeyCode.W))
                {
                    audios[0].Play();
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_Power);
                    animator.SetBool("Move", true);
                    isGround = false;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    audios[0].Play();
                    rigidbody.velocity = new Vector2(rigidbody.velocity.x, jump_Power);
                    animator.SetBool("Move", true);
                    isGround = false;
                }
            }
        }
    }

    // �÷��̾ ���� ����ִ��� Ȯ��
    void CheckGround()
    {
        if(rigidbody.velocity.y <= 0)
        {
            Debug.DrawRay(rigidbody.position, Vector2.down, new Color(0f, 1f, 0f));
            rayHit = Physics2D.Raycast(rigidbody.position, Vector2.down, 1, LayerMask.GetMask("Platform"));
            if (rayHit.collider != null)
            {
                if (rayHit.distance <= 1.5f)
                {
                    isGround = true;
                }
            }
        }
    }

    // �޴� ����
    void OpenMenu()
    {
        if (Input.GetKeyDown("escape"))
        {
            DataManager.Manager.OpenManu();
        }
    }

    // �÷��̾� �浹 �̺�Ʈ
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Respawn" || collision.gameObject.tag == "Trap")                // ������, ���� �±׿� �浹��
        {
            Die();                                                                                      // ĳ���� ���
        }
        else if (collision.gameObject.tag == "Box")                                                     // �ڽ� �±׿� �浹��
        {
            if (!breakBox)                                                                              // ���� �ڽ��� �μ��� ���� �ʰ�
            {
                if(rigidbody.velocity.y >= 0 && transform.position.y <= collision.transform.position.y) // ĳ���Ͱ� �����, ĳ���� ��ġ�� �ڽ� �Ʒ����
                {
                    breakBox = true;                                                                    // �ڽ� �ı��� ���¿� �����ϰ�
                    collision.gameObject.GetComponent<BoxManager>().BreakBox();                         // �ڽ��� �ı�. �ı� �ִϸ��̼� �� �ı� ���¿��� ���
                }
            }
        }
        else if (collision.gameObject.tag == "Fall")                                                    // �߶� �±׿� �浹��
        {
            if (rigidbody.velocity.y <= 0 && transform.position.y >= collision.transform.position.y)    // ĳ���Ͱ� �ϰ���, ĳ���� ��ġ�� �߶� �����
            {
                collision.gameObject.GetComponent<BoxManager>().SlipDown();                             // �߶� �±� ������Ʈ�� �߶���
            }
        }
        else if (collision.gameObject.tag == "Enemy")                                                   // �� �±׿� �浹��
        {
            if (rigidbody.velocity.y < 0 && transform.position.y > collision.transform.position.y)      // ĳ���Ͱ� �ϰ���, ĳ���� ��ġ�� �� �����
            {
                collision.transform.gameObject.GetComponent<Enemy_Base>().Die();                        // �� ĳ���� ���
            }
            else
            {
                Die();                                                                                  // �ƴ϶�� �÷��̾� ĳ���� ���
            }
        }
    }

    // �ڽ� �ı� ���¿��� ���. �ڽ� �ִϸ��̼��� ȣ��
    public void BreakBox()
    {
        breakBox = false;
    }

    // �÷��̾� ���
    public void Die()
    {
        audios[1].Play();
        Time.timeScale = 0f;
        Instantiate(failCanvas);
    }


}
