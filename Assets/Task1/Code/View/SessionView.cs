using System.Globalization;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

public class SessionView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _sessionNumberText;
    [SerializeField] private TextMeshProUGUI _sessionStartText;
    [SerializeField] private TextMeshProUGUI _sessionFinishText;
    [SerializeField] private TextMeshProUGUI _sessionDurationText;

    [Button]
    public void Show(SessionViewData sessionViewData)
    {
        _sessionNumberText.text = sessionViewData.SessionNumber;
        _sessionStartText.text = sessionViewData.SessionStart;
        _sessionFinishText.text = sessionViewData.SessionFinish;
        _sessionDurationText.text = sessionViewData.SessionDuration;
    }
}