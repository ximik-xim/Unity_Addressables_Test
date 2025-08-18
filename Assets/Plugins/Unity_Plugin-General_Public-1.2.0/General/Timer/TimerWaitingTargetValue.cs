using System;
using System.Collections;
using UnityEngine;

public class TimerWaitingTargetValue : MonoBehaviour
{
   private IEnumerator _enumerator;

   private bool _isStartTimer = false;
   public bool IsStartTimer => _isStartTimer;
   
   public event Action OnTimerComplited;

   /// <summary>
   /// В секудах
   /// </summary>
   [SerializeField] 
   private float _targetValue;

   // /// <summary>
   // /// В секудах
   // /// </summary>
   // [SerializeField] 
   // private float _intervalCounter;
   
   public void StartTimer()
   {
      if (_isStartTimer == true)
      {
         BreackTimer();
      }

      _isStartTimer = true;
      
      _enumerator = Timer();
      StartCoroutine(_enumerator);
   }

   public void BreackTimer()
   {
      StopCoroutine(_enumerator);
      _isStartTimer = false;
   }

   private IEnumerator Timer()
   {
      yield return new WaitForSeconds(_targetValue);
      
      _isStartTimer = false;
      OnTimerComplited?.Invoke();
   }
}
