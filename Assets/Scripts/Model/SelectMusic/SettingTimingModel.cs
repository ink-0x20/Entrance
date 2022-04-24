using Entrance.Common;

using UniRx;

namespace Entrance.Model
{
    public class SettingTimingModel
    {
        // **************************************************
        // [UniRx監視イベント]設定選択
        // **************************************************
        // 設定管理番号
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
        // [UniRx監視イベント]タイミング
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
        // 初期処理
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // タイミング
            this.timing.Value = sessionCommon.Timing;
        }
    }
}
