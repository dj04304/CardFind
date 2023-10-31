using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class GameManager : MonoBehaviour
{
    // �ð� text
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

            //newCard�� Cards������ �ű�� ����
            newCards.transform.parent = GameObject.Find("Cards").transform;

            float x = (i / 4) * 1.4f - 2.1f;
            float y = (i % 4) * 1.4f - 3.0f;

            //������ �� ���� x������ i * 1.4f��ŭ ī�尡 ��ġ
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

        //firstCard �� SecondCard�� ���� �� �Ǵ�.
        //Debug.Log("�Ǵ�����!!");

        string firstCardImg = firstCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;
        string secondCardImg = secondCard.transform.Find("Front").GetComponent<SpriteRenderer>().sprite.name;

        // �ҷ��� �̹������� ������ ����
        if(firstCardImg == secondCardImg)
        {
            Debug.Log(firstCardImg);
            Debug.Log(secondCardImg);

            //Debug.Log("��Ī ����!");
            //Card.cs�� �ҷ����� �Լ� GetComponent<Card>()
            firstCard.GetComponent<Card>().DestroyCard();
            secondCard.GetComponent<Card>().DestroyCard();

            // ī���� ���� ���� Ȯ��
            int cardsLeft = GameObject.Find("Cards").transform.childCount;
            if(cardsLeft == 2)
            {
                Invoke("GameEnd", 1.0f);
            }

        }else
        {
            //Debug.Log("����");
            firstCard.GetComponent<Card>().CloseCard();
            secondCard.GetComponent<Card>().CloseCard();
        }
        //�ѹ� �ҷ������� ����ֱ�
        firstCard = null;
        secondCard = null;
    }

    void GameEnd()
    {
        endText.SetActive(true);
        Time.timeScale = 0f;
    }
}
