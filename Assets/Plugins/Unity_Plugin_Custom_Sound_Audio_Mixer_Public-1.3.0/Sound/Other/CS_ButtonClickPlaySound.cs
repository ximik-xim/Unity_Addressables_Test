using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CS_ButtonClickPlaySound : MonoBehaviour
{
 [SerializeField] 
 private AudioSource _source;

 [SerializeField] 
 private Button _button;

 private void Awake()
 {
  _button.onClick.AddListener(OnClick);

 }

 private void OnClick()
 {
  _source.Play();
 }

 private void OnDestroy()
 {
  _button.onClick.RemoveListener(OnClick);
 }
}
