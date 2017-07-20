using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    //boneco do menino
    public GameObject playerTY;
    //player da RV (jogador)
    public GameObject playerVR;

    //cores do menino
    private Color cor_Doente = new Color(255f / 255f, 118f / 255f, 118f / 255f, 255f / 255f); //FF7676FF - vermelho
    private Color cor_Saudavel = Color.white;
    //duração da transição de cor
    private float duration = 1.0F;

    //objetos do menino para trocar de cor
    public GameObject playerTY_head;
    public GameObject playerTY_hand;

    private Renderer hand_Renderer;
    private Renderer head_Renderer;

    //velocidade que anda
    private float velocidade;
    //velocidade de giro
    private float giro;

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

        //busca componentes de cor
        head_Renderer = playerTY_head.GetComponent<Renderer>();
        hand_Renderer = playerTY_hand.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update() {
        //pega valores dos botões do controle
        float v = (Input.GetAxis("Vertical") * velocidade) * Time.deltaTime;
        float h = (Input.GetAxis("Horizontal") * velocidade) * Time.deltaTime;
        //movimenta o menino
        transform.Translate(v * Vector3.forward + h * Vector3.right);
        //rotaciona o menino
        transform.Rotate(0, h * giro, 0);

        //faz a camera VR acompanhar o menino pelo cenario
        Vector3 posCamera = new Vector3(playerTY.transform.position.x + 1.5f, 2.54f, playerTY.transform.position.z - 5f);
        playerVR.transform.position = posCamera;

        //chama funções dos botões
        if (Input.GetButtonDown("Fire2")) AnimaPlayerTY(anima_Dancar);
        if (Input.GetButtonDown("Jump")) AnimaPlayerTY(anima_Pular);
        if (Input.GetButton("Vertical")  || Input.GetButton("Horizontal")) AnimaPlayerTY(anima_Correr);
        if (Input.GetButtonUp("Vertical") || Input.GetButtonUp("Horizontal")) AnimaPlayerTY(anima_Parado);
        
        if (Score.saude < 3)
        {   //se esta doente, troca a cor
            //comandos para fazer transição de cores gradualmente
            float lerp = Mathf.PingPong(Time.time, duration) / duration;
            playerTY_head.GetComponent<Renderer>().material.color = Color.Lerp(cor_Saudavel, cor_Doente, lerp);
            playerTY_hand.GetComponent<Renderer>().material.color = Color.Lerp(cor_Saudavel, cor_Doente, lerp);
        }
        else
        {   //se esta curado, troca a cor
            TrocaCorPlayerTY(cor_Saudavel);
        }
    }

    //função para tocar as animações do menino
    void AnimaPlayerTY(string nome_animacao)
    {
        playerTY.GetComponent<Animation>().Play(nome_animacao);
    }

    //função para trocar a cor do menino
    void TrocaCorPlayerTY (Color cor)
    {
        head_Renderer.material.color = cor;
        hand_Renderer.material.color = cor;
    }
}
