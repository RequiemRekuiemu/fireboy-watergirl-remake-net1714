using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMorph : NetworkBehaviour
{
    private GameObject player;

    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private Animator animator;

    [SerializeField] private Sprite aquaSprite;
    [SerializeField] private Animator aquaAnimator;

    [SerializeField] private GameObject aqua;

    // Start is called before the first frame update
    void Start()
    {
        player = this.gameObject;
        SpriteRenderer sr = this.GetComponent<SpriteRenderer>();
        if (!(OwnerClientId == 0))
        {
            sr.color = Color.blue;
            player.tag = "Aqua";
        }

        /*SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        Destroy(spriteRenderer);
        this.AddComponent<SpriteRenderer>();
        this.GetComponent<SpriteRenderer>().sprite = aquaSprite;
        Animator anim = GetComponent<Animator>();
*//*Destroy(anim);
            this.gameObject.AddComponent<Animator>();
            this.gameObject.GetComponent<Animator>().controller = aquaAnimator;*//*
            player.tag = "Aqua";*/

    }

    // Update is called once per frame
    void Update()
    {

    }
}
