using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;
using Prime31;
using Newtonsoft.Json;
using System.Linq;

using UnionAssets.FLE;

public class UIController : MonoBehaviour 
{


	[System.Serializable]
	public class userdata
	{
		public  string EmailAddress;
		public  string FullName;
		public  string Password;
		public  string Sex;
		public  string DeviceType;
		public  string DeviceToken;
		public  string SessionGUID;

		public string Uniqueid;
		public string LoginType;
	}
	public userdata u;

	public Camera Maincamera;
	public static UIController _Instance;

	public GameObject Man;
	public GameObject Women;


	void Start ()
	{

		Man.SetActive (false);
		Women.SetActive (false);


		if(_Instance==null)
		{
			_Instance=this;
		}
		//StartCoroutine(Download_O_Store("http://ecx.images-amazon.com/images/I/412kC-XN7EL._SL160_.jpg",0));

		//Check for First tile Session id For Auto Login
		if(PlayerPrefs.HasKey("userinfo"))
		{
			userdata temp=Prefs.LoadJson<userdata>("userinfo");
			u=temp;
			//printuserdata();
			if(!isEmptyOrNull(u.SessionGUID))
			{
				//session id is There 
				ChangeScreen(2); //Home Screen
				W_GetImages();   //get images if in this sesstion id data is there
				W_GetAvatar();   //Get Avatars If in this Seesion id is There
				ShowLoading=true;
				Change_Theme();
			}
		}

		#if UNITY_ANDROID
		u.DeviceType="A";
		#endif
		
		#if UNITY_IOS
		u.DeviceType="I";
		#endif
		
		#if UNITY_WP8|| UNITY_WP8_1
		u.DeviceType="W";
		#endif
		u.DeviceToken="zzzzzzzzzzz";


		//W_SignupFbTwitter();
		//W_oGetStores("");
		//V_W_oGetStores("");

	}//End Start


	void OnEnable()
	{
		EtceteraAndroidManager.albumChooserSucceededEvent+=OnTextureLoaded;
		EtceteraAndroidManager.photoChooserSucceededEvent+=OnTextureLoaded;
		EtceteraAndroidManager.albumChooserCancelledEvent+=OnCancelChoose;
		EtceteraAndroidManager.photoChooserCancelledEvent+=OnCancelChoose;
		CropSprite.OnCropDone+=onCropDone;

		//twitter events

		SPTwitter.instance.addEventListener(TwitterEvents.TWITTER_INITED,OnTwitterInit);
		SPTwitter.instance.addEventListener(TwitterEvents.AUTHENTICATION_SUCCEEDED, OnTwitterAuth);	
		SPTwitter.instance.addEventListener(TwitterEvents.USER_DATA_LOADED,OnTwitterUserDataLoaded);
		SPTwitter.instance.addEventListener(TwitterEvents.USER_DATA_FAILED_TO_LOAD, OnTwitterUserDataLoadFailed);
		SPTwitter.instance.Init();
		//SPTwitter.instance.Init(TWITTER_CONSUMER_KEY, TWITTER_CONSUMER_SECRET);

		//SPFacebook.instance.addEventListener(FacebookEvents.FACEBOOK_INITED,  OnFBInit);
		//SPFacebook.instance.addEventListener(FacebookEvents.AUTHENTICATION_SUCCEEDED, OnFBAuth);
		//SPFacebook.instance.addEventListener(FacebookEvents.USER_DATA_LOADED, OnFBUserDataLoaded);
		//SPFacebook.instance.addEventListener(FacebookEvents.USER_DATA_FAILED_TO_LOAD, OnFBUserDataLoadFailed);
		//SPFacebook.instance.Init();
	}



	void OnDisable()
	{
		EtceteraAndroidManager.albumChooserSucceededEvent-=OnTextureLoaded;
		EtceteraAndroidManager.photoChooserSucceededEvent-=OnTextureLoaded;
		EtceteraAndroidManager.albumChooserCancelledEvent-=OnCancelChoose;
		EtceteraAndroidManager.photoChooserCancelledEvent-=OnCancelChoose;
		CropSprite.OnCropDone-=onCropDone;
	}


	public GameObject LoginScreen,SignupScreen,HomeScreen,CropScreen,MenuScreen,ClosetsearchScreen,DressingRoomScreen,PersonalDetailScreen,SettingScreen,SetAvatarScreen,ShowAvatarScreen,LoadingScreen,SearchResultScreen,
						Boy_Dressing_Room;

	public static int Screenid;



	void ChangeScreen(int id)
	{
		InactiveAllScreen();
		Screenid=id;
		switch(id)
		{

			// Login Screen 
			case 0: LoginScreen.SetActive(true);
					InvalidLoginMessage.text="";
					break;	
			//SignupScreen
			case 1: SignupScreen.SetActive(true);
					invalidSignupMessage.text="";
					break;	

			//Home Screen
			case 2: HomeScreen.SetActive(true);
					MenuScreen.SetActive(true);
					Txt_Menuscreen_title.text="Screen Shop";
					break;	

			//CropScreen
			case 3: CropScreen.SetActive(true);
					
					break;
			//ClosetSearch Screen
			case 4:ClosetsearchScreen.SetActive(true);
				   MenuScreen.SetActive(true);
				   Txt_Menuscreen_title.text="Closet Search";
					break;

			case 5:

			 Application.LoadLevel("startScreen");
			Debug.Log(" Gender selection scene open (startScreen) ");
					
					//DressingRoomScreen.SetActive(true);
				   //MenuScreen.SetActive(true);
				   //Txt_Menuscreen_title.text="My Dressing Room";
			Debug.Log(" Previously Case5: dressing_room  was selected ");
				   break;


			case 6:PersonalDetailScreen.SetActive(true);
					MenuScreen.SetActive(true);
					Txt_Menuscreen_title.text="Personal Detail";
					break;
		   //setting screen
		    case 7: 
					SettingScreen.SetActive(true);
					MenuScreen.SetActive(true);
					Txt_Menuscreen_title.text="Settings";
					break;
			//Set Avatar
			case 8: 
				   SetAvatarScreen.SetActive(true);
				   MenuScreen.SetActive(true);
				   Txt_Menuscreen_title.text="Set Avatar";
				   break;
			case 9:
					ShowAvatarScreen.SetActive(true);
					MenuScreen.SetActive(true);
					Txt_Menuscreen_title.text="Avatars";
					break;
		case 10:  //SearchResultScreen
			SearchResultScreen.SetActive(true);
			MenuScreen.SetActive(true);
			Txt_Menuscreen_title.text="Search Result";
			break;

		case 11:
			
		
			Debug.Log(" Gender selection scene open (startScreen) ");


			Boy_Dressing_Room.SetActive(true);
			MenuScreen.SetActive(true);
			Txt_Menuscreen_title.text="Boy Dressing";
			Debug.Log(" Previously Case11: boy dressing_room  was selected ");
			break;
		}
	}

