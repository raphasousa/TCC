using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveSujeira : MonoBehaviour
{
    //player da RV (jogador)
    public GameObject playerVR;

    //posição do player para gerar posição do objeto ao redor dele
    private Vector3 playerPosition;

    //usada quando volta do pause
    public static bool voltou;

    void Start()
    {
        //inicia sujeiras proximas ao menino
        voltou = true;
    }

    void Update() {
        if (GameController.is_paused)
            return;
        if (GameController.playing_sujeira)
        {
            //pega posição do player
            playerPosition = playerVR.transform.localPosition;
            
            //teleporta para proximo ao player quando volta do pause
            if (voltou)
            {
                TeleportRandomly();
                voltou = false;
            }
        }
    }

    //chamada quando acerta o alvo ou chega ao chão
    public void TeleportRandomly()
    {
        //sorteia nova posicao para o objeto ao redor do player
        Vector3 direction = new Vector3(Random.Range(playerPosition.x - 5f, playerPosition.x + 4f), 0.8f,
                                        Random.Range(playerPosition.z, playerPosition.z + 7f));

        //move objeto para nova posição
        transform.localPosition = direction;
    }

    private void OnCollisionEnter(Collision collision)
    {
        //detecta se colidiu com o menino
        if (collision.gameObject.tag == "Player")
        {
            Score.PerdeSaude();
            //reposiciona objeto
            TeleportRandomly();
        }
    }

    void OnParticleCollision (GameObject other)
    {
        //detecta se foi atingido pelas bolhas
        if (gameObject.tag == "Sujeira")
        {
            Score.AddScore(1f);
            //reposiciona objeto
            TeleportRandomly();
        }
    }
}
