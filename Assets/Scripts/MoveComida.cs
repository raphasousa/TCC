using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveComida : MonoBehaviour {
    //player da RV (jogador)
    public GameObject playerVR;
    //posição do player para gerar posição do objeto ao redor dele
    private Vector3 playerPosition;

    //velocidade de deslocamento do objeto
    private float velocidade;

    //usada quando volta do pause
    public static bool voltou;

    void Start()
    {
        velocidade = 0.5f;
        voltou = true;
    }

    void Update()
    {
        if (GameController.playing_comida)
        {
            //pega posição do player
            playerPosition = playerVR.transform.localPosition;

            //teleporta para proximo ao player quando volta do pause
            if (voltou)
            {
                TeleportRandomly();
                voltou = false;
            }

            //pega posição do alvo (chao)
            Vector3 targetPosition = transform.position;
            targetPosition.y = 0f;
            //move objeto em direção ao alvo (chão)
            transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * velocidade);
        }
    }

    //chamada quando acerta o alvo ou chega ao chão
    public void TeleportRandomly()
    {
        //sorteia nova posicao para o objeto ao redor do player
        Vector3 direction = new Vector3(Random.Range(playerPosition.x - 5f, playerPosition.x + 4f), 8f,
                                        Random.Range(playerPosition.z, playerPosition.z + 7f));

        //sorteia nova velocidade para o objeto
        velocidade = Random.Range(0.2f, 0.8f);
        //move objeto para nova posição
        transform.localPosition = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //detecta se colidiu com o alvo (menino)
        if (collision.gameObject.tag == "Player")
        {
            if (gameObject.tag == "Comida_Boa") Score.AddScore(1f);
            if (gameObject.tag == "Comida_Ruim") Score.PerdeSaude();
            //reposiciona objeto
            TeleportRandomly();
        }
        //detecta se chegou ao chão
        if (collision.gameObject.tag == "Chão")
        {
            //reposiciona objeto
            TeleportRandomly();
        }
    }
}
