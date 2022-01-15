using UnityEngine;
using UnityEngine.Events;

public class PlayerAnimationEventReceiver : MonoBehaviour
{
    public UnityEvent<bool> onComboWindowChanged;
    public UnityEvent onCheckCombo;
    public UnityEvent onEndAttack;
    public UnityEvent onTakeCell;
    public UnityEvent onDropCell;
    
    public void ComboWindowOpen()
    {
        onComboWindowChanged.Invoke(true);
    }

    public void ComboWindowClose()
    {
        onComboWindowChanged.Invoke(false);
    }

    public void CheckCombo()
    {
        onCheckCombo.Invoke();
    }

    public void EndAttack()
    {
        onEndAttack.Invoke();
    }

    public void TakeCell()
    {
        onTakeCell.Invoke();
    }

    public void DropCell()
    {
        onDropCell.Invoke();
    }
}