using UnityEngine;

/// <summary>
/// Доп параметры, которые будут находиться на обьекте
/// (нужно на случай, если по особенному надо настроить обьект или его вообще не надо трогать)
/// </summary>
public class SettingsSelectObjectOnTopKeyboard : MonoBehaviour
{
    /// <summary>
    /// Нужен что бы можно было доп логику реализовать
    /// </summary>
    [SerializeField]
    private DKOKeyAndTargetAction _dko;

    public DKOKeyAndTargetAction Dko => _dko;
    
    /// <summary>
    /// Можно ли этот обьект вообще трогать
    /// (Если не надо, то надо этот скрипт добавить на обьект и отключить это поле)
    /// </summary>
    /// <returns></returns>
    public bool IsUseThisGM => _isUseThisGM;
    [SerializeField]
    private bool _isUseThisGM = true;

    
    /// <summary>
    /// Можно ли изменять размеры, позицию у обьекта
    /// </summary>
    public bool IsUseModification => _isUseModification;
    [SerializeField]
    private bool _isUseModification = true;

    /// <summary>
    /// Образец желаймой позиции и размеров обьекта
    /// (если оставить пустым, будет взят образец из логики уст. позиции)
    /// </summary>
    public RectTransform Sample => _sample;
    [SerializeField]
    private RectTransform _sample;
    
    
    /// <summary>
    /// Если включено, то будет игнорировать заданные координаты у шаблона(с шаблона будет браться только размер)
    /// и стараться автоматически установить обьект над клавиатурой 
    /// </summary>
    public bool IsSetObjectPosTopKeyboard => _isSetObjectPosTopKeyboard;
    [SerializeField]
    private bool _isSetObjectPosTopKeyboard = true;
}
