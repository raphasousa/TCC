using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class MoveCube : MonoBehaviour {
    //player da RV (jogador)
    public GameObject playerVR;
    //boneco do menino
    public GameObject playerTY;
    //posição do player para gerar posição do objeto ao redor dele
    private Vector3 playerPosition;

    //velocidade de deslocamento do objeto
    private float velocidade;
    //velocidade de rotação para suavizar o movimento
    private float rotateSpeed;

    private bool naMira = false;

    void Start() {
        velocidade = 0.5f;
        rotateSpeed = 2.0f;
        //objeto inicia fora da mira
        SetGazedAt(naMira);
    }

    void Update() {
        //pega posição do player
        playerPosition = playerVR.transform.localPosition;
        //pega posição do alvo (menino)
        Vector3 targetPosition = playerTY.transform.position;
        //aumenta o y para não atravessar o chão
        targetPosition.y = targetPosition.y + 1;
        //move objeto em direção ao alvo (menino)
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * velocidade);
        //rotaciona objeto para ficar olhando para o alvo (menino)
        LookAtPoint(playerTY.transform.position);

        if (Input.GetButtonDown("Fire2") && naMira) TeleportRandomly();
    }

    public void SetGazedAt(bool gazedAt) {
        //troca cor do objeto quando esta na mira
        GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
        //seta variavel de controle
        if (gazedAt) naMira = true;
        else naMira = false;
    }

    //chamada quando ocorre o "click" e acerta o alvo
    public void TeleportRandomly() {
        //sorteia nova posicao para o objeto ao redor do player
        Vector3 direction = new Vector3(Random.Range(playerPosition.x - 10f, playerPosition.x + 10f), 
                                        Random.Range(playerPosition.y - 0.5f, playerPosition.y + 3f), 
                                        Random.Range(playerPosition.z, playerPosition.z + 10f));
        //sorteia nova velocidade para o objeto
        velocidade = Random.Range(0.2f, 0.8f);
        //move objeto para nova posição
        transform.localPosition = direction;
        //incrementa os pontos
        Score.AddScore(1f);
    }

    //faz objeto olhar na direção do alvo
    private void LookAtPoint(Vector3 target) {
        //pega posição do objeto
        Vector3 position = transform.position;
        //calcula direção entre o alvo e o objeto
        Vector3 direction = target - position;
        //retira rotação em y
        direction.y = 0;
        //calcula a rotação a ser aplicada
        Quaternion newRotation = Quaternion.FromToRotation(Vector3.forward, direction);
        //aplica a rotação no objeto
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //detecta se colidiu com o alvo (menino)
        if (collision.gameObject.tag == "Player")
        {
            //se colidiu perde vida
            Score.PerdeVida();
        }
    }
}
