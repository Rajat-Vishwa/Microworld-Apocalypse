using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public PlayerPower player;
    public Slider slider;
    public RectTransform rectTransform;
    public float minWidthIndex = 920, maxWidthIndex = 750;
    public float leftGap = 20f;

    public float Map(float x, float in_min, float in_max, float out_min, float out_max)
    {
        return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
    }

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPower>();
        rectTransform = gameObject.GetComponent<RectTransform>();
        slider = gameObject.GetComponent<Slider>();
    }
    void Update()
    {
        slider.value = Map(player.Power, 0, player.MaxPower, 0, 1);

        float widthIndex = Map(player.MaxPower, player.minMaxPower, player.maxMaxPower, minWidthIndex, maxWidthIndex);
        rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Left, leftGap, widthIndex);
    }
}