	void InactiveAllScreen()
	{
		Boy_Dressing_Room.SetActive(false);
		LoginScreen.SetActive(false);
		SignupScreen.SetActive(false);
		HomeScreen.SetActive(false);
		CropScreen.SetActive(false);
		MenuScreen.SetActive(false);
		ClosetsearchScreen.SetActive(false);
		DressingRoomScreen.SetActive(false);
		PersonalDetailScreen.SetActive(false);
		SettingScreen.SetActive(false);
		SetAvatarScreen.SetActive(false);
		ShowAvatarScreen.SetActive(false);
		btnswapepanel.SetActive(false);
		SearchResultScreen.SetActive(false);
		MenuStatus=false;
	}

	private void printuserdata()
	{
		Debug.Log("-----------------------------------------");
		Debug.Log("FullName="+u.FullName);
		Debug.Log("Email="+u.EmailAddress);
		Debug.Log("Pass="+u.Password);
		Debug.Log("session="+u.SessionGUID);
		Debug.Log("devicetype="+u.DeviceType);
		Debug.Log("devicetocken="+u.DeviceToken);
		Debug.Log("Uniqueid="+u.Uniqueid);
		Debug.Log("LoginType="+u.LoginType);
		Debug.Log("-----------------------------------------");
	}







	//---------------------------------------------------------------------------------------
	//---------------------------------------------- 3 Login Screen ----------------------------


	public InputField email,pass;  //login screen uname and pass
	public Text InvalidLoginMessage;



	public void Signin()
	{
		email.text=email.text.Trim();
		pass.text=pass.text.Trim();

	
		//Check login Field Validation
		InvalidLoginMessage.text="";
		if(isEmptyOrNull(email.text))
		{
			InvalidLoginMessage.text="Enter Username";
		}
		if(isEmptyOrNull(pass.text))
		{
			InvalidLoginMessage.text+=" Enter Password";
		}

		if(isEmptyOrNull(InvalidLoginMessage.text))
		{
			Hashtable data=new Hashtable();
			data.Add(Webservice.Login.EmailAddress,email.text);
			data.Add(Webservice.Login.Password,pass.text);
			data.Add(Webservice.Login.Devicetype,u.DeviceType);
			data.Add(Webservice.Login.DeviceToken,u.DeviceToken);

			ShowLoading=true;
			HTTP.Request theRequest=new HTTP.Request("post",Webservice.Login.url,data);
			theRequest.Send((r)=>
				{
					Hashtable result=r.response.Object;
					if(result==null)
					{
						Debug.LogWarning( "Could not parse JSON response!" );
						return;
					}else
						{

							bool issuccess=(bool)result["Success"];
							string Message=(string)result["Message"];
							string SessionGUID=(string)result["SessionGUID"];
							string Sex=(string)result["Sex"];

							if(issuccess)
							{
								InvalidLoginMessage.text=Message;

								u.EmailAddress=email.text;
								u.Password=pass.text;
								u.SessionGUID=SessionGUID;
								u.Sex=Sex;

								Debug.Log(SessionGUID);

								SaveUserData();
								Change_Theme();
								//Change Theme Hear as per Male And Female
								ChangeScreen(2); //home screen

								W_GetImages();   //get images if in this sesstion id data is there
								W_GetAvatar();   //On Successufl login Get Avatar Data


							}else
								{
									InvalidLoginMessage.text="Invalid Email or Password";
									ShowLoading=false;
								}

							/*
							foreach(DictionaryEntry a in result)
							{
								Debug.Log(a.Key + "--->" + a.Value);
							}*/
						}
				});
		}

	} //End Signin


	
	public void GotoSignup()
	{
		ClearSignupField();
		ChangeScreen(1); //signup screen            
	}



	//---------------------------------------------------------------------------------------
	//---------------------------------------------------------------------------------------




	//---------------------------------------------------------------------------------------
	//-------------------------------------------2 Signup Screen--------------------------------


	public InputField sup_uname,sup_pass,sup_email;
	public Text invalidSignupMessage;

	private void ClearSignupField()
	{
		sup_uname.text="";
		sup_pass.text="";
		sup_email.text="";
		invalidSignupMessage.text="";
	}

