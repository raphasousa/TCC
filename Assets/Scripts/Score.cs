using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour {
    //formato do texto apresentado na tela
    private const string DISPLAY_TEXT_FORMAT = "Vidas: {0}\nPontos: {1}";

    private Text textField;
    //pontos conquistados
    private static float score;
    //vidas restantes
    public static float vidas;

    //camera é usada para posicionar o texto na tela
    public Camera cam;

    void Awake()
    {
        //procura pelo componente de texto
        textField = GetComponent<Text>();
    }

    void Start()
    {
        //valores iniciais de pontos e vidas
        score = 0;
        vidas = 3;

        //pega a camera principal como referencia
        if (cam == null) {
            cam = Camera.main;
        }
        //posiciona o texto na camera
        if (cam != null) {
            // Tie this to the camera, and do not keep the local orientation.
            transform.SetParent(cam.GetComponent<Transform>(), true);
        }
    }

    void LateUpdate()
    {
        //atualiza texto na tela
        textField.text = string.Format(DISPLAY_TEXT_FORMAT, Mathf.RoundToInt(vidas), Mathf.RoundToInt(score));
    }

    //chamada para adicionar pontos
    public static void AddScore (float points)
    {
        score += points;
    }
    
    //chamada para decrementar a quantidade de vidas
    public static void PerdeVida ()
    {
        vidas = vidas - 1f;
    }
}
