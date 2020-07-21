using UnityEngine;
using UnityEngine.UI;

public class SpeedSliders : MonoBehaviour
{

    public PlayerTankMove tankScript;
    public Slider targetSlider;
    public Slider currentSlider;
    // Start is called before the first frame update
    void Awake()
    {
        targetSlider.maxValue = tankScript.maxSpeed;
        targetSlider.minValue = 0;
        currentSlider.maxValue = tankScript.maxSpeed;
        currentSlider.minValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        targetSlider.value = tankScript.targetVector.magnitude;
        currentSlider.value = tankScript.currentVector.magnitude;
    }
}