	public void Signup()
	{
		invalidSignupMessage.text="";
		sup_uname.text=sup_uname.text.Trim();
		sup_pass.text=sup_pass.text.Trim();
		sup_email.text=sup_email.text.Trim();

		//check for username
		if(isEmptyOrNull(sup_uname.text))
		{
			invalidSignupMessage.text="Enter Username";
		}
		else
		{
			u.FullName=sup_uname.text;
		}

		//check for Password
		if(isEmptyOrNull(sup_pass.text))
		{
			invalidSignupMessage.text+=" Enter Password";
		}
		else
		{
			u.Password=sup_pass.text;
		}

		//Check for Email
		if(isEmail(sup_email.text))
		{
			u.EmailAddress=sup_email.text;
		}
		else
		{
			invalidSignupMessage.text+=" Invalid Email";
		}
		//Set Gender
		u.Sex=Gender;

		if(isEmptyOrNull(invalidSignupMessage.text))
		{
			W_Signup();
			ShowLoading=true;
		}
	} //End Signup Button


	private void W_Signup()
	{
		Hashtable data=new Hashtable();
		data.Add(Webservice.Register.EmailAddress,u.EmailAddress);
		data.Add(Webservice.Register.FullName,u.FullName);
		data.Add(Webservice.Register.Password,u.Password);
		data.Add(Webservice.Register.Sex,u.Sex);
		data.Add(Webservice.Register.Devicetype,u.DeviceType);
		data.Add(Webservice.Register.DeviceToken,u.DeviceToken);

		HTTP.Request theRequest=new HTTP.Request("post",Webservice.Register.url,data);

		theRequest.Send((r) => 
		{
			Hashtable result=r.response.Object;
			if(result==null)
			{
				Debug.LogWarning( "Could not parse JSON response!" );
				return;
			}
			else
			{
				bool issuccess=(bool)result["Success"];
				string Message=(string)result["Message"];
				string SessionGUID=(string)result["SessionGUID"];

				if(issuccess)
				{
					u.SessionGUID=SessionGUID;
					Debug.Log(Message);
					Debug.Log(SessionGUID);
					SaveUserData();
					ShowLoading=false;
					Change_Theme();
					ChangeScreen(2); //home screen

					W_GetImages();   //get images if in this sesstion id data is there
					W_GetAvatar();   //On Successufl login Get Avatar Data
				}
				else
				{
					invalidSignupMessage.text=Message;
					ShowLoading=false;
				}
				/*
				foreach(DictionaryEntry a in result)
				{
					Debug.Log(a.Key + "--->" + a.Value);
				}*/
			}
		});
	}  //End WSignup




	public UnityEngine.UI.Toggle male;
	private string Gender="M";
	public void ToogleGender()
	{
		if(male.isOn)
		{
			Gender="M";
		}
		else
		{
			Gender="F";
		}
	}

	public void GotoLogin()
	{
		InvalidLoginMessage.text="";
		ChangeScreen(0); //login screen
	}





	//SignUP FACEBOOK TWITTER



	private string LoginType="";
	public void Btn_SignupWithTwitter()
	{
		LoginType="T";
		if(!IsTwitterAuthencated) 
		{
			SPTwitter.instance.AuthenticateUser();
		}
	}
	public void Btn_SignupWithFB()
	{
		LoginType="F";
		if(!IsFBAuthencated)
		{
			//SPFacebook.instance.Login();
		}
	}
	//call for Webservice


	private void W_SignupFbTwitter()
	{
		Hashtable data=new Hashtable();
		data.Add(Webservice.Fbtwitter.EmailAddress,u.EmailAddress);//
		data.Add(Webservice.Fbtwitter.FullName,u.FullName);//u.FullName
		data.Add(Webservice.Fbtwitter.Type,u.LoginType);
		data.Add(Webservice.Fbtwitter.UniqueID,u.Uniqueid);
		data.Add(Webservice.Fbtwitter.Devicetype,u.DeviceType);
		data.Add(Webservice.Fbtwitter.DeviceToken,u.DeviceToken);

		HTTP.Request theRequest=new HTTP.Request("post",Webservice.Fbtwitter.url,data);

		theRequest.Send((r)=>
		{
			Hashtable result=r.response.Object;
			if(result==null)
			{
				Debug.LogWarning( "Could not parse JSON response!" );
				return;
			}
			else
			{
				bool issuccess=(bool)result["Success"];
				string Message=(string)result["Message"];
				string SessionGUID=(string)result["SessionGUID"];
				if(issuccess)
				{
					u.SessionGUID=SessionGUID;
					Debug.Log(Message);
					Debug.Log(SessionGUID);
					SaveUserData();
					printuserdata();
					Change_Theme();
					ChangeScreen(2); //home screen

					W_GetImages();   //get images if in this sesstion id data is there
					W_GetAvatar();   //On Successufl login Get Avatar Data
				}
				else
				{
					invalidSignupMessage.text=Message;
					ShowLoading=false;
				}
			}

			foreach(DictionaryEntry a in result)
			{
				Debug.Log("-->>"+a.Key + "--->" + a.Value);
			}
		});       
	}


	//-------------EVENTS FOR TWITTER
	private static bool IsTwitterAuthencated= false;
	void OnTwitterInit()
	{
		Debug.Log("++++++++++++Twitter Init Done");
	}
	void OnTwitterAuth()
	{
		Debug.Log("++++++++++++Twitter Authencation Done");
		IsTwitterAuthencated=true;
		//load userdata hear
		SPTwitter.instance.LoadUserData();
		ShowLoading=true;
	}
	void OnTwitterUserDataLoaded()
	{
		Debug.Log("++++++++++++Twitter user Data loaded Done");

		u.EmailAddress=SPTwitter.instance.userInfo.name+"_"+SPTwitter.instance.userInfo.id+"@gmail.com";
		u.FullName=SPTwitter.instance.userInfo.name;
		u.LoginType=LoginType;
		u.Uniqueid=SPTwitter.instance.userInfo.id;
		//DeviceToken and DeviceToken set in start


		
		//call Webservice hear
		W_SignupFbTwitter();
	}
	void OnTwitterUserDataLoadFailed()
	{
		Debug.Log("++++++++++++Twitter Fata Load Failed");
		ShowLoading=false;
	}


