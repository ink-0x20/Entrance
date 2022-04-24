using Entrance.Common;

using UniRx;

namespace Entrance.Model
{
    public class SettingNotesSpeedModel
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
                case 4:
                    this.settingNumber.Value = settingNumber;
                    break;
                case 5:
                    this.settingNumber.Value = settingNumber;
                    break;
            }
        }

        // **************************************************
        // [UniRx監視イベント]ノーツスピード
        // **************************************************
        private readonly ReactiveProperty<int> notesSpeed = new ReactiveProperty<int>(1);
        public IReadOnlyReactiveProperty<int> NotesSpeed => notesSpeed;
        public void SetNotesSpeed()
        {
            switch (this.settingNumber.Value)
            {
                case 2:
                    // +0.1
                    if (this.notesSpeed.Value + 1 <= SystemConfig.MAX_NOTES_SPEED)
                    {
                        this.notesSpeed.Value += 1;
                    }
                    break;
                case 3:
                    // -0.1
                    if (SystemConfig.MIN_NOTES_SPEED <= this.notesSpeed.Value - 1)
                    {
                        this.notesSpeed.Value -= 1;
                    }
                    break;
                case 4:
                    // +1
                    if (SystemConfig.MAX_NOTES_SPEED < this.notesSpeed.Value + 10)
                    {
                        this.notesSpeed.Value = SystemConfig.MAX_NOTES_SPEED;
                    }
                    else if (this.notesSpeed.Value + 10 <= SystemConfig.MAX_NOTES_SPEED)
                    {
                        this.notesSpeed.Value += 10;
                    }
                    break;
                case 5:
                    // -1
                    if (SystemConfig.MIN_NOTES_SPEED < this.notesSpeed.Value - 10)
                    {
                        this.notesSpeed.Value -= 10;
                    }
                    else if (this.notesSpeed.Value - 10 <= SystemConfig.MIN_NOTES_SPEED)
                    {
                        this.notesSpeed.Value = SystemConfig.MIN_NOTES_SPEED;
                    }
                    break;
            }
        }

        // ********************************************************************************
        // 初期処理
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // ノーツスピード
            this.notesSpeed.Value = sessionCommon.NotesSpeed;
        }
    }
}
