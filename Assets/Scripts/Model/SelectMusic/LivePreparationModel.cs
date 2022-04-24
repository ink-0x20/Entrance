using UniRx;

namespace Entrance.Model
{
    public class LivePreparationModel
    {
        // **************************************************
        // [UniRx監視イベント]準備選択
        // **************************************************
        // 準備管理番号
        private readonly ReactiveProperty<int> preparationNumber = new ReactiveProperty<int>(1);
        public IReadOnlyReactiveProperty<int> PreparationNumber => preparationNumber;
        public void SelectPreparation(int preparationNumber)
        {
            switch (preparationNumber)
            {
                case 1:
                    this.preparationNumber.Value = preparationNumber;
                    break;
                case 2:
                    this.preparationNumber.Value = preparationNumber;
                    break;
                case 3:
                    this.preparationNumber.Value = preparationNumber;
                    break;
                case 4:
                    this.preparationNumber.Value = preparationNumber;
                    break;
            }
        }
    }
}
