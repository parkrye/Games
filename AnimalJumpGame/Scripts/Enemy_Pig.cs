using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pig : Enemy_Base
{
    // ���� ��ȯ ������ Ȯ���ϴ� �Ÿ�
    public float recogDist = 0.5f;

    public override void Move()
    {
        rigidbody.velocity = new Vector2(move_Speed * direction, rigidbody.velocity.y);
    }

    // ���ǿ� �ش��ϸ� ������ �ٲ�
    public override void Turn()
    {
        // �ٴ��� �ִ��� Ȯ��
        Debug.DrawRay(rigidbody.position, Vector2.down, new Color(1f, 1f, 1f));
        rayHit = Physics2D.Raycast(rigidbody.position, Vector2.down, recogDist);
        if (rayHit)
        {
            if (direction == 0)
            {
                direction = 1;
                spriteRenderer.flipX = true;
            }

            if (direction == -1)
            {
                // ���� �ִ��� Ȯ��
                Debug.DrawRay(rigidbody.position, Vector2.left, new Color(0f, 1f, 0f));
                rayHit = Physics2D.Raycast(rigidbody.position, Vector2.left, recogDist, LayerMask.GetMask("Platform"));
                if (rayHit)
                {
                    ChangeDirection();
                }

                // ���ʿ� �ٴ��� �ִ��� Ȯ��
                if (direction == -1)
                {
                    Debug.DrawRay(new Vector2(rigidbody.position.x - 1f, rigidbody.position.y), Vector2.down, new Color(0f, 0f, 1f));
                    rayHit = Physics2D.Raycast(new Vector2(rigidbody.position.x - 1f, rigidbody.position.y), Vector2.down, recogDist, LayerMask.GetMask("Platform"));
                    if (!rayHit)
                    {
                        ChangeDirection();
                    }
                }
            }
            else
            {
                // ���� �ִ��� Ȯ��
                Debug.DrawRay(rigidbody.position, Vector2.right, new Color(0f, 1f, 0f));
                rayHit = Physics2D.Raycast(rigidbody.position, Vector2.right, recogDist, LayerMask.GetMask("Platform"));
                if (rayHit)
                {
                    ChangeDirection();
                }

                // ���ʿ� �ٴ��� �ִ��� Ȯ��
                if (direction == 1)
                {
                    Debug.DrawRay(new Vector2(rigidbody.position.x + 1f, rigidbody.position.y), Vector2.down, new Color(0f, 0f, 1f));
                    rayHit = Physics2D.Raycast(new Vector2(rigidbody.position.x + 1f, rigidbody.position.y), Vector2.down, recogDist, LayerMask.GetMask("Platform"));
                    if (!rayHit)
                    {
                        ChangeDirection();
                    }
                }
            }
        }
    }

    // �ٸ� �� �±׿� �浹�� ���� ��ȯ
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            ChangeDirection();
        }
    }
}
