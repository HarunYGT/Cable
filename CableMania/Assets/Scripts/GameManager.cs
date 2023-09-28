using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject ChoosedObject;
    GameObject ChoosedSocket;
    public bool isMove;

    [Header("Level Settings")]
    public GameObject[] HitControlObject;
    [SerializeField] private GameObject[] Plugs;
    public int TargetSocketNum;
    public List<bool> HitSituation;
    int CompleteNum;
    int HitControlNum;
    LastPlug lastPlug;
    [Header("UI Objects")]
    [SerializeField] private GameObject ControlPanel;
    [SerializeField] private TextMeshProUGUI checkText;

    void Start()  
    {
        for (int i = 0; i < TargetSocketNum-1; i++)
        {
            HitSituation.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, 100))
            {
                if (hit.collider != null)
                {
                    // ## Last Plug
                    if (ChoosedObject == null && !isMove)
                    {
                        if (hit.collider.CompareTag("BluePlug") || hit.collider.CompareTag("YellowPlug") || hit.collider.CompareTag("RedPlug"))
                        {
                            lastPlug = hit.collider.GetComponent<LastPlug>();
                            lastPlug.Move("ChoosePos", lastPlug.presentSocket, lastPlug.presentSocket.GetComponent<Socket>().MovementPos);
                            ChoosedObject = hit.collider.gameObject;
                            ChoosedSocket = lastPlug.presentSocket;
                            isMove = true;
                        }
                    }
                    // ## Last Plug

                    // ## Socket
                    if (hit.collider.CompareTag("Socket"))
                    {
                        if (ChoosedObject != null && !hit.collider.GetComponent<Socket>().Fullness && ChoosedSocket != hit.collider.gameObject)
                        {
                            ChoosedSocket.GetComponent<Socket>().Fullness = false;
                            Socket socket = hit.collider.GetComponent<Socket>();
                            lastPlug.Move("ChangePos", hit.collider.gameObject, socket.MovementPos);
                            socket.Fullness = true;

                            ChoosedObject = null;
                            ChoosedSocket = null;
                            isMove = true;
                        }
                        // ## Socket
                        else if (ChoosedObject == hit.collider.gameObject)
                        {
                            lastPlug.Move("ReturnSocket", hit.collider.gameObject);

                            ChoosedObject = null;
                            ChoosedSocket = null;
                            isMove = true;
                        }
                    }
                }
            }
        }
    }
    public void CheckHit(int hitIndex, bool stat)
    {
        HitSituation[hitIndex] = stat;
    }
    public void CheckPlug()
    {
        foreach (var item in Plugs)
        {
            if (item.GetComponent<LastPlug>().presentSocket.name == item.GetComponent<LastPlug>().socketColor)
            {
                CompleteNum++;
            }
        }
        if (CompleteNum == TargetSocketNum)
        {
            foreach (var item in HitControlObject)
            {
                item.SetActive(true);
            }
            StartCoroutine(CheckTheHit());
        }
        else
        {
            Debug.Log("Not Completed.");
        }
        CompleteNum = 0;
    }
    IEnumerator CheckTheHit()
    {
        ControlPanel.SetActive(true);
        checkText.text = "Being Checked.";
        yield return new WaitForSeconds(4f);

        foreach (var item in HitSituation)
        {
            if(item)
            {
                HitControlNum++;
            }
        }
        if(HitControlNum == HitSituation.Count)
        {
            checkText.text = "Completed!";
        }
        else 
        {
            foreach (var item in HitControlObject)
            {
                item.SetActive(false);
            }
            checkText.text = "Cables Collide!";
            Invoke("ClosePanel",2f);
        }
        HitControlNum =0;
    }
    void ClosePanel()
    {
        ControlPanel.SetActive(false);
    }
}
