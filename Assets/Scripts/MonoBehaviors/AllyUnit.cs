using UnityEngine;

public class AllyUnit : Unit
{
    [field:SerializeField]
    public AllyUnitStatus unit_status { get; private set; }
    [field: SerializeField]
    public UnitGrowthRate growth_rate { get; private set; }

    /// <summary>
    /// �X�e�[�^�X��ݒ肷��
    /// </summary>
    /// <param name="save_data">
    /// �X�e�[�^�X�f�[�^(�Z�[�u�f�[�^�`��)
    /// </param>
    public void SetStatus(AllyUnitSaveData save_data)
    {
        AllyUnitStatus status = new AllyUnitStatus(save_data);
        this.unit_status= status;
    }
    public void GainExp(int exp_amount)
    {

    }

    public void LevelUp()
    {

    }

    public void ClassPromote()
    {

    }
}