using System;
using UnityEngine;

using Entrance.Common;
using Entrance.Utils;

using TMPro;

namespace Entrance.View
{
    public class SettingSevereLevelView : MonoBehaviour
    {
        // **************************************************
        // �C���X�y�N�^�I�u�W�F�N�g
        // **************************************************
        // �ݒ�\��
        [SerializeField]
        private TMP_Text settingData = default;
        // �ݒ�I���A�C�R��
        [SerializeField]
        private GameObject setting1 = default;
        [SerializeField]
        private GameObject setting2 = default;
        [SerializeField]
        private GameObject setting3 = default;

        // **************************************************
        // �C�x���g���X�i�[��`
        // **************************************************
        // �ݒ�I���C�x���g
        public Action selectSettingListener1;
        public Action selectSettingListener2;
        public Action selectSettingListener3;
        // �ݒ茈��C�x���g
        public Action decideListener;
        public Action cancelListener;

        // **************************************************
        // �I���E��I���X�P�[��
        // **************************************************
        private Vector3 selectScale = new Vector3(2.3f, 2.3f, 1.0f);
        private Vector3 nonSelectScale = new Vector3(1.81f, 1.8f, 1.0f);

        // ********************************************************************************
        // ��������
        // ********************************************************************************
        public void Initialize(SessionCommon sessionCommon)
        {
            // ********************************************************************************
            // ���݂̐ݒ��K�p
            // ********************************************************************************
            SetSevereLevel(sessionCommon.SevereLevel);
        }

        void Update()
        {
            // ********************************************************************************
            // �A�v���I������
            // ********************************************************************************
            KeyboardPressUtils.ExitApplication();

            // ********************************************************************************
            // ���͊Ď�
            // ********************************************************************************
            if (KeyboardPressUtils.All4())
            {
                SystemUtils.SafeCall(selectSettingListener3);
            }
            if (KeyboardPressUtils.All5())
            {
                SystemUtils.SafeCall(selectSettingListener1);
            }
            if (KeyboardPressUtils.All6())
            {
                SystemUtils.SafeCall(selectSettingListener2);
            }
            if (KeyboardPressUtils.Enter())
            {
                SystemUtils.SafeCall(decideListener);
            }
            if (KeyboardPressUtils.All0())
            {
                SystemUtils.SafeCall(cancelListener);
            }
        }

        // ********************************************************************************
        // �ݒ��I��
        // ********************************************************************************
        public void SelectSetting(int settingNumber)
        {
            switch (settingNumber)
            {
                case 1:
                    setting1.transform.localScale = selectScale;
                    setting2.transform.localScale = nonSelectScale;
                    setting3.transform.localScale = nonSelectScale;
                    break;
                case 2:
                    setting1.transform.localScale = nonSelectScale;
                    setting2.transform.localScale = selectScale;
                    setting3.transform.localScale = nonSelectScale;
                    break;
                case 3:
                    setting1.transform.localScale = nonSelectScale;
                    setting2.transform.localScale = nonSelectScale;
                    setting3.transform.localScale = selectScale;
                    break;
            }
        }

        // ********************************************************************************
        // �V�r�A���x����\��
        // ********************************************************************************
        public void SetSevereLevel(int severeLevel)
        {
            settingData.SetText(severeLevel.ToString());
        }
    }
}
