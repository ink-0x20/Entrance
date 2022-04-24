using Entrance.Common;

using UniRx;

namespace Entrance.Model
{
    public class SettingSevereLevelModel
    {
        // **************************************************
        // [UniRx�Ď��C�x���g]�ݒ�I��
        // **************************************************
        // �ݒ�Ǘ��ԍ�
        private readonly ReactiveProperty<int> settingNumber = new ReactiveProperty<int>(1);
        public IReadOnlyReactiveProperty<int> SettingNumber => settingNumber;
        public void SelectSetting(int settingNumber)
        {
            switch (settingNumber)
            {
                case 1:
                    this.settingNumber.Value = settingNumber;
                    break;
                case 2:
                    this.settingNumber.Value = settingNumber;
                    break;
                case 3:
                    this.settingNumber.Value = settingNumber;
                    break;
            }
        }

        // **************************************************
        // [UniRx�Ď��C�x���g]�V�r�A���x��
        // **************************************************
        private readonly ReactiveProperty<int> severeLevel = new ReactiveProperty<int>(1);
        public IReadOnlyReactiveProperty<int> SevereLevel => severeLevel;
        public void SetSevereLevel()
        {
            switch (this.settingNumber.Value)
            {
                case 2:
                    // +1
                    if (this.severeLevel.Value + 1 <= SystemConfig.MAX_JUDGE_LEVEL)
                    {
                        this.severeLevel.Value += 1;
                    }
                    break;
                case 3:
                    // -1
                    if (SystemConfig.MIN_JUDGE_LEVEL <= this.severeLevel.Value - 1)
                    {
                        this.severeLevel.Value -= 1;
                    }
                    break;
            }
        }

        // ********************************************************************************
        // ��������
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // �V�r�A���x��
            this.severeLevel.Value = sessionCommon.SevereLevel;
        }
    }
}
