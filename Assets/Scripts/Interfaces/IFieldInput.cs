using UnityEngine;
using UnityEngine.InputSystem;
public interface IFieldInput
{
    /// <summary>
    /// ����������
    /// </summary>
    void Initialize();
    /// <summary>
    /// �������R�[���o�b�N�Ƃ��ēo�^
    /// </summary>
    void SetProcesses();
    /// <summary>
    /// �ړ��A�J�[�\���Ȃǂ̏���
    /// </summary>
    void OnNavigate(InputAction.CallbackContext context);
    /// <summary>
    /// ����@�\�̏���
    /// </summary>
    void OnDecide(InputAction.CallbackContext context);
    /// <summary>
    /// �L�����Z������
    /// </summary>
    void OnCancel(InputAction.CallbackContext context);
}