	//---------------------------EVENTS FOR FACEBOOk---------------------------------
	private static bool IsFBAuthencated= false;
	void OnFBInit()
	{
		Debug.Log("++++++++++++Facebook Init Done");
	}
	void OnFBAuth()
	{
		Debug.Log("++++++++++++FACEBOOK Authencation Done");
		IsFBAuthencated=true;
		//SPFacebook.instance.LoadUserData();
		ShowLoading=true;
	}
	void OnFBUserDataLoaded()
	{
		//u.EmailAddress=SPFacebook.instance.userInfo.email;

		//u.FullName=SPFacebook.instance.userInfo.first_name;
		//u.LoginType=LoginType;
		//u.Uniqueid=SPFacebook.instance.userInfo.id;
		//DeviceToken and DeviceToken set in start

		//call Webservice hear
		W_SignupFbTwitter();

	}
	void OnFBUserDataLoadFailed()
	{
		Debug.Log("++++++++++++FACEBOOK data Load Failed");
		ShowLoading=false;
	}



































	//-------------------------------------End Signup-----------------------------------------
	//---------------------------------------------------------------------------------------






	
	//------------------------------------------------------------------------------------------------------
	//-------------------------------------------4 Screen Shop (home Screen)--------------------------------

	public Transform transphoto;
	public Texture2D EditorTexturetest;
	public void Btn_Capture_Product()   //Open a Camera to take a photo
	{
		EtceteraAndroid.promptToTakePhoto((Screen.width),(Screen.height),"photo.jpg");

		#if UNITY_EDITOR
			OnTextureLoaded("test",EditorTexturetest);
		#endif
	}
	public void Btn_Search_Product()    //Open a gallary to selecte photo
	{
		EtceteraAndroid.promptForPictureFromAlbum( (Screen.width),(Screen.height), "albumImage.jpg" );
	}


	//Event for Camera and photos
	private void OnTextureLoaded(string path,Texture2D Img)
	{
		//Debug.Log("****Ontexture loaded called"+path);
		ChangeScreen(3); //image crop screen

		//scale photo quad
		/*
		float h,w;
		h=Maincamera.orthographicSize *2.0f;
		w=h * Screen.width/Screen.height;
		transphoto.localScale=new Vector3(w,h,1f);
		*/

		//set image to photo
		//transphoto.GetComponent<SpriteRenderer>().sprite=Img;
		transphoto.GetComponent<SpriteRenderer>().sprite=Sprite.Create(Img,new Rect(0,0,Img.width,Img.height),new Vector2(0.5f,0.5f),100f);
		transphoto.gameObject.SetActive(true);


		Btn_Reselect_Item();
	}
	private void OnCancelChoose()
	{
		Debug.Log("****OncancelChoose called");
	}


	public void Btn_Cropscreen_Back()
	{
		ChangeScreen(2);//goto Home SCreen
		transphoto.gameObject.SetActive(false);

		CropSprite.Instance.ReCrop();
		Button_CropScreen_Crop.interactable=true;
	}
	public void Btn_Cropscreen_Crop()
	{
		CropSprite.StopCrop=false;
		Button_CropScreen_Crop.interactable=false;
	}

	private Texture2D tex;
	public Button Button_Confirm_Selection;
	public Button Button_CropScreen_Crop;

	void onCropDone()
	{
		Button_Confirm_Selection.interactable=true;
	}

	public void Btn_Confirm_Selection()
	{

		//upload photo to WEBSERVICE
		tex=CropSprite.Instance.CroppedImage;
			
		if(tex==null)	
			return;

		CS_Reset(); 

		Button_Confirm_Selection.interactable=false;

		Byte[] b=tex.EncodeToPNG();
			string i=Convert.ToBase64String(b);

			W_UploadPNG(i);
			ShowLoading=true;
	}


	void W_UploadPNG(string i)
	{
		Hashtable data=new Hashtable();
		data.Add(Webservice.SaveSearchItem.SessionGUID,u.SessionGUID);
		data.Add(Webservice.SaveSearchItem.ImageData,i);
		HTTP.Request theRequest=new HTTP.Request("post",Webservice.SaveSearchItem.url,data);
		
		theRequest.Send((r) => 
		{
			Hashtable result=r.response.Object;
			if(result==null)
			{
				Debug.LogWarning( "Could not parse JSON response!" );
				return;
			}
			else
			{
				bool issuccess=(bool)result["Success"];
				string Message=(string)result["Message"];
				string ImageUri=(string)result["ImageURI"];
	
				if(issuccess)
				{
					autoShowCsScreen=true;
					W_GetImages();
				}
				else
				{

				}

				foreach(DictionaryEntry a in result)
				{
					Debug.Log(a.Key + "--->" + a.Value);
				}
			}
		});
	}//upload png

	List<string> ImagesURL;
	List<string> Discription;
	public List<Sprite> tex_downloaded;


	void W_GetImages()
	{
		ImagesURL=new List<string>();
		Discription=new List<string>();
	
		ImagesURL.Clear();
		Discription.Clear();

		Hashtable data=new Hashtable();
		data.Add(Webservice.GetItemsList.SessionGUID,u.SessionGUID);

		HTTP.Request theRequest=new HTTP.Request("post",Webservice.GetItemsList.url,data);
		
		theRequest.Send((r) => 
		{
			Hashtable result=r.response.Object;
			if(result==null)
			{
				Debug.LogWarning( "Could not parse JSON response!" );
				return;
			}
			else
			{
				bool issuccess=(bool)result["Success"];
				string Message=(string)result["Message"];

				if(issuccess)
				{
					ArrayList img=new ArrayList();
					img=(ArrayList)result["Items"];

					Debug.Log("Total Image Found="+img.Count);
					if(img.Count==0)
					{
						ShowLoading=false;
					}
					foreach(Hashtable ob in img)
					{
						//Debug.Log(ob["ItemGUID"]+""+ob["ItemURL"]+""+ob["ItemNotes"]);
						ImagesURL.Add((string)ob["ItemURL"]);
						Discription.Add((string)ob["ItemNotes"]);

						//if(ImagesURL.Count>=10)
						//	break;
					}
					Debug.Log("Imageurl count=="+ImagesURL.Count);
					DownloadPNGFromURL(ImagesURL);
				}
				else
				{
					ShowLoading=false;
				}
				/*
				foreach(DictionaryEntry a in result)
				{
					Debug.Log(a.Key + "--->" + a.Value);
				}*/
			}
		});
	}//End Getpng


