
using UnityEngine;
using UnityEngine.UIElements;
using DG.Tweening;

public class StartButtonBlink : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject startButton;

    private Image img;

    void Start()
    {
       // img = startButton.GetComponent<Image>();

        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(1, 0.6f).SetEase(Ease.OutElastic).Play();

    }

    void Update()
    {

        //img.tintColor = new Color(1, 1, 1, Mathf.PingPong(Time.time, 1f)); // wtf, should work
        
    }
}
