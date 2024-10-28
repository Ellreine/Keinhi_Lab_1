using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayWindowConrtoller : MonoBehaviour
{
    [SerializeField] private TMP_Text textNickname;

    public void SetNickname(string nickname)
    {
        textNickname.text = $"You nickname: {nickname}";
    }
}
