using TMPro;
using UnityEngine;

public class InputNicknameWindowConrroller : MonoBehaviour
{
    public TMP_InputField InputNickname => inputNickname;
    [SerializeField] private TMP_InputField inputNickname;

    public void ErrorNickname()
    {
        inputNickname.text = "";
    }
}
