using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackManager : MonoBehaviour
{
    #region Variable
    public List<GameObject> _collectedStack = new List<GameObject>();
    public StackEnum StackEnum;
    [SerializeField] private Transform ammonTransfrom;

    private DollarAdderOnStackCommand _dollarAdderOnStackCommand;
    public BulletBoxAdderOnStackCommand _bulletBoxAdderOnStackCommand;
    private ReturnBulletBoxesCommand _returnBulletBoxesCommand;
    private ReturnMoneyComman _returnMoneyComman;

    #endregion

    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        _dollarAdderOnStackCommand = new DollarAdderOnStackCommand(_collectedStack);
        _bulletBoxAdderOnStackCommand = new BulletBoxAdderOnStackCommand(this, _collectedStack);
        _returnBulletBoxesCommand = new ReturnBulletBoxesCommand(_collectedStack);
        _returnMoneyComman = new ReturnMoneyComman(_collectedStack);
    }
    public void MoneyorBulletBoxAnim()
    {
        if (StackEnum == StackEnum.BulletBox)
        {
            _returnBulletBoxesCommand.Execute();
        }         
        else
        {
            _returnMoneyComman.Execute();
        }          
    }
    public void OnInteractionDollarCollectable(GameObject collectableGameObject)
    {
        if (_collectedStack.Count < 25)
            _dollarAdderOnStackCommand.Execute(collectableGameObject,gameObject);
    }
    public IEnumerator FillBulletBoxStack()
    {
        while (_collectedStack.Count < 6)
        {
            var bulletBox = PoolSignals.Instance.onGetBulletBox?.Invoke();
            _bulletBoxAdderOnStackCommand.Execute(bulletBox);
            yield return new WaitForSecondsRealtime(.2f);
        }
    }
}