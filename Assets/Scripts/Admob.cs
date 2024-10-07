/*
using GoogleMobileAds.Api;
using PreviewLabs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Admob : MonoBehaviour
{
	private sealed class _WaitForNextVideo_c__Iterator0 : IEnumerator, IDisposable, IEnumerator<object>
	{
		internal Admob _this;

		internal object _current;

		internal bool _disposing;

		internal int _PC;

		object IEnumerator<object>.Current
		{
			get
			{
				return this._current;
			}
		}

		object IEnumerator.Current
		{
			get
			{
				return this._current;
			}
		}

		public _WaitForNextVideo_c__Iterator0()
		{
		}

		public bool MoveNext()
		{
			uint num = (uint)this._PC;
			this._PC = -1;
			switch (num)
			{
			case 0u:
				UnityEngine.Debug.Log("Start WaitForNextVideo");
				CanvasUpgrade.instance.CheclAllButtonBoosterInteraction();
				this._current = new WaitUntil(() => this._this.IsRewardAdsLoaded());
				if (!this._disposing)
				{
					this._PC = 1;
				}
				return true;
			case 1u:
				UnityEngine.Debug.Log("End WaitForNextVideo");
				CanvasUpgrade.instance.CheclAllButtonBoosterInteraction();
				this._PC = -1;
				break;
			}
			return false;
		}

		public void Dispose()
		{
			this._disposing = true;
			this._PC = -1;
		}

		public void Reset()
		{
			throw new NotSupportedException();
		}

		internal bool __m__0()
		{
			return this._this.IsRewardAdsLoaded();
		}
	}

	public static Admob instance;

	private string adBanner = "ca-app-pub-3940256099942544/6300978111";

	private string adInterstitial = "ca-app-pub-3940256099942544/1033173712";

	private string adRewarded = "ca-app-pub-3940256099942544/5224354917";

	private BannerView bannerView;

	public InterstitialAd interstitial;

	private RewardBasedVideoAd rewardBasedVideo;

	public bool isAppOpened;

	private bool callRewardAds;

	private bool callInstertitial;

	private bool videoShown;

	private BoosterType boosterType;

	private void Awake()
	{
		Admob.instance = this;
		UnityEngine.Object.DontDestroyOnLoad(this);
		if (Application.platform == RuntimePlatform.Android)
		{
			this.adBanner = "ca-app-pub-3979411024733956/6991548838";
			this.adInterstitial = "ca-app-pub-3979411024733956/4668212792";
			this.adRewarded = "ca-app-pub-3979411024733956/9074678895";
		}
		else if (Application.platform == RuntimePlatform.IPhonePlayer)
		{
			this.adBanner = "ca-app-pub-3940256099942544/2934735716";
			this.adInterstitial = "ca-app-pub-3940256099942544/4411468910";
			this.adRewarded = "ca-app-pub-3940256099942544/1712485313";
		}
	}

	private void Start()
	{
	}

	public void InitAdmob()
	{
		this.rewardBasedVideo = RewardBasedVideoAd.Instance;
		this.rewardBasedVideo.OnAdLoaded += new EventHandler<EventArgs>(this.HandleRewardBasedVideoLoaded);
		this.rewardBasedVideo.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.HandleRewardBasedVideoFailedToLoad);
		this.rewardBasedVideo.OnAdOpening += new EventHandler<EventArgs>(this.HandleRewardBasedVideoOpened);
		this.rewardBasedVideo.OnAdStarted += new EventHandler<EventArgs>(this.HandleRewardBasedVideoStarted);
		this.rewardBasedVideo.OnAdRewarded += new EventHandler<Reward>(this.HandleRewardBasedVideoRewarded);
		this.rewardBasedVideo.OnAdClosed += new EventHandler<EventArgs>(this.HandleRewardBasedVideoClosed);
		this.rewardBasedVideo.OnAdLeavingApplication += new EventHandler<EventArgs>(this.HandleRewardBasedVideoLeftApplication);
		this.RequestInterstitial();
		this.RequestRewardedVideo();
		this.StartCheckRewardAds();
		if (PreviewLabs.PlayerPrefs.GetInt(PlayerPrefKey.KEY_DID_REMOVE_ADS) != 0)
		{
			if (this.bannerView != null)
			{
				this.HideBanner();
			}
		}
		Admob.instance = this;
	}

	public RewardBasedVideoAd GetRewardAds()
	{
		return this.rewardBasedVideo;
	}

	public bool IsRewardAdsLoaded()
	{
		return this.rewardBasedVideo != null && this.rewardBasedVideo.IsLoaded();
	}

	public void RequestBanner()
	{
		this.bannerView = new BannerView(this.adBanner, this.CalculateBannerHeight(), AdPosition.Bottom);
		this.bannerView.OnAdLoaded += new EventHandler<EventArgs>(this.HandleOnAdLoaded);
		this.bannerView.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.HandleOnAdFailedToLoad);
		this.bannerView.OnAdOpening += new EventHandler<EventArgs>(this.HandleOnAdOpened);
		this.bannerView.OnAdClosed += new EventHandler<EventArgs>(this.HandleOnAdClosed);
		this.bannerView.OnAdLeavingApplication += new EventHandler<EventArgs>(this.HandleOnAdLeftApplication);
		this.bannerView.LoadAd(this.CreateAdRequest());
	}

	public AdSize CalculateBannerHeight()
	{
		float dpi = Screen.dpi;
		float num = 1f * (float)Screen.width / (float)Screen.height;
		return AdSize.Banner;
	}

	public void ShowBanner()
	{
		if (PreviewLabs.PlayerPrefs.GetInt(PlayerPrefKey.KEY_DID_REMOVE_ADS) == 0)
		{
			this.bannerView.Show();
		}
		else if (this.bannerView != null)
		{
			this.HideBanner();
		}
	}

	public void HideBanner()
	{
		if (this.bannerView != null)
		{
			this.bannerView.Hide();
		}
	}

	public void HandleOnAdLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleOnAdOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
	}

	public void HandleOnAdClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
	}

	public void HandleOnAdLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeftApplication event received");
	}

	public void RequestInterstitial()
	{
		this.interstitial = new InterstitialAd(this.adInterstitial);
		this.interstitial.OnAdLoaded += new EventHandler<EventArgs>(this.HandleOnAdInterstitialLoaded);
		this.interstitial.OnAdFailedToLoad += new EventHandler<AdFailedToLoadEventArgs>(this.HandleOnAdInterstitialFailedToLoad);
		this.interstitial.OnAdOpening += new EventHandler<EventArgs>(this.HandleOnAdInterstitialOpened);
		this.interstitial.OnAdClosed += new EventHandler<EventArgs>(this.HandleOnAdInterstitialClosed);
		this.interstitial.OnAdLeavingApplication += new EventHandler<EventArgs>(this.HandleOnAdInterstitialLeftApplication);
		this.interstitial.LoadAd(this.CreateAdRequest());
	}

	public void ShowInterstitial()
	{
		if (this.interstitial == null)
		{
			return;
		}
		if (this.interstitial.IsLoaded())
		{
			if (PreviewLabs.PlayerPrefs.GetInt(PlayerPrefKey.KEY_DID_REMOVE_ADS) == 0)
			{
				this.interstitial.Show();
			}
		}
		else
		{
			this.RequestInterstitial();
		}
	}

	public void HandleOnAdInterstitialLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLoaded event received");
	}

	public void HandleOnAdInterstitialFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleFailedToReceiveAd event received with message: " + args.Message);
	}

	public void HandleOnAdInterstitialOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdOpened event received");
		this.callInstertitial = true;
	}

	public void HandleOnAdInterstitialClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdClosed event received");
		this.RequestInterstitial();
	}

	public void HandleOnAdInterstitialLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleAdLeftApplication event received");
	}

	public void RequestRewardedVideo()
	{
		this.rewardBasedVideo.LoadAd(this.CreateAdRequest(), this.adRewarded);
	}

	public void ShowRewardBasedVideo()
	{
		if (this.IsRewardAdsLoaded())
		{
			this.rewardBasedVideo.Show();
			this.isAppOpened = false;
		}
		else
		{
			if (CanvasUpgrade.instance)
			{
				CanvasUpgrade.instance.CheclAllButtonBoosterInteraction();
			}
			this.RequestRewardedVideo();
			MonoBehaviour.print("Reward based video ad is not ready yet");
		}
	}

	public void ShowRewardBasedVideo(BoosterType _boosterType)
	{
		if (this.IsRewardAdsLoaded())
		{
			this.boosterType = _boosterType;
			this.rewardBasedVideo.Show();
			this.isAppOpened = false;
		}
		else
		{
			if (CanvasUpgrade.instance)
			{
				CanvasUpgrade.instance.CheclAllButtonBoosterInteraction();
			}
			this.RequestRewardedVideo();
			MonoBehaviour.print("Reward based video ad is not ready yet");
		}
	}

	public void HandleRewardBasedVideoLoaded(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLoaded event received");
	}

	public void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoFailedToLoad event received with message: " + args.Message);
	}

	public void HandleRewardBasedVideoOpened(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoOpened event received");
		this.callRewardAds = true;
	}

	public void HandleRewardBasedVideoStarted(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoStarted event received");
	}

	public void HandleRewardBasedVideoClosed(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoClosed event received");
		this.RequestRewardedVideo();
	}

	public void HandleRewardBasedVideoRewarded(object sender, Reward args)
	{
		string type = args.Type;
		MonoBehaviour.print("HandleRewardBasedVideoRewarded event received for " + args.Amount.ToString() + " " + type);
		this.videoShown = true;
	}

	public void HandleRewardBasedVideoLeftApplication(object sender, EventArgs args)
	{
		MonoBehaviour.print("HandleRewardBasedVideoLeftApplication event received");
	}

	private AdRequest CreateAdRequest()
	{
		return new AdRequest.Builder().Build();
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			PreviewLabs.PlayerPrefs.SetInt(PlayerPrefKey.KEY_CHECK_APP_OPENED, 0);
		}
		else
		{
			if (this.isAppOpened)
			{
				PreviewLabs.PlayerPrefs.SetInt(PlayerPrefKey.KEY_CHECK_APP_OPENED, 1);
			}
			if (PreviewLabs.PlayerPrefs.GetInt(PlayerPrefKey.KEY_CHECK_APP_OPENED) == 1)
			{
				this.ShowInterstitial();
			}
			else
			{
				this.RequestInterstitial();
				this.isAppOpened = true;
				PreviewLabs.PlayerPrefs.SetInt(PlayerPrefKey.KEY_CHECK_APP_OPENED, 1);
			}
			if (this.callRewardAds)
			{
				UnityEngine.Debug.Log("_Reward Ads");
				if (this.videoShown)
				{
					UnityEngine.Debug.Log("_Rewarded");
					this.RewardAdsCallback();
					this.videoShown = false;
				}
				else
				{
					UnityEngine.Debug.Log("_Closed");
					this.CloseRewardAdsCallback();
				}
				this.callRewardAds = false;
			}
			if (this.callInstertitial)
			{
				this.callInstertitial = false;
			}
		}
	}

	public void RewardAdsCallback()
	{
		if (CanvasUpgrade.instance)
		{
			UnityEngine.Debug.Log("RewardAdsCallback: " + this.boosterType.ToString());
			switch (this.boosterType)
			{
			case BoosterType.Speed:
				CanvasUpgrade.instance.StartSpeedBooster();
				this.boosterType = BoosterType.None;
				break;
			case BoosterType.TimeWarp:
				CanvasUpgrade.instance.GetTimeWarp();
				this.boosterType = BoosterType.None;
				break;
			case BoosterType.Cooldown:
				CanvasUpgrade.instance.StartCooldownReducer();
				this.boosterType = BoosterType.None;
				break;
			case BoosterType.Revenue:
				CanvasUpgrade.instance.StartMoneyBooster();
				this.boosterType = BoosterType.None;
				break;
			}
		}
		if (GameController.instance.canvasPopUp && GameController.instance.canvasPopUp.gameObject.activeSelf)
		{
			GameController.instance.canvasPopUp.GetMoneyX2();
		}
		base.StartCoroutine(this.WaitForNextVideo());
	}

	public void CloseRewardAdsCallback()
	{
		this.boosterType = BoosterType.None;
		base.StartCoroutine(this.WaitForNextVideo());
	}

	public IEnumerator WaitForNextVideo()
	{
		Admob._WaitForNextVideo_c__Iterator0 _WaitForNextVideo_c__Iterator = new Admob._WaitForNextVideo_c__Iterator0();
		_WaitForNextVideo_c__Iterator._this = this;
		return _WaitForNextVideo_c__Iterator;
	}

	public void StartCheckRewardAds()
	{
		base.StartCoroutine(this.WaitForNextVideo());
	}
}
*/