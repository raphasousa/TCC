using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    public GameObject playerTY;
    public GameObject playerVR;

    private float velocidade;
    private float giro;
    private bool vivo;

    // Use this for initialization
    void Start () {
        velocidade = 2.0f;
        giro = 100.0F;
        vivo = true;
    }
	
	// Update is called once per frame
	void Update () {

        float translate = (Input.GetAxis("Vertical") * velocidade) * Time.deltaTime;
        float rotate = (Input.GetAxis("Horizontal") * giro) * Time.deltaTime;

        transform.Translate(0, 0, translate);
        transform.Rotate(0, rotate, 0);

        Vector3 posCamera = new Vector3(playerTY.transform.position.x + 1.5f, 2.54f, playerTY.transform.position.z - 5f);
        playerVR.transform.position = posCamera;

        if (Input.GetButtonDown("Fire1")) Dancar();
        if (Input.GetButtonDown("Jump")) Pular();
        if (Input.GetButton("Horizontal")) Correr();
        if (Input.GetButton("Vertical")) Correr();
        if (Input.GetButtonUp("Vertical") || Input.GetButtonUp("Horizontal")) Parar();

        if (Score.vidas == 0 && vivo) Morrer();
    }

    void Andar()
    {
        playerTY.GetComponent<Animation>().Play("swagger_walk_inPlace");
    }

    void Morrer()
    {
        vivo = false;
        playerTY.GetComponent<Animation>().Play("falling_back_death");
    }

    void Correr()
    {
        playerTY.GetComponent<Animation>().Play("jogging_inPlace");
    }

    void Parar()
    {
        playerTY.GetComponent<Animation>().Play("neutral_idle");
    }

    void Pular()
    {
        playerTY.GetComponent<Animation>().Play("jump");
    }

    void Dancar()
    {
        playerTY.GetComponent<Animation>().Play("hip_hop_dancing");
    }
}
