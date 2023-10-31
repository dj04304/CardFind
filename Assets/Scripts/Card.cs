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

        // 먼저 firstCard가 존재하는지 여부부터 확인
        if(GameManager.I.firstCard == null)
        {
            //firstCard가 없으면 지금 연 카드가 첫번째 카드이다.
            GameManager.I.firstCard = gameObject;
        }else
        {
            // firstCard가 존재하니까 뒤집은 카드가 두번째 카드이다.
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
