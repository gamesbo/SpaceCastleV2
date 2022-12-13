using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

namespace EKTemplate
{
    public class GamePanel : Panel
    {
        public RectTransform coinPanelRect;
        public RectTransform restartButtonRect;

        [HideInInspector] int inGameCurrency;

        private Tween tween;
        public GameObject Tuto;
        public GameObject Tuto2;
        public GameObject Hand;
        private Text moneyText { get { return coinPanelRect.GetChild(1).GetChild(0).GetComponent<Text>(); } }
        private Button restartButton { get { return restartButtonRect.GetComponent<Button>(); } }

        public IEnumerator TutoDelay()
        {
            Tuto.SetActive(true);
            Hand.SetActive(true);

            yield return new WaitForSeconds(4f);
            Tuto.SetActive(false);

            yield return new WaitForSeconds(2f);
            Hand.SetActive(false);
            Tuto2.SetActive(true);
            yield return new WaitForSeconds(4f);
            Tuto2.SetActive(false);
            PlayerPrefs.SetInt("Delay", 1);
        }
        public void Start()
        {
            restartButton.onClick.AddListener(OnClickRestartButton);
            moneyText.text = "0";
            if (PlayerPrefs.GetInt("Delay", 0) == 0)
            {
                StartCoroutine(TutoDelay());
            }
        }
        
        public void SetMoney(float to, float duration = 0.3f)
        {
            if (tween != null) tween.Kill();

            coinPanelRect
            .DOScale(1.2f, duration * 0.5f)
            .SetEase(Ease.Linear)
            .SetLoops(2, LoopType.Yoyo);

            float startFrom = int.Parse(moneyText.text);
            tween = DOTween.To((x) => startFrom = x, startFrom, to, duration)
            .OnUpdate(() =>
            {
                moneyText.text = ((int)startFrom).ToString();
            })
            .OnComplete(() => moneyText.text = ((int)to).ToString());
        }

        public void AddMoney(int amount)
        {
            float startFrom = int.Parse(moneyText.text);
            SetMoney(inGameCurrency + amount);
        }

        private void OnClickRestartButton()
        {
            GameManager.instance.RestartScene();
        }
    }
}