using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Логика InputField с выподающими подсказками
/// </summary>
public class LogicDropdownInputFieldCustom : MonoBehaviour
{
    /// <summary>
    /// Список вариантов при открытии панели
    /// </summary>
    [SerializeField]
    private AbsGetListVariantText _getListVariantTextOpen;
    
    /// <summary>
    /// Список вариантов при вводе текста
    /// </summary>
    [SerializeField]
    private AbsGetListVariantText _getListVariantText;
    
    [SerializeField]
    private ElementVariantText _prefabElementVariant;
    
    //Кол-во элементов которое будет в выпод. окне
    [SerializeField]
    private int _countVisibleElement = 10;

    [SerializeField]
    private GameObject _parentVariant;

    [SerializeField]
    private GameObject _panelVariant;
    
    //Созданные префабы вариантов подсказок
    private List<ElementVariantText> _variantElement = new List<ElementVariantText>();
    
    private ElementVariantText _currentSelectVariant;
    public event Action OnUpdateCurrentSelectVariant;
    
    /// <summary>
    /// Абстракция для работы с текстом
    /// (нужна что бы работать с text и text MeshPro)
    /// </summary>
    [SerializeField]
    private AbsGetAndSetText _text;

    /// <summary>
    /// Поиск будет по ключевым словам или через сравнение с предложением
    /// (если включено, то поиск будет по ключевым словам)
    /// </summary>
    [SerializeField]
    private bool _isSearchByKeywords;
    
    /// <summary>
    /// Будет ли подсветка слов
    /// </summary>
    [SerializeField]
    private bool _useHighlightingWords = true;
    
    [SerializeField]
    private Color _colorWordsHighlighting = new Color(0.1176f, 0.5647f, 1f, 1f);
    
    [SerializeField]
    private AbsTriggerEventInputFieldCustom _triggerInputField;

    
    
    private void Awake()
    {
        _panelVariant.SetActive(true);
        
        var startVariantText = _getListVariantTextOpen.GetVariantText();
        for (int i = 0; i < startVariantText.Count; i++) 
        {
            var element = CreateElement();
            _variantElement.Add(element);
            
            element.SetId(i);
            element.SetTextVisible(startVariantText[i]);

            element.OnSelectItem += OnSelectVariant;
        }
        
        //Если кол-во видимых элементов меньше чем минимально, то добавляем этих элементов
        if (_variantElement.Count < _countVisibleElement) 
        {
            for (int i = _variantElement.Count; i < _countVisibleElement; i++)
            {
                var element = CreateElement();
                _variantElement.Add(element);
            
                element.SetId(i);
                element.SetTextVisible("");

                element.OnSelectItem += OnSelectVariant;

                element.DisactiveVisible();
            }
        }

        _panelVariant.SetActive(false);

        _triggerInputField.OnOpenInputField += OnOpenInputField;
        _triggerInputField.OnCloseInputField += OnCloseInputField;
        _triggerInputField.OnUpdateText += OnUpdateTextInputField;
    }

    //Когда нажали на окно ввода
    private void OnOpenInputField()
    {
        if (_text.GetText() != "" && _text.GetText().Length > 0)
        {
            OnUpdateTextInputField(_text.GetText());
        }
        else
        {
            LogicOnOpenInputField();
        }
    }

    /// <summary>
    /// Логика при нажатии открытия окна InputField
    /// (отдельно, так как, в опр. случае надо пропустить проверку и сразу вызвать)
    /// </summary>
    private void LogicOnOpenInputField()
    {
        var startVariantText = _getListVariantTextOpen.GetVariantText();
        if (startVariantText.Count == 0) 
        {
            _panelVariant.SetActive(false);
        }
        else
        {
            _panelVariant.SetActive(true);
            
            for (int i = 0; i < startVariantText.Count; i++)
            {
                _variantElement[i].transform.SetSiblingIndex(i);
                _variantElement[i].ActiveVisible();
                _variantElement[i].SetId(i);
                _variantElement[i].SetTextVisible(startVariantText[i]);
                _variantElement[i].SetTextReturn(startVariantText[i]);
            }

            if (startVariantText.Count < _countVisibleElement) 
            {
                for (int i = startVariantText.Count; i < _countVisibleElement; i++)
                {
                    _variantElement[i].transform.SetSiblingIndex(i);
                    _variantElement[i].SetId(i);
                    _variantElement[i].SetTextVisible("");
                    _variantElement[i].SetTextReturn("");
                    _variantElement[i].DisactiveVisible();
                }
            
            }
        }
    }
    
