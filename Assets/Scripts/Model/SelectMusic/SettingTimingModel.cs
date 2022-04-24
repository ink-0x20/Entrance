using Entrance.Common;

using UniRx;

namespace Entrance.Model
{
    public class SettingTimingModel
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
        // [UniRx�Ď��C�x���g]�^�C�~���O
        // **************************************************
        private readonly ReactiveProperty<int> timing = new ReactiveProperty<int>(1);
        public IReadOnlyReactiveProperty<int> Timing => timing;
        public void SetTiming()
        {
            switch (this.settingNumber.Value)
            {
                case 2:
                    // +1
                    if (this.timing.Value + 1 <= SystemConfig.MAX_TIMING)
                    {
                        this.timing.Value += 1;
                    }
                    break;
                case 3:
                    // -1
                    if (SystemConfig.MIN_TIMING <= this.timing.Value - 1)
                    {
                        this.timing.Value -= 1;
                    }
                    break;
            }
        }

        // ********************************************************************************
        // ��������
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // �^�C�~���O
            this.timing.Value = sessionCommon.Timing;
        }
    }
}
