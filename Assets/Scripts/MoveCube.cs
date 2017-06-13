using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider))]
public class MoveCube : MonoBehaviour {
    public GameObject playerVR;
    public GameObject playerTY;

    public Material inactiveMaterial;
    public Material gazedAtMaterial;

    private float velocidade;
    private Vector3 startingPosition;

    //Usado no lerp, para suavizar o movimento
    private float rotateSpeed;

    void Start() {
        velocidade = 0.5f;
        rotateSpeed = 2.0f;
        SetGazedAt(false);
    }

    void Update()
    {
        startingPosition = playerVR.transform.localPosition;
        //move objeto em direção ao alvo
        transform.position = Vector3.Lerp(transform.position, playerTY.transform.position, Time.deltaTime * velocidade);
        LookAtPoint(playerTY.transform.position);
    }

    public void SetGazedAt(bool gazedAt) {
        //troca cor do objeto quando esta na mira
        if (inactiveMaterial != null && gazedAtMaterial != null) {
            GetComponent<Renderer>().material = gazedAt ? gazedAtMaterial : inactiveMaterial;
            return;
        }
        GetComponent<Renderer>().material.color = gazedAt ? Color.green : Color.red;
    }

    public void Reset() {
        transform.localPosition = startingPosition;
    }

    public void TeleportRandomly() {
        //sorteia nova posicao para o objeto
        Vector3 direction = new Vector3(Random.Range(startingPosition.x - 10f, startingPosition.x + 10f), 
                                        Random.Range(startingPosition.y - 0.5f, startingPosition.y + 3f), 
                                        Random.Range(startingPosition.z, startingPosition.z + 10f));
        transform.localPosition = direction;
        //incrementa os pontos
        Score.AddScore(1f);
        velocidade = Random.Range(0.2f, 0.8f);
    }

    private void LookAtPoint(Vector3 target) {
        //faz objeto olhar na direção do alvo
        Vector3 position = transform.position;
        Vector3 direction = target - position;
        direction.y = 0;
        
        Quaternion newRotation = Quaternion.FromToRotation(Vector3.forward, direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, newRotation, rotateSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("colidiu");
        if (collision.gameObject.tag == "Player")
        {
            Score.PerdeVida();
        }
    }
}
