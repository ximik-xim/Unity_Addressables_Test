using UnityEngine;

public class ExampleGetInMBS_DKO : MonoBehaviour
{
    [SerializeField] 
    private GetDKOPatch _patchDKO;
   
    private void Awake()
    {
        if (_patchDKO.Init == false)
        {
            _patchDKO.OnInit += OnInitStoragePanel;
            return;
        }

        GetDataDKO();
    }

    private void OnInitStoragePanel()
    {
        _patchDKO.OnInit -= OnInitStoragePanel;
        GetDataDKO();
    }

    private void GetDataDKO()
    {
        //Т.к он DKO ничего не возвращает, то на этом все
        _patchDKO.GetDKO();
    }
}
