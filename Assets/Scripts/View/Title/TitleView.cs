using System;
using UnityEngine;

using Entrance.Utils;

namespace Entrance.View
{
    public class TitleView : MonoBehaviour
    {
        // **************************************************
        // �C���X�y�N�^�I�u�W�F�N�g
        // **************************************************
        // �J�n�e�L�X�g
        [SerializeField]
        private GameObject startText = default;

        // **************************************************
        // �C�x���g���X�i�[��`
        // **************************************************
        // �J�ڃC�x���g
        public Action sceneListener;

        void Update()
        {
            // ********************************************************************************
            // �A�v���I������
            // ********************************************************************************
            KeyboardPressUtils.ExitApplication();

            // ********************************************************************************
            // ���͊Ď�
            // ********************************************************************************
            if (KeyboardPressUtils.Enter())
            {
                SystemUtils.SafeCall(sceneListener);
                startText.SetActive(false);
            }
        }
    }
}
