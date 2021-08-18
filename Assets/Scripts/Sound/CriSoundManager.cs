using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriSoundManager : Singleton<CriSoundManager>
{
    private CriAtomEx.CueInfo[] _cueInfoList;
    private CriAtomExPlayer _atomExPlayer;
    private CriAtomExAcb _atomExAcb;

    IEnumerator Start()
    {
        /* �L���[�V�[�g�t�@�C���̃��[�h�҂� */
        while (CriAtom.CueSheetsAreLoading)
        {
            yield return null;
        }

        /* AtomExPlayer�̐��� */
        _atomExPlayer = new CriAtomExPlayer();

        /* Cue���̎擾 */
        _atomExAcb = CriAtom.GetAcb("CueSheet_0");
        _cueInfoList = _atomExAcb.GetCueInfoList();
    }
    private void OnDestroy()
    {
        _atomExPlayer.Dispose();
    }

    public void PlaySound(CueID cueID)
    {
        _atomExPlayer.SetCue(_atomExAcb, _cueInfoList[(int)cueID].name);
        _atomExPlayer.Start();
    }

    public void StopSound()
    {
        _atomExPlayer.Stop();
    }
}
