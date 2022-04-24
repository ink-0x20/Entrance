using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

using Entrance.Common;
using Entrance.Model;
using Entrance.Utils;

using UniRx;

namespace Entrance.Prezenter
{
    public abstract class BaseWindowPrezenter : MonoBehaviour
    {
        // **************************************************
        // ���ʃZ�b�V����
        // **************************************************
        protected SessionCommon sessionCommon = null;

        // **************************************************
        // �X�N���[���Ǘ�
        // **************************************************
        private Stack<ScreenList> screenManageStack = new Stack<ScreenList>();
        private Dictionary<ScreenList, BaseScreenPrezenter> inactiveScreenManage = new Dictionary<ScreenList, BaseScreenPrezenter>();

        // **************************************************
        // [UniRx�Ď��C�x���g]�X�N���[���J�ڎ��̃f�[�^�A�g�C�x���g
        // **************************************************
        protected readonly ReactiveProperty<ScreenDataLinkage> screenDataLinkage = new ReactiveProperty<ScreenDataLinkage>();
        public IReadOnlyReactiveProperty<ScreenDataLinkage> ScreenDataLinkage => screenDataLinkage;

        // **************************************************
        // abstract���\�b�h��`
        // **************************************************
        // ���������ɌĂ΂�܂�
        public abstract void Initialize();
        // OnWindowOpen�̒��O�ɌĂ΂�܂�
        public abstract void OnWindowOpenBefore();
        // ���g���J�����ۂɌĂ΂�܂�
        public abstract void OnWindowOpen();

        // OnWindowBack�̒��O�ɌĂ΂�܂�
        public abstract void OnWindowBackBefore();
        // ����Window�����邱�Ƃɂ���āA���g����ʂɕ\������鎞�ɌĂ΂�܂�
        public abstract void OnWindowBack();

        // ����Window���J����邱�Ƃɂ���āA���g����ʂ�������鎞�ɌĂ΂�܂�
        public abstract void OnWindowHide();
        // ���g����邱�Ƃɂ���āA���g����ʂ�������鎞�ɌĂ΂�܂�
        public abstract void OnWindowClose();

        // ���g���Ǘ����Ă���X�N���[�����J�ڂ��n�߂����ɌĂ΂�܂�
        public abstract void OnScreenChangeBefore();
        // ���g���Ǘ����Ă���X�N���[�����J�ڂ��I��������ɌĂ΂�܂�
        public abstract void OnScreenChangeAfter();

        // ********************************************************************************
        // �E�B���h�E�����N�����ɃC�x���g�����Ԃɏ�������
        // ********************************************************************************
        public void WindowOpenSync(WindowList window, ScreenDataLinkage screenDataLinkage)
        {
            // �X�N���[���L���b�V��
            screenManageStack.Push(screenDataLinkage.NextScreen);
            // ��������
            Initialize();
            // �E�B���h�E�����O����
            OnWindowOpenBefore();
            // �E�B���h�E����
            OnWindowOpen();
            // �J�ڑO����
            OnScreenChangeBefore();
            // �Z�b�V�����ێ�
            this.sessionCommon = screenDataLinkage.SessionCommon;
            // �V�[���J��
            SceneManager.sceneLoaded += ScreenLoaded;
            SceneManager.LoadScene(screenDataLinkage.NextScreen.ToString(), LoadSceneMode.Additive);
        }

        // ********************************************************************************
        // �E�B���h�E���J�ڎ��ɃC�x���g�����Ԃɏ�������
        // ********************************************************************************
        public void SameWindowSync(ScreenDataLinkage screenDataLinkage)
        {
            // �J�ڑO����
            OnScreenChangeBefore();
            // �Z�b�V�����ێ�
            this.sessionCommon = screenDataLinkage.SessionCommon;
            // �X�N���[�����ݔ���
            if (screenManageStack.Peek() == screenDataLinkage.NextScreen)
            {
                // �ĕ\��
                BaseScreenPrezenter baseScreenPrezenter = inactiveScreenManage[screenDataLinkage.NextScreen];
                baseScreenPrezenter.SetActive(true);
                // ���[�h��ʔ�\��
                GameObject.Find("NowLoading").SetActive(false);
                // �߂�ɂ���Ď��g�ɑJ�ڂ���O�̏���
                baseScreenPrezenter.OnScreenBackDstBefore();
                // �߂�ɂ���Ď��g�ɑJ�ڂ��鎞�̏���
                baseScreenPrezenter.OnScreenBackDst();
                // �v���[���^�[�L���b�V���폜
                inactiveScreenManage.Remove(screenDataLinkage.NextScreen);
            }
            else
            {
                // �X�N���[���L���b�V��
                screenManageStack.Push(screenDataLinkage.NextScreen);
                // �V�[���J��
                SceneManager.sceneLoaded += ScreenLoaded;
                SceneManager.LoadScene(screenDataLinkage.NextScreen.ToString(), LoadSceneMode.Additive);
            }
        }

