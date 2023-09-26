using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    GameObject ChoosedObject;
    GameObject ChoosedSocket;
    public bool isMove;
    void Start()
    {

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
                        if (hit.collider.CompareTag("BluePlug") || hit.collider.CompareTag("YellowPlug"))
                        {
                            LastPlug lastPlug = hit.collider.GetComponent<LastPlug>();
                            lastPlug.ChoosePos(lastPlug.presentSocket.GetComponent<Socket>().MovementPos, lastPlug.presentSocket);
                            ChoosedObject = hit.collider.gameObject;
                            ChoosedSocket = lastPlug.presentSocket;
                            isMove = true;
                        }
                    }
                    // ## Last Plug

                    // ## Socket
                    if(hit.collider.CompareTag("Socket"))
                    {
                        if(ChoosedObject != null && !hit.collider.GetComponent<Socket>().Fullness && ChoosedSocket != hit.collider.gameObject)
                        {
                            ChoosedSocket.GetComponent<Socket>().Fullness = false;
                            Socket socket = hit.collider.GetComponent<Socket>();
                            ChoosedObject.GetComponent<LastPlug>().ChangePos(socket.MovementPos,hit.collider.gameObject);
                            socket.Fullness = true;
                            ChoosedObject = null;
                            ChoosedSocket = null;
                        }
                    }
                    
                    // ## Socket
                }
            }
        }
    }
}
