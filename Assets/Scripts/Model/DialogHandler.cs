using UnityEngine;
using UnityEngine.UI;

using Entrance.Utils;

namespace Entrance.Model
{
    public class DialogHandler : MonoBehaviour
    {
        // **************************************************
        // インスペクタオブジェクト
        // **************************************************
        // ダイアログオブジェクト
        [SerializeField]
        private Text title = default;
        [SerializeField]
        private Text description = default;
        [SerializeField]
        private Button button = default;
        [SerializeField]
        private Text buttonText = default;

        // **************************************************
        // ダイアログプレハブ
        // **************************************************
        private static GameObject dialogPrefab = null;

        // ********************************************************************************
        // ダイアログを表示する
        // ********************************************************************************
        public static Button ShowDialog(string title, string description, string buttonText)
        {
            // ダイアログプレハブ取得
            if (dialogPrefab == null)
            {
                dialogPrefab = Resources.Load("DialogPrefab") as GameObject;
            }
            // オブジェクト生成
            GameObject dialogObject = Instantiate(dialogPrefab);
            DialogHandler handler = dialogObject.GetComponent<DialogHandler>();

            // タイトル適用
            handler.title.text = title;
            // 文言適用
            handler.description.text = description;
            // 文言サイズを調整
            RectTransform rectTransform = handler.description.GetComponent<RectTransform>();
            Vector2 size = rectTransform.sizeDelta;
            size.y = StringUtils.CountLine(description) * 30;
            rectTransform.sizeDelta = size;
            // ボタンのテキスト適用
            handler.buttonText.text = buttonText;
            // ダイアログ破棄イベント登録
            handler.button.onClick.AddListener(() =>
            {
                Destroy(handler.gameObject);
            });
            return handler.button;
        }
    }
}
