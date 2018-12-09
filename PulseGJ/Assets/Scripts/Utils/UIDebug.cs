using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIDebug : MonoBehaviour {

    [System.Serializable]
    public struct Range
    {
        public float Min;
        public float Max;
    }

    [SerializeField]
    private Slider m_timeScaleSlider;

    public Range TimeScaleRange;
    

	
	public void OnTimeScaleChange()
    {
        Time.timeScale = TimeScaleRange.Min + m_timeScaleSlider.value * (TimeScaleRange.Max - TimeScaleRange.Min);
    }
}
