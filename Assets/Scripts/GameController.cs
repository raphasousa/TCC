using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {

    //objetos de cada fase
    public GameObject monstros;
    public GameObject comidas;
    public GameObject sujeiras;

    //objeto de cada menu
    public GameObject menu_pause;
    public GameObject menu_inicio;
    public GameObject menu_tutorial;

    //copia dos menus para poder usar nas chamadas publicas
    private static GameObject menu_pause_static;
    private static GameObject menu_inicio_static;
    private static GameObject menu_tutorial_static;

    //para saber qual fase esta em execução
    public static bool playing_monstro = false;
    public static bool playing_comida = false;
    public static bool playing_sujeira = false;

    //tempo de cada fase em segundos
    private float tempo_monstro = 5f;
    private float tempo_comida = 5f;
    private float tempo_sujeira = 5f;

    //nomes das fases
    private string fase_monstro = "Monstro";
    private string fase_comida = "Comida";
    private string fase_sujeira = "Sujeira";

    //variavel para pausar o jogo, verificada em outros scripts
    public static bool is_paused;

    //variavel para esperar certo tempo apos pausar, para que o pause funcione
    private bool esperando = false;

    //-----------------------------------------
    //----------------- START -----------------
    //-----------------------------------------

    // Use this for initialization
    void Start() {
        //inicia objetos do menu
        menu_pause_static = menu_pause;
        menu_inicio_static = menu_inicio;
        menu_tutorial_static = menu_tutorial;

        //retira todos os objetos ao iniciar jogo
        monstros.SetActive(false);
        comidas.SetActive(false);
        sujeiras.SetActive(false);

        //inicia corotina para gerenciar tempo de execução de cada fase
        StartCoroutine(ControlaFases());

        //inicia jogo pausado
        Show_Menu_Inicial();
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
        if (Input.GetButtonDown("Jump") && Input.GetButtonDown("Fire2") || Input.GetKeyDown(KeyCode.P))
        {   //pausa o jogo
            Pausar_Jogo();
        }

        //----------------------------------------
        //----------------- MENU -----------------
        //----------------------------------------

        if (is_paused && esperando == false)
        {
            if (Input.GetButtonDown("Jump")) //BOTAO A OU FRONTAL DE CIMA
            {   //MENU INICIO
                if (menu_inicio.activeSelf) BotaoJogar();
                //MENU PAUSE
                else if (menu_pause.activeSelf) BotaoReiniciar();
                //MENU TUTORIAL
                else if (menu_tutorial.activeSelf) BotaoVoltarTutorial();
            }
            if (Input.GetButtonDown("Fire2")) //BOTAO B OU FRONTAL DE BAIXO
            {   //MENU INICIO
                if (menu_inicio.activeSelf) BotaoTutorial();
                //MENU PAUSE
                else if (menu_pause.activeSelf) BotaoVoltar();
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
    //----------------- MENU -----------------
    //----------------------------------------

    static void Show_Menu_Inicial()
    {   //mostra menu inicio
        menu_inicio_static.SetActive(true);
        menu_pause_static.SetActive(false);
        menu_tutorial_static.SetActive(false);
        //pausa o jogo
        is_paused = true;
    }

    static void Show_Menu_Tutorial()
    {   //mostra menu tutorial
        menu_inicio_static.SetActive(false);
        menu_pause_static.SetActive(false);
        menu_tutorial_static.SetActive(true);
    }

    void Pausar_Jogo ()
    {   //mostra menu de pause
        menu_pause_static.SetActive(true);
        //trava o menu por alguns segundos, para o pause funcionar
        StartCoroutine(WaitSeconds());
        //pausa o jogo
        is_paused = true;
    }

    static void Iniciar_Jogo()
    {   //da play no jogo
        is_paused = false;
        //retira menus
        menu_pause_static.SetActive(false);
        menu_inicio_static.SetActive(false);
        menu_tutorial_static.SetActive(false);
    }

    IEnumerator WaitSeconds ()
    {   //delay de alguns segundos
        esperando = true;
        yield return new WaitForSeconds(3f);
        esperando = false;
    }

    //------------------------------------------
    //----------------- BOTÕES -----------------
    //------------------------------------------

    public static void BotaoReiniciar()
    {
        Score.ZeraPontos();
        Iniciar_Jogo();
    }

    public static void BotaoVoltar()
    {
        Iniciar_Jogo();
    }

    public static void BotaoJogar()
    {
        Iniciar_Jogo();
    }

    public static void BotaoTutorial()
    {
        Show_Menu_Tutorial();
    }

    public static void BotaoVoltarTutorial()
    {
        Show_Menu_Inicial();
    }
}
