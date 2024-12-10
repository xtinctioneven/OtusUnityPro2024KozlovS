using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

public class SkipButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    private VisualPipeline _visualPipeline;

    [Inject]
    public void Construct(VisualPipeline visualPipeline)
    {
        _visualPipeline = visualPipeline;
    }
    
    private void Start()
    {
        _button.onClick.AddListener(SkipVisual);
    }

    private void SkipVisual()
    {
        _visualPipeline.Disable();
        _button.gameObject.SetActive(false);
    }
}