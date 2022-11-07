using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player_Controller : MonoBehaviour
{
    // �÷��̾� rigid, ��ġ, �̹���, �ִϸ�����
    private new Rigidbody2D rigidbody2D;
    private new Transform transform;
    private SpriteRenderer sprite;
    private Animator animator;

    // ����, ���� ��ġ
    public GameObject sword;
    private Transform swordTransform;

    // �÷��̾� �ִ� hp, ���� hp, ���� ������, ���� �ӵ�, ���� ����, �ǰ� ����, ���� hp��
    private float maxHP;
    private float nowHP;
    private float attackDamage;
    private float attackSpeed;
    private int nowAttack;
    private bool nowhit;
    public Image nowHPbar;

    // �÷��̾� �̵� �ӵ�, ���� �Ŀ�, ����, ���� ����
    private float moveSpeed;
    private float jumpPower;
    private bool isGround;
    private bool alive;

    // �÷��̾� ����, ����ġ, �ٷ�, ��ø, �ǰ�, ����Ʈ
    private int[] data;

    // ��ƼŬ
    public GameObject particle;

    // ȿ����
    public AudioClip audio_jump;
    public AudioClip audio_attack_1;
    public AudioClip audio_attack_2;
    public AudioClip audio_levelup;
    public AudioClip audio_hit;
    public AudioClip audio_die;
    AudioSource audioSource;

    // Start is called before the first frame update
    private void Start()
    {
        // �÷��̾� rigid, ��ġ, �̹���, �ִϸ����� �ʱ�ȭ
        rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        transform = gameObject.GetComponent<Transform>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        animator = gameObject.GetComponent<Animator>();

        // �÷��̾� ���� ��ġ, ũ�� �ʱ�ȭ
        swordTransform = sword.GetComponent<Transform>();
        swordTransform.localScale = new Vector3(0, 0, 1);

        // ȿ����
        audioSource = gameObject.GetComponent<AudioSource>();

        // �ʱⰪ ����
        nowHP = 0;
        if (PlayerPrefs.HasKey("level"))
        {
            data = new int[] { PlayerPrefs.GetInt("level"), PlayerPrefs.GetInt("exp"), PlayerPrefs.GetInt("str"), PlayerPrefs.GetInt("dex"), PlayerPrefs.GetInt("con"), PlayerPrefs.GetInt("point") };
            nowHP = PlayerPrefs.GetInt("hp");
        }
        else
        {
            data = new int[] { 1, 0, 1, 1, 1, 0 };
        }
        SaveData();
        SetPlayingData();

        // �ʱ� ���� ����
        nowAttack = 0;
        nowhit = false;
        isGround = false;
        alive = true;

        // 1/���ݼӵ� �� ���� ���� �غ� ���°� �� �� �ִ�
        Invoke("ReadyToAttack", 1 / attackSpeed);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // ���� hp�ٸ� ����
        nowHPbar.fillAmount = (float)nowHP / (float)maxHP;

        // ����ִٸ�
        if (alive)
        {
            // ����, �̵�, ���� ����
            PlayerAttack();
            PlayerMove();
            PlayerJump();
        }
    }

    // �÷��̾� �̵� �Է� ó��
    private void PlayerMove()
    { 
        // �ǰ� ���°� �ƴ� ��
        if (!nowhit)
        {
            // ������, ���� ȭ��ǥ�� �̵�
            if (Input.GetKey(KeyCode.RightArrow))
            {
                sprite.flipX = false;
                swordTransform.position = new Vector2(transform.position.x + 0.0f, transform.position.y - 0.0f);
                transform.Translate(new Vector3(moveSpeed * Time.deltaTime, 0f, 0f));
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                sprite.flipX = true;
                swordTransform.position = new Vector2(transform.position.x - 0.4f, transform.position.y - 0.0f);
                transform.Translate(new Vector3(-moveSpeed * Time.deltaTime, 0f, 0f));
            }
        }
    }

    // �÷��̾� ���� �Է� ó��
    private void PlayerJump()
    {
        // �ǰ� ���°� �ƴ� ��
        if (!nowhit)
        {
            // �� ���� ��ġ, ����, ����, Ư�� ����ũ�� ����
            RaycastHit2D raycast = Physics2D.Raycast(rigidbody2D.position, Vector3.down, 1, LayerMask.GetMask("Platform"));

            // �÷��̾ ������ ��
            if (rigidbody2D.velocity.y <= 0)
            {
                // ���� ���� ������Ʈ�� ���� ��
                if (raycast.collider != null)
                {
                    // �Ÿ��� 0.1���� �۾�����
                    if (raycast.distance < 0.1f)
                    {
                        // �÷��̾�� ���� �ִٰ� ����Ѵ�
                        isGround = true;
                    }
                }
            }

            // �÷��̾ ���� �ִٸ�
            if (isGround)
            {
                // �����̽� �ٷ� ����
                if (Input.GetKey(KeyCode.Space))
                {
                    isGround = false;
                    audioSource.clip = audio_jump;
                    audioSource.Play();
                    rigidbody2D.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
                }
            }
        }
    }

    // �÷��̾� �⺻ ���� �Է� ó��
    private void PlayerAttack()
    {
        // �ǰ� ���°� �ƴ� ��
        if (!nowhit)
        {
            // �÷��̾ ZŰ�� ������ ���� ���� �غ� ���¶��
            if (Input.GetKey(KeyCode.Z) && nowAttack == 0)
            {
                // ������ ������ Ű���, ���� �� ���°� �ǰ�, ���� �ִϸ��̼��� ���
                audioSource.clip = audio_attack_1;
                audioSource.Play();
                swordTransform.localScale = new Vector3(1, 1, 1);
                nowAttack++;
                animator.SetInteger("attack", nowAttack);
            }
        }
    }
    
    // ���� �غ� �Ϸ�
    private void ReadyToAttack()
    {
        // ���� ���� ���� ���¶��
        if(nowAttack == 2)
        {
            // ���� �غ� ���°� �ȴ�
            nowAttack = 0;
        }

        // 1/���ݼӵ� �� �� �ٽ� ���� �غ� ���°� �� �� �ִ�
        Invoke("ReadyToAttack", 1 / attackSpeed);
    }

    // ���� ���� ���� �Լ�, �ִϸ����Ϳ� ���� ȣ��
    public void AttackOver()
    {
        // ������ ������ �����, ���� ���� ���°� �ǰ�, ���� �ִϸ��̼��� ����
        swordTransform.localScale = new Vector3(0, 0, 1);
        nowAttack++;
        animator.SetInteger("attack", nowAttack);
    }

    // ���� �������� ��ȯ, ���ʹ� �ǰ� �� ȣ��
    public int GetAttacked()
    {
        return nowAttack;
    }

    // ���� ������ ��ȯ, ���ʹ� �ǰ� �� ȣ��
    public float GetAttackDamage()
    {
        return attackDamage;
    }

    // ���ʹ� �浹 �� ȣ��
    public void Damaged(float damage, bool direction)
    {
        if (alive)
        {
            // hit�� true�� ���ȿ��� �������� ���� ����
            if (!nowhit)
            {
                nowHP -= damage;
            }

            // hp�� 0 ���ϰ� �Ǹ� ���
            if (nowHP <= 0)
            {
                alive = false;
                animator.SetTrigger("die");
                audioSource.clip = audio_die;
                audioSource.Play();
                rigidbody2D.AddForce(Vector2.up * 2f, ForceMode2D.Impulse);
            }
            else
            {
                // �ǰ� �ִϸ��̼� ����
                nowhit = true;
                animator.SetBool("hit", nowhit);
                audioSource.clip = audio_hit;
                audioSource.Play();

                // �浹 �ݴ� �������� ����
                if (direction)
                {
                    rigidbody2D.AddForce(Vector2.left * 0.3f + Vector2.up * 0.8f, ForceMode2D.Impulse);
                }
                else
                {
                    rigidbody2D.AddForce(Vector2.right * 0.3f + Vector2.up * 0.8f, ForceMode2D.Impulse);
                }
            }
        }
    }

    // die �ִϸ��̼� ���� �� ȣ��
    public void die()
    {
        SceneManager.LoadScene("02_GameOverScene");
    }

    // �ǰ� ���� ���� �Լ�, �ִϸ����Ϳ� ���� ȣ��
    public void HitOver()
    {
        nowhit = false;
        animator.SetBool("hit", nowhit);
    }

    // ����ġ ȹ�� / ������
    public void AddExp(int exp)
    {
        data[1] += exp;
        if (data[1] >= data[0] * 100)
        {
            audioSource.clip = audio_levelup;
            audioSource.Play();
            Instantiate(particle, transform.position, Quaternion.identity);
            data[1] -= data[0] * 10;
            data[0] += 1;
            data[5] += 3;
            nowHP = maxHP;
        }
    }

    // ������ ����
    public void SaveData()
    {
        PlayerPrefs.SetInt("level", data[0]);
        PlayerPrefs.SetInt("exp", data[1]);
        PlayerPrefs.SetInt("str", data[2]);
        PlayerPrefs.SetInt("dex", data[3]);
        PlayerPrefs.SetInt("con", data[4]);
        PlayerPrefs.SetInt("point", data[5]);
        PlayerPrefs.SetFloat("hp", nowHP);
        if (!PlayerPrefs.HasKey("location"))
        {
            PlayerPrefs.SetInt("location", 0);
        }
        PlayerPrefs.SetString("map", SceneManager.GetActiveScene().name);
    }

    // �÷��� ������ ����
    private void SetPlayingData()
    {
        maxHP = data[4] * 10;
        attackDamage = data[2];
        jumpPower = 5f;
        moveSpeed = 5f;
        attackSpeed = (1 + (data[3] - 10f) / 50) * 2f;
        if (nowHP == 0)
        {
            nowHP = maxHP;
        }
    }

    // ü�� ȸ��
    public void Heal(float healpoint)
    {
        if(healpoint < 0)
        {
            nowHP = maxHP;
        }
        else
        {
            nowHP += healpoint;
            if (nowHP > maxHP)
            {
                nowHP = maxHP;
            }
        }
    }

    // �ɷ�ġ ����
    public void SetData(int[] _data)
    {
        data = _data;
    }
}