    //Когда убрали фокус с окна ввода
    private void OnCloseInputField()
    {
        _panelVariant.SetActive(false);
    }

    public void UpdateTextVariant()
    {
        OnUpdateTextInputField(_text.GetText());
    }

    public void OpenPanel()
    {
        _panelVariant.SetActive(true);
    }
    
    public void ClosePanel()
    {
        _panelVariant.SetActive(false);
    }
    
    
    /// <summary>
    /// При изменении текста
    /// (Event отрабатывает раньше чем текст уст в поле, по этому нельзя исп _text.GetText() )
    /// </summary>
    private void OnUpdateTextInputField(string text)
    {
        if (text != "" && text.Length > 0) 
        {
            _panelVariant.SetActive(true);

            if (_isSearchByKeywords == true) 
            {
                //тут разделяю строку на слова
                string[] textKeywords = text.Split(' ');
                //Список подсказок
                List<VariantHints> potentialPrompts = new List<VariantHints>();
                List<string> listVariantText = _getListVariantText.GetVariantText();
                
                //тут проходимся по каждому заданному варианту подсказки отдельно
                for(int i = 0; i < listVariantText.Count; i++)
                {
                    string variantText = listVariantText[i].ToLower();
                    string copyVariantText = listVariantText[i];
                    
                    bool isCorrect = true;
                    float overlapValue = 0;
                    
                    //проходимся по всему введенному тексту(по словам)
                    for (int j = 0; j < textKeywords.Length; j++)  
                    {
                        //если в подсказке нету указаного слова то идем дальше
                        if (variantText.Contains(textKeywords[j].ToLower()) == false)  
                        {
                            //подумать может сдесь и прирывать тогда цикл(а смысл идти дальше, если уже слово  неподходит)
                            isCorrect = false;
                            //continue;
                        } 
                        else 
                        {
                            //если в посказке есть это слово, то добавляем значение к уровню сходства
                            overlapValue += ((float)textKeywords[j].Length) / ((float)variantText.Length);
                            
                            //тут или цыклом проходиться надо по всем пройденм словм или дуамать как по другому сделать
                            //Если выделение слов включено
                            if (_useHighlightingWords == true && textKeywords[j] != "")
                            {
                                //copyVariantText = copyVariantText.Replace(textKeywords[j], $"<color=#{ColorUtility.ToHtmlStringRGBA(_colorWordsHighlighting)}>{textKeywords[j]}</color>", StringComparison.OrdinalIgnoreCase);
                                copyVariantText = Regex.Replace(copyVariantText, Regex.Escape(textKeywords[j]), match => $"<color=#{ColorUtility.ToHtmlStringRGBA(_colorWordsHighlighting)}>{match.Value}</color>", RegexOptions.IgnoreCase);
                            }
                        }
                    }
                    //Если подсказка прошла проверку и есть хоть 1 совподение по букве, то добавляем элемент в буффер с возможными подсказками
                    if (isCorrect == true)
                    {
                        potentialPrompts.Add(new VariantHints(copyVariantText, listVariantText[i], overlapValue));
                    }
                }
                
                //сортировка по уровню сходства 
                potentialPrompts.Sort(ComparatorPrompt);
                //выводим огрниченное кол-во подсказок

                int countVariantElement = Mathf.Min(_countVisibleElement, potentialPrompts.Count);
                for(int i = 0; i < countVariantElement; i++) 
                {
                    //Устанавливаем подсказки по прядку убывание
                    _variantElement[i].transform.SetSiblingIndex(i);
                    _variantElement[i].ActiveVisible();
                    _variantElement[i].SetId(i);
                    _variantElement[i].SetTextVisible(potentialPrompts[i].TextVisible);
                    _variantElement[i].SetTextReturn(potentialPrompts[i].TextReturn);
                }

                for (int i = countVariantElement; i < _countVisibleElement; i++) 
                {
                    _variantElement[i].transform.SetSiblingIndex(i);
                    _variantElement[i].SetId(i);
                    _variantElement[i].SetTextVisible("");
                    _variantElement[i].SetTextReturn("");
                    _variantElement[i].DisactiveVisible();
                }
            }
            else
            {
                //если не выполнять поиск по ключевым словам, то проверяем тупо как строку
                var potentialPrompts = new List<VariantHints>();
                List<string> listVariantText = _getListVariantText.GetVariantText();
                
                //тут проходимся по каждому заданному варианту подсказки отдельно
                for (int i = 0; i < listVariantText.Count; i++) 
                {
                    string variantText = listVariantText[i].ToLower();
                    string copyVariantText = listVariantText[i];

                    float overlapValue = 0;
                    overlapValue += ((float)text.Length) / ((float)variantText.Length);
                    
                    //проверяем совпадает ли введенный текст и текст в подсказке
                    if (variantText.Contains(text.ToLower()) == true)  
                    {
                        //Если выделение слов включено
                        if (_useHighlightingWords == true)
                        {
                            //copyVariantText = copyVariantText.Replace(text, $"<color=#{ColorUtility.ToHtmlStringRGBA(_colorWordsHighlighting)}>{text}</color>", StringComparison.OrdinalIgnoreCase);
                            copyVariantText = Regex.Replace(copyVariantText, Regex.Escape(text), match => $"<color=#{ColorUtility.ToHtmlStringRGBA(_colorWordsHighlighting)}>{match.Value}</color>", RegexOptions.IgnoreCase);
                        }

                        potentialPrompts.Add(new VariantHints(copyVariantText, listVariantText[i], overlapValue));
                    }
                }
                //опять сортировка по уровню совпадение 
                potentialPrompts.Sort(ComparatorPrompt);

                //выводим огрниченное кол-во подсказок
                int countVariantElement = Mathf.Min(_countVisibleElement, potentialPrompts.Count);
                for(int i = 0; i < countVariantElement; i++) 
                {
                    //Устанавливаем подсказки по прядку убывание
                    _variantElement[i].transform.SetSiblingIndex(i);
                    _variantElement[i].ActiveVisible();
                    _variantElement[i].SetId(i);
                    _variantElement[i].SetTextVisible(potentialPrompts[i].TextVisible);
                    _variantElement[i].SetTextReturn(potentialPrompts[i].TextReturn);
                }

                for (int i = countVariantElement; i < _countVisibleElement; i++) 
                {
                    _variantElement[i].transform.SetSiblingIndex(i);
                    _variantElement[i].SetId(i);
                    _variantElement[i].SetTextVisible("");
                    _variantElement[i].SetTextReturn("");
                    _variantElement[i].DisactiveVisible();
                }
            }
        }
        else
        {
            LogicOnOpenInputField();
        }
    }

    private void OnSelectVariant(int id, string textVisible, string textReturn)
    {
        _currentSelectVariant = _variantElement[id];
        _text.SetText(textReturn);
        OnUpdateCurrentSelectVariant?.Invoke();

        //OnUpdateTextInputField(textReturn);
    }


    private ElementVariantText CreateElement()
    {
        return Instantiate(_prefabElementVariant, _parentVariant.transform);
    }

    int ComparatorPrompt(VariantHints first, VariantHints second) {
        if (first.OverlapValue > second.OverlapValue)
        {
            return -1;
        }

        return 1;
    }

    private void OnDestroy()
    {
        _triggerInputField.OnOpenInputField -= OnOpenInputField;
        _triggerInputField.OnCloseInputField -= OnCloseInputField;
        _triggerInputField.OnUpdateText -= OnUpdateTextInputField;

        foreach (var VARIABLE in _variantElement)
        {
            VARIABLE.OnSelectItem -= OnSelectVariant;
        }
    }
}
