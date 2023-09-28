using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitChecker : MonoBehaviour
{
    public GameManager gameManager;
    public int HitIndex;
    
    // Update is called once per frame
    void Update()
    {
        Collider[] HitColl = Physics.OverlapBox(transform.position,transform.localScale/2,Quaternion.identity);

        for (int i = 0; i < HitColl.Length; i++)
        {
            if(HitColl[i].CompareTag("Cable"))
                gameManager.CheckHit(HitIndex,false);
            else
                gameManager.CheckHit(HitIndex,true);
        }

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position,transform.localScale/2);
    }
}
