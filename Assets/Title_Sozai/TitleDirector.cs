using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //LoadScene���g�����߂ɕK�v

public class TitleDirector : MonoBehaviour
{
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) //�}�E�X���N���b�N���ꂽ��
        if (Input.GetKeyDown(KeyCode.Space))//�X�y�[�X�������ꂽ��
        {
            LoadGameScene(); //GameScene�Ɉړ�����
        }
    }
    public void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene"); //GameScene�Ɉړ�����
    }
}
