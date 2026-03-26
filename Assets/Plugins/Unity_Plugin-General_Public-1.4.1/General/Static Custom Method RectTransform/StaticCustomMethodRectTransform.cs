using UnityEngine;

public class StaticCustomMethodRectTransform : MonoBehaviour
{
    /// <summary>
    /// Вернет верхнюю и нижнюю координату границы данного обьекта (RectTransform)
    /// (В ГЛОБАЛЬНЫХ КООРД)
    /// x - верхняя граница
    /// y - нижняя граница
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetGlobalPositRectUpAndDown(RectTransform targetGmTransform)
    {
        //расстояние от центра Pivot до нижний границы обьекта 
        float distanceDownGm = targetGmTransform.rect.height * targetGmTransform.pivot.y;
        //Получаю именно координату Y нижний границы обьекта
        float posDownGm = targetGmTransform.position.y - distanceDownGm;
        //Получаю именно координату Y верхней границы обьекта
        float posUpGm = posDownGm + targetGmTransform.rect.height;

        return new Vector2(posUpGm, posDownGm);
    }
    
    /// <summary>
    /// Вернет верхнюю и нижнюю координату границы данного обьекта (RectTransform)
    /// (В ЛОКАЛЬНЫХ КООРД)
    /// x - верхняя граница
    /// y - нижняя граница
    /// </summary>
    /// <returns></returns>
    public static Vector2 GetLocalPositRectUpAndDown(RectTransform targetGmTransform)
    {
        //расстояние от центра Pivot до нижний границы обьекта 
        float distanceDownGm = targetGmTransform.rect.height * targetGmTransform.pivot.y;
        //Получаю именно координату Y нижний границы обьекта
        float posDownGm = targetGmTransform.localPosition.y - distanceDownGm;
        //Получаю именно координату Y верхней границы обьекта
        float posUpGm = posDownGm + targetGmTransform.rect.height;

        return new Vector2(posUpGm, posDownGm);
    }
}