	private int No_of_tex;
	private static int NoofCurrentdownload;
	private void DownloadPNGFromURL(List<string> Imageurl)
	{
		tex_downloaded.Clear();
		Information.Clear();

		No_of_tex=0;
		NoofCurrentdownload=0;



		for(int i=Imageurl.Count-1;i>=0;i--)
		{
			StartCoroutine(Downloadpng(Imageurl.ElementAt(i),Discription.ElementAt(i)));
			if((Imageurl.Count - i )>10)
				break;
			No_of_tex++;
		}
	}

	public List<string> Information;
	IEnumerator Downloadpng(string url,string discription)
	{
		WWW w=new WWW(url);
		yield return w;

		Texture2D temp_tex=w.texture;
		Rect r=new Rect(0,0,temp_tex.width,temp_tex.height);

		tex_downloaded.Add(Sprite.Create(temp_tex,r,new Vector2(0,0)));
		Information.Add((string)(discription+"@"+url));

		NoofCurrentdownload++;
		if(NoofCurrentdownload==No_of_tex)
		{
			AddButtontoScrollbar();
			ShowLoading=false;
		}
	}


	public void Btn_Reselect_Item()
	{
		CropSprite.Instance.ReCrop();
		Button_Confirm_Selection.interactable=false;
		Button_CropScreen_Crop.interactable=true;
	}


	//------------------------------------------------------------------------------------------------------
	//-----------------------------------------End Screen Shop (home Screen)--------------------------------





	//------------------------------------------------------------------------------------------------------
	//-----------------------------------------ClosetSearch SCreen------------------------------------------
	public Transform cs_scrollcontentparent;
	public GameObject cs_Button_prefab;
	public Scrollbar cs_scrollbar;

	static bool autoShowCsScreen;
	private void AddButtontoScrollbar()
	{
		Debug.Log("Addbuttontoscroolbar called"+autoShowCsScreen);
		/*for(int i=0;i<tex_downloaded.Count;i++)
		{
			GameObject temp=GameObject.Instantiate(cs_Button_prefab)as GameObject;
			Button b=temp.transform.GetChild(2).GetComponent<Button>();
			string imagepath=Information.ElementAt(i);
			b.onClick.AddListener(()=> Btn_SearchItem(imagepath));

			temp.transform.parent=cs_scrollcontentparent;
			temp.transform.localScale=Vector3.one;
			cs_scrollbar.numberOfSteps=Information.Count;
			cs_scrollbar.value=0f;
			//set Discription
			temp.transform.GetChild(0).GetComponent<Text>().text=Information.ElementAt(i).Split('@')[0];
			//set Image
			temp.transform.GetChild(1).GetComponent<Image>().sprite=tex_downloaded.ElementAt(i);
		}*/

		if(autoShowCsScreen)
		{
			Debug.Log("AddButtontoScrollbarGoto closetauto");
			autoShowCsScreen=false;
			ChangeScreen(4);  //goto closet search screen
		}

	}

	//Goto Home Scren from Closet Screen Search
	private void CS_Reset()
	{
		/*for(int i=0;i<cs_scrollcontentparent.childCount;i++)
		{
			Destroy(cs_scrollcontentparent.GetChild(i).gameObject);
			Destroy(tex_downloaded.ElementAt(i));
		}*/

		for(int i=0;i<tex_downloaded.Count;i++)
		{
			Destroy(tex_downloaded.ElementAt(i));
		}
		Resources.UnloadUnusedAssets();
	}





	public void Btn_SearchItem(string path)
	{
		//Debug.Log(index.Split('@')[1]);
		W_oGetStores(path);
		ChangeScreen(10);
		SR_Reset();
		ShowLoading=true;
	}



	public List<OnlineStore> O_Stores;
	[System.Serializable]
	public class OnlineStore
	{
		public List<string> O_imageLink;
		public string O_location;
		public string O_price;
		public string O_title;
		public string O_url;
		
	}
	
	public List<LocalStore> L_Stores;
	[System.Serializable]
	public class LocalStore
	{
		//	public List<List<string>> L_Addresses;
		public List<string> L_imageLink;
		public string L_StoreName;
		public string L_StoreUrl;		
	}
	public ArrayList Store_img=new ArrayList();
	public ArrayList Online_img=new ArrayList();

