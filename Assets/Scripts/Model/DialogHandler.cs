using UnityEngine;
using UnityEngine.UI;

using Entrance.Utils;

namespace Entrance.Model
{
    public class DialogHandler : MonoBehaviour
    {
        // **************************************************
        // �C���X�y�N�^�I�u�W�F�N�g
        // **************************************************
        // �_�C�A���O�I�u�W�F�N�g
        [SerializeField]
        private Text title = default;
        [SerializeField]
        private Text description = default;
        [SerializeField]
        private Button button = default;
        [SerializeField]
        private Text buttonText = default;

        // **************************************************
        // �_�C�A���O�v���n�u
        // **************************************************
        private static GameObject dialogPrefab = null;

        // ********************************************************************************
        // �_�C�A���O��\������
        // ********************************************************************************
        public static Button ShowDialog(string title, string description, string buttonText)
        {
            // �_�C�A���O�v���n�u�擾
            if (dialogPrefab == null)
            {
                dialogPrefab = Resources.Load("DialogPrefab") as GameObject;
            }
            // �I�u�W�F�N�g����
            GameObject dialogObject = Instantiate(dialogPrefab);
            DialogHandler handler = dialogObject.GetComponent<DialogHandler>();

            // �^�C�g���K�p
            handler.title.text = title;
            // �����K�p
            handler.description.text = description;
            // �����T�C�Y�𒲐�
            RectTransform rectTransform = handler.description.GetComponent<RectTransform>();
            Vector2 size = rectTransform.sizeDelta;
            size.y = StringUtils.CountLine(description) * 30;
            rectTransform.sizeDelta = size;
            // �{�^���̃e�L�X�g�K�p
            handler.buttonText.text = buttonText;
            // �_�C�A���O�j���C�x���g�o�^
            handler.button.onClick.AddListener(() =>
            {
                Destroy(handler.gameObject);
            });
            return handler.button;
        }
    }
}
