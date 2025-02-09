using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public Button startStopButton; // Кнопка Старт/Стоп
    public Image indicator1;       // Индикатор 1
    public Image indicator2;       // Индикатор 2
    public TMPro.TextMeshProUGUI timerText1;        // Текстовое поле 1
    public TMPro.TextMeshProUGUI timerText2;        // Текстовое поле 2

    private bool isRunning = false; // Флаг работы таймеров
    private Coroutine activeCoroutine; // Активная корутина
    private bool isIndicator1Active = true; // Флаг активности индикатора 1

    void Start()
    {
        // Устанавливаем начальные значения
        startStopButton.onClick.AddListener(OnStartStopButtonClick);
        SetIndicators(Color.yellow);
        timerText1.text = "0";
        timerText2.text = "0";
    }

    void OnStartStopButtonClick()
    {
        if (!isRunning)
        {
            // Запуск таймеров
            isRunning = true;
            startStopButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Стоп";

            // Запускаем первую корутину
            isIndicator1Active = true;
            indicator1.color = Color.green;
            indicator2.color = Color.red;
            timerText2.text = "0"; // Сбрасываем таймер второго индикатора

            activeCoroutine = StartCoroutine(TimerCoroutine(timerText1, indicator1, indicator2));
        }
        else
        {
            // Остановка таймеров
            isRunning = false;
            startStopButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Старт";
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
            // Генерация случайного времени от 10 до 20 секунд
            int time = Random.Range(10, 21);
            activeTimerText.text = time.ToString();

            // Ожидание, пока таймер не дойдет до нуля
            while (time > 0)
            {
                yield return new WaitForSeconds(1);
                time--;
                activeTimerText.text = time.ToString();
            }

            // Переключение индикаторов
            activeIndicator.color = Color.red;
            inactiveIndicator.color = Color.green;

            // Меняем активный индикатор
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

            isIndicator1Active = !isIndicator1Active; // Переключаем флаг
        }
    }

    void SetIndicators(Color color)
    {
        indicator1.color = color;
        indicator2.color = color;
    }
}