	private void W_oGetStores(string imgurl)
	{
		Debug.Log("WEbservice Called=="+imgurl);
		Hashtable data=new Hashtable();
		data.Add(Webservice.oGetStores.imageUrl,imgurl);//"https://s3.amazonaws.com/screenshopavatars/dc4fd4f8-e2d9-4b87-a59a-09feb217543a.png");
		data.Add(Webservice.oGetStores.latitude,43.7182412);
		data.Add(Webservice.oGetStores.longitude,-79.378058);
		
		HTTP.Request theRequest=new HTTP.Request("post",Webservice.oGetStores.url,data);
		
		Debug.Log("Request Send ");
		theRequest.Send((r) => 
		     {
			
			Hashtable result=r.response.Object;
			Debug.Log("Response got"+result.Count);  // 3 arry list
			if(result==null)
			{
				Debug.LogWarning( "Could not parse JSON response!" );
				//return;
			}
			else
			{

				Online_img=(ArrayList)result["OnlineStores"];
				Debug.Log("Total OnlineStores Found="+Online_img.Count);
				
				
				Store_img=(ArrayList)result["Stores"];
				Debug.Log("Total Stores Found="+Store_img.Count);
				
				
				
				int O_count=0;
				foreach(Hashtable ob in Online_img)
				{
					O_Stores.Add(new OnlineStore());
					if(	O_Stores[O_count] != null)
					{
						O_Stores[O_count].O_location=(string)ob["Location"];
						O_Stores[O_count].O_price=(string)ob["Price"];
						O_Stores[O_count].O_title=(string)ob["Title"];
						O_Stores[O_count].O_url=(string)ob["Url"];
						O_Stores[O_count].O_imageLink=new List<string>();
						foreach(string s in (ArrayList)ob["ImageLinks"] )
						{
							O_Stores[O_count].O_imageLink.Add(s);
						}
						if(O_Stores[O_count].O_imageLink.Count>0)
						{
							StartCoroutine( Download_O_Store(O_Stores[O_count].O_imageLink.ElementAt(0),O_count) );
							Debug.Log("======"+O_Stores[O_count].O_imageLink.ElementAt(0));

						}
					}	
					
					O_count++;
				}
				O_count=0;
				
				ShowLoading=false;
				Btn_Online();
				
				int L_count=0;
				foreach(Hashtable ob in Store_img)
				{

					
					L_Stores.Add(new LocalStore());
					if(	L_Stores[L_count] != null){

						L_Stores[L_count].L_StoreName=(string)ob["StoreName"];
						L_Stores[L_count].L_StoreUrl=(string)ob["StoreUrl"];
						L_Stores[L_count].L_imageLink=new List<string>();
						//	L_Stores[L_count].L_Addresses=new List<string>();
						foreach(string s in (ArrayList)ob["ImageLinks"] )
						{
							L_Stores[L_count].L_imageLink.Add(s);
						}
						if(L_Stores[L_count].L_imageLink.Count>0)
						{
							StartCoroutine(Download_L_Store(L_Stores[L_count].L_imageLink.ElementAt(0),L_count));
							Debug.Log("****"+L_Stores[L_count].L_imageLink.ElementAt(0));
						}
						//						foreach(Hashtable s in (ArrayList)ob["Addresses"] )
						//						{
						//							foreach(DictionaryEntry a in s)
						//							{
						//								Debug.Log(a.Key + "--->" + a.Value);
						//							}
						//
						//							//L_Stores[L_count].L_Addresses.Add(s);
						//						}
					}	
					
					L_count++;
				}
				L_count=0;
				

				
				
				
				
				
				Debug.Log("------------------------");
				foreach(DictionaryEntry a in result)
				{
					Debug.Log(a.Key + "--->" + a.Value);
				}
			}
		});
	}





	public GameObject PanelOnline,PanelOffline;
	public Transform Parent_offline,Parent_online;
	public GameObject btn_storeonline,btn_storeoffline;

	public void Btn_Online()
	{
		PanelOnline.SetActive(true);
		PanelOffline.SetActive(false);
	}
	public void Btn_offline()
	{
		PanelOnline.SetActive(false);
		PanelOffline.SetActive(true);
	}


	IEnumerator  Download_O_Store(string firsturl,int index)
	{
		Debug.Log("==>"+firsturl);
		WWW w=new WWW(firsturl);
		yield return w;
		Texture2D tex2d=w.texture;
		Rect r=new Rect(0,0,tex2d.width,tex2d.height);
		GameObject temp=GameObject.Instantiate(btn_storeonline)as GameObject;
		temp.transform.GetChild(0).GetComponent<Image>().sprite=Sprite.Create(tex2d,r,Vector2.zero);
		temp.transform.parent=Parent_online;
		temp.transform.localScale=Vector3.one;


		temp.transform.GetChild(1).GetChild(0).GetComponent<Text>().text=O_Stores[index].O_title;
		temp.transform.GetChild(1).GetChild(1).GetComponent<Text>().text=O_Stores[index].O_url;
		temp.transform.GetChild(1).GetChild(2).GetComponent<Text>().text=O_Stores[index].O_price;
	}

	private void SR_Reset()
	{
		for(int i=0;i<Parent_online.childCount;i++)
		{
			Destroy(Parent_online.GetChild(i));
		}
		for(int i=0;i<Parent_offline.childCount;i++)
		{
			Destroy(Parent_offline.GetChild(i));
		}
	}

	IEnumerator  Download_L_Store(string firsturl,int index)
	{
		WWW w=new WWW(firsturl);
		yield return w;
		Texture2D tex2d=w.texture;
		Rect r=new Rect(0,0,tex2d.width,tex2d.height);
		GameObject temp=GameObject.Instantiate(btn_storeoffline)as GameObject;
		temp.transform.GetChild(0).GetComponent<Image>().sprite=Sprite.Create(tex2d,r,Vector2.zero);
		temp.transform.parent=Parent_offline;
		temp.transform.localScale=Vector3.one;
		

		temp.transform.GetChild(1).GetChild(0).GetComponent<Text>().text=L_Stores[index].L_StoreName;
		temp.transform.GetChild(1).GetChild(1).GetComponent<Text>().text=L_Stores[index].L_StoreUrl;
		//temp.transform.GetChild(1).GetChild(2).GetComponent<Text>().text=O_Stores[index].O_price;
	}






	
	//-----------------------------------------End ClosetSearch SCreen--------------------------------
	//------------------------------------------------------------------------------------------------------








	
	//------------------------------------------------------------------------------------------------------
	//-------------------------------------------8 PERSONAL DETAIL------------------------------------------
	public GameObject[] img_selectedGender;  //selected Gender change on Login as per gender

