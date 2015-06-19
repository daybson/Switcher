using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Switcher : MonoBehaviour
{
	#region Fields

    private Button buton;
    private RectTransform rectTransButton;
    private RectTransform rectTransOnOffSlider;
    private Image background;
    public Color onColor;
    public Color offColor;

    [SerializeField]
    private bool isOn;
    public bool IsOn
    {
        get { return isOn; }
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
        this.background = transform.FindChild("BackGround").GetComponent<Image>();
    }

	#endregion

	#region Public Methods 

    public void SetStatus(bool isOn)
    {
        StartCoroutine(!this.isOn ? "SlideButtonOn" : "SlideButtonOff");
        this.isOn = isOn;
    }

	#endregion

	#region Private Methods

    private IEnumerator SlideButtonOn()
    {
        while (this.rectTransButton.anchoredPosition.x + this.slideFactor / 2 <= this.rectTransOnOffSlider.sizeDelta.x / 2)
        {
            this.rectTransButton.anchoredPosition =
                new Vector2(
                    this.rectTransButton.anchoredPosition.x + this.slideFactor,
                    this.rectTransButton.anchoredPosition.y);
            yield return new WaitForSeconds(this.waitTime);
        }

        this.background.color = this.onColor;

        if (!(this.rectTransButton.anchoredPosition.x + this.slideFactor / 2 <= this.rectTransOnOffSlider.sizeDelta.x / 2))
            StopCoroutine("SlideButtonOn");
    }

    private IEnumerator SlideButtonOff()
    {
        this.background.color = this.offColor;

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
