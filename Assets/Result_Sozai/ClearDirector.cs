using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //LoadScene���g�����߂ɕK�v

public class ClearDirector : MonoBehaviour
{
    void Update()
    {
        //if (Input.GetMouseButtonDown(0)) //�}�E�X���N���b�N���ꂽ��
        if (Input.GetKeyDown(KeyCode.Space))//�X�y�[�X�������ꂽ��
            {
                SceneManager.LoadScene("TitleScene"); //GameScene�Ɉړ�����
        }
    }
}
