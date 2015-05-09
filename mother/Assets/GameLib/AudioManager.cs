﻿/*
*   @author Kyuzen
*/

using UnityEngine;
using System;
using System.Collections;

public enum SoundSETye : int
{
	SE001_PUSH_NUKO,
	SE002,
	SE003,
	SE004,
	SE005,
	SE006
}

public enum SoundBGMType : int
{
	BGM001,
	BGM002,
	BGM003,
	BGM004,
	BGM005,
}

// 音管理クラス
public class AudioManager : SingletonMonoBehaviourFast<AudioManager>
{
	// 音量
	public static SoundVolume volume = new SoundVolume();
	
	public AudioSource BGMsource;
	private AudioSource[] SEsources = new AudioSource[5]; // 数は自分で調節
	
	// BGM
	private AudioClip[] BGM;
	// SE
	private AudioClip[] SE;

	Transform comTransform;
	Transform comCamraTrans;
	
	protected override void Awake()
	{
		CheckInstance();
		
		// BGM AudioSource
		BGMsource = gameObject.AddComponent<AudioSource>();
		BGMsource.loop = true;
		
		// SE AudioSource
		for (int i = 0; i < SEsources.Length; i++)
		{
			SEsources[i] = gameObject.AddComponent<AudioSource>();
		}
		
		// ロード
		BGM = new AudioClip[]{
			Resources.Load<AudioClip>("Sound/bgm/000"), // 0
			Resources.Load<AudioClip>("Sound/bgm/001"), // 1
			Resources.Load<AudioClip>("Sound/bgm/002"), // 2
			Resources.Load<AudioClip>("Sound/bgm/003"), // 3
			Resources.Load<AudioClip>("Sound/bgm/004"),
			Resources.Load<AudioClip>("Sound/bgm/005"),
			Resources.Load<AudioClip>("Sound/bgm/006"),
			Resources.Load<AudioClip>("Sound/bgm/007"),
		};
		SE = new AudioClip[]{
			Resources.Load<AudioClip>("Sound/se/se_n_pack_ataak"),
			Resources.Load<AudioClip>("Sound/se/10_good"),
			Resources.Load<AudioClip>("Sound/se/11_combo1"),
			Resources.Load<AudioClip>("Sound/se/12_combo2"),//14_combo4
			Resources.Load<AudioClip>("Sound/se/13_combo3"),
			Resources.Load<AudioClip>("Sound/se/14_combo4"),
		};

		comTransform = GetComponent<Transform> ();
		comCamraTrans = Camera.main.transform;
	}
	
	void Update()
	{
		if (BGMsource == null) return;
		
		// ミュート設定
		BGMsource.mute = volume.Mute;
		foreach (AudioSource source in SEsources)
		{
			source.mute = volume.Mute;
		}
		
		// ボリューム設定
		BGMsource.volume = volume.BGM;
		foreach (AudioSource source in SEsources)
		{
			source.volume = volume.SE;
		}
		
		comTransform.position = comCamraTrans.position;		
	}
	
	// BGM再生
	public void PlayBGM(SoundBGMType type)
	{
		// 同じBGMの場合は何もしない
		if (BGMsource && BGMsource.clip != BGM[(int)type])
		{
			BGMsource.Stop();
			BGMsource.clip = BGM[(int)type];
			BGMsource.Play();
		}
	}
	
	// BGM停止
	public void StopBGM()
	{
		if (BGMsource)
		{
			BGMsource.Stop();
			// BGMsource.clip = null;
		}
	}
	
	public void StartBGM()
	{
		if (BGMsource)
		{
			BGMsource.Play();
		}
	}
	
	// SE再生
	public void PlaySE(SoundSETye type)
	{
		
		// 再生中で無いAudioSouceで鳴らす
		foreach (AudioSource source in SEsources)
		{
			if (source && false == source.isPlaying)
			{
				source.clip = SE[(int)type];
				source.Play();
				break;
			}
		}
	}
	
	// SE停止
	public void StopSE()
	{
		// 全てのSE用のAudioSouceを停止する
		foreach (AudioSource source in SEsources)
		{
			source.Stop();
			source.clip = null;
		}
	}
	
}

// 音量クラス
[Serializable]
public class SoundVolume
{
	public float BGM = 1.0f;
	public float Voice = 1.0f;
	public float SE = 1.0f;
	public bool Mute = false;
	
	public void Init()
	{
		BGM = 1.0f;
		Voice = 1.0f;
		SE = 1.0f;
		Mute = false;
	}
}