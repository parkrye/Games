using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxManager : MonoBehaviour
{
    public AudioSource audio;

    // �ڽ� �ı� �ִϸ��̼� ���� �̺�Ʈ
    public void BreakBox()
    {
        audio.Play();
        GetComponent<Animator>().SetTrigger("Hit");
    }

    // �ڽ� �ı� �ִϸ��̼� ���� �̺�Ʈ
    void AnimationEnd()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        for (int i = 0; i < players.Length; i++)
        {
            players[i].GetComponent<PlayerManager>().BreakBox();
        }
        Destroy(gameObject);
    }

    // ���� �±׿� �浹�� �ı�
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Trap")
        {
            Destroy(gameObject);
        }
    }

    // �߶� �̺�Ʈ
    public void SlipDown()
    {
        StartCoroutine(SandFlash());
    }

    IEnumerator SandFlash()
    {
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.bodyType = RigidbodyType2D.Dynamic;
        yield return new WaitForSeconds(1.5f);
        Destroy(gameObject);
    }
}
