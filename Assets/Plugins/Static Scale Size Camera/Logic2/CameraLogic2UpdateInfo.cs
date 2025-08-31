using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 /// <summary>
 /// В наследники этого класса будут передоваться данные об изменений разрешения экрана
 /// </summary>
public abstract class CameraLogic2UpdateInfo : MonoBehaviour
 {
  public abstract void UpdateData(DataEventUpdateCameraV2 data);

 }
