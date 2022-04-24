using System;
using UnityEngine;

using Entrance.Common;
using Entrance.Model;

using UniRx;

namespace Entrance.Prezenter
{
    public abstract class BaseScreenPrezenter : MonoBehaviour
    {
        // **************************************************
        // �X�N���[���J�ڃC�x���g
        // **************************************************
        public Action screenChangeListener;

        // **************************************************
        // [UniRx�Ď��C�x���g]�X�N���[���J�ڎ��̃f�[�^�A�g�C�x���g
        // **************************************************
        protected readonly ReactiveProperty<ScreenDataLinkage> screenDataLinkage = new ReactiveProperty<ScreenDataLinkage>();
        public IReadOnlyReactiveProperty<ScreenDataLinkage> ScreenDataLinkage => screenDataLinkage;

        // **************************************************
        // ���ʃZ�b�V����
        // **************************************************
        protected SessionCommon sessionCommon = null;

        // ********************************************************************************
        // abstract���\�b�h��`
        // ********************************************************************************
        // ���������ɌĂ΂�܂�
        public abstract void Initialize();

        // OnScreenOepn�̒��O�ɌĂ΂�܂�
        public abstract void OnScreenOepnBefore();
        // ���g�ɑJ�ڂ��Ă���ۂɌĂ΂�܂�
        public abstract void OnScreenOepn();
        // OnScreenOepn�̒���ɌĂ΂�܂�
        public abstract void OnScreenOepnAfter();

        // ����Screen�ɑJ�ڂ���ۂɌĂ΂�܂�
        public abstract void OnScreenChange();

        // �O��Screen�ɖ߂�ۂɓǂ܂�܂�
        public abstract void OnScreenBackSrc();

        // OnScreenBackDst�̒��O�ɌĂ΂�܂�
        public abstract void OnScreenBackDstBefore();
        // �߂�ɂ���Ď��g�ɑJ�ڂ��Ă���ۂɌĂ΂�܂�
        public abstract void OnScreenBackDst();

        // ���g���Ǘ����Ă���Window������ۂɌĂ΂�܂�
        public abstract void OnWindowClose();
        // Android�o�b�N�L�[�������ꂽ�ۂɌĂ΂�܂�
        public abstract void AndroidBack();

        // ********************************************************************************
        // �E�B���h�E�����N�����ɃC�x���g�����Ԃɏ�������
        // ********************************************************************************
        public void ScreenOpenSync(SessionCommon sessionCommon)
        {
            // �Z�b�V������ۑ�
            this.sessionCommon = sessionCommon;
            // ��������
            Initialize();
            // �X�N���[�������O����
            OnScreenOepnBefore();
            // �X�N���[������
            OnScreenOepn();
            // �X�N���[�������㏈��
            OnScreenOepnAfter();
        }

        // ********************************************************************************
        // �X�N���[���\����؂�ւ���
        // ********************************************************************************
        public void SetActive(bool activeFlg)
        {
            gameObject.SetActive(activeFlg);
            if (!activeFlg)
            {
                screenDataLinkage.Value = new ScreenDataLinkage(activeFlg);
            }
        }
    }
}
