using UniRx;

namespace Entrance.Model
{
    public class MusicGameModel
    {
        // **************************************************
        // �m�[�c����
        // **************************************************
        private int perfect = 0;
        private int great = 0;
        private int good = 0;
        private int bad = 0;
        private int miss = 0;
        public void PlusPerfect()
        {
            perfect++;
        }
        public void PlusGreat()
        {
            great++;
        }
        public void PlusGood()
        {
            good++;
        }
        public void PlusBad()
        {
            bad++;
        }
        public void PlusMiss()
        {
            miss++;
        }
        public int Perfect { get => perfect; }
        public int Great { get => great; }
        public int Good { get => good; }
        public int Bad { get => bad; }
        public int Miss { get => miss; }

        // **************************************************
        // [UniRx�Ď��C�x���g]�R���{�C�x���g
        // **************************************************
        // �R���{
        private readonly ReactiveProperty<int> combo = new ReactiveProperty<int>(0);
        public IReadOnlyReactiveProperty<int> Combo => combo;

        public void PlusCombo()
        {
            combo.Value++;
            if (maxCombo < combo.Value)
            {
                maxCombo = combo.Value;
            }
        }
        public void ResetCombo()
        {
            combo.Value = 0;
        }
        // �ő�R���{
        private int maxCombo = 0;
        public int MaxCombo { get => maxCombo; }

        // **************************************************
        // [UniRx�Ď��C�x���g]�X�R�A�C�x���g
        // **************************************************
        private readonly ReactiveProperty<float> score = new ReactiveProperty<float>(0.0f);
        public IReadOnlyReactiveProperty<float> Score => score;

        public void NormalScore()
        {
            this.score.Value += 420.0f;
        }
        public void FeverScore()
        {
            this.score.Value += 950.0f;
        }
    }
}
