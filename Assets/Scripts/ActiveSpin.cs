using UnityEngine;

public class ActiveSpin : MonoBehaviour
{
    private Animator _animator;
    private int _currentCylinder = 0;
    private bool _isSpinned = false;

    [SerializeField] private Spin[] _cylinders;

    #region MonoBehaviour methods
    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (_isSpinned == false || CheckAllCylinders() == false) return;


        if (CheckCylinder(_currentCylinder) && _currentCylinder < _cylinders.Length - 1)
        {
            _cylinders[_currentCylinder + 1].StartSpin();
            _currentCylinder++;
        }
        else
        {
            _currentCylinder = 0;
            _isSpinned = false;
        }

    }

    private void OnMouseDown()
    {
        if (_isSpinned == true) return;
        if (_animator.GetCurrentAnimatorStateInfo(0).IsName("ActiveSpin") == false)
        {
            _isSpinned = true;
            _animator.SetTrigger("Spin");
            _cylinders[_currentCylinder].StartSpin();
        }
    }
    #endregion

    private bool CheckCylinder(int numb)
    {
        if (_cylinders[numb].IsSpinning)
        {
            return false;
        }
        return true;
    }

    private bool CheckAllCylinders()
    {
        for (int i = 0; i < _cylinders.Length; i++)
        {
            if (_cylinders[i].IsSpinning)
            {
                return false;
            }
        }
        return true;
    }
}
