using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Button startStopButton; // ������ �����/����
    public Image indicator1;       // ��������� 1
    public Image indicator2;       // ��������� 2
    public TMPro.TextMeshProUGUI timerText1;        // ��������� ���� 1
    public TMPro.TextMeshProUGUI timerText2;        // ��������� ���� 2

    private bool isRunning = false; // ���� ������ ��������
    private Coroutine activeCoroutine; // �������� ��������
    private bool isIndicator1Active = true; // ���� ���������� ���������� 1

    void Start()
    {
        // ������������� ��������� ��������
        startStopButton.onClick.AddListener(OnStartStopButtonClick);
        SetIndicators(Color.yellow);
        timerText1.text = "0";
        timerText2.text = "0";
    }

    void OnStartStopButtonClick()
    {
        if (!isRunning)
        {
            // ������ ��������
            isRunning = true;
            startStopButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "����";

            // ��������� ������ ��������
            isIndicator1Active = true;
            indicator1.color = Color.green;
            indicator2.color = Color.red;
            timerText2.text = "0"; // ���������� ������ ������� ����������

            activeCoroutine = StartCoroutine(TimerCoroutine(timerText1, indicator1, indicator2));
        }
        else
        {
            // ��������� ��������
            isRunning = false;
            startStopButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "�����";
            SetIndicators(Color.yellow);

            if (activeCoroutine != null)
                StopCoroutine(activeCoroutine);

            timerText1.text = "0";
            timerText2.text = "0";
        }
    }

    IEnumerator TimerCoroutine(TMPro.TextMeshProUGUI activeTimerText, Image activeIndicator, Image inactiveIndicator)
    {
        while (isRunning)
        {
            // ��������� ���������� ������� �� 10 �� 20 ������
            int time = Random.Range(10, 21);
            activeTimerText.text = time.ToString();

            // ��������, ���� ������ �� ������ �� ����
            while (time > 0)
            {
                yield return new WaitForSeconds(1);
                time--;
                activeTimerText.text = time.ToString();
            }

            // ������������ �����������
            activeIndicator.color = Color.red;
            inactiveIndicator.color = Color.green;

            // ������ �������� ���������
            if (isIndicator1Active)
            {
                activeTimerText = timerText2;
                activeIndicator = indicator2;
                inactiveIndicator = indicator1;
            }
            else
            {
                activeTimerText = timerText1;
                activeIndicator = indicator1;
                inactiveIndicator = indicator2;
            }

            isIndicator1Active = !isIndicator1Active; // ����������� ����
        }
    }

    void SetIndicators(Color color)
    {
        indicator1.color = color;
        indicator2.color = color;
    }
}