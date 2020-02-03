
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartButtonBlink : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startButton;

    public Image img;

    public float blinkMultiplier = 4;

    void Start()
    {

        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(1, 0.6f).SetEase(Ease.OutElastic).Play();

    }

    void Update()
    {

        img.color = new Color(1, 1, 1, Mathf.Round(Mathf.PingPong(Time.time*blinkMultiplier, 1f)));
        
    }
}
