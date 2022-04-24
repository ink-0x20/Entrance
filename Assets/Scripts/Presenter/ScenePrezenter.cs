using UnityEngine;
using System.Collections.Generic;
using System.IO;

using Entrance.Common;
using Entrance.Model;
using Entrance.Utils;

using UniRx;

namespace Entrance.Prezenter
{
    public class ScenePrezenter : MonoBehaviour
    {
        // **************************************************
        // �C���X�y�N�^�I�u�W�F�N�g
        // **************************************************
        // Window�v���n�u
        [SerializeField]
        private GameObject titleWindowPrefab = default;
        [SerializeField]
        private GameObject selectWindowPrefab = default;
        [SerializeField]
        private GameObject gameMusicWindowPrefab = default;
        [SerializeField]
        private GameObject resultWindowPrefab = default;
        // NCMB�v���n�u
        [SerializeField]
        private GameObject NCMBSettingswPrefab = default;
        // ���[�h���
        [SerializeField]
        private GameObject nowLoading = default;

        // **************************************************
        // �E�B���h�E�Ǘ����X�g
        // **************************************************
        private static Stack<WindowManage> windowManageStack = new Stack<WindowManage>();

        // ********************************************************************************
        // �v���O�����̋N��
        // ********************************************************************************
        private void Start()
        {
            var application = Resources.Load<TextAsset>("ApplicationKey");
            var client = Resources.Load<TextAsset>("ClientKey");
            if (application == null || client == null)
            {
                // ********************************************************************************
                // �T�[�o�A�N�Z�X�L�[�Ȃ��̏ꍇ�̓f�t�H���g�ݒ�ŋN��
                // ********************************************************************************
                // �����Z�b�V�����𐶐�
                SessionCommon sessionCommon = new SessionCommon();
                sessionCommon.DefaultLoadFlg = true;
                sessionCommon.OnLoaded();
                // �^�C�g���X�N���[���֑J��
                ControllWindow(new ScreenDataLinkage(ScreenList.TitleScreen, sessionCommon));
            }
            else
            {
                // ********************************************************************************
                // �T�[�o�A�N�Z�X�L�[����̏ꍇ�̓T�[�o����ݒ�擾���N��
                // ********************************************************************************
                // NCMB�I�u�W�F�N�g�z�u
                GameObject ncmb = Instantiate(NCMBSettingswPrefab);
                ncmb.name = "NCMBSettings";
                // �^�C�g���X�N���[���֑J��
                ControllWindow(new ScreenDataLinkage(ScreenList.TitleScreen));
            }
        }

        // ********************************************************************************
        // �J�ڐ�X�N���[������E�B���h�E����肵�Ǘ�����
        // ********************************************************************************
        public void ControllWindow(ScreenDataLinkage ScreenDataLinkage)
        {
            // �ϐ��錾
            WindowList nextWindow = WindowList.TitleWindow;
            bool destroyFlg = false;
            GameObject windowObject = null;
            BaseWindowPrezenter baseWindowPrezenter = null;

            // ���[�h�e�L�X�g�\��
            nowLoading.SetActive(true);

            // �J�ڐ�X�N���[������A�Ή�����E�B���h�E�����
            nextWindow = SceneUtils.GetWindowName(ScreenDataLinkage.NextScreen);
            destroyFlg = SceneUtils.IsWindowDestroy(nextWindow);

            if (ScreenDataLinkage.NextScreen == ScreenList.TitleScreen)
            {
                // �^�C�g����ʂ̏ꍇ�A�S�E�B���h�E�E�X�N���[�������Z�b�g
                while (windowManageStack.Count != 0)
                {
                    WindowManage windowInfo = windowManageStack.Pop();
                    // �j���O����
                    windowInfo.BaseWindowPrezenter.OnWindowClose();
                    // �S�X�N���[���j��
                    windowInfo.BaseWindowPrezenter.AllScreenUnload();
                    // �E�B���h�E�j��
                    Destroy(windowInfo.WindowObject);
                }
            }
            else
            {
                // �E�B���h�E���ݔ���
                foreach (WindowManage windowInfo in windowManageStack)
                {
                    if (windowInfo.WindowName.Equals(nextWindow))
                    {
                        windowObject = windowInfo.WindowObject;
                        baseWindowPrezenter = windowInfo.BaseWindowPrezenter;
                        break;
                    }
                }
            }

            if (baseWindowPrezenter == null)
            {
                // ********************************************************************************
                // �O�̃E�B���h�E�𒲐�����
                // ********************************************************************************
                if (windowManageStack.Count != 0)
                {
                    if (destroyFlg)
                    {
                        WindowManage windowInfo = windowManageStack.Pop();
                        // �j���O����
                        windowInfo.BaseWindowPrezenter.OnWindowClose();
                        // �S�X�N���[���j��
                        windowInfo.BaseWindowPrezenter.AllScreenUnload();
                        // �E�B���h�E�j��
                        Destroy(windowInfo.WindowObject);
                    }
                    else
                    {
                        WindowManage windowInfo = windowManageStack.Peek();
                        // �E�B���h�E��\���O����
                        windowInfo.BaseWindowPrezenter.OnWindowHide();
                        // �E�B���h�E��\��
                        windowInfo.WindowObject.SetActive(false);
                    }
                }

                // ********************************************************************************
                // �E�B���h�E�𐶐�����
                // ********************************************************************************
                switch (nextWindow)
                {
                    case WindowList.SelectWindow:
                        windowObject = Instantiate(selectWindowPrefab, Vector3.zero, Quaternion.identity);
                        break;
                    case WindowList.MusicGameWindow:
                        windowObject = Instantiate(gameMusicWindowPrefab, Vector3.zero, Quaternion.identity);
                        break;
                    case WindowList.ResultWindow:
                        windowObject = Instantiate(resultWindowPrefab, Vector3.zero, Quaternion.identity);
                        break;
                    default:
                        windowObject = Instantiate(titleWindowPrefab, Vector3.zero, Quaternion.identity);
                        break;
                }

                // ********************************************************************************
                // �V�����E�B���h�E�֑J�ڂ���
                // ********************************************************************************
                // �E�B���h�E�̏�������
                baseWindowPrezenter = windowObject.GetComponent<BaseWindowPrezenter>();
                baseWindowPrezenter.ScreenDataLinkage
                    .Skip(1)
                    .Subscribe(ControllWindow)
                    .AddTo(gameObject);
                // �L���b�V��
                windowManageStack.Push(new WindowManage(nextWindow, windowObject, baseWindowPrezenter));
                // �J��
                baseWindowPrezenter.WindowOpenSync(nextWindow, ScreenDataLinkage);
            }
            else
            {
                // ********************************************************************************
                // �����E�B���h�E�֑J�ڂ���
                // ********************************************************************************
                baseWindowPrezenter.SameWindowSync(ScreenDataLinkage);
            }
        }
    }
}
