using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollection : MonoBehaviour
{
    private int coins = 0;
    public Text collectableCounter;
    [SerializeField] private AudioSource coinSound;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        {
            coinSound.Play();
            Destroy(collision.gameObject);
            coins++;
            collectableCounter.text = "Coins:" + coins;
        }
    }
}
