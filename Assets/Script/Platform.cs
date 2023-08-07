using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    Rigidbody rigidBody;
    public GameObject diamond;


    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        int randDiamond = Random.Range(0, 5);

        Vector3 diamondPos = transform.position;
        diamondPos.y += 1f;
        if (randDiamond<2) //random sayi 0 gelirse spawnlar. 1 2 3 4 gelirse spawnlamaz.
        {
            
            GameObject diamondInstance = Instantiate(diamond, diamondPos, diamond.transform.rotation);
            diamondInstance.transform.SetParent(gameObject.transform);
        }

   
    }


    void Update()
    {
        //rigidBody.useGravity = true;
        rigidBody.velocity = new Vector3(0, -20, 0);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player") // karakter platformdam cikis yaptiginda fall metodu calisir iskinematic deaktif olur.
        {
            Invoke("Fall", 0.1f);
        }
    }

    void Fall()
    {
        GetComponent<Rigidbody>().isKinematic = false;
        Destroy(gameObject, 1f);
        
    }
}
