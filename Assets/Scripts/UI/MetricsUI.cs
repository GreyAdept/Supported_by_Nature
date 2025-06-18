using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetricsUI : MonoBehaviour
{
    private TurnManager turnManager;
    [SerializeField] private Slider waterQualitySlider;
    [SerializeField] private Slider pollutionLevelSlider;
    [SerializeField] private Slider biodiversityLevelSlider;
    private void Start()
    {
        //turnManager = TurnManager.Instance;
        //turnManager.onMetricsUpdated.AddListener(UpdateMetricsUI);
    }
    private void UpdateMetricsUI(Dictionary<MetricType, float> metrics)
    {
        foreach(var metric in metrics)
        {
            switch(metric.Key)
            {
                case MetricType.WaterQuality:
                    waterQualitySlider.value = metric.Value/100;
                    break;
                case MetricType.PollutionLevel:
                    pollutionLevelSlider.value = metric.Value/100;
                    break;
                case MetricType.BiodiversityLevel:
                    biodiversityLevelSlider.value = metric.Value/100;
                    break;
            }
        }
    }
}
