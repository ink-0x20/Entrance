using System.Collections.Generic;

using Entrance.Common;

namespace Entrance.Model
{
    public struct ScreenSetting
    {
        // **************************************************
        // �E�B���h�E���Ƃ̏���`
        // **************************************************
        // �j���t���O�i�J�ڎ��ɔj�����邩�j
        private bool destroyFlg;
        // ���g�̃E�B���h�E���Ǘ�����X�N���[��
        private List<ScreenList> screenList;

        // **************************************************
        // �R���X�g���N�^
        // **************************************************
        public ScreenSetting(bool destroyFlg, List<ScreenList> screenList)
        {
            this.destroyFlg = destroyFlg;
            this.screenList = screenList;
        }

        public bool DestroyFlg { get => destroyFlg; }

        // ********************************************************************************
        // ���g�̃E�B���h�E���Ǘ����Ă���X�N���[�����𔻒肷��
        // ********************************************************************************
        public bool IsExistScreen(ScreenList nextScreen)
        {
            return screenList.Contains(nextScreen);
        }
    }
}
