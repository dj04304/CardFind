using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class GameManager : MonoBehaviour
{
    // 시간 text
    public Text timeText;

    //GameObject
    public GameObject card;
    public GameObject firstCard;
    public GameObject secondCard;
    public GameObject endText;

    float time;

    public static GameManager I;

    void Awake()
    {
        I = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        int[] rtans = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6, 7, 7 };
        rtans = rtans.OrderBy(item => Random.Range(-1.0f, 1.0f)).ToArray();
        for (int i = 0; i < 16;  i++)
        {
            GameObject newCards = Instantiate(card);

            //newCard를 Cards안으로 옮기는 역할
            newCards.transform.parent = GameObject.Find("Cards").transform;

            float x = (i / 4) * 1.4f - 2.1f;
            float y = (i % 4) * 1.4f - 3.0f;

            //생성할 때 마다 x축으로 i * 1.4f만큼 카드가 배치
            newCards.transform.position = new Vector3(x, y, 0);

            Debug.Log(rtans[i]);

            string rtanName = "rtan" + rtans[i].ToString();
            newCards.transform.Find("Front").GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(rtanName);

            Time.timeScale = 1.0f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        timeText.text = time.ToString("N2");

        if(time > 30.0f)
        {
            Invoke("GameEnd", 1.0f);
        }
    }

    public void IsMatched()
    {

        //firstCard 와 SecondCard가 같은 지 판단.
        //Debug.Log("판단하자!!");

        string firstCardImg = firstCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImg = secondCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;

        // 불러온 이미지들이 같으면 삭제
        if(firstCardImg == secondCardImg)
        {
            Debug.Log(firstCardImg);
            Debug.Log(secondCardImg);

            //Debug.Log("매칭 성공!");
            //Card.cs를 불러오는 함수 GetComponent<Card>()
            firstCard.GetComponent<Card>().DestroyCard();
            secondCard.GetComponent<Card>().DestroyCard();

            // 카드의 남은 개수 확인
            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            if(cardsLeft == 2)
            {
                Invoke("GameEnd", 1.0f);
            }

        }else
        {
            //Debug.Log("덮자");
            firstCard.GetComponent<Card>().CloseCard();
            secondCard.GetComponent<Card>().CloseCard();
        }
        //한번 불러왔으면 비워주기
        firstCard = null;
        secondCard = null;
    }

    void GameEnd()
    {
        endText.SetActive(true);
        Time.timeScale = 0f;
    }
}
