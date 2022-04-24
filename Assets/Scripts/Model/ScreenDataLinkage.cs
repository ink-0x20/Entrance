using Entrance.Common;

namespace Entrance.Model
{
    public struct ScreenDataLinkage
    {
        // **************************************************
        // ���̃X�N���[��
        // **************************************************
        private ScreenList nextScreen;

        // **************************************************
        // ���ʃZ�b�V����
        // **************************************************
        private SessionCommon sessionCommon;

        // **************************************************
        // �A�N�e�B�u��ԕێ�
        // **************************************************
        private bool activeFlg;

        // ********************************************************************************
        // ��A�N�e�B�u���̃R���X�g���N�^
        // ********************************************************************************
        public ScreenDataLinkage(bool activeFlg)
        {
            this.nextScreen = ScreenList.TitleScreen;
            this.sessionCommon = null;
            this.activeFlg = activeFlg;
        }

        // ********************************************************************************
        // ����E���Z�b�g���̑J�ڐ�݂̂̃R���X�g���N�^
        // ********************************************************************************
        public ScreenDataLinkage(ScreenList nextScreen)
        {
            this.nextScreen = nextScreen;
            this.sessionCommon = null;
            this.activeFlg = true;
        }

        // ********************************************************************************
        // �ʏ�J�ڎ��̃R���X�g���N�^
        // ********************************************************************************
        public ScreenDataLinkage(ScreenList nextScreen, SessionCommon sessionCommon)
        {
            this.nextScreen = nextScreen;
            this.sessionCommon = sessionCommon;
            this.activeFlg = true;
        }

        public ScreenList NextScreen { get => nextScreen; }
        public SessionCommon SessionCommon { get => sessionCommon; }
        public bool ActiveFlg { get => activeFlg; }
    }
}
