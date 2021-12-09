using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;
//using UnityStandardAssets.ImageEffects;

public class GameManager : MonoBehaviour
{
	//[Header("___________________________________________")] 
	[Header("************* Timer Settings *************")] 
	[ContextMenuItem("Reset", "ResetMe")] public float ThreeStars;
	public float TwoStars; 
	public float  OneStar;
	//[Space] public float TimeDecAmount = 10;
	[HideInInspector]public float Counter = 0;
	
	[Header("___________________________________________")] 
	[Header("************* Levels Settings *************")] 
	public GameObject LevelsHolder;
	public GameObject CurrentLevel;
	[Space] public int CurrentLevelIndex = 0;
	public static AssetBundle CurrentAssetBundle{ get; set;}
	
	[Header("___________________________________________")] 
	[Header("************* Sound Settings *************")] 
	public AudioClip ActionSound;
	[HideInInspector]public AudioClip RightAnswerSound;
	[HideInInspector]public AudioClip WrongAnswerSound;
	public AudioClip HintSound;
	[Space]
	public AudioClip RightAnswerAudio;
	public AudioClip WrongAnswerAudio;
	[Header("___________________________________________")] 
	[Header("************* Other Settings *************")] 
	public bool NoInitialScreen;
	public bool ReloadSceneOnProgress;
	public bool DontAnswerWhenTitle;
	[HideInInspector]
	public Sprite LevelsHolderSprite
	{
		get
		{
			if (_instance.LevelsHolder == null)
			{
				return null;
			}
			if (_instance.LevelsHolder.GetComponent<SpriteRenderer>() == null)
			{
				return null;
			}
			return _instance.LevelsHolder.GetComponent<SpriteRenderer>().sprite; 
		}

		set
		{
			_instance.LevelsHolder.GetComponent<SpriteRenderer>().sprite  = value;	
		}
	}

	public GameObject InitialScreen;
	[Space] public GameObject ShowingAnswerObject;
	public GameObject EndScreen;
	public GameObject CollectingArea;
	public int GameProgress;
	
	[HideInInspector] public int Id;
	[HideInInspector] public int TrialTime;
	[HideInInspector] public bool DontRepeatAnswer;
	[HideInInspector] public bool NextLevel;
	[HideInInspector] public bool GetInitialScreenOnEnd;
	[HideInInspector] public bool GetInitialScreenOnPrev;

	public int AnsweredQuestions;
	private AudioSource[] _audioSource;
	private bool _triggerOnce;
	private float _timeToShowResult;

	[HideInInspector] public static WWW www; 
	private int _questionsInTheScene;
	/*************AudioSource order*********
	 *0 background looping sound
	 *1 title
	 *2 second sound
	 *3 action
	 *4 Result
	 *5 Item(hovering)
	 *6 answer
	 *7 hint
	 *8 Extra
	 *9 Extra Looping
	 */

	private static GameManager _instance;
	public static GameManager Instance
	{
		get
		{
			if (_instance != null)
			{
				return _instance;
			}

			_instance = FindObjectOfType<GameManager>();
			return _instance;
		}
	}


	public void PlayItemSound(AudioClip audioClip)
	{
	
		
		if (audioClip != null)
		{
			if (IsTitleSoundPlaying())
			{
				return;
			}
			_audioSource[5].clip = audioClip;
			_audioSource[5].Play();
		}
	}
	
	

	
	public bool IsTitleSoundPlaying()
	{
		return _audioSource[1].isPlaying;
	}

	


}