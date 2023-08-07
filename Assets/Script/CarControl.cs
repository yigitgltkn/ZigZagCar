using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarControl : MonoBehaviour
{
    public GameObject pickUpEffect;
    
    public float speed = 10.0f;

    bool movingLeft = true;
    bool fistInput = false;

    void Update()
    {
        if (GameManager.instance.gameStarted)  
        {
            CheckInput();
            Move();  

        }
        if (transform.position.y <= -2) //arac y'de -2 ye gelirse GameManager'daki GameOver tetiklenir
        {
            GameManager.instance.GameOver();
        }   
    }
    void Move()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    void CheckInput()   
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!fistInput)
            {
                fistInput = true;
                //if (fistInput)
                //{
                //    fistInput = false;
                //    return;

                //}
            }

            if (Input.GetMouseButtonDown(0))
            {
            ChangeDirection();
            }
        }
    }
    void ChangeDirection()
    {
        if (movingLeft)
        {
            movingLeft = false;
            transform.rotation = Quaternion.Euler(0, 90, 0);
        }
        else
        {
            movingLeft = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Diamond")
        {
            GameManager.instance.IncrementScore();

            Instantiate(pickUpEffect, other.transform.position, pickUpEffect.transform.rotation);


            other.gameObject.SetActive(false);
        }
    }

}


