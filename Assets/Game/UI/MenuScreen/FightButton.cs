using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class FightButton : MonoBehaviour
{
    private Button _button;
    void Start()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(LoadBattleScene);
    }

    private void LoadBattleScene()
    {
        _button.onClick.RemoveAllListeners();
        SceneManager.LoadScene("BattleScene");
    }
    
}
