using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Switcher : MonoBehaviour
{
    #region Fields

    private Button buton;
    private RectTransform rectTransButton;
    private RectTransform rectTransOnOffSlider;
    private Image backgroundOn;
    private Image backgroundOff;
    public Color onColor;
    public Color offColor;
    public Color disbledColor;

    [SerializeField]
    private bool isOn;
    public bool IsOn
    {
        get { return isOn; }
        set { SetStatus(value); }
    }

    [SerializeField]
    private float slideFactor = 3f;

    [SerializeField]
    private float waitTime = 0.01f;

    #endregion


    #region MonoBehavior Methods

    private void Awake()
    {
        this.buton = GetComponentInChildren<Button>();
        this.buton.onClick.AddListener(delegate { SetStatus(!this.isOn); });
        this.rectTransButton = this.buton.GetComponent<RectTransform>();
        this.rectTransOnOffSlider = this.GetComponent<RectTransform>();
        this.backgroundOn = transform.FindChild("TextContainer/ON").GetComponent<Image>();
        this.backgroundOff = transform.FindChild("TextContainer/OFF").GetComponent<Image>();

        //make the button size corret, since layout components can not be apllied because the hierarchy structure
        //if you Switcher has the risk to be resized on game, put this block code below on a Update method to keep the button's size
        var switcherRectTransform = this.transform as RectTransform;
        this.rectTransButton.pivot = Vector2.up;
        this.rectTransButton.anchoredPosition = Vector2.zero;
        this.rectTransButton.anchorMax = Vector2.up;
        this.rectTransButton.anchorMin = Vector2.up;
        this.rectTransButton.sizeDelta = new Vector2(switcherRectTransform.sizeDelta.x / 2, switcherRectTransform.sizeDelta.y);
    }

    private void Start()
    {
        SetStatus(false);
    }

    #endregion


    #region Private Methods

    private void SetStatus(bool isOn)
    {
        StartCoroutine(isOn ? "SlideButtonOn" : "SlideButtonOff");
        this.isOn = isOn;
    }

    private IEnumerator SlideButtonOn()
    {
        this.backgroundOff.color = this.disbledColor;
        this.backgroundOn.color = this.onColor;

        while (this.rectTransButton.anchoredPosition.x + this.slideFactor / 2 <= this.rectTransOnOffSlider.sizeDelta.x / 2)
        {
            this.rectTransButton.anchoredPosition =
                new Vector2(
                    this.rectTransButton.anchoredPosition.x + this.slideFactor,
                    this.rectTransButton.anchoredPosition.y);
            yield return new WaitForSeconds(this.waitTime);
        }

        if (!(this.rectTransButton.anchoredPosition.x + this.slideFactor / 2 <= this.rectTransOnOffSlider.sizeDelta.x / 2))
            StopCoroutine("SlideButtonOn");
    }

    private IEnumerator SlideButtonOff()
    {
        this.backgroundOn.color = this.disbledColor;
        this.backgroundOff.color = this.offColor;

        while (this.rectTransButton.anchoredPosition.x / 2 >= this.slideFactor / 2)
        {
            this.rectTransButton.anchoredPosition =
                new Vector2(
                    this.rectTransButton.anchoredPosition.x - this.slideFactor,
                    this.rectTransButton.anchoredPosition.y);
            yield return new WaitForSeconds(this.waitTime);
        }

        if (!(this.rectTransButton.anchoredPosition.x / 2 >= this.slideFactor / 2))
            StopCoroutine("SlideButtonOff");
    }

    #endregion
}
