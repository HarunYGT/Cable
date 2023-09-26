using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastPlug : MonoBehaviour
{
    public GameObject presentSocket; 
    [SerializeField] private string socketColor;
    [SerializeField] private GameManager gameManager; 

    bool PosChange,Choosed,SocketSit;
    GameObject MovementPos;
    GameObject SocketHimself;

    public void ChoosePos(GameObject ObjectToGo,GameObject Socket)
    {
        MovementPos = ObjectToGo;
        Choosed = true;
    }
    public void ChangePos(GameObject ObjectToGo,GameObject Socket)
    {
        SocketHimself = Socket;
        MovementPos = ObjectToGo;
        PosChange = true;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Choosed)
        {
            transform.position = Vector3.Lerp(transform.position,MovementPos.transform.position,.040f);
            if(Vector3.Distance(transform.position,MovementPos.transform.position) < 0.10f)
            {
                Choosed = false; 
            }
        }
        if(PosChange)
        {
            transform.position = Vector3.Lerp(transform.position,MovementPos.transform.position,.040f);
            if(Vector3.Distance(transform.position,MovementPos.transform.position) < 0.10f)
            {
                PosChange = false;
                SocketSit = true;
            }
        }
        if(SocketSit)
        {
            transform.position = Vector3.Lerp(transform.position, SocketHimself.transform.position,.040f);
            if(Vector3.Distance(transform.position,MovementPos.transform.position) < 0.10f)
            {
                SocketSit = false;
                gameManager.isMove = false;
                presentSocket = SocketHimself;
                //Will do.
            }
        }
    }
}