        // ********************************************************************************
        // �X�N���[�����N�����ɃC�x���g�����Ԃɏ�������
        // ********************************************************************************
        private void ScreenLoaded(Scene scene, LoadSceneMode mode)
        {
            BaseScreenPrezenter baseScreenPrezenter = scene.GetRootGameObjects()[0].GetComponent<BaseScreenPrezenter>();
            baseScreenPrezenter.ScreenDataLinkage
                .Skip(1)
                .Subscribe((screenDataLinkage) =>
                {
                    if (screenDataLinkage.ActiveFlg)
                    {
                        // ********************************************************************************
                        // �J�ڃC�x���g�o�^
                        // ********************************************************************************
                        if (screenManageStack.Count != 0)
                        {
                            // �O�̃X�N���[���̑J�ڑO����
                            baseScreenPrezenter.OnScreenChange();
                            // �J�ڏ��擾
                            ScreenList previousScreen = screenManageStack.Peek();
                            WindowList previousWindow = SceneUtils.GetWindowName(previousScreen);
                            WindowList nextWindow = SceneUtils.GetWindowName(screenDataLinkage.NextScreen);
                            if (previousWindow == nextWindow)
                            {
                                if (screenManageStack.Contains(screenDataLinkage.NextScreen))
                                {
                                    // ********************************************************************************
                                    // �����E�B���h�E�E�����X�N���[��
                                    // ********************************************************************************
                                    // �J�ڏ��ɖ߂��Ă���
                                    ScreenList backScreen = screenManageStack.Pop();
                                    while (screenManageStack.Count != 0)
                                    {
                                        // �����E�B���h�E�ŁA�����X�N���[���̂��߁A�X�N���[���̂ݔj��
                                        SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(backScreen.ToString()), UnloadSceneOptions.None);
                                        if (screenManageStack.Peek() == screenDataLinkage.NextScreen)
                                        {
                                            // �J�ڐ�܂ŗ�����I��
                                            break;
                                        }
                                        backScreen = screenManageStack.Pop();
                                    }
                                }
                                else
                                {
                                    // ********************************************************************************
                                    // �����E�B���h�E�E�V�K�X�N���[��
                                    // ********************************************************************************
                                    // �v���[���^�[�L���b�V��
                                    inactiveScreenManage.Add(previousScreen, baseScreenPrezenter);
                                    // �����E�B���h�E�ŁA�V�����X�N���[���̂��ߔ�\��
                                    baseScreenPrezenter.SetActive(false);
                                }
                            }
                            else if (SceneUtils.IsWindowDestroy(nextWindow))
                            {
                                // ********************************************************************************
                                // �Ⴄ�E�B���h�E�E�j��
                                // ********************************************************************************
                                // �Ⴄ�E�B���h�E�̂��ߔj���O����
                                baseScreenPrezenter.OnWindowClose();
                                // �j��
                                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(screenManageStack.Pop().ToString()), UnloadSceneOptions.None);
                            }
                            else
                            {
                                // ********************************************************************************
                                // �Ⴄ�E�B���h�E�E��\��
                                // ********************************************************************************
                                // �v���[���^�[�L���b�V��
                                inactiveScreenManage.Add(previousScreen, baseScreenPrezenter);
                                // �Ⴄ�E�B���h�E�����j�����Ȃ����ߔ�\��
                                baseScreenPrezenter.SetActive(false);
                            }
                            // �V�[���Ƀf�[�^�A�g
                            this.screenDataLinkage.Value = screenDataLinkage;
                        }
                    }
                })
                .AddTo(baseScreenPrezenter.gameObject);
            // �J��
            baseScreenPrezenter.ScreenOpenSync(sessionCommon);
            // �J�ڌ㏈��
            OnScreenChangeAfter();
            // ���[�h��ʔ�\��
            GameObject.Find("NowLoading").SetActive(false);
            // 1��݂̂̎��s�̂��ߍ폜
            SceneManager.sceneLoaded -= ScreenLoaded;
        }

        // ********************************************************************************
        // ���ݐ������̃X�N���[�������ׂĔj������
        // ********************************************************************************
        public void AllScreenUnload()
        {
            while (screenManageStack.Count != 0)
            {
                SceneManager.UnloadSceneAsync(SceneManager.GetSceneByName(screenManageStack.Pop().ToString()), UnloadSceneOptions.None);
            }
        }
    }
}
