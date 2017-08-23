using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //objetos de cada fase
    public GameObject monstros;
    public GameObject comidas;
    public GameObject sujeiras;
    public GameObject menu;

    private static GameObject menu_static;

    //para saber qual fase esta em execução
    public static bool playing_monstro = false;
    public static bool playing_comida = false;
    public static bool playing_sujeira = false;

    //tempo de cada fase em segundos
    private float tempo_monstro = 30f;
    private float tempo_comida = 30f;
    private float tempo_sujeira = 30f;

    //nomes das fases
    private string fase_monstro = "Monstro";
    private string fase_comida = "Comida";
    private string fase_sujeira = "Sujeira";

    //variavel para pausar o jogo
    public static bool is_paused;
    private bool esperando = false;

    //-----------------------------------------
    //----------------- START -----------------
    //-----------------------------------------

    // Use this for initialization
    void Start() {
        //inicia objeto do menu
        menu_static = menu;

        //retira todos os objetos ao iniciar jogo
        monstros.SetActive(false);
        comidas.SetActive(false);
        sujeiras.SetActive(false);

        //inicia corotina para gerenciar tempo de execução de cada fase
        StartCoroutine(ControlaFases());

        //inicia jogo pausado
        Pausar_Jogo();
    }

    //------------------------------------------
    //----------------- UPDATE -----------------
    //------------------------------------------

    // Update is called once per frame
    void Update()
    {
        //-----------------------------------------
        //----------------- PAUSE -----------------
        //-----------------------------------------

        //se apertar os dois botoes juntos ===> PAUSE
        if (Input.GetButtonDown("Jump") && Input.GetButtonDown("Fire2"))
        {   //pausa o jogo
            Pausar_Jogo();
        }

        //----------------------------------------
        //----------------- MENU -----------------
        //----------------------------------------

        if (is_paused && esperando == false)
        {
            if (Input.GetButtonDown("Jump")) //BOTAO A OU FRONTAL DE CIMA
            {
                BotaoReiniciar();
            }
            if (Input.GetButtonDown("Fire2")) //BOTAO B OU FRONTAL DE BAIXO
            {
                BotaoVoltar();
            }
        }
    }

    //-----------------------------------------
    //----------------- FASES -----------------
    //-----------------------------------------

    IEnumerator ControlaFases()
    {
        while (true)
        {
            //inicia fase do monstro
            Start_Fase(fase_monstro);
            yield return new WaitForSeconds(tempo_monstro);
            Pause_Fase(fase_monstro);

            //inicia fase da comida
            Start_Fase(fase_comida);
            yield return new WaitForSeconds(tempo_comida);
            Pause_Fase(fase_comida);

            //inicia fase da sujeira
            Start_Fase(fase_sujeira);
            yield return new WaitForSeconds(tempo_sujeira);
            Pause_Fase(fase_sujeira);
        }
    }

    void Start_Fase (string fase)
    {
        if (fase == fase_monstro)
        {
            //ativa o update do MoveMonstro
            playing_monstro = true;
            MoveMonstro.voltou = true;
            //ativa os objetos na tela
            monstros.SetActive(true);
        }
        else if (fase == fase_comida)
        {
            //ativa o update do MoveComida
            playing_comida = true;
            MoveComida.voltou = true;
            //ativa os objetos na tela
            comidas.SetActive(true);
        }
        else if (fase == fase_sujeira)
        {
            //ativa o update do MoveSujeira
            playing_sujeira = true;
            MoveSujeira.voltou = true;
            //ativa os objetos na tela
            sujeiras.SetActive(true);
        }
    }

    void Pause_Fase(string fase)
    {
        if (fase == fase_monstro)
        {
            //pausa o update do MoveMonstro
            playing_monstro = false;
            //desativa os objetos na tela
            monstros.SetActive(false);
        }
        else if (fase == fase_comida)
        {
            //pausa o update do MoveComida
            playing_comida = false;
            //desativa os objetos na tela
            comidas.SetActive(false);
        }
        else if (fase == fase_sujeira)
        {
            //pausa o update do MoveSujeira
            playing_sujeira = false;
            //desativa os objetos na tela
            sujeiras.SetActive(false);
        }
    }

    //----------------------------------------
    //----------------- JOGO -----------------
    //----------------------------------------

    void Pausar_Jogo ()
    {   //mostra menu de pause
        menu_static.SetActive(true);
        //pausa o jogo
        StartCoroutine(WaitSeconds());
        is_paused = true;
    }

    static void Iniciar_Jogo()
    {   //da play no jogo
        is_paused = false;
        //retira menu de pause
        menu_static.SetActive(false);
    }

    IEnumerator WaitSeconds ()
    {   //delay de alguns segundos
        esperando = true;
        yield return new WaitForSeconds(3f);
        esperando = false;
    }

    //----------------------------------------
    //----------------- MENU -----------------
    //----------------------------------------

    public static void BotaoReiniciar()
    {
        Score.ZeraPontos();
        Iniciar_Jogo();
    }

    public static void BotaoVoltar()
    {
        Iniciar_Jogo();
    }
}
