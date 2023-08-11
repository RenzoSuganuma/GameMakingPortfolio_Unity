using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
/// <summary>
/// Unity UIToolkit�Ή��ł̃N���X 2023/06/06
/// </summary>
public class HorrorMainMenuUISystem : MonoBehaviour
{
    VisualTreeAsset _elementTemplate;
    UIDocument _document;

    private  UnityEngine.UIElements.Button _startButton, _settingsButton, _quitButton, _backToMainMenu, _gotoProlougeButton;

    private UnityEngine.UIElements.GroupBox _groupBoxMain, _groupBoxSettings;

    private UnityEngine.UIElements.VisualElement _keyMouConfGrp, _xboxConfGrp;

    private  UnityEngine.UIElements.ProgressBar _sceneLoadingProgressBar;

    private UnityEngine.UIElements.Toggle _configuePageSelect;

    private bool _calledNextScene = false;

    private void OnEnable()
    {
        if (GetComponent<UIDocument>() != null)//UIDocumen���擾�o�����炻�̂܂܎擾
        {
            _document = GetComponent<UIDocument>();
        }
    }

    private void Start()
    {
        GroupBoxController(_document.rootVisualElement);
    }

    private void Update()
    {
        ButtonInputChecker(_document.rootVisualElement);
        ConfigueToggleCheck();
    }

    public void ButtonInputChecker(VisualElement root)//Button�̐錾�������܂��Ȃ���
    {
        {                                   /*GroupBox_All_Main*/
            this._startButton = root.Q<UnityEngine.UIElements.Button>("StartGame_Button");//�n�߂�{�^��
            this._startButton.clicked += StartButtonClicked;

            this._settingsButton = root.Q<UnityEngine.UIElements.Button>("Settings_Button");//�ݒ�{�^��
            this._settingsButton.clicked += SettingButtonClicked;

            this._quitButton = root.Q<UnityEngine.UIElements.Button>("Quit_Button");//OS�ɖ߂�{�^��
            this._quitButton.clicked += QuitButtonClicked;

            this._backToMainMenu = root.Q<UnityEngine.UIElements.Button>("Setting_BackToMainButton");//���C�����j���[�ɖ߂�{�^��
            this._backToMainMenu.clicked += BackToMainMenuClicked;
            
            this._gotoProlougeButton = root.Q<UnityEngine.UIElements.Button>("Prolouge_Button");//�v�����[�O�ɖ߂�{�^��
            this._gotoProlougeButton.clicked += GotoProlouge;
        }                                   /*GroupBox_All_Main*/
    }

    private void StartButtonClicked()//�n�߂�{�^��
    {
        //throw new System.NotImplementedException();
        Debug.Log(_startButton.text);
        MoveToGameSceneFirst(this._document.rootVisualElement);
    }

    private void SettingButtonClicked()//�ݒ�{�^��
    {
        Debug.Log(_settingsButton.text);
        SelectSettingsGroupBox();
    }

    private void QuitButtonClicked()//OS�ɖ߂�{�^��
    {
        Debug.Log(_quitButton.text);
        GoToOS();
    }

    private void BackToMainMenuClicked()//���C�����j���[�ɖ߂�{�^��
    {
        Debug.Log(_backToMainMenu.text);
        SelectMainGroupBox();
    }

    private void GotoProlouge()
    {
        if (!this._calledNextScene)
            StartCoroutine(LoadScene("Prologue"));
    }


    public void GroupBoxController(VisualElement root)
    {
        this._groupBoxMain = root.Q<UnityEngine.UIElements.GroupBox>("GroupBox_All_Main");//���C�����j���[��ʂ̃O���{�b�N�X
        this._groupBoxMain.visible = true;

        this._groupBoxSettings = root.Q<UnityEngine.UIElements.GroupBox>("GroupBox_All_Settings");//�e��ݒ��ʂ̃O���{�b�N�X
        this._groupBoxSettings.visible = false;
    }
    public void ConfigueController(VisualElement root)
    {
        this._configuePageSelect = root.Q<UnityEngine.UIElements.Toggle>("ConfigueSelect");
        this._keyMouConfGrp = root.Q<UnityEngine.UIElements.VisualElement>("KeyMouConfigueGroup");//�e��ݒ��ʂ̃O���{�b�N�X
        this._xboxConfGrp = root.Q<UnityEngine.UIElements.VisualElement>("XboxConfigueGroup");//�e��ݒ��ʂ̃O���{�b�N�X
        this._keyMouConfGrp.visible = true;
        this._xboxConfGrp.visible = false;
    }
    public void ConfigueToggleCheck()
    {
        if (this._groupBoxSettings.visible)
        {
            Debug.Log($"TOGGLE{this._configuePageSelect.value}");
            this._keyMouConfGrp.visible = !this._configuePageSelect.value;
            this._xboxConfGrp.visible = this._configuePageSelect.value;
        }
    }

    public void SelectMainGroupBox()//���C�����j���[��ʂ̃O���{�b�N�X
    {
        this._groupBoxMain.visible = true;
        this._groupBoxSettings.visible = false;
        this._keyMouConfGrp.visible = this._xboxConfGrp.visible = false;
        //this._groupBoxLoadingScene.visible = false;
    }

    public void SelectSettingsGroupBox()//�e��ݒ��ʂ̃O���{�b�N�X
    {
        this._groupBoxSettings.visible = true;
        this._groupBoxMain.visible = false;
        ConfigueController(this._document.rootVisualElement);
        //this._groupBoxLoadingScene.visible = false;
    }

    public void MoveToGameSceneFirst(VisualElement root)
    {
        this._sceneLoadingProgressBar = root.Q<UnityEngine.UIElements.ProgressBar>("LoadingScene_Progress");
        this._sceneLoadingProgressBar.value = 0;//�v���O�o�[�l������
        this._sceneLoadingProgressBar.visible = true;
        this._groupBoxMain.visible = false;
        this._groupBoxSettings.visible = false;
        if(!this._calledNextScene)
            StartCoroutine(LoadScene("FirstStage"));
    }

    IEnumerator LoadScene(string scenename)
    {
        this._calledNextScene = true;
        AsyncOperation async = SceneManager.LoadSceneAsync(scenename);
        while (!async.isDone)
        {
            this._sceneLoadingProgressBar.value = async.progress;
            yield return null;
        }
    }

    void GoToOS()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;//�Q�[���v���C�I��
        #endif

        #if UNITY_STANDALONE_WIN
            Application.Quit();//�Q�[���v���C�I��
        #endif
    }
}