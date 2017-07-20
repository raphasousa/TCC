using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour {
    //formato do texto apresentado na tela
    private const string DISPLAY_TEXT_FORMAT = "Saúde: {0}\nPontos: {1}";

    private Text textField;
    //pontos conquistados
    private static float score;
    //vidas restantes
    public static float saude;
    //pontos na sequencia
    public static float pontos_seguidos;

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
        saude = 0;
        pontos_seguidos = 0;

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
        textField.text = string.Format(DISPLAY_TEXT_FORMAT, Mathf.RoundToInt(saude), Mathf.RoundToInt(score));
    }

    //chamada para adicionar pontos
    public static void AddScore (float points)
    {
        score += points;
        pontos_seguidos += 1;
        //se marcar tres pontos seguidos, cura o personagem
        if (pontos_seguidos == 3) ResetSaude();
    }
    
    //chamada para decrementar a quantidade de vidas
    public static void PerdeSaude()
    {
        saude = saude - 1f;
        if (saude < 0) saude = 0;
        pontos_seguidos = 0;
    }

    //chamada para reiniciar a quantidade de vidas
    public static void ResetSaude()
    {
        saude = 3f;
        pontos_seguidos = 0;
    }
}