	public InputField Txt_Height,Txt_Weight,Txt_Chest;


	public void Btn_PD_GotoDressingRoom()
	{
		ChangeScreen(5);
	}

	public void GotoBoyDressingRoom()
	{
		ChangeScreen(11);
	}

	//hight Scroller
	public void inc(String text)
	{	

		if(text=="H")
		{
			Txt_Height.text=Incrementbyone(Txt_Height.text,500);
		}
		else if(text=="W")
		{
			Txt_Weight.text=Incrementbyone(Txt_Weight.text,500);
		}
		else if(text=="C")
		{
			Txt_Chest.text=Incrementbyone(Txt_Chest.text,99);
		}
	}
	public void dec(String text)
	{

		if(text=="H")
		{
			Txt_Height.text=Decrementbyone(Txt_Height.text,0);
		}
		else if(text=="W")
		{
			Txt_Weight.text=Decrementbyone(Txt_Weight.text,0);
		}
		else if(text=="C")
		{
			Txt_Chest.text=Decrementbyone(Txt_Chest.text,0);
		}
	}

	private string Incrementbyone(string val,int max)
	{
		int i=int.Parse(val);

		if(i<max)
		return (i+1).ToString();
		else
		return val;
	}
	private string Decrementbyone(string val,int min)
	{
		int i=int.Parse(val);

		if(i>min)
			return (i-1).ToString();
		else
			return val;
	}



		
	//------------------------------------------------------------------------------------------------------
	//-------------------------------------------END PERSONAL DETAIL--------------------------------------------



	//------------------------------------------------------------------------------------------------------
	//-------------------------------------------View Avatar Screen--------------------------------------------

	[System.Serializable]
	public class Avatarss
	{
		public string AvatarGUID;
		public string AvatarImage; //image url
		public string AvatarName;
		public string Height;
		public string Sex;
		public string Width;
		public Sprite AvatarSprite;
	}

	public List<Avatarss> avatars;
	private static int NoofAvatars;
	public Transform VA_Parent;
	public GameObject VA_Button_Prefab;

	void W_GetAvatar()
	{
		ScrollRect_Avatars.enabled=true;
		Hashtable data=new Hashtable();
		data.Add(Webservice.GetAvatarDetails.SessionGUID,u.SessionGUID);
		
		HTTP.Request theRequest=new HTTP.Request("post",Webservice.GetAvatarDetails.url,data);
		
		theRequest.Send((r) => 
		                {
			Hashtable result=r.response.Object;
			if(result==null)
			{
				Debug.LogWarning( "Could not parse JSON response!" );
				return;
			}
			else
			{
				bool issuccess=(bool)result["Success"];
				string Message=(string)result["Message"];
				
				
				if(issuccess)
				{
					ArrayList img=new ArrayList();
					img=(ArrayList)result["AvatarList"];
					
					Debug.Log("Total Avatar Found="+img.Count);
					NoofAvatars=img.Count;
					foreach(Hashtable ob in img)
					{
						//Debug.Log("AvatarGUID=="+ob["AvatarGUID"]    +"AvatarImage=="+ob["AvatarImage"]   +"AvatarName=="+ob["AvatarName"]  +"Height=="+ob["Height"]    +"Sex=="+ ob["Sex"]    +"Width=="+ ob["Width"]);
						StartCoroutine(DownloadAvatar(ob));
					}



				}
				else
				{

				}
				/*
				foreach(DictionaryEntry a in result)
				{
					Debug.Log(a.Key + "--->" + a.Value);
				}*/
			}
		});
	}//End W_getAvatars

	

	IEnumerator DownloadAvatar(Hashtable Ht)
	{
		Avatarss temp=new Avatarss();
		temp.AvatarImage=(string)Ht["AvatarImage"];
		temp.AvatarGUID=(string)Ht["AvatarGUID"];
		temp.AvatarName=(string)Ht["AvatarName"];
		temp.Height=(string)Ht["Height"];
		temp.Sex=(string)Ht["Sex"] ;
		temp.Width=(string)Ht["Width"];


		WWW w=new WWW(temp.AvatarImage);
		yield return w;
		Texture2D tex2d=w.texture;
		Rect r=new Rect(0,0,tex2d.width,tex2d.height);
		temp.AvatarSprite=Sprite.Create(tex2d,r,Vector2.zero);
		avatars.Add(temp);

		if(avatars.Count==NoofAvatars)
		{
			//Call Only Once
			AddAvatartoScrollbar();

		}

	}

	public ScrollRect ScrollRect_Avatars;
	private void AddAvatartoScrollbar()
	{
		for(int i=0;i<avatars.Count;i++)
		{
			GameObject temp=GameObject.Instantiate(VA_Button_Prefab)as GameObject;

			temp.transform.parent=VA_Parent;
			temp.transform.localScale=Vector3.one;

			//set Discription
			temp.transform.GetChild(0).GetComponent<Text>().text=avatars.ElementAt(i).AvatarName;
			//set Image
			temp.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite=avatars.ElementAt(i).AvatarSprite;
		}
		if(avatars.Count>4)
		{
			//Enable Scroll Rect
			ScrollRect_Avatars.enabled=true;
		}

	
	}
	private void VA_Reset()
	{
		for(int i=0;i<VA_Parent.childCount;i++)
		{
			Destroy(VA_Parent.GetChild(i).gameObject);
			Destroy(avatars.ElementAt(i).AvatarSprite);
		}
		avatars.Clear();
		Resources.UnloadUnusedAssets();
	}


	//------------------------------------------------------------------------------------------------------
	//-------------------------------------------End View Avatar Screen--------------------------------------------







