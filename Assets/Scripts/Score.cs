using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class Score : MonoBehaviour
{
    private const string DISPLAY_TEXT_FORMAT = "Vidas: {0}\nPontos: {1}";

    private Text textField;
    private static float score;
    public static float vidas;

    public Camera cam;

    void Awake()
    {
        textField = GetComponent<Text>();
    }

    void Start()
    {
        score = 0;
        vidas = 3;
        if (cam == null)
        {
            cam = Camera.main;
        }

        if (cam != null)
        {
            // Tie this to the camera, and do not keep the local orientation.
            transform.SetParent(cam.GetComponent<Transform>(), true);
        }
    }

    void LateUpdate()
    {
        textField.text = string.Format(DISPLAY_TEXT_FORMAT, Mathf.RoundToInt(vidas), Mathf.RoundToInt(score));
    }

    public static void AddScore (float points)
    {
        score += points;
    }
    
    public static void PerdeVida ()
    {
        vidas = vidas - 1f;
    }
}
