using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class Controller : MonoBehaviour
{
    private SwerveController _swerveInputSystem;

    //Float
    [SerializeField] private float swerveSpeed = 0.5f;
    [SerializeField] private float maxSwerveAmount = 1f;
    public float CharSpeed;
    public float Health = 3;

    // int
    public int MoneyTotal;
    public int MoneyRemain;
    public int HealthCounter;

    // Bool
    public bool isWin = false;
    public bool IsDetectBarrier = false;
    public bool isGameOver = false;

    public Animator CharAnim;

    //Camera
    public GameObject CharCam;
    public GameObject AnimCamera;

    //UI
    public GameObject holdDrag;

    //Partical Effects
    public GameObject MoneyBlast;
    public GameObject MoneyBlastBlue;
    public GameObject DamageBlast;
    public GameObject Confetti;

    public GameObject WinScreen;
    public GameObject LoseScreen;

    //Counter (Health, Money, Money Collected
    public TextMeshProUGUI moneyRemaintxt;
    public TextMeshProUGUI moneyTotalTxt;
    public TextMeshProUGUI HealthTxt;
    public TextMeshProUGUI loseScreenCollected;
    public TextMeshProUGUI WinScreenCollected;

    //Camera Shake
    public float shakeTime;
    public float ShakePower;

    private void Start()
    {
        //Oyun Baþladýðýnda Oyuncunun parasýný yazdýrýr
        moneyTotalTxt.text = "" + MoneyTotal;
    }

    private void Awake()
    {
        //Para kayýt
        MoneyTotal = PlayerPrefs.GetInt("MoneyTotall", 0);
        _swerveInputSystem = GetComponent<SwerveController>();
    }

    private void Update()
    {
        //Animasyon ve Swerve Control
        if (_swerveInputSystem._isHolding == true)
        {
            if (isGameOver == false)
            {
                if (isWin == false)
                {
                    float swerveAmount = Time.deltaTime * swerveSpeed * _swerveInputSystem.MoveFactorX;
                    swerveAmount = Mathf.Clamp(swerveAmount, -maxSwerveAmount, maxSwerveAmount);
                    transform.Translate(swerveAmount, 0, CharSpeed * Time.deltaTime);
                    CharAnim.SetBool("isRunning", true);
                    holdDrag.SetActive(false);
                }
            }
        }
        if (_swerveInputSystem._isHolding == false)
        {
            CharAnim.SetBool("isRunning", false);
        }
        if (Health <= 0)
        {
            loseScreenCollected.text = "" + MoneyRemain;
            loseScreenCollected.color = Color.red;
            isGameOver = true;
            LoseScreen.SetActive(true);
            CharAnim.SetBool("isRunning", false);
        }
        GiveHealth();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Barriee")
        {
            Health--;
            HealthTxt.text = "" + Health;
            IsDetectBarrier = true;

            //Shake
            if (IsDetectBarrier == true)
            {
            Camera.main.DOShakeRotation(shakeTime, ShakePower, fadeOut: true);
            Camera.main.DOShakePosition(shakeTime, ShakePower, fadeOut: true);
            }

            StartCoroutine(FalseDetect());
            //Partical Effect
            Instantiate(DamageBlast, transform.position, Quaternion.identity);
        }
        if (collision.gameObject.tag == "BlueMoney")
        {
            MoneyRemain += 50;
            Destroy(collision.gameObject);
            moneyRemaintxt.text = "+" + MoneyRemain;
            Instantiate(MoneyBlastBlue, transform.position, Quaternion.identity);
            HealthCounter++;
        }
        if (collision.gameObject.tag == "YellowMoney")
        {
            MoneyRemain += 10;
            Destroy(collision.gameObject);
            moneyRemaintxt.text = "+" + MoneyRemain;
            Instantiate(MoneyBlast, transform.position, Quaternion.identity);
            HealthCounter++;
        }
        if (collision.gameObject.tag == "Finish")
        {
            isWin = true;
            CharAnim.SetBool("isVictory", true);
            AnimCamera.SetActive(true);
            CharCam.SetActive(false);
            MoneyTotal += MoneyRemain;
            moneyTotalTxt.text = "" + MoneyTotal;
            moneyRemaintxt.text = "0";
            PlayerPrefs.SetInt("MoneyTotall", MoneyTotal);
            Confetti.SetActive(true);
            WinScreen.SetActive(true);
            WinScreenCollected.text = "" + MoneyRemain;
            WinScreenCollected.color = Color.green;
        }
    }

    public void GiveHealth()
    {
        if (HealthCounter == 15)
        {
            Health++;
            HealthCounter = 0;
            HealthTxt.text = "" + Health;
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator FalseDetect()
    {
        yield return new WaitForSeconds(0.5f);
        IsDetectBarrier = false;
        yield break;
    }
}