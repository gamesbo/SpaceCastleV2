using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
namespace Economy
{

    public class InGameEconomy : MonoBehaviour
    {
        public Text TotalMoneyUI;
        public int TotalMoney;
        public Image Icon;
        bool oneShot;

        private void Start()
        {

            if (TotalMoneyUI.text == "0000000" && PlayerPrefs.GetInt("TotalMoney") == 1)
            {
                TotalMoneyUI.GetComponent<Text>().text = TotalMoney.ToString();
                PlayerPrefs.SetInt("Wave", 0);
            }

            else
            {
                TotalMoney = PlayerPrefs.GetInt("TotalMoney");
                TotalMoneyUI.text = TotalMoney.ToString();
            }

            StartCoroutine(DelayMoney());

            //TotalMoneyUI.GetComponent<Text>().text = "0";
            //PlayerPrefs.SetInt("TotalMoney", 0);
        }

       IEnumerator DelayMoney()
        {
            yield return new WaitForSeconds(1f);
            CalculateMoney(1);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                CalculateMoney(10);
            }
        }

        public void CalculateMoney(int value)
        {

            Haptic.LightTaptic();
            TotalMoney = PlayerPrefs.GetInt("TotalMoney");
            AbstractEconomy conn2 = new AbstractPlayerWallet();
            TotalMoney = conn2.CalculateNewCase(TotalMoney, value);
            Icon.transform.DOScale(Vector3.one, 0f);
            Icon.transform.transform.DOShakeScale(1f, 0.5f, 20);
            Icon.transform.DOScale(Vector3.one, 0f).SetDelay(1f);
            //TotalMoneyUI.text = TotalMoney.ToString();
            int ey = int.Parse(TotalMoneyUI.text);
            float GetVelo = ey;
            float SetVelo = TotalMoney;

            DOTween.To(() => GetVelo, x => GetVelo = x, SetVelo, 0.2f)
                .OnUpdate(() =>
                {
                    int ey = (int)(GetVelo);
                    TotalMoneyUI.text = ey.ToString();
                });

            PlayerPrefs.SetInt("TotalMoney", TotalMoney);
        }
    }

    abstract class AbstractEconomy
    {
        public abstract int CalculateNewCase(int CurrentMoney, int NewValue);
    }

    class AbstractPlayerWallet : AbstractEconomy
    {

        public override int CalculateNewCase(int CurrentMoney, int NewValue)
        {
            int newManyCase = Mathf.Clamp(CurrentMoney + NewValue, 0, 999999999);
            return newManyCase;
        }
    }
}
