
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Нужен что бы заполнить Dropdown данными, разными способами
/// </summary>
public abstract class AbsDropdownGetOptionData : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;


    public abstract List<Dropdown.OptionData> GetListKeyDropdown();
}
