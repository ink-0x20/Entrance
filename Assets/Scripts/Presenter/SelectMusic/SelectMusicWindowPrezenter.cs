namespace Entrance.Prezenter
{
    public class SelectMusicWindowPrezenter : BaseWindowPrezenter
    {
        // ********************************************************************************
        // 初期化時に呼ばれます
        // ********************************************************************************
        public override void Initialize()
        {
            // ********************************************************************************
            // 特になし
            // ********************************************************************************
        }

        // ********************************************************************************
        // OnWindowOpenの直前に呼ばれます
        // ********************************************************************************
        public override void OnWindowOpenBefore()
        {
            // ********************************************************************************
            // 特になし
            // ********************************************************************************
        }

        // ********************************************************************************
        // 自身が開かれる際に呼ばれます
        // ********************************************************************************
        public override void OnWindowOpen()
        {
            // ********************************************************************************
            // 特になし
            // ********************************************************************************
        }

        // ********************************************************************************
        // OnWindowBackの直前に呼ばれます
        // ********************************************************************************
        public override void OnWindowBackBefore()
        {
        }

        // ********************************************************************************
        // 他のWindowが閉じることによって、自身が画面に表示される時に呼ばれます
        // ********************************************************************************
        public override void OnWindowBack()
        {
        }

        // ********************************************************************************
        // 他のWindowが開かれることによって、自身が画面から消える時に呼ばれます
        // ********************************************************************************
        public override void OnWindowHide()
        {
            // ********************************************************************************
            // 特になし
            // ********************************************************************************
        }

        // ********************************************************************************
        // 自身を閉じることによって、自身が画面から消える時に呼ばれます
        // ********************************************************************************
        public override void OnWindowClose()
        {
            // ********************************************************************************
            // 特になし
            // ********************************************************************************
        }

        // ********************************************************************************
        // 自身が管理しているスクリーンが遷移し始めた時に呼ばれます
        // ********************************************************************************
        public override void OnScreenChangeBefore()
        {
            // ********************************************************************************
            // 特になし
            // ********************************************************************************
        }

        // ********************************************************************************
        // 自身が管理しているスクリーンが遷移し終わった時に呼ばれます
        // ********************************************************************************
        public override void OnScreenChangeAfter()
        {
            // ********************************************************************************
            // 特になし
            // ********************************************************************************
        }
    }
}
