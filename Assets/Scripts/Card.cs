using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenCard()
    {
        // Animator isOpen parameter = true
        anim.SetBool("isOpen", true);
        // Front setActive = true
        transform.Find("Front").gameObject.SetActive(true);
        // Back setActrive = false
        transform.Find("Back").gameObject.SetActive(false);

        // ���� firstCard�� �����ϴ��� ���κ��� Ȯ��
        if(GameManager.I.firstCard == null)
        {
            //firstCard�� ������ ���� �� ī�尡 ù��° ī���̴�.
            GameManager.I.firstCard = gameObject;
        }else
        {
            // firstCard�� �����ϴϱ� ������ ī�尡 �ι�° ī���̴�.
            GameManager.I.secondCard = gameObject;
            GameManager.I.IsMatched();
        }
    }

    public void DestroyCard()
    {
        Invoke("DestroyCardInvoke", 1.0f);
    }
    void DestroyCardInvoke()
    {
        Destroy(gameObject);
    }

        public void CloseCard()
    {
        Invoke("CloseCardInvoke", 1.0f);
    }

    void CloseCardInvoke()
    {
        anim.SetBool("isOpen", false);
        transform.Find("Back").gameObject.SetActive(true);
        transform.Find("Front").gameObject.SetActive(false);
    } 

}
