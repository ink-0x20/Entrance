using UnityEngine;

using Entrance.Common;
using Entrance.Model;
using Entrance.View;

using UniRx;

namespace Entrance.Prezenter
{
    public class SelectDifficultPrezenter : BaseScreenPrezenter
    {
        // **************************************************
        // View
        // **************************************************
        [SerializeField]
        private SelectDifficultView selectDifficultView = default;

        // **************************************************
        // Model
        // **************************************************
        private SelectDifficultModel selectDifficultModel = new SelectDifficultModel();

        // ********************************************************************************
        // ���������ɌĂ΂�܂�
        // ********************************************************************************
        public override void Initialize()
        {
            // ********************************************************************************
            // View�̏������iUI���A�j���[�V�����O�̏�Ԃɂ���j
            // ********************************************************************************
            selectDifficultModel.Initialize(sessionCommon);
            selectDifficultView.Initialize(sessionCommon);

            // ********************************************************************************
            // �ǉ��Ő�������ׂ�UI�p�[�c�̐����Ɣz�u
            // ********************************************************************************

            // ********************************************************************************
            // Bind�����iModel�̒l�̕ύX�̊Ď��ݒ�j
            // ********************************************************************************
            // ��Փx�Ǘ��ԍ�
            selectDifficultModel.DifficultNumber
                .Subscribe(selectDifficultView.SelectDifficult)
                .AddTo(gameObject);
            // ��Փx���X�g
            selectDifficultModel.DifficultList
                .Subscribe(selectDifficultView.SetDifficulyList)
                .AddTo(gameObject);

            // ********************************************************************************
            // SetEvents�����iView�̃C�x���g�̊Ď��ݒ�j
            // ********************************************************************************
            // ��Փx�I���C�x���g
            selectDifficultView.selectDifficultListener1 = () => selectDifficultModel.SelectDifficult(1);
            selectDifficultView.selectDifficultListener2 = () => selectDifficultModel.SelectDifficult(2);
            selectDifficultView.selectDifficultListener3 = () => selectDifficultModel.SelectDifficult(3);
            selectDifficultView.selectDifficultListener4 = () => selectDifficultModel.SelectDifficult(4);
            selectDifficultView.selectDifficultListener5 = () => selectDifficultModel.SelectDifficult(5);
            // ��Փx����C�x���g
            selectDifficultView.decideListener = () =>
            {
                // ��Փx��ݒ�
                sessionCommon.DifficultyType = selectDifficultModel.DifficultyType;
                // ����ʂ֑J��
                screenDataLinkage.Value = new ScreenDataLinkage(ScreenList.SelectPreparationScreen, sessionCommon);
            };
            // �L�����Z���C�x���g
            selectDifficultView.cancelListener = () =>
            {
                // ����ʂ֑J��
                screenDataLinkage.Value = new ScreenDataLinkage(ScreenList.SelectMusicScreen, sessionCommon);
            };

            // ********************************************************************************
            // ��ʂ̕`��ɕK�v�Ȓǉ��ʐM
            // ********************************************************************************
        }

        // ********************************************************************************
        // OnScreenOepn�̒��O�ɌĂ΂�܂�
        // ********************************************************************************
        public override void OnScreenOepnBefore()
        {
            // ********************************************************************************
            // Initialize��OnScreenOepnBefore�̊ԂŁA�O��Screen����p�����[�^���K�v�ɉ����ēn����Ă��Ă��܂��B
            // ********************************************************************************

            // ********************************************************************************
            // �����ł́A��������ɓn���ꂽ�p�����[�^�ɉ����āA��ʂ��\�������O�ɍς܂��Ă��������������s��
            // ********************************************************************************

            // ********************************************************************************
            // �n���ꂽ�p�����[�^��Model�ɓK��
            // ********************************************************************************

            // ********************************************************************************
            // �n���ꂽ�p�����[�^�Ɉˑ������ǉ�UI����
            // ********************************************************************************

            // ********************************************************************************
            // �n���ꂽ�p�����[�^�Ɉˑ������ǉ��ʐM
            // ********************************************************************************
        }

        // ********************************************************************************
        // ���g�ɑJ�ڂ��Ă���ۂɌĂ΂�܂�
        // ********************************************************************************
        public override void OnScreenOepn()
        {
            // ********************************************************************************
            // UI��\������A�j���[�V�����̍Đ�
            // ********************************************************************************
        }

        // ********************************************************************************
        // OnScreenOepn�̒���ɌĂ΂�܂�
        // ********************************************************************************
        public override void OnScreenOepnAfter()
        {
            // ********************************************************************************
            // ���ɂȂ�
            // ********************************************************************************
        }

        // ********************************************************************************
        // ����Screen�ɑJ�ڂ���ۂɌĂ΂�܂�
        // ********************************************************************************
        public override void OnScreenChange()
        {
            // ********************************************************************************
            // UI���\���ɂ���A�j���[�V�����̍Đ�
            // ********************************************************************************
        }

        // ********************************************************************************
        // �O��Screen�ɖ߂�ۂɓǂ܂�܂�
        // ********************************************************************************
        public override void OnScreenBackSrc()
        {
            // ********************************************************************************
            // UI���\���ɂ���A�j���[�V�����̍Đ�
            // ********************************************************************************
        }

        // ********************************************************************************
        // OnScreenBackDst�̒��O�ɌĂ΂�܂�
        // ********************************************************************************
        public override void OnScreenBackDstBefore()
        {
            // ********************************************************************************
            // ���ɂȂ�
            // ********************************************************************************
        }

        // ********************************************************************************
        // �߂�ɂ���Ď��g�ɑJ�ڂ��Ă���ۂɌĂ΂�܂�
        // ********************************************************************************
        public override void OnScreenBackDst()
        {
            // ********************************************************************************
            // UI��\������A�j���[�V�����̍Đ�
            // ********************************************************************************
        }

        // ********************************************************************************
        // ���g���Ǘ����Ă���Window������ۂɌĂ΂�܂�
        // ********************************************************************************
        public override void OnWindowClose()
        {
            // ********************************************************************************
            // ���ɂȂ�
            // ********************************************************************************
        }

        // ********************************************************************************
        // Android�o�b�N�L�[�������ꂽ�ۂɌĂ΂�܂�
        // ********************************************************************************
        public override void AndroidBack()
        {
            // ********************************************************************************
            // ���ɂȂ�
            // ********************************************************************************
        }
    }
}