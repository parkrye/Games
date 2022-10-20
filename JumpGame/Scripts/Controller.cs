using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    // �÷��̾�
    public Component player;

    // �÷��̾� ������Ʈ
    private Transform player_Transform;
    private Rigidbody2D player_Rigidbody2D;
    private SpriteRenderer player_SpriteRenderer;
    private Animator animator;
    private AudioSource audioSource;

    // �̵� �ӵ� / ���� �Ŀ�
    private float moveSpeed;
    private float jumpPower;

    // ���� �̵� ����, ���� ���ִ��� ����
    private bool inputLeft = false;
    private bool inputRight = false;
    private int maxCount;
    private int count;

    // ���� ����
    public GameOver_Manager panel_GameOver;
    public GameObject underline;
    private AudioSource diedAudioSource;

    // �ʱ�ȭ
    private void Awake()
    {
        player_Transform = player.transform;
        player_Rigidbody2D = player.GetComponent<Rigidbody2D>();
        player_SpriteRenderer = player.GetComponent<SpriteRenderer>();
        animator = player.GetComponent<Animator>();
        audioSource = player.GetComponent<AudioSource>();

        diedAudioSource = panel_GameOver.GetComponent<AudioSource>();

        moveSpeed = 2;
        jumpPower = 300;
        inputLeft = false;
        inputRight = false;
        maxCount = 1;
        count = 1;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        // ���� ��ư�� ������ �ִ� ���¶��
        if (inputLeft)
        {
            // �����Ӹ��� �������� moveSpeed��ŭ �̵��Ѵ�
            player_Transform.position += Vector3.left * moveSpeed * Time.deltaTime;
        }

        // ������ ��ư�� ������ �ִ� ���¶��
        if (inputRight)
        {
            // �����Ӹ��� ���������� moveSpeed��ŭ �̵��Ѵ�
            player_Transform.position += Vector3.right * moveSpeed * Time.deltaTime;
        }

        // ���� �÷��̾��� ���� �̵��� ���� �� == ���� ������ ��
        if (player_Rigidbody2D.velocity.y == 0)
        {
            count = maxCount;
            AnimvationPlay();
        }

        if(player_Transform.position.y <= underline.transform.position.y)
        {
            diedAudioSource.Play();

            Score_Manager scoreText = FindObjectOfType<Score_Manager>();
            scoreText.SetHighScore(scoreText.GetScore());

            panel_GameOver.Show();
            gameObject.SetActive(false);
        }
    }

    // ���� ��ư�� ������ �� �̺�Ʈ
    public void LeftClickDown()
    {
        // �̹��� ��������, �ִϸ��̼� �۵�, ���� ��ư ���� Ȱ��ȭ
        inputLeft = true;
        player_SpriteRenderer.flipX = false;
        AnimvationPlay();
    }

    // ���� ��ư�� �������� �� �̺�Ʈ
    public void LeftClickUp()
    {
        // �ִϸ��̼� �۵�, ���� ��ư ���� ��Ȱ��ȭ
        inputLeft = false;
        AnimvationPlay();
    }

    // ������ ��ư�� ������ �� �̺�Ʈ
    public void RightClickDown()
    {
        // �̹��� ����������, �ִϸ��̼� �۵�, ������ ��ư ���� Ȱ��ȭ
        inputRight = true;
        player_SpriteRenderer.flipX = true;
        AnimvationPlay();
    }

    // ������ ��ư�� �������� �� �̺�Ʈ
    public void RightClickUp()
    {
        // �ִϸ��̼� �۵�, ������ ��ư ���� ��Ȱ��ȭ
        inputRight = false;
        AnimvationPlay();
    }

    // ����
    public void JumpClickDown()
    {
        // �� ���� �ִ� ������ ���� ���� ����
        if (count > 0 & player_Rigidbody2D.velocity.y == 0)
        {
            count--;
            // moveSpeed�� ������ �������� �б�
            player_Rigidbody2D.AddForce(new Vector2(0, jumpPower));
            audioSource.Play();
            AnimvationPlay();
        }
    }

    // �ִϸ��̼� ���� 0:��� 1:�̵�
    private void AnimvationPlay()
    {
        // ���� �̵�, ������ �̵�, ���� �� �ϳ��� �ش�Ǹ�
        if (inputLeft | inputRight | count < maxCount)
        {
            animator.SetFloat("moving", 1);
        }
        else
        {
            animator.SetFloat("moving", 0);
        }
    }
}
