using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
/// <summary>
/// Unity UIToolkit�Ή��ł̃N���X 2023/06/06
/// </summary>
public class UISystemForHorrorGame : MonoBehaviour
{
    /// <summary> �����K�w��UIDocument </summary>
    UIDocument _document;

    /// <summary> �A�C�e�����\���̃N���X </summary>
    public ItemTextController _itemTextController;

    /// <summary> �E�����A�C�e�����\���̃N���X </summary>
    public ItemPickedTextController _itemPickedTextController;
    
    /// <summary> �ڕW�\���̃N���X </summary>
    public ObjectiveTextController _objectiveTextController;

    /// <summary> �ꎞ��~�e�L�X�g�̕\���N���X </summary>
    public PausedTextController _pausedTextController;

    /// <summary> �^�C�g���ɖ߂�{�^���̊Ǘ��N���X </summary>
    public BacktoTitleButtonChecker _backtoTitleButtonChecker;

    /// <summary> �⌾�̊Ǘ��N���X </summary>
    public DiyingWillController _diyingWillController;

    /// <summary> ���x�̃X���C�_�[�Ǘ��N���X /// </summary>
    public SencetivitySliderController _sencetivitySliderController;

    private void Awake()
    {
        if (GetComponent<UIDocument>() != null)//UIDocumen���擾�o�����炻�̂܂܎擾
        {
            _document = GetComponent<UIDocument>();
            //public �̃N���X�ɑ��
            this._itemTextController = new ItemTextController(_document.rootVisualElement);
            this._itemPickedTextController = new ItemPickedTextController(_document.rootVisualElement);
            this._objectiveTextController = new ObjectiveTextController(_document.rootVisualElement);
            this._pausedTextController = new PausedTextController(_document.rootVisualElement);
            this._backtoTitleButtonChecker = new BacktoTitleButtonChecker(_document.rootVisualElement);
            this._diyingWillController = new DiyingWillController(this._document.rootVisualElement);
            this._sencetivitySliderController = new SencetivitySliderController(this._document.rootVisualElement);
        }
    }
}

/// <summary>
/// �E����A�C�e������\������N���X
/// </summary>
public class ItemTextController
{
    private UnityEngine.UIElements.Label _label;
    
    public ItemTextController(VisualElement root)
    {
        this._label = root.Q<UnityEngine.UIElements.Label>("ItemText");//()���̕������Name�Ńo�C���h����Ă��镶����
        //�l�̏�����
        this._label.text = string.Empty;
    }

    public void OutPutTextToDisplay(string title)
    {
        this._label.text = title;
    }
}

/// <summary>
/// �E�����A�C�e������\������N���X
/// </summary>
public class ItemPickedTextController
{
    private UnityEngine.UIElements.Label _label;
    public ItemPickedTextController(VisualElement root)
    {
        this._label = root.Q<UnityEngine.UIElements.Label>("ItemPickedText");//()���̕������Name�Ńo�C���h����Ă��镶����
        //�l�̏�����
        this._label.text = string.Empty;
    }

    public void OutPutTextToDisplay(string title)
    {
        this._label.text = title;
    }
}

/// <summary>
/// �ڕW��\������N���X
/// </summary>
public class ObjectiveTextController
{
    private UnityEngine.UIElements.Label _label;
    public ObjectiveTextController(VisualElement root)
    {
        this._label = root.Q<UnityEngine.UIElements.Label>("ObjectiveText");//()���̕������Name�Ńo�C���h����Ă��镶����
        //�l�̏�����
        this._label.text = string.Empty;
    }

    public void OutPutTextToDisplay(string title)
    {
        this._label.text = title;
    }
}

/// <summary>
/// �ꎞ��~���̃e�L�X�g��\������
/// </summary>
public class PausedTextController
{
    private UnityEngine.UIElements.Label _label;
    public PausedTextController(VisualElement root)
    {
        this._label = root.Q<UnityEngine.UIElements.Label>("PausedLabel");//()���̕������Name�Ńo�C���h����Ă��镶����
        //������
        this._label.visible = false;
    }

    public void SetVisible(bool isVisible)
    {
        this._label.visible = isVisible;
    }
}

/// <summary>
/// �^�C�g���ɖ߂�{�^���̊Ǘ��N���X
/// </summary>
public sealed class BacktoTitleButtonChecker
{
    private readonly UnityEngine.UIElements.Button _button;
    private bool _calledTitleScene = false;

    public BacktoTitleButtonChecker(VisualElement root)//Button�̐錾�������܂��Ȃ���
    {
        _button = root.Q<UnityEngine.UIElements.Button>("BackToMainMenuButton");
        _button.clicked += buttonClicked;
        this._button.visible = false;
    }

    private void buttonClicked()
    {
        Debug.Log(_button.text);
        BackToTitleClickedNow();
    }

    public void SetVisible(bool isVisible)
    {
        this._button.visible = isVisible;
    }

    private void BackToTitleClickedNow()
    {
        if (!this._calledTitleScene)
        {
            SetVisible(false);
            SceneManager.LoadScene("StartMenu");
        }
    }
}

/// <summary>
/// �⌾�̃e�L�X�g�ɃA�N�Z�X����N���X
/// </summary>
public class DiyingWillController
{
    private UnityEngine.UIElements.Label _label;
    public DiyingWillController(VisualElement root)
    {
        this._label = root.Q<UnityEngine.UIElements.Label>("DiyingWillLabel");//()���̕������Name�Ńo�C���h����Ă��镶����
        this._label.text = "�⌾";
        //������
        this._label.visible = false;
    }

    public void OutputTextToDisplay(string text)
    {
        this._label.text = text;
    }

    public void SetVisible(bool isVisible)
    {
        this._label.visible = isVisible;
    }

    public bool GetVisible()
    {
        return this._label.visible;
    }
}

/// <summary>
/// ���_�ړ����x�p�̃X���C�_�[
/// </summary>
public class SencetivitySliderController
{
    private UnityEngine.UIElements.Slider _slider;

    public SencetivitySliderController(VisualElement root)
    {
        this._slider = root.Q<UnityEngine.UIElements.Slider>("SenceSlider");//���x�̃X���C�_�[
        //������
        this._slider.visible = false;
    }

    public void SetSencitivity(ref float sence)
    {
        sence = this._slider.value;
    }

    public void SetVisible(bool visible)
    {
        this._slider.visible = visible;
    }

    public bool GetVisible()
    {
        return (this._slider.visible);
    }
}