	//------------------------------------------------------------------------------------------------------
	//-------------------------------------------9 (Menu Screen)--------------------------------------------
	public Animator Anim_Panel;
	private bool MenuStatus=false;
	public Text Txt_Menuscreen_title;
	public GameObject btnswapepanel;


	//Menu Screen on Off Button
	public void Btn_MenuScreen()
	{
		if(MenuStatus) //on
		{
			//off
			Anim_Panel.Play("AnimMenuOFF");
			MenuStatus=false;
			btnswapepanel.SetActive(false);
			Debug.Log("oooooonnnnnnnn");
		}
		else
		{ 
			//on
			Anim_Panel.Play("AnimMenuOn");
			MenuStatus=true;
			btnswapepanel.SetActive(true);
			Debug.Log("ooofffffffffff");

			Man.SetActive(false);
			Women.SetActive(false);



		}

	}
	public void Btn_Menustar()
	{

	}
	public void Btn_Menu_Home()
	{
		ChangeScreen(2);
	}

	public void Btn_Menu_GetAvatars()
	{
		//Get Avatar Webservice Call
		ChangeScreen(9);//got to Show Avatar screen
	}
	public void Btn_Menu_SetAvatars()
	{
		ChangeScreen(8);
	}
	public void Btn_Menu_MyDressingRoom()
	{
		Man.SetActive (false);
		Women.SetActive (false);

		ChangeScreen(5);  //go to MyDressing Room

	}
	public void Btn_Menu_MyCloset()
	{
		ChangeScreen(4); //Go to Mycloset Screen
	}
	public void Btn_Menu_Profile()
	{
		ChangeScreen(6);  //goto personal Detail 
	}
	public void Btn_Menu_Setting()
	{
		ChangeScreen(7);//goto Setting Screen
	}

	public void Btn_Logout()
	{
		if(IsTwitterAuthencated)
		{
			IsTwitterAuthencated=false;
			SPTwitter.instance.LogOut();
		}


		u.SessionGUID=null;
		SaveUserData();
		CS_Reset();  //Reset Closet Search Data and Buttons
		VA_Reset();  //Reset View Avatars Data And Buttons
		SR_Reset();

		Debug.Log(u.EmailAddress + "Logged Out");
		ChangeScreen(0);//goto LoginScreen
	}



	//------------------------------------------------------------------------------------------------------
	//-------------------------------------------End Menu Screen--------------------------------------------

	//---------------------------------------------------------------------------------------
	//----------------------------------Helper Methods---------------------------------------
	bool isEmptyOrNull(string text)
	{
		return ((text==""||text==null)?true:false);

	}
	bool isEmail(string text)
	{
		       Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
		       Match match = regex.Match(text);
		       if (match.Success)
		      	return true;
		       else
		       	return false;
	}

	private void SaveUserData()
	{
		Prefs.SaveJson<userdata>("userinfo",u);
	}
	private bool ShowLoading
	{
		set
		{
			LoadingScreen.SetActive(value);
		}
	}



	//---------------------------------------------------------------------------------------
	//----------------------------------Theme Changer---------------------------------------

	public Image[] Img_Ref_Button;
	public Sprite[] Img_Ref_Button_sprite ;

	public Image[] Img_Ref_Homescreen;
	public Sprite[] Img_Ref_Homescreen_sprite;

	public Image[] Img_Ref_Top;
	public Sprite[] Image_Ref_Top_sprite;

	public Image[] Img_ClosestSearch_Panel;
	public Sprite[] Img_ClosestSearch_Panel_sprite;


	public Image[] Img_loading_panel;
	public Color[] Theme_Color;

	private void Change_Theme()
	{
		if(u.Sex=="M")
		{
			//change button
			for(int i=0; i<(Img_Ref_Button.Length);i++)
			{
				Img_Ref_Button[i].sprite=Img_Ref_Button_sprite[0];
			}
			//change homescreen buttons
			Img_Ref_Homescreen[0].sprite=Img_Ref_Homescreen_sprite[0];
			Img_Ref_Homescreen[1].sprite=Img_Ref_Homescreen_sprite[1];

			//Change Top Header Images
			for(int i=0;i<Img_Ref_Top.Length;i++)
			{
				Img_Ref_Top[i].sprite=Image_Ref_Top_sprite[0];
			}

			// Change Closet Search panel 
			for(int i=0;i<Img_ClosestSearch_Panel.Length;i++)
			{
				Img_ClosestSearch_Panel[i].sprite=Img_ClosestSearch_Panel_sprite[0];
			}

			//change loading and closear search handle color
			Img_loading_panel[0].color=Theme_Color[0];
			Img_loading_panel[1].color=Theme_Color[0];
			Img_loading_panel[2].color=Theme_Color[0];

		}
		else
		{
			//change button
			for(int i=0;i<Img_Ref_Button.Length;i++)
			{
				Img_Ref_Button[i].sprite=Img_Ref_Button_sprite[1];
			}

			//change homescreen buttons
			Img_Ref_Homescreen[0].sprite=Img_Ref_Homescreen_sprite[2];
			Img_Ref_Homescreen[1].sprite=Img_Ref_Homescreen_sprite[3];

			//Change Top Header Images
			for(int i=0;i<Img_Ref_Top.Length;i++)
			{
				Img_Ref_Top[i].sprite=Image_Ref_Top_sprite[1];
			}
			// Change Closet Search panel 
			for(int i=0;i<Img_ClosestSearch_Panel.Length;i++)
			{
				Img_ClosestSearch_Panel[i].sprite=Img_ClosestSearch_Panel_sprite[1];
			}
			//change loading and closear search handle color
			Img_loading_panel[0].color=Theme_Color[1];
			Img_loading_panel[1].color=Theme_Color[1];
			Img_loading_panel[2].color=Theme_Color[1];
		}

	}





}//End UIController




