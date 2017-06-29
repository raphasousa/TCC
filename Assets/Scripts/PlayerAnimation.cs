using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour {
    //boneco do menino
    public GameObject playerTY;
    //player da RV (jogador)
    public GameObject playerVR;

    //velocidade que anda
    private float velocidade;
    //velocidade de giro
    private float giro;
    //bool para saber se ja morreu
    private bool vivo;

    //nome das animações
    private string anima_Andar = "swagger_walk_inPlace";
    private string anima_Correr = "jogging_inPlace";
    private string anima_Pular = "jump";
    private string anima_Dancar = "hip_hop_dancing";
    private string anima_Morrer = "falling_back_death";
    private string anima_Parado = "neutral_idle";

    // Use this for initialization
    void Start () {
        velocidade = 2.0f;
        giro = 50.0F;
        vivo = true;
    }

    // Update is called once per frame
    void Update() {
        //pega valores dos botões do controle
        float v = (Input.GetAxis("Vertical") * velocidade) * Time.deltaTime;
        float h = (Input.GetAxis("Horizontal") * velocidade) * Time.deltaTime;
        //movimenta o menino
        transform.Translate(v * Vector3.forward + h * Vector3.right);

        //rotaciona o boneco
        float rotate = h * giro;
        transform.Rotate(0, rotate, 0);

        //faz a camera VR acompanhar o boneco pelo cenario
        Vector3 posCamera = new Vector3(playerTY.transform.position.x + 1.5f, 2.54f, playerTY.transform.position.z - 5f);
        playerVR.transform.position = posCamera;

        //chama funções dos botões
        //if (Input.GetButtonDown("Fire1")) AnimaPlayerTY(anima_Dancar);
        //if (Input.GetButtonDown("Fire2")) AnimaPlayerTY(anima_Dancar);
        if (Input.GetButtonDown("Fire2")) AnimaPlayerTY(anima_Dancar);
        if (Input.GetButtonDown("Jump")) AnimaPlayerTY(anima_Pular);
        if (Input.GetButton("Vertical")  || Input.GetButton("Horizontal")) AnimaPlayerTY(anima_Correr);
        if (Input.GetButtonUp("Vertical") || Input.GetButtonUp("Horizontal")) AnimaPlayerTY(anima_Parado);

        if (Score.vidas == 0 && vivo) Morrer();
    }

    //função para tocar as animações
    void AnimaPlayerTY(string nome_animacao)
    {
        playerTY.GetComponent<Animation>().Play(nome_animacao);
    }

    void Morrer()
    {
        vivo = false;
        AnimaPlayerTY(anima_Morrer);
    }
}
