using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class EnemyHealthBar : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Image foreGroundImage;
    [SerializeField] private Image backGroundImage;
    
    private void Update() {
        transform.position = Camera.main.WorldToScreenPoint(target.position);
    }
    public void SetHealthBarEnemyPercent(float percent) {
        float parentWidth = GetComponent<RectTransform>().rect.width;
        float width = parentWidth * percent;
        foreGroundImage.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, width);
    }
}
