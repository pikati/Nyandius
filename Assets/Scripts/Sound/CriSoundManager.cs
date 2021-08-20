using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CriSoundManager : Singleton<CriSoundManager>
{
    private CriAtomEx.CueInfo[] _cueInfoList;
    private CriAtomExPlayer _atomExPlayer;
    private CriAtomExPlayer _BGMPlayer;
    private CriAtomExAcb _atomExAcb;

    IEnumerator Start()
    {
        /* キューシートファイルのロード待ち */
        while (CriAtom.CueSheetsAreLoading)
        {
            yield return null;
        }

        /* AtomExPlayerの生成 */
        _atomExPlayer = new CriAtomExPlayer();
        _BGMPlayer = new CriAtomExPlayer();
        _BGMPlayer.AttachFader();
        _BGMPlayer.SetFadeOutTime(5000);
        _BGMPlayer.SetFadeInTime(1000);
        /* Cue情報の取得 */
        _atomExAcb = CriAtom.GetAcb("CueSheet_0");
        _cueInfoList = _atomExAcb.GetCueInfoList();
    }
    private void OnDestroy()
    {
        _atomExPlayer.Dispose();
    }

    public void PlaySE(CueID cueID)
    {
        if (cueID >= CueID.Intoro) return;
        if(cueID == CueID.Lazer || cueID == CueID.Shot)
        {
            _atomExPlayer.SetVolume(0.3f);
        }
        _atomExPlayer.SetCue(_atomExAcb, _cueInfoList[(int)cueID].name);
        _atomExPlayer.Start();
        _atomExPlayer.SetVolume(1.0f);
    }

    public void PlayBGM(CueID cueID)
    {
        if (cueID < CueID.Intoro) return;
        _BGMPlayer.SetCue(_atomExAcb, _cueInfoList[(int)cueID].name);
        _BGMPlayer.Start();
    }


    public void StopSE()
    {
        _atomExPlayer.Stop();
    }

    public void StopBGM()
    {
        _BGMPlayer.Stop();
    }
}
