using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Логика кодов через консоль(аля промокоды и т.д)
/// </summary>
public class InputConsoleCode : MonoBehaviour
{  
   public event Action<string> OnInputCode;
   public void SetCode(string code)
   {
      OnInputCode?.Invoke(code);
   }
}
